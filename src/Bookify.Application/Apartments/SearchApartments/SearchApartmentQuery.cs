using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.Responses;

namespace Bookify.Application.Apartments.SearchApartments;

public sealed record SearchApartmentQuery(
    DateOnly StartDate,
    DateOnly EndDate)
    : IQuery<IReadOnlyList<ApartmentResponse>>;