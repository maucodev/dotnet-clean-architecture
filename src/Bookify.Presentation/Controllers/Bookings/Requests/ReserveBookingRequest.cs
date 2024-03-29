﻿namespace Bookify.Presentation.Controllers.Bookings.Requests;

public sealed record ReserveBookingRequest(
    Guid ApartmentId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate);