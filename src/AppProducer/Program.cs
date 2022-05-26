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

Console.WriteLine("Send emails to queue!");

var emailsToSend = Enumerable.Range(0, 50)
                             .Select(i => EmailModel.Create($"email{i}.google.com.br",
                                                            $"Subject - {i}",
                                                            $"{i} - Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                                            $"Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                                            $"when an unknown printer took a galley of type and scrambled it to make a type specimen book "))
                             .ToArray();

emailSender.SendEmail(emailsToSend);

Console.WriteLine("All email's sent!");

static ServiceCollection SetupServices()
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddScoped<IMessageSender, MessageSender>()
                     .AddScoped<IEmailSender, EmailSender>();
    return serviceCollection;
}