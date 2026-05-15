using System.Text.RegularExpressions;

namespace ProductCodeApi.ApiController.Models;

public static class ProductCodeValidator
{
    private static readonly Regex Pattern = new(
        @"^[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static bool TryNormalize(string? input, out string normalized, out string errorMessage)
    {
        normalized = string.Empty;
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            errorMessage = "กรุณากรอกรหัสสินค้า";
            return false;
        }

        normalized = input.Trim().ToUpperInvariant();

        if (normalized.Any(c => c is not (>= '0' and <= '9') and not (>= 'A' and <= 'Z') and not '-'))
        {
            errorMessage =
                "ใช้ได้เฉพาะตัวอักษร A-Z (พิมพ์ใหญ่) และตัวเลข 0-9 และเครื่องหมายขีดตามรูปแบบ";
            return false;
        }

        if (!Pattern.IsMatch(normalized))
        {
            errorMessage =
                "รูปแบบต้องเป็น xxxxx-xxxxx-xxxxx-xxxxx-xxxxx-xxxxx (กลุ่มละ 5 ตัว รวม 30 ตัวอักษร ไม่นับขีด)";
            return false;
        }

        var lettersDigits = normalized.Replace("-", "", StringComparison.Ordinal);
        if (lettersDigits.Length != 30)
        {
            errorMessage = "รหัสสินค้าต้องมีความยาวรวม 30 ตัวอักษร (ไม่นับขีด)";
            return false;
        }

        return true;
    }
}
