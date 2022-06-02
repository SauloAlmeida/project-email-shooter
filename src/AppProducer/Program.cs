using AppProducer.Application.Contracts;
using AppProducer.Application.Models;
using AppProducer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("App Started!");

Console.WriteLine("Configure services..");

ServiceCollection serviceCollection = SetupServices();

var serviceProvider = serviceCollection.BuildServiceProvider();

Console.WriteLine("Get EmailSender service..");

var emailSender = serviceProvider.GetService<IEmailSender>();

new Seed(emailSender).Execute();

Console.WriteLine("All email's sent!");

static ServiceCollection SetupServices()
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddScoped<IMessageSender, MessageSender>()
                     .AddScoped<IEmailSender, EmailSender>();
    return serviceCollection;
}