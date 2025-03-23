using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduQuiz.DomainEntities;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.Identity;
using System.Reflection.Emit;
namespace EduQuiz.Repository;

public class ApplicationDbContext : IdentityDbContext<EduQuizUser>
{
    public DbSet<Quiz> Quizzes { get; set; }
    //public DbSet<Answer> Answers { get; set; }
    //public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
    //public DbSet<Tag> Tags { get; set; }
    public DbSet<EduQuizUser> EduQuizUsers { get; set; }
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
