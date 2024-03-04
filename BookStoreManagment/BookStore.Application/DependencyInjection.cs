using BookStore.Application.Abstractions.IServices;
using BookStore.Application.Services;
using BookStore.Application.Services.AuthServices;
using BookStore.Application.Services.EmailSenderService;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ISendEmailService,SendEmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
