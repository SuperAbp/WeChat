using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace SuperAbp.WeChat.Pay;

public class WeChatPayException : BusinessException
{
    public WeChatPayException(string message,
        string? code = null,
        string? details = null,
        Exception? innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
        Details = details;
    }

    /// <summary>Constructor for serializing.</summary>
    public WeChatPayException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}