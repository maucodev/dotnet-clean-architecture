﻿using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users.Entity;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found");


    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid credentials");
}