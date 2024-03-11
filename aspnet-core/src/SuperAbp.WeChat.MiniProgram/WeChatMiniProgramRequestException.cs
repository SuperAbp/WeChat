using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace SuperAbp.WeChat.MiniProgram;

public class WeChatMiniProgramRequestException : WeChatMiniProgramException
{
    public WeChatMiniProgramRequestException(string message,
        string? code = null,
        string? details = null,
        Exception? innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(message, code, details, innerException, logLevel)
    {
    }

    public WeChatMiniProgramRequestException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
    {
    }
}