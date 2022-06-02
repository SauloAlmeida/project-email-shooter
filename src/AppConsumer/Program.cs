using AppConsumer.Application.Contracts;
using AppConsumer.Application.Models;
using AppConsumer.Application.Options;
using AppConsumer.Infrastructure;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetupServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetupServices(IServiceCollection services, ConfigurationManager configuration)
{
    Console.WriteLine("Setup Services!");

    services.AddSingleton<IMessageBrokerSettings, MessageBrokerSettings>();

    services.AddSingleton<IEmailDispatcher, EmailDispatcher>();

    services.AddHostedService<ConsumerService>();
}