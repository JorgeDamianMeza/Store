using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.ShoppingCart.Application;
using Store.ShoppingCart.Persistence;
using Store.ShoppingCart.RemoteInterface;
using Store.ShoppingCart.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBookService, BooksService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(New.Handler).Assembly);
builder.Services.AddHttpClient("Books", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Books"]);
});


//


builder.Services.AddDbContext<CartContext>(opt =>
{    
    opt.UseMySql(builder.Configuration.GetConnectionString("ConnectionDatabase"),new MySqlServerVersion("8.0.29"));
});

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
