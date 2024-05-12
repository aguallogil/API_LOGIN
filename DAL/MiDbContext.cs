using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options):base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
