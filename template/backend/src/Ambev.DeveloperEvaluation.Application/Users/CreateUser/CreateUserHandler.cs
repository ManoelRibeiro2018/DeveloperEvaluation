using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultResponse<CreateUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly CreateUserCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateUserCommand</param>
    public CreateUserHandler(IUserRepository userRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        CreateUserCommandValidator validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<ResultResponse<CreateUserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return ResultResponse<CreateUserResult>.Failure(400, validationResult.Errors);

        var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
            return ResultResponse<CreateUserResult>.Failure(409, $"User with email {command.Email} already exists");

        var user = _mapper.Map<User>(command);
        user.CreatedAt = DateTime.UtcNow;
        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        var result = _mapper.Map<CreateUserResult>(createdUser);
        return ResultResponse<CreateUserResult>.Successful(result, 201, "User created successfully");
    }
}
