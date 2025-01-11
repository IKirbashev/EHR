using ElectronicHealthRecord.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Biomarker> Biomarkers { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base to configure Identity-related tables

            // Configure User table
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.DateOfBirth).IsRequired();
            });

            // Configure Relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Biomarkers)
                .WithOne(b => b.User)   
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Medications)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CalendarEvents)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Folders)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.DiaryEntries)
                .WithOne(d => d.Folder)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiaryEntry>()
                .HasOne(d => d.User)
                .WithMany(u => u.DiaryEntries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
