using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using Volo.Abp;

namespace SuperAbp.WeChat.MiniProgram;

[Serializable]
public class WeChatMiniProgramException : BusinessException
{
    public WeChatMiniProgramException(
        string message,
        string? code = null,
        string? details = null,
        System.Exception? innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
        Details = details;
    }

    /// <summary>Constructor for serializing.</summary>
    public WeChatMiniProgramException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}