namespace Bookify.Domain.Bookings.Entities;

public record DateRange
{
    public DateOnly Start { get; init; }

    public DateOnly End { get; init; }

    public int TotalDays => End.DayNumber - Start.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new InvalidOperationException("End date precedes start date");
        }

        return new DateRange
        {
            Start = start,
            End = end
        };
    }
}