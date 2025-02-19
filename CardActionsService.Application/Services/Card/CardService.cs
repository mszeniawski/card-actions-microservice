using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.Card;

public class CardService : ICardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();
        
    public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    {
        await Task.Delay(100); 
        if (!_userCards.TryGetValue(userId, out var cards) || !cards.TryGetValue(cardNumber, out var cardDetails))
        {
            return null;
        }
        return cardDetails;
    }
        
    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        for (var i = 1; i <= 3; i++)
        {
            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card{i}{cardIndex}";
                    cards.Add(cardNumber, new CardDetails(cardNumber, cardType, cardStatus, cardIndex % 2 == 0));
                    cardIndex++;
                }
            }
            userCards.Add($"User{i}", cards);
        }
        return userCards;
    }
}