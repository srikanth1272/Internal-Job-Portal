using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SkillLibrary.Models;

public partial class SkillDBContext : DbContext
{
    public SkillDBContext()
    {
    }

    public SkillDBContext(DbContextOptions<SkillDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=SkillDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skill__DFA09187AE2F71AC");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillCategory)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SkillLevel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
