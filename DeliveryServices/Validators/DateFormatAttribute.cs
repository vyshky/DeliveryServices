using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class DateFormatAttribute : ValidationAttribute
{
    private readonly string _dateFormat;

    public DateFormatAttribute(string dateFormat)
    {
        _dateFormat = dateFormat;
        ErrorMessage = $"Неверный формат даты. Ожидается формат: {dateFormat}.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Оставляем обработку Required атрибуту
        }

        var dateString = value as string;
        if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}
