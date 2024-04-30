using AppDev2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppDev2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // Change DbSet<UserModel> name to Users
        public DbSet<UserModel1> User { get; set; }
        public DbSet<JobListingModel> JobListings { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            var keysProperties = modelBuilder.Model.GetEntityTypes()
                .Select(x => x.FindPrimaryKey())
                .SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }
            // Seed data for JobListingModel
            modelBuilder.Entity<JobListingModel>().HasData(
                new JobListingModel
                {
                    JobListingId = 1,
                    Title = "Software Engineer",
                    Description = "I am a software engineer",
                    ApplicationDeadline = DateTime.Today,
                    Location = "New York, NY"
                });

            // Seed data for ApplicationModel
            modelBuilder.Entity<ApplicationModel>().HasData(
                      new ApplicationModel
                      {
                          ApplicationId = 1,
                          JobListingId = 1,
                          Message = "Sample message", // Provide a value for the required property 'Message'
                          DisplayOrder = 1                            // Other properties for the application
                      });
            modelBuilder.Entity<RoleModel>().HasData(
                     new RoleModel
                     {
                         Name = "Admin",
                         NormalizedName = "Admin".ToUpper(),
                     });
            modelBuilder.Entity<RoleModel>().HasData(
                   new RoleModel
                   {
                       Name = "Customer",
                       NormalizedName = "Customer".ToUpper(),
                   });

        }
    }
}