using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class AssetRequestValidator : AbstractValidator<AssetRequest>
    {
        public AssetRequestValidator()
        {   
            // RuleFor(x => x.AssetName)
            //     .Cascade(CascadeMode.StopOnFirstFailure)
            //     .NotNull()
            //     .NotEmpty()
            //     .Matches(StringPattern.Name);

            //RuleFor(x => x.Specification)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotNull()
            //    .NotEmpty()
            //    .Matches(StringPattern.Name);

            RuleFor(x => x.InstalledDate)
                .Must(Over1Month)
                .WithMessage("Installed Date must not be over 30 days");

            RuleFor(x => x.State)
                .IsInEnum()
                .WithMessage("State Invalid");
        }
        protected bool Over1Month(DateTime date)
        {
            var now = DateTime.Now;
            var month = now.Month + 1;

            var compareDate = new DateTime(now.Year, month, now.Day);

            return DateTime.Compare(compareDate, date) >= 0 ;

        }

    }
}
