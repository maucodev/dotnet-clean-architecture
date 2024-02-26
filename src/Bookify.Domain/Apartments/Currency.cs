namespace Bookify.Domain.Apartments;

public record Currency(string Code)
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Mxn = new("MXN");
    
    internal static readonly Currency None = new(string.Empty);

    public string? Code { get; init; } = Code;

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new InvalidOperationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = [Usd, Eur, Mxn];
}