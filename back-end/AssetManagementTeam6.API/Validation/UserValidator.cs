using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleSet("UpdateUser", () =>
            {
                RuleFor(x => x.Id).GreaterThan(0);
            });

            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.Password)
                 .NotNull()
                 .NotEmpty()
                 .Matches(StringPattern.UserPassword);

                RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .Matches(StringPattern.UserName);

                RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .Matches(StringPattern.UserFirstName);

                RuleFor(x => x.LastName)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserLastName);
            });

            RuleFor(x => x.DateOfBirth)
                .Must(BeOver18);
        }

        protected bool BeOver18(DateTime date)
        {
            var now = DateTime.Now;
            var year = now.Year - 18;

            var compareDate = new DateTime(year, now.Month, now.Day);

            return DateTime.Compare(compareDate, date) >= 0;

        }
    }
}
