using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application;
using Store.Author.HandlerRabbit;
using Store.Messenger.Email.SendGridLibrary.Interface;
using Store.Persistence;
using Store.RabbitMQ.Bus.BusRabbit;
using Store.RabbitMQ.Bus.EventQueue;
using Store.RabbitMQ.Bus.Implement;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
});

builder.Services.AddTransient<ISendGrid, Store.Messenger.Email.SendGridLibrary.Implement.SendGrid>();

builder.Services.AddTransient<EmailEventHandler>();

builder.Services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<New>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conf = builder.Configuration.GetConnectionString("ConnectionDatabase");

builder.Services.AddDbContext<AuthorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDatabase"));
});

builder.Services.AddMediatR(typeof(New.Handler).Assembly);

builder.Services.AddAutoMapper(typeof(Query.Handler));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IRabbitEventBus>();
options.Subscribe<EmailEventQueue, EmailEventHandler>();



app.MapControllers();

app.Run();
