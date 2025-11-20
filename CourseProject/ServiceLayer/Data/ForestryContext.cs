using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer.Data;

public partial class ForestryContext : DbContext
{
    public ForestryContext()
    {
    }

    public ForestryContext(DbContextOptions<ForestryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<ForestPlot> ForestPlots { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SilvicultureEvent> SilvicultureEvents { get; set; }

    public virtual DbSet<TreeType> TreeTypes { get; set; }

    public virtual DbSet<TreesNumber> TreesNumbers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G32B90F; Database=CourseProjectBase; Trusted_Connection=True; Trust Server Certificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK_EeventType");

            entity.ToTable("EventType");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<ForestPlot>(entity =>
        {
            entity.HasKey(e => e.PlotId);

            entity.ToTable("ForestPlot");

            entity.Property(e => e.PlotId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.ForestPlots)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForestPlot_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<SilvicultureEvent>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("SilvicultureEvent");

            entity.Property(e => e.Description).HasMaxLength(250);

            entity.HasOne(d => d.EventType).WithMany(p => p.SilvicultureEvents)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SilvicultureEvent_EeventType");

            entity.HasOne(d => d.Plot).WithMany(p => p.SilvicultureEvents)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SilvicultureEvent_ForestPlot");

            entity.HasOne(d => d.TreeType).WithMany(p => p.SilvicultureEvents)
                .HasForeignKey(d => d.TreeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SilvicultureEvent_TreeType");
        });

        modelBuilder.Entity<TreeType>(entity =>
        {
            entity.ToTable("TreeType");

            entity.Property(e => e.Name).HasMaxLength(12);
        });

        modelBuilder.Entity<TreesNumber>(entity =>
        {
            entity.HasKey(e => new { e.PlotId, e.TreeTypeId });

            entity.ToTable("TreesNumber");

            entity.HasOne(d => d.Plot).WithMany(p => p.TreesNumbers)
                .HasForeignKey(d => d.PlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TreesNumber_ForestPlot");

            entity.HasOne(d => d.TreeType).WithMany(p => p.TreesNumbers)
                .HasForeignKey(d => d.TreeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TreesNumber_TreeType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
