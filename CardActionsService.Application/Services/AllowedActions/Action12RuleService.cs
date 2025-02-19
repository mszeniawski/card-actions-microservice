using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class Action12RuleService : IAllowedActionRuleService
{
    public AllowedAction AllowedAction => AllowedAction.Action12;
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.CardType is CardType.Prepaid or CardType.Debit or CardType.Credit &&
               cardDetails.CardStatus is CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active;
    }
}