using RetroConfigurator.Domain.Enums;

namespace RetroConfigurator.Domain.ValueObjects;

public class ConsoleConfig
{
    public ConsoleType ConsoleType { get; private set; }
    public int NumberOfControllers { get; private set; }
    public bool HDMISupport { get; private set; }
    public bool CustomColor { get; private set; }
    public string? ColorHex { get; private set; }

    private ConsoleConfig()
    {
        // EF Core constructor
    }

    public ConsoleConfig(
        ConsoleType consoleType,
        int numberOfControllers,
        bool hdmiSupport,
        bool customColor,
        string? colorHex = null)
    {
        if (numberOfControllers < 1 || numberOfControllers > 4)
            throw new ArgumentException("Number of controllers must be between 1 and 4", nameof(numberOfControllers));

        if (customColor && string.IsNullOrWhiteSpace(colorHex))
            throw new ArgumentException("Color hex is required when custom color is selected", nameof(colorHex));

        if (customColor && !IsValidHexColor(colorHex!))
            throw new ArgumentException("Invalid hex color format", nameof(colorHex));

        ConsoleType = consoleType;
        NumberOfControllers = numberOfControllers;
        HDMISupport = hdmiSupport;
        CustomColor = customColor;
        ColorHex = colorHex;
    }

    private static bool IsValidHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return false;

        hex = hex.TrimStart('#');
        return hex.Length == 6 && hex.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ConsoleConfig other)
            return false;

        return ConsoleType == other.ConsoleType
            && NumberOfControllers == other.NumberOfControllers
            && HDMISupport == other.HDMISupport
            && CustomColor == other.CustomColor
            && ColorHex == other.ColorHex;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ConsoleType, NumberOfControllers, HDMISupport, CustomColor, ColorHex);
    }
}
