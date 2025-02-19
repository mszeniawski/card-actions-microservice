using CardActionsService.Application.Exceptions;
using CardActionsService.Application.Extensions;
using CardActionsService.Application.Services.AllowedActions;
using CardActionsService.Application.Services.Card;
using MediatR;

namespace CardActionsService.Application.Queries.Card.GetAllowedActions;

public class GetAllowedActionsQueryHandler(ICardService cardService, IAllowedActionRuleServiceFactory allowedActionRuleServiceFactory)
    : IRequestHandler<GetAllowedActionsQuery, GetAllowedActionsQueryResponse>
{
    public async Task<GetAllowedActionsQueryResponse> Handle(
        GetAllowedActionsQuery request,
        CancellationToken cancellationToken)
    {
        var userCard = await cardService
            .GetCardDetails(request.UserId, request.CardNumber)
            .ConfigureAwait(false);

        if (userCard is null)
        {
            throw new UserCardDetailsNotFoundException("User card not found");
        }

        var actionsList = AllowedActionsEnumExtensions.GetActionsList();
        var allowedActions = new List<string>();
        
        actionsList.ForEach(action =>
        {
            var actionRuleService = allowedActionRuleServiceFactory.GetService(action);

            if (!actionRuleService.IsAllowed(userCard)) return;
            
            var actionName = action.GetActionName();
            if (actionName is not null)
            {
                allowedActions.Add(actionName);
            }
        });

        return new GetAllowedActionsQueryResponse(allowedActions);
    }
}