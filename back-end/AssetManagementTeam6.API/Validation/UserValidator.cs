using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Validation
{
    [ExcludeFromCodeCoverage]
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleSet("UpdateUser", () =>
            {
                // RuleFor(x => x.Id).GreaterThan(0);
            });

            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.Password)
                     .NotNull()
                     .NotEmpty()
                     .Matches(StringPattern.UserPassword).WithMessage("Password must be at least 8 characters and contain at least 1 capital letter and a number");

                RuleFor(x => x.UserName)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserName).WithMessage("The user name must be from 6 to 50 characters, can contain only alphabetic  and numeric characters, and must start with an alphabetic character.");

                RuleFor(x => x.FirstName)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserFirstName).WithMessage("First name can only contain alphabetic characters and must start with an alphabetic character.");

                RuleFor(x => x.LastName)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserLastName).WithMessage("Last name can only contain alphabetic characters and must start with an alphabetic character.");
            });

            RuleFor(x => x.DateOfBirth)
                    .Must(BeOver18).WithMessage("User is under 18. Please select a different date.");

            RuleFor(m => m.JoinedDate)
                    .GreaterThan(m => m.DateOfBirth).WithMessage("Joined date is not later than Date of Birth. Please select a different date.")
                    .Must(CheckWeekend).WithMessage("Joined date is Saturday or Sunday. Please select a different date.");
        }

        protected bool BeOver18(DateTime date)
        {
            var now = DateTime.Now;
            var year = now.Year - 18;

            var compareDate = new DateTime(year, now.Month, now.Day);

            return DateTime.Compare(compareDate, date) >= 0;
        }

        protected bool BeUnder100(DateTime date)
        {
            var now = DateTime.Now;
            var year = now.Year - 100;

            var compareDate = new DateTime(year, now.Month, now.Day);

            return DateTime.Compare(compareDate, date) < 0;
        }

        protected bool CheckWeekend(DateTime date)
        {
            DayOfWeek day = date.DayOfWeek;

            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                return false;
            }

            return true;
        }

    }
}
