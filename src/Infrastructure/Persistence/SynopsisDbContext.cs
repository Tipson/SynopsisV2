using Microsoft.EntityFrameworkCore;
using Synopsis.Infrastructure.DbContext.Entities;
using SynopsisV2.Application.Common.Models;

namespace SynopsisV2.Infrastructure.Persistence;

public partial class SynopsisDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SynopsisDbContext(DbContextOptions<SynopsisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorDto> Errors { get; set; } = null!;

    public virtual DbSet<PartnerDto> Partners { get; set; } = null!;

    public virtual DbSet<SpeakerDto> Speakers { get; set; } = null!;

    public virtual DbSet<TicketDto> Tickets { get; set; } = null!;
    public virtual DbSet<AgendaDto> Agendas { get; set; } = null!;
    public virtual DbSet<SiteDto> Sites { get; set; } = null!;
    public virtual DbSet<LogoDto> Logos { get; set; } = null!;
    public virtual DbSet<FeedbackDto> Feedbacks { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Error>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("ID");
            entity.Property(e => e.Controller).HasColumnOrder(3);
            entity.Property(e => e.Date)
                .HasColumnOrder(1)
                .HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnOrder(2);
            entity.Property(e => e.Method).HasColumnOrder(4);
            entity.Property(e => e.Source).HasColumnOrder(5);
        });

        modelBuilder.Entity<PartnerDto>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("ID");
            entity.Property(e => e.Important).HasColumnOrder(4);
            entity.Property(e => e.IsShow)
                .HasColumnOrder(6)
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Logo).HasColumnOrder(2);
            entity.Property(e => e.Name).HasColumnOrder(1);
            entity.Property(e => e.Type).HasColumnOrder(3);
            entity.Property(e => e.SynopsisType).HasColumnOrder(5);
            entity.Property(e => e.Url)
                .HasColumnOrder(7)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<SpeakerDto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speakers__02899F87A657070E");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Important).HasColumnOrder(8);
            entity.Property(e => e.IsCommission).HasColumnOrder(10);
            entity.Property(e => e.IsFavorite).HasColumnOrder(11);
            entity.Property(e => e.IsShow).HasColumnOrder(9);
            entity.Property(e => e.NameEn).HasColumnOrder(1);
            entity.Property(e => e.NameRu).HasColumnOrder(2);
            entity.Property(e => e.PartnerId).HasColumnOrder(12)                 
                .HasColumnName("PartnerId");
            entity.Property(e => e.Photo).HasColumnOrder(5);
            entity.Property(e => e.PositionEn).HasColumnOrder(3);
            entity.Property(e => e.PositionRu).HasColumnOrder(4);
            entity.Property(e => e.SynopsisType).HasColumnOrder(7);
            entity.Property(e => e.Type).HasColumnOrder(6);
            entity.HasOne(e => e.Partner)
                .WithMany(e => e.Speakers)
                .HasForeignKey(e => e.PartnerId);
        });

        modelBuilder.Entity<>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnOrder(0)
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasColumnOrder(3);
            entity.Property(e => e.Login).HasColumnOrder(6);
            entity.Property(e => e.Mail).HasColumnOrder(1);
            entity.Property(e => e.Name).HasColumnOrder(7);
            entity.Property(e => e.Telegram).HasColumnOrder(4);
            entity.Property(e => e.Type).HasColumnOrder(2);
            entity.Property(e => e.WalletBsc)
                .HasColumnOrder(5)
                .HasColumnName("WalletBSC");
        });

        modelBuilder.Entity<AgendaDto>(entity =>
        {
            entity.HasIndex(e => e.Id)
                .IsUnique();
            entity.Property(e => e.SynopsisType);
            entity.Property(e => e.TopicEn);
            entity.Property(e => e.TopicRu);
            entity.Property(e => e.StartTime);
            entity.HasMany(e => e.Speakers)
                .WithMany(e => e.Agendas);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.Property(e => e.SpeakerId);
            entity.Property(e => e.CompanyNameEn);
            entity.Property(e => e.CompanyNameRu);
            entity.Property(e => e.Link);

            entity.HasIndex(e => new { e.SpeakerId, e.Link }).IsUnique();
            entity.HasKey(e => new { e.SpeakerId, e.Link });
            entity.HasOne(e => e.Speaker)
                .WithMany(e => e.Sites)
                .HasForeignKey(e => e.SpeakerId);
        });

        modelBuilder.Entity<LogoDto>(entity =>
        {
            entity.Property(e => e.SpeakerId);
            entity.Property(e => e.Logo);

            entity.HasIndex(e => new { e.SpeakerId, e.Logo }).IsUnique();
            entity.HasKey (e => new{e.Logo, e.SpeakerId });
            entity.HasOne(e => e.Speaker)
                .WithMany(e => e.Logos)
                .HasForeignKey(e => e.SpeakerId);
        });

        modelBuilder.Entity<FeedbackDto>(entity =>
        {
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Email);
            entity.Property(e => e.Body);
            entity.Property(e => e.DateTimeSend);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
