namespace SuperAbp.WeChat.Pay;

public class WeChatPayErrorResponseModel
{
    public string Code { get; set; } = default!;

    public string Message { get; set; } = default!;
}