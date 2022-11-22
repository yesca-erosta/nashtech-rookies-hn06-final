using AssetManagementTeam6.API.Dtos.Requests;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class UserValidator : AbstractValidator<CreateUserRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} should be not empty!")
            .Matches("^[A-Za-z]\\w{5, 29}$").WithMessage("The given {PropertyName} is Invalid ");

            RuleFor(x => x.LastName)
           .Cascade(CascadeMode.StopOnFirstFailure)
           .NotNull().WithMessage("{PropertyName} should be not empty!")
           .Matches("^[A-Za-z]\\w{5, 29}$").WithMessage("The given {PropertyName} is Invalid ");
        }
    }
}
