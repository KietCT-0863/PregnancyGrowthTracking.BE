using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL;

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

    public virtual DbSet<AcStandard> AcStandards { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EfwAlert> EfwAlerts { get; set; }

    public virtual DbSet<EfwStandard> EfwStandards { get; set; }

    public virtual DbSet<FlAlert> FlAlerts { get; set; }

    public virtual DbSet<FlStandard> FlStandards { get; set; }

    public virtual DbSet<Foetu> Foetus { get; set; }

    public virtual DbSet<GrowthCheck> GrowthChecks { get; set; }

    public virtual DbSet<GrowthValue> GrowthValues { get; set; }

    public virtual DbSet<HcAlert> HcAlerts { get; set; }

    public virtual DbSet<HcStandard> HcStandards { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Standard> Standards { get; set; }

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
            entity.HasKey(e => e.AcAlertsId).HasName("PK__AC_Alert__C2AEC94C9C043987");

            entity.ToTable("AC_Alerts");

            entity.Property(e => e.AcAlertsId).HasColumnName("AC_Alerts_Id");
            entity.Property(e => e.AcId).HasColumnName("AC_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.Ac).WithMany(p => p.AcAlerts)
                .HasForeignKey(d => d.AcId)
                .HasConstraintName("FK__AC_Alerts__AC_Id__214BF109");
        });

        modelBuilder.Entity<AcStandard>(entity =>
        {
            entity.HasKey(e => e.AcId).HasName("PK__AC_Stand__BF51180856688109");

            entity.ToTable("AC_Standard");

            entity.Property(e => e.AcId).HasColumnName("AC_Id");

            entity.HasOne(d => d.Standard).WithMany(p => p.AcStandards)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("FK__AC_Standa__Stand__15DA3E5D");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Blog__B2C2B5CF1CADD8CF");

            entity.ToTable("Blog");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Blog__CategoryId__40C49C62");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BCFE1070E");

            entity.ToTable("Category");

            entity.Property(e => e.Category1)
                .HasMaxLength(100)
                .HasColumnName("Category");
        });

        modelBuilder.Entity<EfwAlert>(entity =>
        {
            entity.HasKey(e => e.EfwAlertsId).HasName("PK__EFW_Aler__09801C5705A3F8C4");

            entity.ToTable("EFW_Alerts");

            entity.Property(e => e.EfwAlertsId).HasColumnName("EFW_Alerts_Id");
            entity.Property(e => e.EfwId).HasColumnName("EFW_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.Efw).WithMany(p => p.EfwAlerts)
                .HasForeignKey(d => d.EfwId)
                .HasConstraintName("FK__EFW_Alert__EFW_I__29E1370A");
        });

        modelBuilder.Entity<EfwStandard>(entity =>
        {
            entity.HasKey(e => e.EfwId).HasName("PK__EFW_Stan__71508FF3E4C7441E");

            entity.ToTable("EFW_Standard");

            entity.Property(e => e.EfwId).HasColumnName("EFW_Id");

            entity.HasOne(d => d.Standard).WithMany(p => p.EfwStandards)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("FK__EFW_Stand__Stand__1E6F845E");
        });

        modelBuilder.Entity<FlAlert>(entity =>
        {
            entity.HasKey(e => e.FlAlertsId).HasName("PK__FL_Alert__993334F7CE3C76EB");

            entity.ToTable("FL_Alerts");

            entity.Property(e => e.FlAlertsId).HasColumnName("FL_Alerts_Id");
            entity.Property(e => e.FlId).HasColumnName("FL_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.Fl).WithMany(p => p.FlAlerts)
                .HasForeignKey(d => d.FlId)
                .HasConstraintName("FK__FL_Alerts__FL_Id__24285DB4");
        });

        modelBuilder.Entity<FlStandard>(entity =>
        {
            entity.HasKey(e => e.FlId).HasName("PK__FL_Stand__FEEBB4AA1C3AC946");

            entity.ToTable("FL_Standard");

            entity.Property(e => e.FlId).HasColumnName("FL_Id");

            entity.HasOne(d => d.Standard).WithMany(p => p.FlStandards)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("FK__FL_Standa__Stand__18B6AB08");
        });

        modelBuilder.Entity<Foetu>(entity =>
        {
            entity.HasKey(e => e.FoetusId).HasName("PK__Foetus__3291CDA26F4C3209");

            entity.HasOne(d => d.User).WithMany(p => p.Foetus)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Foetus__UserId__0A688BB1");
        });

        modelBuilder.Entity<GrowthCheck>(entity =>
        {
            entity.HasKey(e => e.GrowthCheckId).HasName("PK__GrowthCh__70A97993D6F2AE3D");

            entity.ToTable("GrowthCheck");

            entity.HasOne(d => d.Foetus).WithMany(p => p.GrowthChecks)
                .HasForeignKey(d => d.FoetusId)
                .HasConstraintName("FK__GrowthChe__Foetu__0D44F85C");
        });

        modelBuilder.Entity<GrowthValue>(entity =>
        {
            entity.HasKey(e => e.GrowthValueId).HasName("PK__GrowthVa__A306D462FF458C7E");

            entity.ToTable("GrowthValue");

            entity.Property(e => e.Ac).HasColumnName("AC");
            entity.Property(e => e.Efw).HasColumnName("EFW");
            entity.Property(e => e.Fl).HasColumnName("FL");
            entity.Property(e => e.Hc).HasColumnName("HC");

            entity.HasOne(d => d.GrowthCheck).WithMany(p => p.GrowthValues)
                .HasForeignKey(d => d.GrowthCheckId)
                .HasConstraintName("FK__GrowthVal__Growt__1209AD79");

            entity.HasOne(d => d.Standard).WithMany(p => p.GrowthValues)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("FK__GrowthVal__Stand__12FDD1B2");
        });

        modelBuilder.Entity<HcAlert>(entity =>
        {
            entity.HasKey(e => e.HcAlertsId).HasName("PK__HC_Alert__1F372C2E499CEFB3");

            entity.ToTable("HC_Alerts");

            entity.Property(e => e.HcAlertsId).HasColumnName("HC_Alerts_Id");
            entity.Property(e => e.HcId).HasColumnName("HC_Id");
            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.Hc).WithMany(p => p.HcAlerts)
                .HasForeignKey(d => d.HcId)
                .HasConstraintName("FK__HC_Alerts__HC_Id__2704CA5F");
        });

        modelBuilder.Entity<HcStandard>(entity =>
        {
            entity.HasKey(e => e.HcId).HasName("PK__HC_Stand__3BF2C9B01F520EE7");

            entity.ToTable("HC_Standard");

            entity.Property(e => e.HcId).HasColumnName("HC_Id");

            entity.HasOne(d => d.Standard).WithMany(p => p.HcStandards)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("FK__HC_Standa__Stand__1B9317B3");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Membersh__92A786796969F1BC");

            entity.ToTable("Membership");

            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38933B7014");

            entity.ToTable("Payment");

            entity.HasOne(d => d.Membership).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MembershipId)
                .HasConstraintName("FK__Payment__Members__2F9A1060");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserId__2EA5EC27");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A06F80705");

            entity.ToTable("Role");

            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Standard>(entity =>
        {
            entity.HasKey(e => e.StandardId).HasName("PK__Standard__BB33D20C315AC782");

            entity.ToTable("Standard");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CBB25C1B5");

            entity.ToTable("User");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(64).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleId__078C1F06");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__UserComm__C3B4DFCA063FF905");

            entity.ToTable("UserComment");

            entity.Property(e => e.Detail).HasMaxLength(255);

            entity.HasOne(d => d.Blog).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__UserComme__BlogI__3BFFE745");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserComme__UserI__3B0BC30C");
        });

        modelBuilder.Entity<UserForum>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__UserForu__54379E30DBDF50EE");

            entity.ToTable("UserForum");

            entity.Property(e => e.Detail).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserForums)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserForum__UserI__382F5661");
        });

        modelBuilder.Entity<UserNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__UserNote__EACE355F91162695");

            entity.ToTable("UserNote");

            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.UserNotePhoto).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserNotes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserNote__UserId__3552E9B6");
        });

        modelBuilder.Entity<UserReminder>(entity =>
        {
            entity.HasKey(e => e.RemindId).HasName("PK__UserRemi__C0874AD525F2C319");

            entity.Property(e => e.Notification).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserReminders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRemin__UserI__32767D0B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
