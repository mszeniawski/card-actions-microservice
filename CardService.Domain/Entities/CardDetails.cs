using CardService.Domain.Enums;

namespace CardService.Domain.Entities;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);