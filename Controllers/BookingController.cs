using System.Text.Json;
using HLPL_lab_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HLPL_lab_1.Controllers
{
    public class BookingController : Controller
    {
        private const decimal TicketPrice = 180m;

        private static readonly IReadOnlyDictionary<string, string[]> MovieSchedule =
            new Dictionary<string, string[]>
            {
                ["Муфаса: Король Лев"] = ["12:00", "14:50", "17:30", "20:20"],
                ["Ваяна 2"] = ["14:00", "18:45"],
                ["Соник 3"] = ["11:30", "13:00", "19:20"],
                ["Пригоди Падінгтона 3"] = ["10:00"],
                ["Володар перснів: Війна рогіримів"] = ["20:40"]
            };

        [HttpGet]
        public IActionResult Index()
        {
            var model = BuildModel();
            FillPageData("Заповніть форму, щоб оформити попереднє бронювання квитків.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BookingViewModel model)
        {
            if (!MovieSchedule.TryGetValue(model.MovieTitle, out var showtimes) || !showtimes.Contains(model.Showtime))
            {
                ModelState.AddModelError(nameof(model.Showtime), "Оберіть доступний сеанс для цього фільму.");
            }

            if (!ModelState.IsValid)
            {
                FillPageData("Перевірте поля форми та виправте помилки.");
                return View(BuildModel(model));
            }

            ViewData["TotalPrice"] = model.TicketCount * TicketPrice;
            return View("Confirmation", BuildModel(model));
        }

        private void FillPageData(string bookingHint)
        {
            ViewData["BookingHint"] = bookingHint;
            ViewData["TicketPrice"] = TicketPrice;
            ViewData["AvailableSeats"] = 48;
            ViewData["MovieScheduleJson"] = JsonSerializer.Serialize(MovieSchedule);
        }

        private static BookingViewModel BuildModel(BookingViewModel? source = null)
        {
            var movies = MovieSchedule.Keys.ToArray();
            var selectedMovie = source?.MovieTitle;
            if (string.IsNullOrWhiteSpace(selectedMovie) || !MovieSchedule.ContainsKey(selectedMovie))
            {
                selectedMovie = movies[0];
            }

            var showtimes = MovieSchedule[selectedMovie];
            var selectedShowtime = source?.Showtime;
            if (string.IsNullOrWhiteSpace(selectedShowtime) || !showtimes.Contains(selectedShowtime))
            {
                selectedShowtime = showtimes[0];
            }

            return new BookingViewModel
            {
                CustomerName = source?.CustomerName ?? string.Empty,
                PhoneNumber = source?.PhoneNumber ?? string.Empty,
                MovieTitle = selectedMovie,
                Showtime = selectedShowtime,
                TicketCount = source?.TicketCount is >= 1 and <= 10 ? source.TicketCount : 1,
                Comment = source?.Comment ?? string.Empty,
                AvailableMovies = movies,
                AvailableShowtimes = showtimes
            };
        }
    }
}
