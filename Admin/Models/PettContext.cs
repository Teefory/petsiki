using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using petsk.Models;




namespace petsk.Models;

public partial class PettContext : DbContext
{
    public PettContext()
    {
    }
    //static PettContext()
    //{
    //    NpgsqlConnection.GlobalTypeMapper.MapEnum<CompetitionsRoli>("roli");
    //}


    public DbSet<Users> Entities { get; set; }

    //Need to let the Npgsql library know about mapped enums
    static PettContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CompetitionsRole>("role_name");
    }



    public PettContext(DbContextOptions<PettContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collecting> Collectings { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<RecordingWalk> RecordingWalks { get; set; }

    public virtual DbSet<Shelter> Shelters { get; set; }

    public virtual DbSet<Users> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=pett;Username=postgres;Password=1234;Persist Security Info=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("role_name", new[] { "User", "Administrator" });

        modelBuilder.Entity<Collecting>(entity =>
        {
            entity.HasKey(e => e.IdCollecting).HasName("collecting_pkey");

            entity.ToTable("collecting");

            entity.Property(e => e.IdCollecting).HasColumnName("id_collecting");
            entity.Property(e => e.AlreadyAssembled)
                .HasPrecision(10, 2)
                .HasColumnName("already_assembled");
            entity.Property(e => e.ClosingDate).HasColumnName("closing_date");
            entity.Property(e => e.DescriptionC).HasColumnName("description_c");
            entity.Property(e => e.IdPet).HasColumnName("id_pet");
            entity.Property(e => e.IdShelter).HasColumnName("id_shelter");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.OpeningDate).HasColumnName("opening_date");
            entity.Property(e => e.RequiredAmount)
                .HasPrecision(10, 2)
                .HasColumnName("required_amount");

            entity.HasOne(d => d.IdPetNavigation).WithMany(p => p.Collectings)
                .HasForeignKey(d => d.IdPet)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("collecting_id_pet_fkey");

            entity.HasOne(d => d.IdShelterNavigation).WithMany(p => p.Collectings)
                .HasForeignKey(d => d.IdShelter)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("collecting_id_shelter_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Collectings)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("collecting_id_user_fkey");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.IdDonation).HasName("donation_pkey");

            entity.ToTable("donation");

            entity.Property(e => e.IdDonation).HasColumnName("id_donation");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IdCollecting).HasColumnName("id_collecting");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdCollectingNavigation).WithMany(p => p.Donations)
                .HasForeignKey(d => d.IdCollecting)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("donation_id_collecting_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Donations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("donation_id_user_fkey");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.IdExpenses).HasName("expenses_pkey");

            entity.ToTable("expenses");

            entity.Property(e => e.IdExpenses).HasColumnName("id_expenses");
            entity.Property(e => e.AmountSpent)
                .HasPrecision(10, 2)
                .HasColumnName("amount_spent");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IdCollecting).HasColumnName("id_collecting");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TypeExpense)
                .HasMaxLength(20)
                .HasColumnName("type_expense");

            entity.HasOne(d => d.IdCollectingNavigation).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.IdCollecting)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("expenses_id_collecting_fkey");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.IdPet).HasName("pet_pkey");

            entity.ToTable("pet");

            entity.Property(e => e.IdPet).HasColumnName("id_pet");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DescriptionP).HasColumnName("description_p");
            entity.Property(e => e.IdShelter).HasColumnName("id_shelter");
            entity.Property(e => e.MedicalInformation)
                .HasMaxLength(100)
                .HasColumnName("medical_information");
            entity.Property(e => e.Nickname)
                .HasMaxLength(10)
                .HasColumnName("nickname");

            entity.HasOne(d => d.IdShelterNavigation).WithMany(p => p.Pets)
                .HasForeignKey(d => d.IdShelter)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pet_id_shelter_fkey");
        });

        modelBuilder.Entity<RecordingWalk>(entity =>
        {
            entity.HasKey(e => new { e.DataR, e.IdPet }).HasName("recording_walk_pkey");

            entity.ToTable("recording_walk");

            entity.Property(e => e.DataR).HasColumnName("data_r");
            entity.Property(e => e.IdPet).HasColumnName("id_pet");
            entity.Property(e => e.BeginWalk).HasColumnName("begin_walk");
            entity.Property(e => e.EndWalk).HasColumnName("end_walk");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdPetNavigation).WithMany(p => p.RecordingWalks)
                .HasForeignKey(d => d.IdPet)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("recording_walk_id_pet_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RecordingWalks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("recording_walk_id_user_fkey");
        });

        modelBuilder.Entity<Shelter>(entity =>
        {
            entity.HasKey(e => e.IdShelter).HasName("shelter_pkey");

            entity.ToTable("shelter");

            entity.Property(e => e.IdShelter).HasColumnName("id_shelter");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.AmountOfMoneyCollected)
                .HasPrecision(10, 2)
                .HasColumnName("amount_of_money_collected");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(15)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(15)
                .HasColumnName("surname");
            entity.Property(e => e.Role)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
