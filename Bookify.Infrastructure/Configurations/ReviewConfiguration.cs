﻿using Bookify.Domain.Apartments.Entity;
using Bookify.Domain.Bookings.Entity;
using Bookify.Domain.Reviews.Entity;
using Bookify.Domain.Users.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(review => review.Id);

        builder
            .Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

        builder
            .Property(review => review.Comment)
            .HasMaxLength(200)
            .HasConversion(comment => comment.Value, value => new Comment(value));

        builder
            .HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(review => review.ApartmentId);

        builder
            .HasOne<Booking>()
            .WithMany()
            .HasForeignKey(review => review.BookingId);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}