using System.Buffers.Text;
using System.Data.Entity;
using CodeBreaker.Models;

namespace CodeBreaker
{
    public class PrimeContext : DbContext
    {
        public virtual DbSet<XY> Primes { get; set; }

        public PrimeContext() : base("Primes"){}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XY>().ToTable("Primes");
            modelBuilder.Entity<XY>().Property(p => p.X).HasColumnName("X");
            modelBuilder.Entity<XY>().Property(p => p.Y).HasColumnName("Y");
        }
    }
}
