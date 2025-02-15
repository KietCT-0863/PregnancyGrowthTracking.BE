using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.BLL.Services.Vnpay;
using PregnancyGrowthTracking.DAL;
using PregnancyGrowthTracking.DAL.Repositories;

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



            builder.Services.AddDbContext<PregnancyGrowthTrackingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Cấu hình để xử lý decimal đúng cách
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
    });



            var app = builder.Build();

            // Thay đổi phần này
            // Chỉ sử dụng Swagger trong môi trường Development và Staging
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pregnancy Growth Tracking API V1");
                    // Đặt Swagger là trang mặc định
                    c.RoutePrefix = string.Empty;
                });
            }
            else 
            {
                // Trong môi trường Production
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pregnancy Growth Tracking API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();  //  Đặt Authentication trước Authorization
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }

    }
}
