using CardService.Domain.Enums;

namespace CardActionsService.Application.Services.AllowedActions;

public class AllowedActionRuleServiceFactory : IAllowedActionRuleServiceFactory
{
    private readonly Dictionary<AllowedAction, IAllowedActionRuleService> _services;
    
    public AllowedActionRuleServiceFactory(IEnumerable<IAllowedActionRuleService> services)
    {
        _services = services.ToDictionary(s => s.AllowedAction);
    }

    public IAllowedActionRuleService GetService(AllowedAction allowedAction)
    {
        if (_services.TryGetValue(allowedAction, out var service))
        {
            return service;
        }

        throw new NotImplementedException($"Missing service for: {allowedAction}");
    }
}