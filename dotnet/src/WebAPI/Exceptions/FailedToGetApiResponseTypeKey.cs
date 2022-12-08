using System.Runtime.Serialization;

namespace JIL;

public class FailedToGetApiResponseTypeKeyException : Exception
{
    public Type ResponseType { get; }

    public FailedToGetApiResponseTypeKeyException(Type responseType)
    {
        ResponseType = responseType;
    }

    public FailedToGetApiResponseTypeKeyException(Type responseType, string? message) : base(message)
    {
        ResponseType = responseType;
    }

    public FailedToGetApiResponseTypeKeyException(Type responseType, string? message, Exception? innerException) : base(message, innerException)
    {
        ResponseType = responseType;
    }

    protected FailedToGetApiResponseTypeKeyException(Type responseType, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ResponseType = responseType;
    }
}
