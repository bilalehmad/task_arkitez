using Microsoft.EntityFrameworkCore;
using TaskProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskProject.DBContext
{
    public class DBContextIdentity: DbContext
    {
        public DBContextIdentity(DbContextOptions dbContextOptions): base(dbContextOptions)
        {


        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(i => i.Products)
                .WithOne(s => s.Categories)
                .HasForeignKey(f => f.CatID);
        }
    }
}
