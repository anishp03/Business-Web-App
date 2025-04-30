using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TechTickles.Models;

public class ApplicationDbContext: DbContext{
  
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

  public DbSet<Customer> Customers { get; set; }
  public DbSet<CreditCard> CreditCards { get; set; }
  public DbSet<Review> Reviews { get; set; }
  public DbSet<CustomerSubscription> CustomerSubscriptions { get; set; }
  public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
  public DbSet<Feature> Features { get; set; }
  public DbSet<Cart> Cart { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Customer>()
        .HasIndex(c => c.Email)
        .IsUnique()
        .HasFilter("CHAR_LENGTH(Email) <= 255");

        modelBuilder.Entity<SubscriptionPlan>()
        .HasMany(s => s.Feature)
        .WithMany(f => f.SubscriptionPlan)
        .UsingEntity(j => j.ToTable("SubscriptionPlanFeatures"));

        modelBuilder.Entity<Feature>().HasData(
            new Feature { FeatureId = 1, Name = "No Features", Descripton = "No Features" },
            new Feature { FeatureId = 2, Name = "Real-Time Malware Protection", Descripton = "Detects malware while you are on the web and as soon as a malicious site attempts to invade our software will remove it immediately" },
            new Feature { FeatureId = 3, Name = "Virtual Private Network(VPN)", Descripton = "High-Speed secure internet connection that blocks your IP address and prevents network based attacks" },
            new Feature { FeatureId = 4, Name = "Real-Time Antivirus Protection", Descripton = "Real-Time Virus Detection while you are browsing and downloading from the web, then our software will remove this without you knowing" },
            new Feature { FeatureId = 5, Name = "Data Scrubber", Descripton = "Clean your digital footprint from spam websites and scams and preventing your digital footprint to leave a trail on all the sits you visit" },
            new Feature { FeatureId = 6, Name = "Identity Theft Protection", Descripton = "Tracks where your information is being used and detects if your personal information has been leaked such as payment information, and key identity informaton" },
            new Feature { FeatureId = 7, Name = "Ad Blocker", Descripton = "Prevent pop-up ads from injecting malware into your system. This software detects websites trying to give pop-up ads and prevents it from being shown" },
            new Feature { FeatureId = 8, Name = "Limited Malware and Virus Scan", Descripton = "Scan through your computer's internal drive and history for potential malware rooted inside" },
            new Feature { FeatureId = 9, Name = "Password Manager", Descripton = "Securely store your passwords and generate strong passwords for you to use. Protected by our own database your passwords will be protected from being leaked" },
            new Feature { FeatureId = 10, Name = "Secure File Shredder", Descripton = "Permanently delete files from your computer and prevent them from being recovered by hackers. Important for confidential files related to your personal data" }

        );

        modelBuilder.Entity<SubscriptionPlan>().HasData(
          new SubscriptionPlan
            {
                PlanId = 1,
                Name = "None",
                Price = 0,
                Description = "You are not subscribed to any plan!"
            },
            new SubscriptionPlan
            {
                PlanId = 2,
                Name = "Tickles Basic",
                Price = 50.00,
                Description = "This is a basic plan that includes featues that are essential to protect your information on the web like: Ad Blocker, Limited Malware and Virus Scans, and an efficient Password Manager"
            },
            new SubscriptionPlan
            {
                PlanId = 3,
                Name = "Tickles Plus",
                Price = 89.00,
                Description = "Middle tier plan providing more security measures for users that are more active on the web. This plan includes all the features from the Basic plan and adds: Virtual Private Network(VPN), Identity Theft Protection, and a Secure File Shredder"
            },
            new SubscriptionPlan
            {
                PlanId = 4,
                Name = "Tickles Ultimate",
                Price = 101.00,
                Description = "Top of the line plan with every security measure taken to protect your information on the web. This plan includes all of the features of the other plans and adds : Data Scrubber, Real-Time Malware and Virus Protection"
            }
        );

        // Linking Features to Subscription Plans
        modelBuilder.Entity<SubscriptionPlan>()
            .HasMany(s => s.Feature)
            .WithMany(f => f.SubscriptionPlan)
            .UsingEntity<Dictionary<string, object>>(

              j => j.HasOne<Feature>()
                .WithMany()
                .HasForeignKey("FeatureFeatureId"),

              j => j.HasOne<SubscriptionPlan>()
                .WithMany()
                .HasForeignKey("SubscriptionPlanPlanId"),

              j => { 
                j.HasData(
                new { SubscriptionPlanPlanId = 1, FeatureFeatureId = 1 },

                new { SubscriptionPlanPlanId = 2, FeatureFeatureId = 8 },
                new { SubscriptionPlanPlanId = 2, FeatureFeatureId = 7 },
                new { SubscriptionPlanPlanId = 2, FeatureFeatureId = 9 },

                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 7 },
                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 8 },
                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 3 },
                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 6 },
                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 9 },
                new { SubscriptionPlanPlanId = 3, FeatureFeatureId = 10 },
                
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 7 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 8 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 3 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 6 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 9 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 10 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 5 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 4 },
                new { SubscriptionPlanPlanId = 4, FeatureFeatureId = 2 }
    
              
            );});
    }
    
}