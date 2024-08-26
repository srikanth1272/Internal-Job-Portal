using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobPostLibrary.Models;

public partial class JobPostDBContext : DbContext
{
    public JobPostDBContext()
    {
    }

    public JobPostDBContext(DbContextOptions<JobPostDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=JobPostDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__056690C20D40035B");

            entity.ToTable("Job");

            entity.Property(e => e.JobId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__JobPost__AA126018F921A1C0");

            entity.ToTable("JobPost");

            entity.Property(e => e.JobId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
