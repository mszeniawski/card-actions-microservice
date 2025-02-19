using MediatR;

namespace CardActionsService.Application.Queries.Card.GetAllowedActions;

public class GetAllowedActionsQuery: IRequest<GetAllowedActionsQueryResponse>
{
    public string UserId { get; set; }

    public string CardNumber { get; set; }

    public GetAllowedActionsQuery(string userId, string cardNumber)
    {
        UserId = userId;
        CardNumber = cardNumber;
    }
}