using Microsoft.EntityFrameworkCore;
using BeltExam.Models;

namespace BeltExam.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Join> Joins { get; set; }
    }
}