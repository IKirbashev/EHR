using ElectronicHealthRecord.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Biomarker> Biomarkers { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Biomarkers)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Medications)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CalendarEvents)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Folders)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.DiaryEntries)
                .WithOne(d => d.Folder)
                .HasForeignKey(d => d.FolderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
