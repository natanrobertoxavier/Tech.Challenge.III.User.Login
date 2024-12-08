using System.Runtime.Serialization;

namespace User.Login.Exceptions.ExceptionBase;

public class TechChallengeException : SystemException
{
    public TechChallengeException(string mensagem) : base(mensagem)
    {
    }

    protected TechChallengeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}