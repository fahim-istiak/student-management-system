using System.Data.Entity;
using Student_Management_System__MVC_.Models;

namespace Student_Management_System__MVC_.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext() : base("name=StudentDBContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(s => s.Age)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.ImagePath)
                .HasMaxLength(255);

            base.OnModelCreating(modelBuilder);
        }
    }
} 