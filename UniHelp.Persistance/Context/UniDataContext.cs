using DefaultNamespace;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using TaskEntity = UniHelp.Domain.Entities.Task;

namespace UniHelp.Persistance.Context;

public class UniDataContext : IdentityDbContext<User>
{
    public UniDataContext(DbContextOptions<UniDataContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<StudentTask> StudentTasks { get; set; }
    public DbSet<TestQuestion> TestQuestions { get; set; }
    public DbSet<AnswerVariant> AnswerVariants { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentClasses)
            .HasForeignKey(sc => sc.StudentId);
        
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Class)
            .WithMany(c => c.StudentClasses)
            .HasForeignKey(sc => sc.ClassId);

        modelBuilder.Entity<TestQuestion>()
            .HasOne(q => q.CorrectAnswer)
            .WithMany()
            .HasForeignKey(q => q.CorrectAnswerId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AnswerVariant>()
            .HasOne(av => av.Question)
            .WithMany(q => q.AnswerVariants)
            .HasForeignKey(av => av.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
            .HasOne(sc => sc.User)
            .WithOne(u => u.Student)
            .HasForeignKey<User>(u => u.StudentId);
        
        modelBuilder.Entity<Teacher>()
            .HasOne(sc => sc.User)
            .WithOne(u => u.Teacher)
            .HasForeignKey<User>(u => u.TeacherId);
    }
}