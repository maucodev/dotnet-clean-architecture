using System.Collections.Generic;
using System.Linq;

namespace Bookify.Domain.Apartments.Entity;

public record Currency
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Mxn = new("MXN");
    
    internal static readonly Currency None = new(string.Empty);

    public static readonly IReadOnlyCollection<Currency> All = [Usd, Mxn];

    public string Code { get; init; }

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        return All.First(item => item.Code == code);
    }
}