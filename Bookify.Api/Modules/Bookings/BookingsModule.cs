using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;
using MediatR;

namespace Bookify.Api.Modules.Bookings;

public static class BookingsModule
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet(
                pattern: "bookings/{id}",
                handler: GetBooking)
            .WithName(nameof(GetBooking))
            .RequireAuthorization();

        builder
            .MapPost(
                pattern: "bookings",
                handler: ReserveBooking)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetBooking(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> ReserveBooking(ReserveBookingRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(
            request.ApartmentId,
            request.UserId,
            request.StartDate,
            request.EndDate);

        var result = await sender.Send(command, cancellationToken);

        return result.IsFailure 
            ? Results.BadRequest(result.Error) 
            : Results.CreatedAtRoute(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}