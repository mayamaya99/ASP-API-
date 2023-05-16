using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI_mongo.Models;

public partial class NamesDbContext : DbContext
{
    public NamesDbContext()
    {
    }

    public NamesDbContext(DbContextOptions<NamesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Origin> Origins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //=> optionsBuilder.UseSqlServer("Server= tcp:elmisqlserver.database.windows.net,1433; Database=NamesDB;"+"Trusted_Connection=True;" +"\nTrustServerCertificate=True");

    => optionsBuilder.UseSqlServer("Server=DESKTOP-K74556U\\SQLEXPRESS;Database=NamesDB;Trusted_Connection=True;\nTrustServerCertificate=True");
    //=> optionsBuilder.UseSqlServer("Server=tcp:elmisqlserver.database.windows.net,1433; Database=NamesDB; User ID=azureuser;Password=Luqman2011;TrustServerCertificate=False; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("genderID");
            entity.Property(e => e.Gender1)
                .HasMaxLength(50)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.ToTable("Name");

            entity.Property(e => e.NameId).HasColumnName("nameID");
            entity.Property(e => e.GenderId).HasColumnName("genderID");
            entity.Property(e => e.Name1)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OriginId).HasColumnName("originID");
            entity.Property(e => e.YearLeast).HasColumnName("yearLeast");
            entity.Property(e => e.YearMost).HasColumnName("yearMost");
        });

        modelBuilder.Entity<Origin>(entity =>
        {
            entity.ToTable("Origin");

            entity.Property(e => e.OriginId).HasColumnName("originID");
            entity.Property(e => e.Usage)
                .HasMaxLength(50)
                .HasColumnName("usage");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
