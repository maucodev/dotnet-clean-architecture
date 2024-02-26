using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings.Entities;

public record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);