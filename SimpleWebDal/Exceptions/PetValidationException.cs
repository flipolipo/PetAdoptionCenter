namespace SimpleWebDal.Exceptions;

public class PetValidationException : Exception
{
    public PetValidationException(string message) : base(message) { }
}
