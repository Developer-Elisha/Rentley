using Microsoft.EntityFrameworkCore;
using Rentley.Models;

namespace Rentley.Db_Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Your_Connection_String"); // Ensure you have your connection string
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Verify_Img>()
                .ToTable("tblVerify");


            modelBuilder.Entity<Rents>()
                .HasOne(r => r.User_Temp)
                .WithMany()
                .HasForeignKey(r => r.User_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rents>()
                .HasOne(r => r.Products)
                .WithMany()
                .HasForeignKey(r => r.Prod_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rents>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Users> tblUsers { get; set; }
        public DbSet<User_temp> tblUser_temp { get; set; }
        public DbSet<products> tblProducts { get; set; }
        public DbSet<category> tblCategory { get; set; }
        public DbSet<Verify_Img> tblVerify { get; set; }
        public DbSet<Rents> tblRents { get; set; }
    }
}