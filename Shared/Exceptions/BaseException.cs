﻿using Shared.Responses;

namespace Shared.Exceptions
{
public class BaseException : Exception
{
    public enum EType : byte
    {
        Unknown,
        FromTo,
        Value,
        Database
    }

    public ErrorResponse Error { get; }
    public EType Type { get; }

    protected BaseException(EType type, ErrorResponse error) : base(error.Message)
    {
        Type = type;

        Error = error;
    }
}
}
