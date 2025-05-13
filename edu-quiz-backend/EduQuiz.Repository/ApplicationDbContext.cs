using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduQuiz.DomainEntities;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.Identity;
using System.Reflection.Emit;
using Newtonsoft.Json;

namespace EduQuiz.Repository;

public class ApplicationDbContext : IdentityDbContext<EduQuizUser>
{
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Reccomendation> Reccomendations { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<EduQuizUser> EduQuizUsers { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
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

        modelBuilder.Entity<Quiz>()
            .HasMany(q => q.Questions)
            .WithOne(a => a.Quiz)
            .HasForeignKey(a => a.QuizId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(q => q.QuestionId);

        modelBuilder.Entity<Result>()
            .HasOne<EduQuizUser>()
            .WithMany()
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Result>()
            .HasOne<Quiz>()
            .WithMany()
            .HasForeignKey(r => r.QuizId);

        modelBuilder.Entity<Reccomendation>()
            .HasOne<EduQuizUser>()
            .WithMany()
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Reccomendation>()
            .HasOne<Quiz>()
            .WithMany()
            .HasForeignKey(r => r.QuizId);
        modelBuilder.Entity<UserAnswer>()
            .Property(u => u.SelectedAnswerIds)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<Guid>>(v)
            );
    }

}
