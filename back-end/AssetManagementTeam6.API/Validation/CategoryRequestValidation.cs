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
             .NotEmpty().WithMessage("Category ID is required")
             .NotNull().WithMessage("Category ID is required")
             .Matches(StringPattern.CategoryID).WithMessage("The Category ID must be from 2 to 8 characters and must be capital letters");
            //Edit
            RuleFor(x => x.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("Category Name is required")
            .NotNull().WithMessage("Category Name is required")
            .Matches(StringPattern.Name).WithMessage("The Category Name is invalid! Do not enter the first number");
        }
    }
}
