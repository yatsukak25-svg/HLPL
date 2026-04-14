using System.Diagnostics;
using HLPL_lab_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HLPL_lab_1.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IReadOnlyDictionary<string, MovieInfoViewModel> Movies =
            new Dictionary<string, MovieInfoViewModel>(StringComparer.OrdinalIgnoreCase)
            {
                ["mufasa-lion-king"] = new MovieInfoViewModel
                {
                    Slug = "mufasa-lion-king",
                    Title = "Муфаса: Король Лев",
                    Genre = "Анімація, пригоди, сімейний",
                    Duration = "118 хв",
                    AgeRating = "0+",
                    Language = "Українська",
                    Format = "3D",
                    Country = "США",
                    Year = "2024",
                    Director = "Баррі Дженкінс",
                    Cast = "озвучення: Аарон П'єр, Келвін Гаррісон мл., Сет Роген",
                    Description = "Історія про шлях Муфаси від самотнього левеняти до легендарного короля Прайду. Фільм поєднує пригодницький сюжет, музику та сімейну драму.",
                    PosterPath = "/images/posters/mufasa.svg",
                    Showtimes = ["12:00", "14:50", "17:30", "20:20"]
                },
                ["vaiana-2"] = new MovieInfoViewModel
                {
                    Slug = "vaiana-2",
                    Title = "Ваяна 2",
                    Genre = "Анімація, пригоди, сімейний",
                    Duration = "100 хв",
                    AgeRating = "0+",
                    Language = "Українська",
                    Format = "3D",
                    Country = "США",
                    Year = "2024",
                    Director = "Девід Деррік-мол.",
                    Cast = "озвучення: Аулі'ї Кравальйо, Двейн Джонсон",
                    Description = "Ваяна вирушає у нову морську подорож, де на неї чекають небезпечні води, давні таємниці та зустрічі з новими союзниками.",
                    PosterPath = "/images/posters/vaiana2.svg",
                    Showtimes = ["14:00", "18:45"]
                },
                ["sonic-3"] = new MovieInfoViewModel
                {
                    Slug = "sonic-3",
                    Title = "Соник 3 / Їжак Сонік 3",
                    Genre = "Пригоди, комедія, сімейний",
                    Duration = "109 хв",
                    AgeRating = "6+",
                    Language = "Українська",
                    Format = "2D та 3D",
                    Country = "США, Японія",
                    Year = "2024",
                    Director = "Джефф Фаулер",
                    Cast = "Бен Шварц, Джим Керрі, Джеймс Марсден, Ідріс Ельба",
                    Description = "Соник, Тейлз і Наклз стикаються з новим сильним суперником. Команда має об'єднатися, щоб зупинити загрозу світового масштабу.",
                    PosterPath = "/images/posters/sonic3.svg",
                    Showtimes = ["11:30", "13:00", "19:20"]
                },
                ["paddington-3"] = new MovieInfoViewModel
                {
                    Slug = "paddington-3",
                    Title = "Пригоди Падінгтона 3",
                    Genre = "Сімейний, комедія, пригоди",
                    Duration = "106 хв",
                    AgeRating = "0+",
                    Language = "Українська",
                    Format = "3D",
                    Country = "Велика Британія, Франція",
                    Year = "2024",
                    Director = "Дагал Вілсон",
                    Cast = "Бен Вішоу, Г'ю Бонневіль, Емілі Мортімер",
                    Description = "Падінгтон разом з родиною Браунів вирушає у мандрівку, яка перетворюється на веселу й зворушливу пригоду.",
                    PosterPath = "/images/posters/paddington3.svg",
                    Showtimes = ["10:00"]
                },
                ["lotr-war-rohirrim"] = new MovieInfoViewModel
                {
                    Slug = "lotr-war-rohirrim",
                    Title = "Володар перснів: Війна рогіримів",
                    Genre = "Аніме, фентезі, бойовик",
                    Duration = "134 хв",
                    AgeRating = "12+",
                    Language = "Українська",
                    Format = "2D",
                    Country = "США, Японія",
                    Year = "2024",
                    Director = "Кенджі Каміяма",
                    Cast = "озвучення: Браян Кокс, Гая Вайз",
                    Description = "Події розгортаються задовго до трилогії Пітера Джексона. Це розповідь про короля Гельма Молоторукого і війну за Роган.",
                    PosterPath = "/images/posters/lotr-war.svg",
                    Showtimes = ["20:40"]
                },
                ["leopold"] = new MovieInfoViewModel
                {
                    Slug = "leopold",
                    Title = "Леопольд",
                    Genre = "Сімейний, комедія",
                    Duration = "95 хв",
                    AgeRating = "0+",
                    Language = "Українська",
                    Format = "2D",
                    Country = "Європа",
                    Year = "2024",
                    Director = "інформація уточнюється",
                    Cast = "сімейний акторський склад",
                    Description = "Добра сімейна історія з гумором і теплими персонажами, розрахована на спільний перегляд з дітьми.",
                    PosterPath = "/images/posters/family-pack.svg",
                    Showtimes = ["13:50", "17:00"]
                },
                ["heretic"] = new MovieInfoViewModel
                {
                    Slug = "heretic",
                    Title = "Єретик",
                    Genre = "Трилер, жахи",
                    Duration = "111 хв",
                    AgeRating = "16+",
                    Language = "Українська",
                    Format = "2D",
                    Country = "США",
                    Year = "2024",
                    Director = "Скотт Бек, Браян Вудс",
                    Cast = "Г'ю Грант, Софі Тетчер, Хлоя Іст",
                    Description = "Психологічний трилер про небезпечну зустріч, яка швидко перетворюється на боротьбу за виживання та контроль.",
                    PosterPath = "/images/posters/family-pack.svg",
                    Showtimes = ["16:40"]
                },
                ["freaky-friday-2"] = new MovieInfoViewModel
                {
                    Slug = "freaky-friday-2",
                    Title = "Ще одна шалена п'ятниця",
                    Genre = "Комедія, сімейний",
                    Duration = "104 хв",
                    AgeRating = "12+",
                    Language = "Українська",
                    Format = "2D",
                    Country = "США",
                    Year = "2025",
                    Director = "Ніша Ганатра",
                    Cast = "Джеймі Лі Кертіс, Ліндсі Лохан",
                    Description = "Продовження культової історії про обмін тілами й хаос у родині. Більше гумору, ностальгії та сімейних конфліктів.",
                    PosterPath = "/images/posters/family-pack.svg",
                    Showtimes = ["18:20"]
                },
                ["three-cats-winter-vacation"] = new MovieInfoViewModel
                {
                    Slug = "three-cats-winter-vacation",
                    Title = "Три коти. Зимові канікули",
                    Genre = "Анімація, сімейний",
                    Duration = "74 хв",
                    AgeRating = "0+",
                    Language = "Українська",
                    Format = "2D",
                    Country = "Україна",
                    Year = "2024",
                    Director = "інформація уточнюється",
                    Cast = "озвучення мультфільму",
                    Description = "Зимова історія про улюблених героїв, пригоди та святковий настрій для наймолодших глядачів.",
                    PosterPath = "/images/posters/family-pack.svg",
                    Showtimes = ["10:10"]
                }
            };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Movie(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug) || !Movies.TryGetValue(slug, out var movie))
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
