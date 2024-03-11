namespace SuperAbp.WeChat.Official.TemplateMessages;

/// <summary>
/// 模板消息
/// </summary>
public interface IWeChatOfficialTemplateMessageClient
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="input"></param>
    /// <returns>消息Id</returns>
    Task<long> SendMessageAsync(SendMessageRequestModel input);
}