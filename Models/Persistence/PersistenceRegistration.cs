using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models.Application.Interfaces.Repositories;
using MyBlog.Models.Application.Interfaces.UnitOfWorks;
using MyBlog.Models.Domain.Entities;
using MyBlog.Models.Persistence.Context;
using MyBlog.Models.Persistence.Repositories;
using MyBlog.Models.Persistence.UnitOfWorks;

namespace MyBlog.Models.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.SignIn.RequireConfirmedEmail = false;
        }).AddRoles<Role>()
           .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}
