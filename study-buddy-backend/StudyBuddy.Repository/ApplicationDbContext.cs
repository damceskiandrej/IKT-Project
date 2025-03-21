using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.DomainEntities;
using StudyBuddy.DomainEntities.Domain;
using StudyBuddy.DomainEntities.Identity;
using System.Reflection.Emit;
namespace StudyBuddy.Repository;

public class ApplicationDbContext : IdentityDbContext<StudyBuddyUser>
{
    public DbSet<Quiz> Quizzes { get; set; }
    //public DbSet<Answer> Answers { get; set; }
    //public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
    //public DbSet<Tag> Tags { get; set; }
    public DbSet<StudyBuddyUser> StudyBuddyUsers { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { 

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Quiz>()
        //    .HasMany(q => q.Answers)
        //    .WithOne(a => a.Quiz)
        //    .HasForeignKey(a => a.QuizId);

        //modelBuilder.Entity<Quiz>()
        //    .HasMany(q => q.CorrectAnswers)
        //    .WithOne(ca => ca.Answer)
        //    .HasForeignKey(ca => ca.);

        //modelBuilder.Entity<Quiz>()
        //    .HasMany(q => q.Tags)
        //    .WithOne(t => t.Quiz)
        //    .HasForeignKey(t => t.QuizId);
    }
}
