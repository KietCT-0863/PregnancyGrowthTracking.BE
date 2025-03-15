using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PregnancyGrowthTrackingDbContext : DbContext
{
    public PregnancyGrowthTrackingDbContext()
    {
    }

    public PregnancyGrowthTrackingDbContext(DbContextOptions<PregnancyGrowthTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blog { get; set; }

    public virtual DbSet<BlogCate> BlogCate { get; set; }

    public virtual DbSet<Category> Categorie { get; set; }

    public virtual DbSet<CommentLike> CommentLikes { get; set; }

    public virtual DbSet<Foetus> Foetus { get; set; }

    public virtual DbSet<GrowthDatum> GrowthData { get; set; }

    public virtual DbSet<GrowthStandard> GrowthStandards { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<PostTag> PostTags { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserNote> UserNotes { get; set; }

    public virtual DbSet<UserReminder> UserReminders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());

    private string? GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E307BA47D4B");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogImageUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<BlogCate>(entity =>
        {
            entity.HasKey(e => e.BlogCateId).HasName("PK__BlogCate__67D473BD5F0657D8");

            entity.ToTable("BlogCate");

            entity.HasIndex(e => new { e.BlogId, e.CategoryId }, "UQ__BlogCate__F5A70D91838F474C").IsUnique();

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogCates)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__BlogCate__BlogId__7A672E12");

            entity.HasOne(d => d.Category).WithMany(p => p.BlogCates)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__BlogCate__Catego__7B5B524B");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B3BA0914D");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<CommentLike>(entity =>
        {
            entity.HasKey(e => e.CommentLikeId).HasName("PK__CommentL__D36E157DE4DCF9E6");

            entity.ToTable("CommentLike");

            entity.HasIndex(e => new { e.CommentId, e.UserId }, "UC_CommentLike").IsUnique();

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CommentLi__Comme__72910220");

            entity.HasOne(d => d.User).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CommentLi__UserI__73852659");
        });

        modelBuilder.Entity<Foetus>(entity =>
        {
            entity.HasKey(e => e.FoetusId).HasName("PK__tmp_ms_x__3291CDA294419FD2");

            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Foetus)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Foetus__UserId__17036CC0");
        });

        modelBuilder.Entity<GrowthDatum>(entity =>
        {
            entity.HasKey(e => e.GrowthDataId).HasName("PK__GrowthDa__AB04941B98C2EFB7");

            entity.Property(e => e.Ac).HasColumnName("AC");
            entity.Property(e => e.Efw).HasColumnName("EFW");
            entity.Property(e => e.Fl).HasColumnName("FL");
            entity.Property(e => e.Hc).HasColumnName("HC");

            entity.HasOne(d => d.Foetus).WithMany(p => p.GrowthData)
                .HasForeignKey(d => d.FoetusId)
                .HasConstraintName("FK__GrowthDat__Foetu__160F4887");

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.GrowthData)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK_GrowthData_GrowthStandard");
        });

        modelBuilder.Entity<GrowthStandard>(entity =>
        {
            entity.HasKey(e => e.GrowthStandardId).HasName("PK__GrowthSt__35704C05BC686B91");

            entity.ToTable("GrowthStandard");

            entity.Property(e => e.AcMedian).HasColumnName("AC_Median");
            entity.Property(e => e.EfwMedian).HasColumnName("EFW_Median");
            entity.Property(e => e.FlMedian).HasColumnName("FL_Median");
            entity.Property(e => e.HcMedian).HasColumnName("HC_Median");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Membersh__92A78679BDCF06DD");

            entity.ToTable("Membership");

            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38C080539E");

            entity.ToTable("Payment");

            entity.HasOne(d => d.Membership).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MembershipId)
                .HasConstraintName("FK__Payment__Members__7F2BE32F");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserId__00200768");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__tmp_ms_x__AA126018161537AE");

            entity.ToTable("Post");

            entity.Property(e => e.Body).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__UserId__7FEAFD3E");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__PostComm__C3B4DFCA65E05A03");

            entity.ToTable("PostComment");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__PostI__02C769E9");

            entity.HasOne(d => d.User).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostComme__UserI__6DCC4D03");
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => e.PostLikeId).HasName("PK__PostLike__4CF65C1904663E8E");

            entity.ToTable("PostLike");

            entity.HasIndex(e => new { e.PostId, e.UserId }, "UC_PostLike").IsUnique();

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLike__PostId__00DF2177");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLike__UserId__65370702");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasKey(e => e.PostTagId).HasName("PK__PostTag__325724FDAAAAFAE8");

            entity.ToTable("PostTag");

            entity.HasOne(d => d.Post).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostTag__PostId__01D345B0");

            entity.HasOne(d => d.Tag).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostTag__TagId__690797E6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AC6CC0A7F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CF9AC56A11A01");

            entity.ToTable("Tag");

            entity.Property(e => e.TagName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C4B1F909C");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__01142BA1");
        });

        modelBuilder.Entity<UserNote>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserNote");

            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Diagnosis).HasMaxLength(255);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.NoteId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UserReminder>(entity =>
        {
            entity.HasKey(e => e.RemindId).HasName("PK__UserRemi__C0874AD500AD4BE0");

            entity.Property(e => e.Notification).HasMaxLength(510);
            entity.Property(e => e.ReminderType).HasMaxLength(510);
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(510);

            entity.HasOne(d => d.User).WithMany(p => p.UserReminders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRemin__UserI__3B40CD36");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
