using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DynamicChatbotAPI.Models;

public partial class DynamicChatbotApiContext : DbContext
{
    public DynamicChatbotApiContext()
    {
    }

    public DynamicChatbotApiContext(DbContextOptions<DynamicChatbotApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<FollowupQuestion> FollowupQuestions { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J8EMH81;Database=DynamicChatbotAPI;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__Action__74EFC21794296062");

            entity.ToTable("Action");

            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.ActionName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("action_name");
            entity.Property(e => e.ApiUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("api_url");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Actions)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Action__company___398D8EEE");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__3E267235CC03EB09");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
        });

        modelBuilder.Entity<FollowupQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Followup__2EC21549DB762F60");

            entity.ToTable("FollowupQuestion");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("question_text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("question_type");
            entity.Property(e => e.SequenceOrder).HasColumnName("sequence_order");

            entity.HasOne(d => d.Action).WithMany(p => p.FollowupQuestions)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK__FollowupQ__actio__3C69FB99");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Options__F4EACE1B2CB0EBFC");

            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.OptionText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option_text");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Options__questio__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
