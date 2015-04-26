using DotNetBay.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.EF
{
    class MainDbContext : System.Data.Entity.DbContext
    {
      public MainDbContext() : base("DatabaseConnection")
      {
          this.Configuration.LazyLoadingEnabled = false;
          this.Configuration.ProxyCreationEnabled = false;
      }


      public DbSet<Auction> Auctions { get; set; }
      public DbSet<Member> Members { get; set; }
      public DbSet<Bid> Bids { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          base.OnModelCreating(modelBuilder);

          modelBuilder.Conventions.Add(new DateTime2Convention());

          modelBuilder.Entity<Auction>().HasRequired(a => a.Seller).WithMany(member => member.Auctions);
          modelBuilder.Entity<Auction>().HasMany(a => a.Bids).WithRequired(b => b.Auction);
      }

    }


}
