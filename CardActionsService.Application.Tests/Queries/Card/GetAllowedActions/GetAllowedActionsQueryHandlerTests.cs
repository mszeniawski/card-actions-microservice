using CardActionsService.Application.Queries.Card.GetAllowedActions;
using CardActionsService.Application.Services.AllowedActions;
using CardActionsService.Application.Services.Card;
using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using FluentAssertions;
using Moq;

namespace CardActionsService.Application.Tests.Queries.Card.GetAllowedActions;

public class GetAllowedActionsQueryHandlerTests
{
    [Theory]
    [InlineData(CardType.Prepaid, CardStatus.Ordered, true, new string[] {"ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Ordered, false, new string[] {"ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Inactive, true, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Inactive, false, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Active, true, new string[] {"ACTION1","ACTION3","ACTION4","ACTION6","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Active, false, new string[] {"ACTION1","ACTION3","ACTION4","ACTION7","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Prepaid, CardStatus.Restricted, true, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Restricted, false, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Blocked, true, new string[] {"ACTION3","ACTION4","ACTION6","ACTION7","ACTION8","ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Blocked, false, new string[] {"ACTION3","ACTION4","ACTION8","ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Expired, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Expired, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Closed, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Prepaid, CardStatus.Closed, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    
    [InlineData(CardType.Debit, CardStatus.Ordered, true, new string[] {"ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Ordered, false, new string[] {"ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Inactive, true, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Inactive, false, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Active, true, new string[] {"ACTION1","ACTION3","ACTION4","ACTION6","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Active, false, new string[] {"ACTION1","ACTION3","ACTION4","ACTION7","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Debit, CardStatus.Restricted, true, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Restricted, false, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Blocked, true, new string[] {"ACTION3","ACTION4","ACTION6","ACTION7","ACTION8","ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Blocked, false, new string[] {"ACTION3","ACTION4","ACTION8","ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Expired, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Expired, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Closed, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Debit, CardStatus.Closed, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    
    [InlineData(CardType.Credit, CardStatus.Ordered, true, new string[] {"ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Ordered, false, new string[] {"ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Inactive, true, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Inactive, false, new string[] {"ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Active, true, new string[] {"ACTION1","ACTION3","ACTION4","ACTION6","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Active, false, new string[] {"ACTION1","ACTION3","ACTION4","ACTION7","ACTION8","ACTION9","ACTION10","ACTION11","ACTION12","ACTION13"})]
    [InlineData(CardType.Credit, CardStatus.Restricted, true, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Restricted, false, new string[] {"ACTION3","ACTION4","ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Blocked, true, new string[] {"ACTION3","ACTION4","ACTION6","ACTION7","ACTION8","ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Blocked, false, new string[] {"ACTION3","ACTION4","ACTION8","ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Expired, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Expired, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Closed, true, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    [InlineData(CardType.Credit, CardStatus.Closed, false, new string[] {"ACTION3", "ACTION4", "ACTION9"})]
    public async Task Handle_GetAllowedActions(CardType cardType, CardStatus status, bool isPinSet, string[] expectedActions)
    {
        var request = new GetAllowedActionsQuery("test", "test");
        var handler = GetHandler(cardType, status, isPinSet);
        
        var result = await handler.Handle(request, CancellationToken.None);
        
        result.AllowedActions.Should().BeEquivalentTo(expectedActions);
    }

    private GetAllowedActionsQueryHandler GetHandler(CardType cardType, CardStatus status, bool isPinSet)
    {
        var cardService = new Mock<ICardService>();
        
        cardService
            .Setup(x => x.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CardDetails("test", cardType, status, isPinSet));

        var actionServices = new List<IAllowedActionRuleService>()
        {
            new Action1RuleService(),
            new Action2RuleService(),
            new Action3RuleService(),
            new Action4RuleService(),
            new Action5RuleService(),
            new Action6RuleService(),
            new Action7RuleService(),
            new Action8RuleService(),
            new Action9RuleService(),
            new Action10RuleService(),
            new Action11RuleService(),
            new Action12RuleService(),
            new Action13RuleService(),
        };
        
        return new GetAllowedActionsQueryHandler(cardService.Object, new AllowedActionRuleServiceFactory(actionServices));
    }
}