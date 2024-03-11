using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Pay.Native;

public class WeChatPayNativeCreateOrderResponseModel
{
    [JsonPropertyName("code_url")] public string Url { get; set; } = default!;
}