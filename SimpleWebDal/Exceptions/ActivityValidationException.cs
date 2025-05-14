namespace SimpleWebDal.Exceptions;

public class ActivityValidationException : Exception
{
    public ActivityValidationException(string message) : base(message) { }
}
