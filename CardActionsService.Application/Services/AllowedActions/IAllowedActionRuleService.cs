using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public interface IAllowedActionRuleService
{
    public AllowedAction AllowedAction { get; }
    /// <summary>
    /// Check if specified action is allowed based on card details
    /// </summary>
    /// <param name="cardDetails">Card details</param>
    /// <returns>Is allowed bool value</returns>
    bool IsAllowed(CardDetails cardDetails);
}