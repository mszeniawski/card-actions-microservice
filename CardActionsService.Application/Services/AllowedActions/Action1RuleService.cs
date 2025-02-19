using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action1RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action1;

    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Active;
    }
}