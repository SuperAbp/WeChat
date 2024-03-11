using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace SuperAbp.WeChat.Official;

public class WeChatOfficialRequestException : WeChatOfficialException
{
    public WeChatOfficialRequestException(string message, string? code = null, string? details = null, Exception? innerException = null, LogLevel logLevel = LogLevel.Warning) : base(message, code, details, innerException, logLevel)
    {
    }

    public WeChatOfficialRequestException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }
}