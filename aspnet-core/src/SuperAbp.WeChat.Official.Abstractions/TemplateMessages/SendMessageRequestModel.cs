using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Official.TemplateMessages;

/// <summary>
/// 发送信息
/// </summary>
/// <param name="OpenId">OpenId</param>
/// <param name="TemplateId">模板Id</param>
/// <param name="Data">模板数据</param>
/// <param name="MiniProgramInfo">小程序信息</param>
/// <param name="Url">跳转地址</param>
/// <param name="ClientMessageId">防重入id</param>
public record SendMessageRequestModel(
    [property: JsonPropertyName("touser")] string OpenId,
    [property: JsonPropertyName("template_id")] string TemplateId,
    [property: JsonPropertyName("data")] object Data,
    [property: JsonPropertyName("miniprogram")] SendMessageRequestModel.MiniProgram? MiniProgramInfo = null,
    [property: JsonPropertyName("url")] string? Url = null,
    [property: JsonPropertyName("client_msg_id")] string? ClientMessageId = null)
{
    /// <summary>
    /// 小程序
    /// </summary>
    /// <param name="ApplicationId">应用Id</param>
    /// <param name="PagePath">页面路径</param>
    public record MiniProgram(
        [property: JsonPropertyName("appid")] string ApplicationId,
        [property: JsonPropertyName("pagepath")] string? PagePath = null);
}