Створи ASP.NET Core MVC проект на тему:

“Веб-платформа для організації занять між репетитором та учнями”

Мета проекту:
Розробити сайт, де репетитор може керувати учнями, розкладом занять, домашніми завданнями та оплатами, а учень може переглядати свій розклад, домашні завдання, матеріали та статус оплат.

Технології:
- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- Bootstrap
- SQL Server або SQLite
- Clean Architecture / Layered Architecture
- Repository Pattern
- DTO + Mapping
- Sessions
- Async methods

Основні ролі:
1. Admin
2. Tutor
3. Student

Функціонал для Tutor:
- перегляд власного кабінету
- список учнів
- створення / редагування / видалення учнів
- створення занять
- перегляд календаря або списку запланованих занять
- додавання домашнього завдання
- перегляд виконаних / невиконаних домашніх завдань
- ведення оплат
- перегляд доходу за місяць
- перегляд боргів учнів

Функціонал для Student:
- перегляд особистого кабінету
- перегляд розкладу занять
- перегляд домашніх завдань
- позначення домашнього завдання як виконаного
- перегляд навчальних матеріалів
- перегляд статусу оплат

Функціонал для Admin:
- керування користувачами
- керування предметами
- перегляд усіх занять
- перегляд усіх оплат

Сутності бази даних:
1. ApplicationUser
   - Id
   - UserName
   - Email
   - FullName
   - PhoneNumber

2. TutorProfile
   - Id
   - UserId
   - Bio
   - Experience
   - HourlyRate

3. StudentProfile
   - Id
   - UserId
   - Grade
   - Notes

4. Subject
   - Id
   - Name
   - Description

5. Lesson
   - Id
   - TutorId
   - StudentId
   - SubjectId
   - StartTime
   - EndTime
   - Price
   - Status: Planned, Completed, Cancelled
   - Notes

6. Homework
   - Id
   - LessonId
   - Title
   - Description
   - Deadline
   - Status: Assigned, Submitted, Checked
   - FileUrl або Link

7. Payment
   - Id
   - LessonId
   - Amount
   - IsPaid
   - PaymentDate
   - Comment

8. LearningMaterial
   - Id
   - SubjectId
   - Title
   - Description
   - Url

Обов’язкові можливості:
- CRUD для Lessons, Homework, Payments, Subjects, Students
- фільтрація занять за датою, учнем, предметом і статусом
- пагінація списку занять
- валідація українського номера телефону
- всі методи контролерів і сервісів мають бути async
- використати Generic Repository Pattern
- винести бізнес-логіку в Services
- створити DTO:
  - LessonDto
  - HomeworkDto
  - PaymentDto
  - StudentDto
- реалізувати Mapping між Entity та DTO
- реалізувати Specifications:
  - LessonsByDateSpecification
  - LessonsByStudentSpecification
  - UnpaidLessonsSpecification
  - TutorLessonsWithStudentAndSubjectSpecification
- реалізувати Session-функціонал:
  “обрані навчальні матеріали” або “чернетка заняття”
- реалізувати Partial View для списку занять або домашніх завдань
- додати інтеграцію з будь-яким простим зовнішнім API, наприклад:
  API свят або календаря, щоб показувати попередження, якщо заняття заплановане на святковий день
- створити окрему DLL/Class Library для бухгалтерських розрахунків:
  - CalculateMonthlyIncome
  - CalculateStudentDebt
  - CalculateLessonTotal

Архітектура проекту:
- Web
  - Controllers
  - Views
  - ViewModels
- Application
  - DTOs
  - Services
  - Interfaces
  - Specifications
- Domain
  - Entities
  - Enums
- Infrastructure
  - DbContext
  - Repositories
  - Migrations
- AccountingLibrary
  - клас для розрахунків оплат

Потрібно реалізовувати проект поетапно:

Stage 1:
Створи базовий ASP.NET Core MVC проект з Identity, Bootstrap layout, Home page, navigation menu.

Stage 2:
Додай всі Domain entities, ApplicationDbContext, зв’язки між таблицями та міграції.

Stage 3:
Додай ролі Admin, Tutor, Student і seed-дані для тестування.

Stage 4:
Реалізуй CRUD для Subjects, Students, Lessons, Homework, Payments.

Stage 5:
Перенеси логіку з контролерів у Services, додай async methods.

Stage 6:
Додай Generic Repository Pattern.

Stage 7:
Додай DTO та Mapping.

Stage 8:
Додай Specifications для фільтрації занять і оплат.

Stage 9:
Додай пагінацію для Lessons.

Stage 10:
Додай Session-функціонал для обраних матеріалів або чернетки заняття.

Stage 11:
Додай Partial Views для списку занять або домашніх завдань.

Stage 12:
Додай валідацію українського номера телефону.

Stage 13:
Додай зовнішнє API.

Stage 14:
Додай DLL/Class Library для бухгалтерських розрахунків.

Stage 15:
Підготуй проект до деплою: appsettings, міграції, README, інструкція запуску.

Важливо:
- Пиши простий і зрозумілий код.
- Не ускладнюй архітектуру надмірно.
- Коментуй основні класи.
- Після кожного етапу пояснюй, які файли були створені або змінені.
- Проект має бути придатним для демонстрації на лабораторних роботах.