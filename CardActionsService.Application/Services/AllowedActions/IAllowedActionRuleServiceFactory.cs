using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public interface IAllowedActionRuleServiceFactory
{
    /// <summary>
    /// Get rule service to check if specific action is allowed
    /// </summary>
    /// <param name="allowedAction">Allowed action</param>
    /// <returns>Rule service</returns>
    IAllowedActionRuleService GetService(AllowedAction allowedAction);
}