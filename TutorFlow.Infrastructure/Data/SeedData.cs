using System.Text;
using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        await context.Database.MigrateAsync();

        foreach (var role in new[] { "Admin", "Tutor", "Student" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var admin = await EnsureUserAsync(userManager, "admin@hlpl.local", "Адміністратор", "+380501112233", "Admin123!");
        var tutorUser = await EnsureUserAsync(userManager, "tutor@hlpl.local", "Олена Ковальчук", "+380671234567", "Tutor123!");
        var studentUser1 = await EnsureUserAsync(userManager, "student1@hlpl.local", "Марко Іваненко", "+380931112233", "Student123!");
        var studentUser2 = await EnsureUserAsync(userManager, "student2@hlpl.local", "Софія Мельник", "+380991112244", "Student123!");

        await EnsureRoleAsync(userManager, admin, "Admin");
        await EnsureRoleAsync(userManager, tutorUser, "Tutor");
        await EnsureRoleAsync(userManager, studentUser1, "Student");
        await EnsureRoleAsync(userManager, studentUser2, "Student");

        await RepairCorruptedTextAsync(context);

        if (!await context.TutorProfiles.AnyAsync())
        {
            context.TutorProfiles.Add(new TutorProfile
            {
                UserId = tutorUser.Id,
                Bio = "Репетитор з математики та фізики.",
                Experience = 7,
                HourlyRate = 500m
            });
        }

        if (!await context.StudentProfiles.AnyAsync())
        {
            context.StudentProfiles.AddRange(
                new StudentProfile
                {
                    UserId = studentUser1.Id,
                    Grade = 9,
                    Notes = "Підготовка до контрольних."
                },
                new StudentProfile
                {
                    UserId = studentUser2.Id,
                    Grade = 11,
                    Notes = "Підготовка до НМТ."
                });
        }

        if (!await context.Subjects.AnyAsync())
        {
            context.Subjects.AddRange(
                new Subject { Name = "Математика", Description = "Алгебра та геометрія." },
                new Subject { Name = "Фізика", Description = "Механіка, електрика, оптика." },
                new Subject { Name = "Англійська", Description = "Граматика та розмовна практика." });
        }

        await context.SaveChangesAsync();

        if (!await context.LearningMaterials.AnyAsync())
        {
            var math = await context.Subjects.FirstAsync(x => x.Name == "Математика");
            var physics = await context.Subjects.FirstAsync(x => x.Name == "Фізика");

            context.LearningMaterials.AddRange(
                new LearningMaterial
                {
                    SubjectId = math.Id,
                    Title = "Добірка задач з алгебри",
                    Description = "Посилання на вправи для самостійної роботи.",
                    Url = "https://example.com/math-practice"
                },
                new LearningMaterial
                {
                    SubjectId = physics.Id,
                    Title = "Відео з механіки",
                    Description = "Базові пояснення та приклади.",
                    Url = "https://example.com/physics-video"
                });
        }

        await context.SaveChangesAsync();

        if (!await context.Lessons.AnyAsync())
        {
            var tutor = await context.TutorProfiles.FirstAsync();
            var students = await context.StudentProfiles.OrderBy(x => x.Id).ToListAsync();
            var subjects = await context.Subjects.OrderBy(x => x.Id).ToListAsync();

            var lesson1 = new Lesson
            {
                TutorId = tutor.Id,
                StudentId = students[0].Id,
                SubjectId = subjects[0].Id,
                StartTime = DateTime.Today.AddDays(1).AddHours(16),
                EndTime = DateTime.Today.AddDays(1).AddHours(17),
                Price = 500m,
                Status = LessonStatus.Planned,
                Notes = "Повторення квадратних рівнянь."
            };

            var lesson2 = new Lesson
            {
                TutorId = tutor.Id,
                StudentId = students[1].Id,
                SubjectId = subjects[1].Id,
                StartTime = DateTime.Today.AddDays(-2).AddHours(18),
                EndTime = DateTime.Today.AddDays(-2).AddHours(19),
                Price = 550m,
                Status = LessonStatus.Completed,
                Notes = "Закон Ома та задачі."
            };

            context.Lessons.AddRange(lesson1, lesson2);
            await context.SaveChangesAsync();

            context.Homeworks.AddRange(
                new Homework
                {
                    LessonId = lesson1.Id,
                    Title = "Задачі на дискримінант",
                    Description = "Розв'язати 10 задач.",
                    Deadline = DateTime.Today.AddDays(5),
                    Status = HomeworkStatus.Assigned,
                    FileUrl = "https://example.com/homework-1"
                },
                new Homework
                {
                    LessonId = lesson2.Id,
                    Title = "Конспект з електрики",
                    Description = "Підготувати 2 сторінки конспекту.",
                    Deadline = DateTime.Today.AddDays(-1),
                    Status = HomeworkStatus.Submitted,
                    FileUrl = "https://example.com/homework-2"
                });

            context.Payments.AddRange(
                new Payment
                {
                    LessonId = lesson1.Id,
                    Amount = 500m,
                    IsPaid = false,
                    Comment = "Очікується оплата"
                },
                new Payment
                {
                    LessonId = lesson2.Id,
                    Amount = 550m,
                    IsPaid = true,
                    PaymentDate = DateTime.Today.AddDays(-1),
                    Comment = "Сплачено готівкою"
                });
        }

        await context.SaveChangesAsync();
    }

    private static async Task RepairCorruptedTextAsync(ApplicationDbContext context)
    {
        var users = await context.Users.ToListAsync();
        foreach (var user in users)
        {
            user.FullName = NormalizeText(user.FullName) ?? user.FullName;
        }

        var tutorProfiles = await context.TutorProfiles.ToListAsync();
        foreach (var profile in tutorProfiles)
        {
            profile.Bio = NormalizeText(profile.Bio);
        }

        var studentProfiles = await context.StudentProfiles.ToListAsync();
        foreach (var profile in studentProfiles)
        {
            profile.Notes = NormalizeText(profile.Notes);
        }

        var subjects = await context.Subjects.ToListAsync();
        foreach (var subject in subjects)
        {
            subject.Name = NormalizeText(subject.Name) ?? subject.Name;
            subject.Description = NormalizeText(subject.Description);
        }

        var materials = await context.LearningMaterials.ToListAsync();
        foreach (var material in materials)
        {
            material.Title = NormalizeText(material.Title) ?? material.Title;
            material.Description = NormalizeText(material.Description);
        }

        var lessons = await context.Lessons.ToListAsync();
        foreach (var lesson in lessons)
        {
            lesson.Notes = NormalizeText(lesson.Notes);
        }

        var homeworks = await context.Homeworks.ToListAsync();
        foreach (var homework in homeworks)
        {
            homework.Title = NormalizeText(homework.Title) ?? homework.Title;
            homework.Description = NormalizeText(homework.Description);
        }

        var payments = await context.Payments.ToListAsync();
        foreach (var payment in payments)
        {
            payment.Comment = NormalizeText(payment.Comment);
        }

        await context.SaveChangesAsync();
    }

    private static string? NormalizeText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || !LooksCorrupted(value))
        {
            return value;
        }

        var bytes = Encoding.GetEncoding(1251).GetBytes(value);
        return Encoding.UTF8.GetString(bytes);
    }

    private static bool LooksCorrupted(string value) =>
        value.Contains("Р") || value.Contains("С") || value.Contains("Ð") || value.Contains("Ñ");

    private static async Task<ApplicationUser> EnsureUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string fullName,
        string phone,
        string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            return user;
        }

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            PhoneNumber = phone,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(x => x.Description)));
        }

        return user;
    }

    private static async Task EnsureRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
    {
        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
