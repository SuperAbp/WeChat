using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace SuperAbp.WeChat.Official;

[Serializable]
public class WeChatOfficialException : BusinessException, IBusinessException
{
    public WeChatOfficialException(
        string message,
        string? code = null,
        string? details = null,
        Exception? innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
        Details = details;
    }

    /// <summary>Constructor for serializing.</summary>
    public WeChatOfficialException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}