using BookStore.Application.Abstractions.IRepositories; // OrderRepository, UserRepository ishllatish uchun
using BookStore.Infrastructure.Persistance;             // BookStoreDbContext ishlashi uchun
using BookStore.Infrastructure.Repositories;            // BookStoreDbContext ishlatish uchun
using Microsoft.AspNetCore.Authentication.JwtBearer;    // JwtBearerDefaults, JwtBearerEvents ishlashi uchun
using Microsoft.EntityFrameworkCore;                    // UseNpgsql ishlashi uchun
using Microsoft.Extensions.Configuration;               // IConfiguration ishlashi uchun
using Microsoft.Extensions.DependencyInjection;         // IServiceCollection ishlashi uchun
using Microsoft.IdentityModel.Tokens;                   // TokenValidationParameters, SymmetricSecurityKey, SecurityTokenExpiredException ishlashi uchun
using System.Text;                                      // Encoding ishlashi uchun

namespace BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("BookStoreConnection"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            return services;
        }

    }
}
