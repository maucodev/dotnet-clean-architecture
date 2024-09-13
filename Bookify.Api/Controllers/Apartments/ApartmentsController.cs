using Asp.Versioning;
using Bookify.Api.Authorization;
using Bookify.Api.Versioning;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/apartments")]
public class ApartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public ApartmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [HasPermission(Permissions.ApartmentsRead)]
    public async Task<IActionResult> SearchApartments(
        DateOnly startDate, 
        DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var query = new SearchApartmentQuery(startDate, endDate);
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}