using Bookify.Domain.Roles.Entity;
using Bookify.Domain.UnitTests.Infrastructure;
using Bookify.Domain.UnitTests.Mocks;
using Bookify.Domain.Users.Entity;
using Bookify.Domain.Users.Events;
using FluentAssertions;

namespace Bookify.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        // Act
        var user = User.Create(
            MockUserData.FirstName, 
            MockUserData.LastName, 
            MockUserData.Email);

        // Assert
        user.Value.FirstName.Should().Be(MockUserData.FirstName);
        user.Value.LastName.Should().Be(MockUserData.LastName);
        user.Value.Email.Should().Be(MockUserData.Email);
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        // Arrange
        var user = User.Create(
            MockUserData.FirstName, 
            MockUserData.LastName, 
            MockUserData.Email);

        // Act
        var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user.Value);

        // Assert
        domainEvent.UserId.Should().Be(user.Value.Id);
    }

    [Fact]
    public void Create_Should_AddRegisteredRoleToUser()
    {
        // Act
        var user = User.Create(
            MockUserData.FirstName, 
            MockUserData.LastName, 
            MockUserData.Email);

        // Assert
        user.Value.Roles.Should().Contain(Role.Registered);
    }
}