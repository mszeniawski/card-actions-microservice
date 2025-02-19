using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action3RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action3;
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active
                   or CardStatus.Restricted or CardStatus.Blocked or CardStatus.Expired or CardStatus.Closed;
    }
}