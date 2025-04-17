using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "API for managing Articles, Blogs, and Authors"
                });
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1" });
                //c.TagActionsBy(ta =>
                //{
                //    return new List<string> { ta.ActionDescriptor.DisplayName! };
                //});
            });

            builder.Services.AddDbContext<ThoughtfulDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            services.AddIdentity<AppUser, Role>()
          .AddEntityFrameworkStores<ThoughtfulDbContext>()
          .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "FrontEndUI", policy =>
                {
                    policy.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            builder.Services.AddMediatR(typeof(Program));
            builder.Services.AddAutoMapper(typeof(Program));
            services.AddScoped<JwtService>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false

                };
            });


            return services;
        }
    }
}
