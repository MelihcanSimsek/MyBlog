using FluentValidation;
using MediatR;
using MyBlog.Models.Application.Bases;
using MyBlog.Models.Application.Behaviours;
using MyBlog.Models.Application.Exceptions;
using System.Reflection;

namespace MyBlog.Models.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient<ExceptionMiddleware>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));
        return services;
    }

    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
    {
        var rules = assembly.GetTypes().Where(p => p.IsSubclassOf(type) && type != p).ToList();
        foreach (var rule in rules)
            services.AddTransient(rule);

        return services;
    }
}
