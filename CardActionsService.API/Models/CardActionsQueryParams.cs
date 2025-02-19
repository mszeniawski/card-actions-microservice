namespace CardActionsService.API.Models;

public class CardActionsQueryParams
{
    public string UserId { get; set; }

    public string CardNumber { get; set; }
    
    public CardActionsQueryParams(string userId, string cardNumber)
    {
        UserId = userId;
        CardNumber = cardNumber;
    }
}