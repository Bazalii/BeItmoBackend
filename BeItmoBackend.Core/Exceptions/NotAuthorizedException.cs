﻿namespace BeItmoBackend.Core.Exceptions;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException(string message)
        : base(message)
    {
    }
}