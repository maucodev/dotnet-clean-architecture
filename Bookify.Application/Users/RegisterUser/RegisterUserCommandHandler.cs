﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Entity;
using Bookify.Domain.Users.Repository;

namespace Bookify.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email));

        var identityId = await _authenticationService.RegisterAsync(user.Value, request.Password, cancellationToken);

        user.Value.SetIdentityId(identityId);

        _userRepository.Add(user.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Value.Id;
    }
}