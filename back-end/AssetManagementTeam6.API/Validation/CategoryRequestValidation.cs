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
             .NotNull()
             .NotEmpty()
             .Matches(StringPattern.CategoryID);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Matches(StringPattern.Name);           
        }
    }
}
