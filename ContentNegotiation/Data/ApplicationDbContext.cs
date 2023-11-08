using ContentNegotiation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ContentNegotiation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student {Id = 1,FirstName = "Shakeel",LastName = "Abbas"},
                new Student { Id = 2, FirstName = "Bilal", LastName = "Asghar" }
            );
        }
    }
}
