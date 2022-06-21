using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Store.Geteway.ImplementRemote;
using Store.Geteway.InterfaceRemote;
using Store.Geteway.MessageHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOcelot().AddDelegatingHandler<BookHandler>();
builder.Services.AddSingleton<IAuthorRemote, AuthorRemote>();
builder.Services.AddHttpClient("AuthorService", config => 
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Author"]);
});

var app = builder.Build();

builder.Configuration.AddJsonFile($"ocelot.json");



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

 app.UseOcelot();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
