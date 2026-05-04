using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Services;
using TutorFlow.Domain.Entities;
using TutorFlow.Infrastructure.Data;
using TutorFlow.Infrastructure.Repositories;
using TutorFlow.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TutorFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IHomeworkService, HomeworkService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ILearningMaterialService, LearningMaterialService>();
        services.AddScoped<IUserManagementService, UserManagementService>();

        services.AddHttpClient<IHolidayApiService, HolidayApiService>(client =>
        {
            client.BaseAddress = new Uri("https://date.nager.at/api/v3/PublicHolidays/");
        });

        return services;
    }
}

