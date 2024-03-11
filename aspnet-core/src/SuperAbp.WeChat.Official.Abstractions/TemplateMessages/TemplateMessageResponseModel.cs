using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Official.TemplateMessages;

public class TemplateMessageResponseModel
{
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("errmsg")] public string ErrorMessage { get; set; } = default!;

    [JsonPropertyName("msgid")]
    public long MessageId { get; set; }
}