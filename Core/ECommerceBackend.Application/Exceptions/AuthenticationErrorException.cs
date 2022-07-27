namespace ECommerceBackend.Application.Exceptions;

public class AuthenticationErrorException : Exception
{
    public AuthenticationErrorException() : base("Error while verifying user!")
    {

    }

    public AuthenticationErrorException(string? message) : base(message)
    {

    }

    public AuthenticationErrorException(string? message, Exception? exception) : base(message, exception)   
    {

    }
}