using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Data;

public partial class TravelAppDbContext : DbContext
{
    public TravelAppDbContext()
    {
    }

    public TravelAppDbContext(DbContextOptions<TravelAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<TripPlace> TripPlaces { get; set; }

    public virtual DbSet<TripPlan> TripPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("Photos_pkey");

            entity.HasIndex(e => e.PlaceId, "fki_fk_photos_places");

            entity.HasIndex(e => e.UserId, "fki_fk_photos_users");

            entity.Property(e => e.PhotoId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("PhotoID");
            entity.Property(e => e.FileUrl)
                .HasMaxLength(100)
                .HasColumnName("FileURL");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Place).WithMany(p => p.Photos)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("fk_photos_places");

            entity.HasOne(d => d.User).WithMany(p => p.Photos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_photos_users");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("Place_pkey");

            entity.Property(e => e.PlaceId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("PlaceID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OsmId).HasMaxLength(50).HasColumnName("OsmID");
        });

        modelBuilder.Entity<TripPlace>(entity =>
        {
            entity.HasKey(e => e.TripPlaceId).HasName("TripPlaces_pkey");

            entity.HasIndex(e => e.PlaceId, "fki_fk_tripplaces_places");

            entity.HasIndex(e => e.TripPlanId, "fki_fk_tripplaces_tripplans");

            entity.Property(e => e.TripPlaceId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("TripPlaceID");
            entity.Property(e => e.Note).HasMaxLength(100);
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.TripPlanId).HasColumnName("TripPlanID");

            entity.HasOne(d => d.Place).WithMany(p => p.TripPlaces)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("fk_tripplaces_places");

            entity.HasOne(d => d.TripPlan).WithMany(p => p.TripPlaces)
                .HasForeignKey(d => d.TripPlanId)
                .HasConstraintName("fk_tripplaces_tripplans");
        });

        modelBuilder.Entity<TripPlan>(entity =>
        {
            entity.HasKey(e => e.TripPlanId).HasName("TripPlan_pkey");

            entity.Property(e => e.TripPlanId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("TripPlanID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TripPlans)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_tripplans_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey");

            entity.HasIndex(e => e.UserId, "fki_fk_tripplans_users");

            entity.HasIndex(e => e.Email, "uq_users_email").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LoginDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasDefaultValue(0);
            entity.Property(e => e.SignUpDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValue(0);
            entity.Property(e => e.RefreshToken).HasMaxLength(2048);
            entity.Property(e => e.RefreshTokenCreatedAt);
            entity.Property(e => e.RefreshTokenExpiresAt);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
