using System.Reflection;
using Application.Common.Interfaces.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services
            .AddFluentEmail("notification@keydetect.com")
            .AddRazorRenderer()
            .AddMailKitSender(new FluentEmail.MailKitSmtp.SmtpClientOptions
            {
                Server = "sandbox.smtp.mailtrap.io",
                Port = 587,
                User = "1f97dca448fe7b",
                Password = "7393027e678693",
                RequiresAuthentication = true,
                SocketOptions = MailKit.Security.SecureSocketOptions.StartTls
            });

        return services;
    }
}