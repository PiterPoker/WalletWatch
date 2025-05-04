using System.Reflection;
using Expenses.Application.Interfaces.Services;
using Expenses.Application.Mappings;
using Expenses.Application.Services;
using Expenses.Domain.Implementations.Factories;
using Expenses.Domain.Interfaces.Factories;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Infrastructure;
using Expenses.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API;

/// <summary>
/// Provides custom extension methods for configuring services in the Expenses API.
/// </summary>
internal static class CustomServicesExtensions
{
    /// <summary>
    /// Adds the custom DbContext to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> used to retrieve the connection string. Optional.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the connection string is null.</exception>
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? configuration?.GetConnectionString("ExpensesDB");

        if (connectionString is not null)
        {
            services.AddDbContext<ExpensesContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
        else
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        return services;
    }

    /// <summary>
    /// Adds custom Swagger configuration to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });

        return services;
    }

    /// <summary>
    /// Adds custom AutoMapper mappings to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCustomMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthorProfile));
        services.AddAutoMapper(typeof(CategoryProfile));
        services.AddAutoMapper(typeof(ExpenseProfile));
        services.AddAutoMapper(typeof(WalletProfile));
        services.AddAutoMapper(typeof(ColorProfile));

        return services;
    }

    /// <summary>
    /// Adds custom repositories to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();

        return services;
    }

    /// <summary>
    /// Adds custom services to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
    
    public static IServiceCollection AddCustomFactories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryFactory, CategoryFactory>();
        services.AddScoped<IExpenseFactory, ExpenseFactory>();

        return services;
    }

    public static IServiceCollection AddCustomMassTransit(this IServiceCollection services) 
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetEntryAssembly());
            x.UsingRabbitMq((context, cfg) =>
            {
                //TO-DO Environment.GetEnvironmentVariable("") - hostname, virtual host, username, password
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}