using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TutorFlow.Domain.Validation;

public sealed partial class UkrainianPhoneAttribute : ValidationAttribute
{
    public UkrainianPhoneAttribute()
    {
        ErrorMessage = "Введіть український номер телефону у форматі +380XXXXXXXXX.";
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        var regex = new Regex(@"^\+380\d{9}$");
        return value is string phone && regex.IsMatch(phone);
    }
}
