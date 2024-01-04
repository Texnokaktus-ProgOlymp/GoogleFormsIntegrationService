using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.DataAccess.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ContestStage> ContestStages => Set<ContestStage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(builder =>
        {
            builder.HasKey(application => application.Id);
            builder.HasAlternateKey(application => application.ResponseId);

            builder.Property(application => application.Submitted)
                   .HasConversion(time => time.ToUniversalTime(),
                                  time => DateTime.SpecifyKind(time, DateTimeKind.Utc));

            builder.HasOne(application => application.ContestStage)
                   .WithMany()
                   .HasForeignKey(application => application.ContestStageId);
        });

        modelBuilder.Entity<ContestStage>(builder =>
        {
            builder.HasKey(stage => stage.Id);
            builder.Property(stage => stage.Id).ValueGeneratedNever();

            builder.HasIndex(stage => stage.FormId)
                   .HasFilter($"[{nameof(ContestStage.FormId)}] IS NOT NULL")
                   .IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
