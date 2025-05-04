using AuthWalletWatch.API.Implementation.Services;
using AuthWalletWatch.API.Interfaces;
using AuthWalletWatch.API.Services;
using AuthWalletWatch.Application.Implementation.Services;
using AuthWalletWatch.Application.Interfaces;
using AuthWalletWatch.Infrastructure.EF;
using AuthWalletWatch.Infrastructure.Models;
using AuthWalletWatch.Infrastructure.NoSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace AuthWalletWatch.API;

internal static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? configuration?.GetConnectionString("PostgreSQL");

        if (connectionString is null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }


        services.AddDbContext<AuthWalletWatchDBContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, JwtTokenService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IEmailSender<ApplicationUser>, EmailServices>();
        services.AddTransient<ITokenRepository, RedisTokenRepository>();

        return services;
    }
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthWalletWatch.Application.Mapping.ClaimProfile));
        services.AddAutoMapper(typeof(AuthWalletWatch.Application.Mapping.ProfileProfile));
        services.AddAutoMapper(typeof(AuthWalletWatch.Application.Mapping.RoleProfile));
        services.AddAutoMapper(typeof(AuthWalletWatch.Application.Mapping.UserProfile));

        return services;
    }
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityApiEndpoints<ApplicationUser>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AuthWalletWatchDBContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidateIssuer = true,
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey"))),
                ValidateIssuerSigningKey = true,
            };
        });


        return services;
    }

    public static IServiceCollection AddCustomRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_REDIS") ?? configuration?.GetConnectionString("Redis");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));

        services.AddSingleton<StackExchange.Redis.IDatabase>(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());


        return services;
    }
}
