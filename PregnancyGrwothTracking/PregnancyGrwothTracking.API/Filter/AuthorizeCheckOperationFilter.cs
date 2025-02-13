using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .ToList();

        if (authorizeAttributes.Any())
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
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
                }
            };

            var roles = authorizeAttributes
                .Where(a => !string.IsNullOrEmpty(a.Roles))
                .Select(a => a.Roles.ToLower())
                .Distinct()
                .ToList();

            if (roles.Any())
            {
                operation.Description += $" 🛡 **Chỉ cho phép các role: {string.Join(", ", roles.Select(r => r.ToLower()))}**";

            }
        }
    }
}
