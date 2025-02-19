using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action8RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action8;
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active or CardStatus.Blocked;
    }
}