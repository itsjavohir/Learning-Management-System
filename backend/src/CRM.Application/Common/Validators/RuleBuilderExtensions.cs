using FluentValidation;

namespace CRM.Application.Common.Validators;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string?> MustBeValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(500).WithMessage("Url must not exceed 500 characters")
            .Must(url => string.IsNullOrWhiteSpace(url)
                || (Uri.TryCreate(url, UriKind.Absolute, out var uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            .WithMessage("Invalid URL format");
    }

    public static IRuleBuilderOptions<T, string?> MustBeValidPhone<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(phone => string.IsNullOrWhiteSpace(phone)
                || System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+?\d{9,15}$"))
            .WithMessage("Invalid phone number format");
    }

    public static IRuleBuilderOptions<T, string?> MustBeValidTelegramUsername<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(username => string.IsNullOrWhiteSpace(username)
                || System.Text.RegularExpressions.Regex.IsMatch(username, @"^@?[A-Za-z0-9_]{5,32}$"))
            .WithMessage("Invalid Telegram username format");
    }

    public static IRuleBuilderOptions<T, DateTime?> MustBeValidDateOfBirth<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date is null || (date.Value < DateTime.UtcNow && date.Value.Year >= 1900))
            .WithMessage("Date of birth must be in the past and not earlier than 1900");
    }
}
