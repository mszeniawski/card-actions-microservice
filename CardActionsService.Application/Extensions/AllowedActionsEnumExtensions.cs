using CardActionsService.Domain.Enums;

namespace CardActionsService.Application.Extensions;

public static class AllowedActionsEnumExtensions
{
    public static string? GetActionName(this AllowedAction allowedAction)
        => Enum.GetName(allowedAction.GetType(), allowedAction)?.ToUpper();

    public static List<AllowedAction> GetActionsList() => 
        Enum.GetValues(typeof(AllowedAction)).Cast<AllowedAction>().ToList();
}