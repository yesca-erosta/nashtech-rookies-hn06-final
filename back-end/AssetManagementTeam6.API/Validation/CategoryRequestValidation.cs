using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.Data.Entities;
using Common.Constants;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Validation
{
    [ExcludeFromCodeCoverage]
    public class CategoryRequestValidatior : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidatior()
        {

            RuleFor(x => x.Id)
             .Cascade(CascadeMode.StopOnFirstFailure)
             .NotNull().WithMessage("the Category ID should have 2-8 characters")
             .NotEmpty()
             .Matches(StringPattern.CategoryID).WithMessage("the Category ID is invalid!");

            RuleFor(x => x.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("should contain the alphabet and numeric!")
            .NotEmpty()
            .Matches(StringPattern.Name).WithMessage("the Category ID should have 2-8 characters");
        }
    }
}
