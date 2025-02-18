using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PregnancyGrowthTracking.DAL.Entities;

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

    public virtual DbSet<AcAlert> AcAlerts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EfwAlert> EfwAlerts { get; set; }

    public virtual DbSet<FlAlert> FlAlerts { get; set; }

    public virtual DbSet<Foetu> Foetus { get; set; }

    public virtual DbSet<ForumTag> ForumTags { get; set; }

    public virtual DbSet<GrowthDatum> GrowthData { get; set; }

    public virtual DbSet<GrowthStandard> GrowthStandards { get; set; }

    public virtual DbSet<HcAlert> HcAlerts { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserForum> UserForums { get; set; }

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
        modelBuilder.Entity<AcAlert>(entity =>
        {
            entity.HasKey(e => e.AcAlertsId).HasName("PK__AC_Alert__C2AEC94CF3551F8F");

            entity.ToTable("AC_Alerts");

            entity.Property(e => e.AcAlertsId).HasColumnName("AC_Alerts_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.AcAlerts)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK__AC_Alerts__Growt__5812160E");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Blog__B2C2B5CFADFFB01A");

            entity.ToTable("Blog");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Blog__CategoryId__59063A47");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BCDF81237");

            entity.ToTable("Category");

            entity.Property(e => e.Category1)
                .HasMaxLength(100)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<EfwAlert>(entity =>
        {
            entity.HasKey(e => e.EfwAlertsId).HasName("PK__EFW_Aler__09801C5793FCC792");

            entity.ToTable("EFW_Alerts");

            entity.Property(e => e.EfwAlertsId).HasColumnName("EFW_Alerts_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.EfwAlerts)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK__EFW_Alert__Growt__59FA5E80");
        });

        modelBuilder.Entity<FlAlert>(entity =>
        {
            entity.HasKey(e => e.FlAlertsId).HasName("PK__FL_Alert__993334F76F20F112");

            entity.ToTable("FL_Alerts");

            entity.Property(e => e.FlAlertsId).HasColumnName("FL_Alerts_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.FlAlerts)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK__FL_Alerts__Growt__5AEE82B9");
        });

        modelBuilder.Entity<Foetu>(entity =>
        {
            entity.HasKey(e => e.FoetusId).HasName("PK__Foetus__3291CDA2244862C2");

            entity.HasOne(d => d.User).WithMany(p => p.Foetus)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Foetus__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<ForumTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__ForumTag__657CF9ACD5DA33C1");

            entity.ToTable("ForumTag");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<GrowthDatum>(entity =>
        {
            entity.HasKey(e => e.GrowthDataId).HasName("PK__GrowthDa__AB04941B8A6ECEA0");

            entity.Property(e => e.Ac).HasColumnName("AC");
            entity.Property(e => e.Efw).HasColumnName("EFW");
            entity.Property(e => e.Fl).HasColumnName("FL");
            entity.Property(e => e.Hc).HasColumnName("HC");

            entity.HasOne(d => d.Foetus).WithMany(p => p.GrowthData)
                .HasForeignKey(d => d.FoetusId)
                .HasConstraintName("FK__GrowthDat__Foetu__5CD6CB2B");

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.GrowthData)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK_GrowthData_GrowthStandard");
        });

        modelBuilder.Entity<GrowthStandard>(entity =>
        {
            entity.HasKey(e => e.GrowthStandardId).HasName("PK__GrowthSt__35704C0515579100");

            entity.ToTable("GrowthStandard");

            entity.Property(e => e.AcMedian).HasColumnName("AC_Median");
            entity.Property(e => e.EfwMedian).HasColumnName("EFW_Median");
            entity.Property(e => e.FlMedian).HasColumnName("FL_Median");
            entity.Property(e => e.HcMedian).HasColumnName("HC_Median");
        });

        modelBuilder.Entity<HcAlert>(entity =>
        {
            entity.HasKey(e => e.HcAlertsId).HasName("PK__HC_Alert__1F372C2EFBE5711D");

            entity.ToTable("HC_Alerts");

            entity.Property(e => e.HcAlertsId).HasColumnName("HC_Alerts_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.GrowthStandard).WithMany(p => p.HcAlerts)
                .HasForeignKey(d => d.GrowthStandardId)
                .HasConstraintName("FK__HC_Alerts__Growt__5EBF139D");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Membersh__92A78679B733E091");

            entity.ToTable("Membership");

            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A386FAF2FB1");

            entity.ToTable("Payment");

            entity.HasOne(d => d.Membership).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MembershipId)
                .HasConstraintName("FK__Payment__Members__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserId__60A75C0F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AA4E3066C");

            entity.ToTable("Role");

            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C6136E9AE");

            entity.ToTable("User");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UserImage).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__619B8048");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__UserComm__C3B4DFCA502B5D80");

            entity.ToTable("UserComment");

            entity.Property(e => e.Detail).HasMaxLength(255);

            entity.HasOne(d => d.Blog).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__UserComme__BlogI__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserComme__UserI__6383C8BA");
        });

        modelBuilder.Entity<UserForum>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__UserForu__54379E30305483C7");

            entity.ToTable("UserForum");

            entity.Property(e => e.Detail).HasMaxLength(255);

            entity.HasOne(d => d.Tag).WithMany(p => p.UserForums)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_UserForum_ForumTag");

            entity.HasOne(d => d.User).WithMany(p => p.UserForums)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserForum__UserI__6477ECF3");
        });

        modelBuilder.Entity<UserNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__UserNote__EACE355FE018F24F");

            entity.ToTable("UserNote");

            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.UserNotePhoto).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserNotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserNote__UserId__66603565");
        });

        modelBuilder.Entity<UserReminder>(entity =>
        {
            entity.HasKey(e => e.RemindId).HasName("PK__UserRemi__C0874AD5B3733051");

            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserReminders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRemin__UserI__6754599E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
