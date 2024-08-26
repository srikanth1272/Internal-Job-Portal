using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobLibrary.Models;

public partial class JobDBContext : DbContext
{
    public JobDBContext()
    {
    }

    public JobDBContext(DbContextOptions<JobDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=JobDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Job__056690C29255520F");

            entity.ToTable("Job");

            entity.Property(e => e.JobId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.JobDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
