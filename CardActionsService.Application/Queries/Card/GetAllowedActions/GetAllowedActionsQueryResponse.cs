namespace CardActionsService.Application.Queries.Card.GetAllowedActions;

public class GetAllowedActionsQueryResponse(List<string> allowedActions)
{
    public List<string> AllowedActions { get; set; } = allowedActions;
}