using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using FluentValidation;

namespace AssetManagementTeam6.API.Validation
{
    public class CategoryRequestValidatior : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidatior()
        {

            RuleFor(x => x.Id)
             .Cascade(CascadeMode.StopOnFirstFailure)
<<<<<<< HEAD
             .NotNull().WithMessage("the Category ID should have 2-8 characters")
=======
             .NotNull()
>>>>>>> 68de8b6f41432e82f8094b73590018c5d0c8f405
             .NotEmpty()
             .Matches(StringPattern.CategoryID).WithMessage("the Category ID is invalid!");

            RuleFor(x => x.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
<<<<<<< HEAD
            .NotNull().WithMessage("should contain the alphabet and numeric!")
=======
            .NotNull()
>>>>>>> 68de8b6f41432e82f8094b73590018c5d0c8f405
            .NotEmpty()
            .Matches(StringPattern.Name).WithMessage("the Category ID should have 2-8 characters");
        }
    }
}
