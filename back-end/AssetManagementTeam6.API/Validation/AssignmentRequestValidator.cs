﻿using AssetManagementTeam6.API.Dtos.Requests;
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
                .WithMessage("Assigned Date only accept for current date or future date");
            RuleFor(x => x.Note)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(StringPattern.Note)
                .WithMessage("Invalid Note. Please try again");
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
