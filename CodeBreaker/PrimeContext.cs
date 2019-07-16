using System.Data.Entity;
using CodeBreaker.Models;

namespace CodeBreaker
{
    public class PrimeContext : DbContext
    {
        public DbSet<Prime> Primes { get; set; }
    }
}