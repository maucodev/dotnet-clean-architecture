namespace Bookify.Application.Exceptions;

public record ValidationError(PropertyName PropertyName, ErrorMessage ErrorMessage);