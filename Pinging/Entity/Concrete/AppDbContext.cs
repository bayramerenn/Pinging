using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinging.Entity.Concrete
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
   
        public DbSet<Ip> Ips { get; set; }
        public DbSet<AdslType> AdslTypes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AdslDescription>(eb =>
        //    {
        //        eb.HasNoKey();
        //    });
        //}
    }
}
