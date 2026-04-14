# HLPL_lab_1 Project Guide

## Purpose
This is an ASP.NET Core MVC study project for variant 14: cinema website.

- Lab 1: cinema home page, schedule, movie cards, posters, Bootstrap layout.
- Lab 2: new `Booking` section, separate controller, model binding, form submission back to controller.

## Lab 1 summary

### Required by the assignment
- Create an ASP.NET Core MVC project.
- Adapt header, content and footer to the chosen topic.
- Use Bootstrap components.
- Build a cinema web page for variant 14.

### Implemented here
- Cinema theme: `Kinopalats Rivne`.
- Custom header, navigation and footer.
- Main page with movie cards.
- Schedule page.
- Movie details page.
- Bootstrap grid, buttons, cards and forms.

## Lab 2 summary

### Required by the assignment
- Add a new menu item.
- Create a controller and a view for that item.
- Pass a string or number from controller to view.
- Create a model for the chosen topic.
- Pass the model from controller to view.
- Pass the model from view back to controller.

### Implemented here
- New menu item: `Booking`.
- New controller: `BookingController`.
- New model: `BookingViewModel`.
- Controller sends `BookingHint`, `TicketPrice`, `AvailableSeats` to the view.
- Controller sends `BookingViewModel` to the booking form.
- Booking form posts the model back to the controller.
- Confirmation page shows submitted data and total price.

## File-by-file explanation

### Root files

#### [Program.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Program.cs)
Application entry point.

- Registers MVC with `AddControllersWithViews()`.
- Configures middleware.
- Sets the default route.

#### [HLPL_lab_1.csproj](/C:/Users/Bozhena/source/repos/HLPL_lab_1/HLPL_lab_1.csproj)
.NET project file.

- Uses `Microsoft.NET.Sdk.Web`.
- Targets `net8.0`.

#### [HLPL_lab_1.sln](/C:/Users/Bozhena/source/repos/HLPL_lab_1/HLPL_lab_1.sln)
Visual Studio solution file.

#### [appsettings.json](/C:/Users/Bozhena/source/repos/HLPL_lab_1/appsettings.json)
Base application settings.

#### [appsettings.Development.json](/C:/Users/Bozhena/source/repos/HLPL_lab_1/appsettings.Development.json)
Development-only settings.

#### [HLPL_lab_1.csproj.user](/C:/Users/Bozhena/source/repos/HLPL_lab_1/HLPL_lab_1.csproj.user)
Local Visual Studio user settings.

### Controllers

#### [Controllers/HomeController.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Controllers/HomeController.cs)
Main site controller.

- Returns `Index`, `Schedule`, `Movie`, `Privacy`, `Error`.
- Stores demo movie data.
- Sends `MovieInfoViewModel` to the movie details page.

#### [Controllers/BookingController.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Controllers/BookingController.cs)
Controller added for Lab 2.

- `GET Index()` shows the booking form.
- `POST Index(BookingViewModel model)` receives submitted form data.
- Sends helper values through `ViewData`.
- Opens the confirmation view after successful validation.

### Models

#### [Models/MovieInfoViewModel.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Models/MovieInfoViewModel.cs)
Movie data model for the cinema pages.

#### [Models/BookingViewModel.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Models/BookingViewModel.cs)
Booking form model for Lab 2.

- Customer name.
- Phone number.
- Movie title.
- Showtime.
- Ticket count.
- Comment.
- Validation attributes.

#### [Models/ErrorViewModel.cs](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Models/ErrorViewModel.cs)
Default error model.

### Views

#### [Views/_ViewImports.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/_ViewImports.cshtml)
Imports namespaces and tag helpers for Razor views.

#### [Views/_ViewStart.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/_ViewStart.cshtml)
Sets the shared layout for views.

#### [Views/Shared/_Layout.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Shared/_Layout.cshtml)
Main shared layout.

- Header.
- Navigation.
- Footer.
- CSS and JS includes.

#### [Views/Shared/_Layout.cshtml.css](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Shared/_Layout.cshtml.css)
Scoped CSS for layout.

#### [Views/Shared/_ValidationScriptsPartial.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Shared/_ValidationScriptsPartial.cshtml)
Client-side validation scripts partial.

#### [Views/Shared/Error.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Shared/Error.cshtml)
Error page.

#### [Views/Home/Index.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Home/Index.cshtml)
Cinema home page.

- Hero section.
- Movie cards.
- Contact block.

#### [Views/Home/Schedule.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Home/Schedule.cshtml)
Cinema schedule page.

- Session cards.
- Filters.
- Small JavaScript interactions.

#### [Views/Home/Movie.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Home/Movie.cshtml)
Movie details page.

#### [Views/Home/Privacy.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Home/Privacy.cshtml)
Privacy information page for the study project.

#### [Views/Booking/Index.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Booking/Index.cshtml)
Booking form page.

- Receives `BookingViewModel`.
- Displays values from `ViewData`.
- Posts data back to the controller.

#### [Views/Booking/Confirmation.cshtml](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Views/Booking/Confirmation.cshtml)
Booking confirmation page.

### Properties

#### [Properties/launchSettings.json](/C:/Users/Bozhena/source/repos/HLPL_lab_1/Properties/launchSettings.json)
Launch profiles for local development.

### Static files

#### [wwwroot/css/site.css](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/css/site.css)
Global site styles.

#### [wwwroot/js/site.js](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/js/site.js)
Global site JavaScript file.

#### [wwwroot/favicon.ico](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/favicon.ico)
Site icon.

#### [wwwroot/images/cinema-bg.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/cinema-bg.svg)
Main cinema background image.

#### [wwwroot/images/posters/mufasa.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/mufasa.svg)
Poster for `Mufasa: The Lion King`.

#### [wwwroot/images/posters/vaiana2.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/vaiana2.svg)
Poster for `Vaiana 2`.

#### [wwwroot/images/posters/sonic3.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/sonic3.svg)
Poster for `Sonic 3`.

#### [wwwroot/images/posters/paddington3.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/paddington3.svg)
Poster for `Paddington 3`.

#### [wwwroot/images/posters/lotr-war.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/lotr-war.svg)
Poster for `The Lord of the Rings: The War of the Rohirrim`.

#### [wwwroot/images/posters/family-pack.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/posters/family-pack.svg)
Shared poster for several secondary cards.

#### [wwwroot/img/noise.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/img/noise.svg)
Decorative texture.

#### [wwwroot/images/glass-hotel.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/glass-hotel.svg)
Additional SVG asset.

#### [wwwroot/images/kyiv-1944.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/kyiv-1944.svg)
Additional SVG asset.

#### [wwwroot/images/nebula.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/nebula.svg)
Additional SVG asset.

#### [wwwroot/images/silent-orbit.svg](/C:/Users/Bozhena/source/repos/HLPL_lab_1/wwwroot/images/silent-orbit.svg)
Additional SVG asset.

## Generated and external folders

#### `wwwroot/lib/`
External libraries: Bootstrap, jQuery, validation packages.

#### `bin/`
Build output generated by .NET.

#### `obj/`
Intermediate build files generated by .NET.

#### `build-check/`
Auxiliary build artifacts in this workspace.

## How to verify
1. Run `dotnet build`.
2. Start the project.
3. Open `Home`, `Schedule`, `Booking`, `Privacy`.
4. Submit the booking form and check the confirmation page.
