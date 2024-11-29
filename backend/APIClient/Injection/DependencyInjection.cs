using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
using Service.Interface;
using Service.Mapper;
using Service.Services;

namespace APIClient.Injection
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServicesInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //CONNECT TO DATABASE
            services.AddDbContext<SWP391TicketResellPlatformContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //REPOSITORY

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();


            //GENERIC REPOSITORY
            services.AddScoped<IGenericRepository<Wallet>, GenericRepository<Wallet>>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<UserRole>, GenericRepository<UserRole>>();
            services.AddScoped<IGenericRepository<Ticket>, GenericRepository<Ticket>>();
            services.AddScoped<IGenericRepository<Member>, GenericRepository<Member>>();
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddScoped<IGenericRepository<OrderItem>, GenericRepository<OrderItem>>();
            //SERVICE
            services.AddScoped<IAuthService, AuthService>();


            //UNIT OF WORK
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //OTHER
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);

            return services;
        }
    }
}
