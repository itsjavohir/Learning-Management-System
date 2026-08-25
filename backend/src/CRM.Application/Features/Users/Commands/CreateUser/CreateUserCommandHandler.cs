using System.ComponentModel;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork,IPasswordHasher passwordHasher,IEmailService emailService)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
{
    private static string GenerateTemporaryPassword()
{
    return System.Security.Cryptography.RandomNumberGenerator.GetInt32(10000, 99999).ToString();
}
    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        
        var request = command.Request;
        var passwordRaw = GenerateTemporaryPassword();

        var existingUser = await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
        if (existingUser != null)
        {
            return Result<CreateUserResponse>.Fail("User already exists", ErrorType.Conflict);
        }
          
 var existingUserEmail = await unitOfWork.User.GetByEmailAsync(request.Email, cancellationToken);
if (existingUserEmail != null)
{
    return Result<CreateUserResponse>.Fail("User with this email already exists", ErrorType.Conflict);
}

         var role = await unitOfWork.Role.GetByIdAsync(request.RoleId,cancellationToken);
         if(role == null)
        {
            return Result<CreateUserResponse>.Fail("Role Not Found",ErrorType.NotFound);
        }
         
        
         var passwordHash = passwordHasher.Hash(passwordRaw);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PhoneNumber = request.PhoneNumber,
            RoleId = request.RoleId,
            
        };
        await unitOfWork.User.AddAsync(user, cancellationToken);

        if(role.Name == "Student")
        {
            var student = new Student {UserId  = user.Id};
            await unitOfWork.Student.AddAsync(student, cancellationToken);
        }
        else if( role.Name == "Mentor")
        {
            var mentor = new Mentor {UserId = user.Id};
            await unitOfWork.Mentor.AddAsync(mentor,cancellationToken);
        }
        

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await emailService.SendWelcomeAsync(user.Email!,user.FullName,passwordRaw,cancellationToken);

        var response = new CreateUserResponse(
        user.Id,
        user.FirstName,
        user.LastName,
        user.PhoneNumber,
        role.Name,
        passwordRaw
        );

        return Result<CreateUserResponse>.Ok(response);
    }
}