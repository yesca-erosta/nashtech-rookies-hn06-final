using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
             .NotNull()
             .NotEmpty()
             .Matches(StringPattern.UserPassword);
        }
    }
}
