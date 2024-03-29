﻿using AssetManagementTeam6.API.Dtos.Requests;
using Common.Constants;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Validation
{
    [ExcludeFromCodeCoverage]
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
             .NotNull()
             .NotEmpty()
             .Matches(StringPattern.UserPassword).WithMessage("Password must be at least 8 characters and contain at least 1 capital letter and a number");
        }
    }
}
