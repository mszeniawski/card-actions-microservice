using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action11RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action11;
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Inactive or CardStatus.Active;
    }
}