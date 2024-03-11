using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.MiniProgram.Account;

public class CodeToSessionResponseModel
{
    [JsonPropertyName("openid")] public virtual string OpenId { get; set; } = default!;

    [JsonPropertyName("session_key")]
    public virtual string SessionKey { get; set; } = default!;

    [JsonPropertyName("unionid")]
    public virtual string? UnionId { get; set; }

    [JsonPropertyName("errcode")]
    public virtual long ErrorCode { get; set; }

    [JsonPropertyName("errmsg")]
    public virtual string ErrorMessage { get; set; } = default!;
}