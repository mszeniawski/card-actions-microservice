using CardActionsService.API.Models;
using CardActionsService.API.Validators;
using CardActionsService.Application.Queries.Card.GetAllowedActions;
using CardActionsService.Application.Services.AllowedActions;
using CardActionsService.Application.Services.Card;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllowedActionsQuery).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<CardActionsQueryParamsValidator>();

builder.Services.AddScoped<ICardService, CardActionsService.Application.Services.Card.CardService>();

var serviceTypes = typeof(IAllowedActionRuleService).Assembly.GetTypes()
    .Where(t => typeof(IAllowedActionRuleService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

foreach (var serviceType in serviceTypes)
{
    builder.Services.AddScoped(typeof(IAllowedActionRuleService), serviceType);
}

builder.Services.AddScoped<IAllowedActionRuleServiceFactory, AllowedActionRuleServiceFactory>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            Error = "An unexpected error occurred",
            Message = ex.Message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
});


app.MapGet("/api/card-allowed-actions", async (
    IMediator mediator,
    IValidator<CardActionsQueryParams> validator,
    [FromQuery] string userId,
    [FromQuery] string cardNumber) =>
{
    
    var validationResult = await validator
        .ValidateAsync(new CardActionsQueryParams(userId, cardNumber));

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(new
        {
            Errors = validationResult.Errors.ToDictionary(
                x => x.PropertyName, 
                x => x.ErrorMessage
            )
        });
    }
    
    var allowedActionsQuery = new GetAllowedActionsQuery(userId, cardNumber);
    
    var result = await mediator.Send(allowedActionsQuery);
    
    return Results.Ok(result);
});

app.Run();