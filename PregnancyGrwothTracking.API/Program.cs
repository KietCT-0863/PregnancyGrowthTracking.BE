using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.BLL.Services.Vnpay;
using PregnancyGrowthTracking.DAL;
using PregnancyGrowthTracking.DAL.Repositories;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon;
using PregnancyGrowthTracking.DAL.Entities;


namespace PregnancyGrwothTracking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //  Cấu hình Authentication với JWT Bearer
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero

                    };
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId");
                    googleOptions.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret");
                    googleOptions.CallbackPath = "/signin-google";
                    googleOptions.Scope.Add("email");
                    googleOptions.Scope.Add("profile");
                    googleOptions.SaveTokens = true;
                });

            //  Kích hoạt Authorization
            builder.Services.AddAuthorization();

            //Connect VNPay API
            builder.Services.AddScoped<IVnPayService, VnPayService>();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pregnancy Growth Tracking API",
                    Version = "v1"
                });
                //  Thêm Security Definition cho JWT Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Nhập 'Bearer [token]' vào đây."
                });
                //  Bắt Swagger yêu cầu Authorization cho API có `[Authorize]`
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

                //  Hiển thị role của API trong Swagger UI
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddSingleton<IS3Repository, S3Repository>();
            builder.Services.AddSingleton<IS3Service, S3Service>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://your-azure-app-url.azurewebsites.net")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<PregnancyGrowthTrackingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; 
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
    });

            // ✅ Đọc cấu hình từ appsettings.json
            var awsOptions = builder.Configuration.GetSection("AWS");

            // ✅ Đăng ký IAmazonS3 bằng Factory
            builder.Services.AddSingleton<IAmazonS3>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new AmazonS3Client(
                    config["AWS:AccessKey"],
                    config["AWS:SecretKey"],
                    Amazon.RegionEndpoint.GetBySystemName(config["AWS:Region"])
                );
            });

            

            var app = builder.Build();

            app.UseDeveloperExceptionPage(); //  Hiển thị lỗi chi tiết khi chạy API
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();  //  Đặt Authentication trước Authorization
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }

    }
}
