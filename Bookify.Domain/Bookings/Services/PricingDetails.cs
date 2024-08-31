using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings.Services;

public record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);