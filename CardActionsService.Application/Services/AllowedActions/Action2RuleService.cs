using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action2RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action2;
    
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Inactive;
    }
}