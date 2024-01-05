using MassTransit;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Jobs;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Logic;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Models.Configuration;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secrets.json", false);

// Add services to the container.
builder.Services
       .AddRouting(options => options.LowercaseUrls = true)
       .AddControllersWithViews();

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddServiceOptions()
       .AddGoogleClientServices()
       .AddLogicServices()
       .AddStackExchangeRedisCache(options => options.Configuration = "raspberrypi.local");

builder.Services.AddOptions<FormSettings>().BindConfiguration(nameof(FormSettings));

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host(builder.Configuration.GetConnectionString("DefaultRabbitMq"));
        factoryConfigurator.ConfigureEndpoints(context);
    });
});

builder.Services
       .AddQuartz(configurator =>
        {
            var jobSettings = builder.Configuration.GetSection(nameof(JobSettings)).Get<JobSettings>()
                           ?? throw new("Unable to read job settings");

            configurator.AddJob<ReadFormJob>(jobConfigurator => jobConfigurator.WithIdentity(nameof(ReadFormJob)).DisallowConcurrentExecution());
            configurator.AddTrigger(triggerConfigurator => triggerConfigurator.ForJob(nameof(ReadFormJob))
                                                                              .WithIdentity($"{nameof(ReadFormJob)}-trigger")
                                                                              .WithCronSchedule(jobSettings.ReadFormJob.CronSchedule));
        })
       .AddQuartzHostedService();

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

await app.RunAsync();
