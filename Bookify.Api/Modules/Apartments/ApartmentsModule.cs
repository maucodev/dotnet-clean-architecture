using Bookify.Api.Authorization;
using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Modules.Apartments;

public static class ApartmentsModule
{
    public static void MapApartmentsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet(
                pattern: "apartments",
                handler: SearchApartments)
            .RequireAuthorization(Permissions.ApartmentsRead);
    }

    private static async Task<IResult> SearchApartments(
        [FromQuery] DateOnly startDate, 
        [FromQuery] DateOnly endDate,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new SearchApartmentQuery(startDate, endDate);
        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}