using System.ComponentModel.DataAnnotations;

namespace HLPL_lab_1.Models
{
    public class BookingViewModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Вкажіть ім'я.")]
        [StringLength(60, ErrorMessage = "Ім'я не може бути довшим за 60 символів.")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Вкажіть номер телефону.")]
        [Phone(ErrorMessage = "Вкажіть коректний номер телефону.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Фільм")]
        [Required(ErrorMessage = "Оберіть фільм.")]
        public string MovieTitle { get; set; } = string.Empty;

        [Display(Name = "Час сеансу")]
        [Required(ErrorMessage = "Оберіть час сеансу.")]
        public string Showtime { get; set; } = string.Empty;

        [Display(Name = "Кількість квитків")]
        [Range(1, 10, ErrorMessage = "Кількість квитків має бути від 1 до 10.")]
        public int TicketCount { get; set; } = 1;

        [Display(Name = "Коментар")]
        [StringLength(200, ErrorMessage = "Коментар не може бути довшим за 200 символів.")]
        public string Comment { get; set; } = string.Empty;

        public IReadOnlyList<string> AvailableMovies { get; init; } = [];
        public IReadOnlyList<string> AvailableShowtimes { get; init; } = [];
    }
}
