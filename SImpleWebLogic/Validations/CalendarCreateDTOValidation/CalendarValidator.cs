using FluentValidation;
using SimpleWebDal.DTOs.CalendarDTOs;

namespace SImpleWebLogic.Validations.CalendarCreateDTOValidation;

public class CalendarValidator : AbstractValidator<CalendarActivityCreateDTO>
{
    public CalendarValidator() { }
}
