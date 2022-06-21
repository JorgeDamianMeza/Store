using Microsoft.EntityFrameworkCore;
using Store.Book.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Store.Book.Application;
using Store.RabbitMQ.Bus.Implement;
using Store.RabbitMQ.Bus.BusRabbit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<New>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDatabase"));
});

builder.Services.AddMediatR(typeof(New.Handler).Assembly);

builder.Services.AddAutoMapper(typeof(Query.Execute));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
