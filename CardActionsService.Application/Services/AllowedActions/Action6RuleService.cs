using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action6RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action6;
    public bool IsAllowed(CardDetails cardDetails)
    {
        var cardStatusRuleValue = cardDetails.CardStatus switch
        {
            CardStatus.Ordered => cardDetails.IsPinSet,
            CardStatus.Inactive => cardDetails.IsPinSet,
            CardStatus.Active => cardDetails.IsPinSet,
            CardStatus.Blocked => cardDetails.IsPinSet,
            _ => false
        };

        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardStatusRuleValue;
    }
}