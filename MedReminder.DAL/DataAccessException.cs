﻿namespace MedReminder.Dal;

public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
