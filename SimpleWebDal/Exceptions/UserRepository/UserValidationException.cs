namespace SimpleWebDal.Exceptions.UserRepository;

public class UserValidationException : Exception
{
    public UserValidationException(string message) : base(message)
    {
    }
}
