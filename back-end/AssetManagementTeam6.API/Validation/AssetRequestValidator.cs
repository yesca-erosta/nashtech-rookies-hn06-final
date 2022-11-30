using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class AssetRequestValidator : AbstractValidator<AssetRequest>
    {
        public AssetRequestValidator()
        {
            RuleFor(x => x.AssetName)
                .NotEmpty()
                .WithMessage("Asset Name is required");
                
            RuleFor(x => x.Specification)
                .NotEmpty()
                .WithMessage("Specification is required");

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
            var now = DateTime.Now;
            var month = now.Month + 1;

            var compareDate = new DateTime(now.Year, month, now.Day);

            return DateTime.Compare(compareDate, date) >= 0 ;

        }

    }
}
