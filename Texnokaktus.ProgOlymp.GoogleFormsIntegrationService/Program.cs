using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddRouting(options => options.LowercaseUrls = true)
       .AddControllersWithViews();

builder.Services
       .AddGoogleClientServices()
       .AddLogicServices()
       .AddStackExchangeRedisCache(options => options.Configuration = "raspberrypi.local");

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
