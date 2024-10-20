using DevFreela.Core.Auth;
using DevFreela.Core.MessageBus;
using DevFreela.Core.Payment;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Payment;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevFreela.Infrastructure;

public static class InfrastructureModule {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services
            .AddPersistence(configuration)
            .AddRepositories()
            .AddUnitOfWork()
            .AddAuthentication(configuration)
            .AddMessageBus()
            .AddServices();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DevFreelaCs");

        services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevFreelaCs")));
        // builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreelaCS")); // Banco em mem�ria

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration) {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddMessageBus(this IServiceCollection services) {
        services.AddScoped<IMessageBusService, MessageBusService>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) {
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}