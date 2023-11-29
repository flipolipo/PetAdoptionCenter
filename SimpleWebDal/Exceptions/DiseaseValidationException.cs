namespace SimpleWebDal.Exceptions;

public class DiseaseValidationException : Exception
{
    public DiseaseValidationException(string message) : base(message) { }
}
