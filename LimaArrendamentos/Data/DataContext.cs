using LimaArrendamentos.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LimaArrendamentos.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Ad> Ads { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Bondsman> Bondsmen { get; set; }

        public DbSet<SendEmail> SendEmails { get; set; }

        public DbSet<EnergyClass> EnergyClasses { get; set; }
        public DbSet<Favorite> Favorites{ get; set; }

        public DbSet<Lease> Leases { get; set; }

        public DbSet<LeaseMessage> LeaseMessages { get; set; }
        public DbSet<LeaseDetailTemp> LeaseDetailTemp { get; set; }

        public DbSet<PostalCode> PostalCodes { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Realty> Realties { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }

        public DbSet<Typology> Typologies { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
