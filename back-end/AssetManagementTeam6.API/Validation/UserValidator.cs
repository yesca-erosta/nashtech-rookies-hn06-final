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
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotNull()
                     .NotEmpty()
                     .Matches(StringPattern.UserPasswordLength).WithMessage("The password should be 8-16 characters")
                     .Matches(StringPattern.UserPassword).WithMessage("The password should be have 1 Capital character, 1 normal character and 1 digit");

                RuleFor(x => x.UserName)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserName).WithMessage("The user name must be from 6 to 50 characters, can contain only alphabetic  and numeric characters, and must start with an alphabetic character.");

                RuleFor(x => x.FirstName)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserFirstName).WithMessage("First name can only contain alphabetic characters and must start with an alphabetic character.");

                RuleFor(x => x.LastName)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .Matches(StringPattern.UserLastName).WithMessage("Last name can only contain alphabetic characters and must start with an alphabetic character.");
            });

            RuleFor(x => x.DateOfBirth)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(BeOver18).WithMessage("User is under 18. Please select a different date.");

            RuleFor(m => m.JoinedDate)
                    .Cascade(CascadeMode.StopOnFirstFailure)
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
