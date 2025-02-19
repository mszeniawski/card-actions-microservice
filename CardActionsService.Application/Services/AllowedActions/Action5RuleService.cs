using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action5RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action5;
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active
                   or CardStatus.Restricted or CardStatus.Blocked or CardStatus.Expired or CardStatus.Closed;
    }
}