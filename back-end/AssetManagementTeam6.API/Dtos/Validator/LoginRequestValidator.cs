using AssetManagementTeam6.API.Dtos.Requests;
using FluentValidation;

namespace AssetManagementTeam6.API.Dtos.Validator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} should be not empty!")
                .Matches("^[A-Za-z]\\w{5, 29}$").WithMessage("The given {PropertyName} is Invalid ");
            RuleFor(x => x.Password)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull().WithMessage("{PropertyName} should be not empty!")
               .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\\S+$).{8, 20}$")
                    .WithMessage("The given {PropertyName} is Invalid ");
        }
    }
}
