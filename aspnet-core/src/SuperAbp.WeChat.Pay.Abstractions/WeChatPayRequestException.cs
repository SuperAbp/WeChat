using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace SuperAbp.WeChat.Pay;

public class WeChatPayRequestException : WeChatPayException
{
    public WeChatPayRequestException(string message,
        string? code = null,
        string? details = null,
        Exception? innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(message, code, details, innerException, logLevel)
    {
        Details = details;
    }

    /// <summary>Constructor for serializing.</summary>
    public WeChatPayRequestException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}