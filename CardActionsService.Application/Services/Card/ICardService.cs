using CardActionsService.Domain.Entities;

namespace CardActionsService.Application.Services.Card;

public interface ICardService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}