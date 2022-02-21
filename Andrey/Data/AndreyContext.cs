#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Andrey.Models;

namespace Andrey.Data
{
    public class AndreyContext : DbContext
    {
        public AndreyContext (DbContextOptions<AndreyContext> options)
            : base(options)
        {
        }

        public DbSet<Andrey.Models.User> User { get; set; }

        public DbSet<Andrey.Models.Student> Student { get; set; }

        public DbSet<Andrey.Models.Teacher> Teacher { get; set; }

        public DbSet<Andrey.Models.Course> Course { get; set; }

        public DbSet<Andrey.Models.Lessong> Lessong { get; set; }

        public DbSet<Andrey.Models.Comment> Comment { get; set; }

        public DbSet<Andrey.Models.Sertificate> Sertificate { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sertificate>()
                .HasOne(p => p.Course)
                .WithMany(t => t.Sertificates)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sertificate>()
                .HasOne(p => p.Student)
                .WithMany(t => t.Sertificates)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(t => t.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Andrey.Models.UserData> UserData { get; set; }
        public DbSet<Andrey.Models.AppMaintenance> AppMaintenances { get; set; }
        public object Users { get; internal set; }
    }
}
