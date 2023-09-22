using FluentValidation;
using SimpleWebDal.DTOs.CalendarDTOs;

namespace SImpleWebLogic.Validations.CalendarCreateValidation;

public class CalendarValidator : AbstractValidator<CalendarActivityCreateDTO>
{
    public CalendarValidator()
    {
        RuleFor(calendar => calendar.DateWithTime)
         .NotEmpty().WithMessage("ActivityDate cannot be empty.")
         .Must(date => date > DateTime.Now).WithMessage("ActivityDate must be in the future.");
        When(calendar => calendar.Activities.Any(), () =>
        {
            RuleForEach(calendar => calendar.Activities)
                .NotNull().WithMessage("Activities cannot be null when there are activities.");
               // .SetValidator(new ActivityValidator());
        });
    }
}
