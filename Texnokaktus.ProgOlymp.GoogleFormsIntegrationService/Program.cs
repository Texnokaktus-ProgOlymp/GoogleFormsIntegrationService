using MassTransit;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secrets.json", false);

// Add services to the container.
builder.Services
       .AddRouting(options => options.LowercaseUrls = true)
       .AddControllersWithViews();

builder.Services
       .AddServiceOptions()
       .AddGoogleClientServices()
       .AddLogicServices()
       .AddStackExchangeRedisCache(options => options.Configuration = "raspberrypi.local");

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host(builder.Configuration.GetConnectionString("DefaultRabbitMq"));
        factoryConfigurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
