namespace ProductCodeApp;

public static class ProductCodeInputHelper
{
    public const int MaxAlphanumericLength = 30;


    public static string FormatForDisplay(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        var digits = new string(input.Where(static c => char.IsLetterOrDigit(c)).ToArray())
            .ToUpperInvariant();

        if (digits.Length > MaxAlphanumericLength)
        {
            digits = digits[..MaxAlphanumericLength];
        }

        if (digits.Length == 0)
        {
            return string.Empty;
        }

        var groups = new List<string>();
        for (var i = 0; i < digits.Length; i += 5)
        {
            groups.Add(digits.Substring(i, Math.Min(5, digits.Length - i)));
        }

        return string.Join('-', groups);
    }

    public static int CountAlphanumeric(string? text) =>
        string.IsNullOrEmpty(text) ? 0 : text.Count(char.IsLetterOrDigit);


    public static int GetCaretIndex(string formatted, int alphanumericBeforeCaret)
    {
        if (alphanumericBeforeCaret <= 0)
        {
            return 0;
        }

        var seen = 0;
        for (var i = 0; i < formatted.Length; i++)
        {
            if (!char.IsLetterOrDigit(formatted[i]))
            {
                continue;
            }

            seen++;
            if (seen == alphanumericBeforeCaret)
            {
                return i + 1;
            }
        }

        return formatted.Length;
    }
}
