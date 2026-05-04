using TutorFlow.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TutorProfile> TutorProfiles => Set<TutorProfile>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Homework> Homeworks => Set<Homework>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<LearningMaterial> LearningMaterials => Set<LearningMaterial>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TutorProfile>()
            .HasOne(x => x.User)
            .WithOne(x => x.TutorProfile)
            .HasForeignKey<TutorProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentProfile>()
            .HasOne(x => x.User)
            .WithOne(x => x.StudentProfile)
            .HasForeignKey<StudentProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Lesson>()
            .HasOne(x => x.Tutor)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Lesson>()
            .HasOne(x => x.Student)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Lesson>()
            .HasOne(x => x.Subject)
            .WithMany(x => x.Lessons)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Homework>()
            .HasOne(x => x.Lesson)
            .WithOne(x => x.Homework)
            .HasForeignKey<Homework>(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Payment>()
            .HasOne(x => x.Lesson)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LearningMaterial>()
            .HasOne(x => x.Subject)
            .WithMany(x => x.LearningMaterials)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TutorProfile>().Navigation(x => x.User).AutoInclude();
        builder.Entity<StudentProfile>().Navigation(x => x.User).AutoInclude();
        builder.Entity<Lesson>().Navigation(x => x.Tutor).AutoInclude();
        builder.Entity<Lesson>().Navigation(x => x.Student).AutoInclude();
        builder.Entity<Lesson>().Navigation(x => x.Subject).AutoInclude();
        builder.Entity<Lesson>().Navigation(x => x.Payment).AutoInclude();
        builder.Entity<Homework>().Navigation(x => x.Lesson).AutoInclude();
        builder.Entity<Payment>().Navigation(x => x.Lesson).AutoInclude();
        builder.Entity<LearningMaterial>().Navigation(x => x.Subject).AutoInclude();
    }
}

