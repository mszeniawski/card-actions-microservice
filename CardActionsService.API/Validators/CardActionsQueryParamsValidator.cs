using CardActionsService.API.Models;
using FluentValidation;

namespace CardActionsService.API.Validators;

public class CardActionsQueryParamsValidator : AbstractValidator<CardActionsQueryParams>
{
    public CardActionsQueryParamsValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required");
    }
}