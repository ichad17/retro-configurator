using RetroConfigurator.Domain.ValueObjects;

namespace RetroConfigurator.API.DTOs;

public record CreateOrderRequest(
    int ConsoleType,
    int NumberOfControllers,
    bool HDMISupport,
    bool CustomColor,
    string? ColorHex,
    string CustomerEmail
);

public record OrderResponse(
    Guid Id,
    ConsoleConfigDto Configuration,
    decimal TotalPrice,
    int Status,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    string CustomerEmail
);

public record ConsoleConfigDto(
    int ConsoleType,
    int NumberOfControllers,
    bool HDMISupport,
    bool CustomColor,
    string? ColorHex
);
