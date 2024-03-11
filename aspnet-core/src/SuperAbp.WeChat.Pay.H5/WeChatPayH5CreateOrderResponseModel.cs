using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Pay.H5;

public class WeChatPayH5CreateOrderResponseModel
{
    [JsonPropertyName("h5_url")]
    public string Url { get; set; } = default!;
}