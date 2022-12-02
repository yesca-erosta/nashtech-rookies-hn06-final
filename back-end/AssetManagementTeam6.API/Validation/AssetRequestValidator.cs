using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Validation
{
    [ExcludeFromCodeCoverage]
    public class AssetRequestValidator : AbstractValidator<AssetRequest>
    {
        public AssetRequestValidator()
        {
            RuleFor(x => x.AssetName)
                .NotEmpty()
                .WithMessage("Asset Name is required")
                .Matches(StringPattern.UserFirstName).WithMessage("Invalid Name. Please try again");
                
            RuleFor(x => x.Specification)
                .NotEmpty()
                .WithMessage("Specification is required")
                .Matches(StringPattern.Specification).WithMessage("Invalid Specification. Please try again");
            //Edit

            RuleFor(x => x.InstalledDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Installed Date is required")
                .Must(Over1Month)
                .WithMessage("Installed Date must not be over 30 days");

            RuleFor(x => x.State)
                .IsInEnum()
                .WithMessage("State Invalid");
        }
        protected bool Over1Month(DateTime date)
        {

            // TODO: check day 30, 31
            var now = DateTime.Now;
            var month = now.Month == 12 ? 1 : now.Month + 1;
            var year = now.Month == 12 ? now.Year + 1 : now.Year;
            var compareDate = new DateTime(year, month, now.Day);

            return DateTime.Compare(compareDate, date) >= 0 ;

        }

    }
}
