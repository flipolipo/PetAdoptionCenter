using FluentValidation;
using SimpleWebDal.Models.CalendarModel;

namespace SImpleWebLogic.Validations.CalendarCreateValidation;

public class CalendarValidator : AbstractValidator<CalendarActivity>
{
    public CalendarValidator()
    {
        RuleFor(calendar => calendar.DateWithTime)
         .NotEmpty().WithMessage("ActivityDate cannot be empty.")
         .Must(date => date > DateTime.Now).WithMessage("ActivityDate must be in the future.");
        RuleForEach(calendar => calendar.Activities).SetValidator(new ActivityValidator());
    }
}
