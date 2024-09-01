﻿using System.Data;
using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartments.Entity;
using Dapper;

namespace Bookify.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = sqlConnectionFactory.CreateConnection();

        if (ExistsData(connection))
        {
            return;
        }

        var faker = new Faker();

        List<object> apartments = [];

        for (var i = 0; i < 100; i++)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Company.CompanyName(),
                Description = "Amazing view",
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(50, 1000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(25, 200),
                CleaningFeeCurrency = "USD",
                Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                LastBooked = DateTime.MinValue
            });
        }

        const string query = """
           INSERT INTO public.apartments
           (id, name, description, address_country, address_state, address_zip_code, address_city, address_street, price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, amenities, last_booked)
           VALUES(@Id, @Name, @Description, @Country, @State, @ZipCode, @City, @Street, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleaningFeeCurrency, @Amenities, @LastBooked);
           """;

        connection.Execute(query, apartments);
    }

    private static bool ExistsData(IDbConnection connection)
    {
        const string query = """
             SELECT CASE 
                WHEN COUNT(*) > 0 THEN 1
                ELSE 0
                END AS HasRows
             FROM public.apartments;
             """;

        var result = connection.ExecuteScalar<int>(query);

        return result == 1;
    }
}