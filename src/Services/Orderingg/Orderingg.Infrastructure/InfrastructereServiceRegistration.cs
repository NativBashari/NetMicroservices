using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orderingg.Application.Contracts.Infrastructure;
using Orderingg.Application.Contracts.Persistence;
using Orderingg.Application.Models;
using Orderingg.Infrastructure.Mail;
using Orderingg.Infrastructure.Persistence;
using Orderingg.Infrastructure.Repositories;

namespace Orderingg.Infrastructure
{
    public static class InfrastructereServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
        
            services.AddScoped(typeof(IAsyncRepository<>),typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
