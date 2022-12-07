using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Validation
{
    [ExcludeFromCodeCoverage]
    public class AssignmentRequestValidator : AbstractValidator<AssignmentRequest>
    {
        public AssignmentRequestValidator()
        {
            RuleFor(x => x.AssignedDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(IsAssignedDate)
                .WithMessage("The assigned date must be current date or future date");
            RuleFor(x => x.Note)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(StringPattern.Note)
                .WithMessage("The note should not be longer than 255 characters");
        }

        protected bool IsAssignedDate(DateTime date)
        {
            var now = DateTime.UtcNow;

            if (date.Year == now.Year && date.Month == now.Month && date.Day == now.Day){
                return true;
            }

            return DateTime.Compare(now, date) <= 0;
        }

    }
}
