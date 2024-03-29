﻿namespace ECommerceBackend.Application.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException() : base("UserName or password is wrong")
    {

    }

    public NotFoundUserException(string? message) : base(message)
    {

    }

    public NotFoundUserException(string? message, Exception? exception) : base(message, exception)
    {

    }
}