using BookStore.Application;            // AddApplication ishlashi uchun
using BookStore.Application.Services.EmailSenderService;
using BookStore.Infrastructure;             // AddInfrastructure ishlashi uchun
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;         // OpenApiInfo, OpenApiSecurityScheme, OpenApiReference, OpenApiSecurityRequirement ishalshi uchun
using System.Text;
namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);              // Extension methodalar<<VVV
            builder.Services.AddApplication();



            builder.Services.AddControllers();

            //Swaggeri nastroyka qilish
            builder.Services.AddSwaggerGen(options =>       //Swaggerni nastroyka qilish. Swaggerda Authorization qilish uchun quluf iconcasini chiqarish uchun
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1.0.0", Description = "Namoyish etadi!" });

                var securityShceme = new OpenApiSecurityScheme
                {
                    Description = "Greeting Methodini ishlatish uchun Avtorizatsiya qilishingiz kerak",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityShceme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securityShceme,new[] {"Bearer"} }
                };
                options.AddSecurityRequirement(securityRequirement);
            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(  // Token Eskirgan yoki yoqlikga tekshiradi
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudence"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = (context) =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IsTokenExpired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
