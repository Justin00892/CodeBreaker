using CodeBreaker.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker
{
    public class PrimeContext : DbContext
    {
        public DbSet<Prime> Primes { get; set; }
    }
}