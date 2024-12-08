using System.Runtime.Serialization;

namespace User.Login.Exceptions.ExceptionBase;

[Serializable]
public class InvalidLoginException : TechChallengeException
{
    public InvalidLoginException() : base(ErrorsMessages.InvalidLogin)
    {
    }

    protected InvalidLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
