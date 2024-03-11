namespace SuperAbp.WeChat.Official.Account;

public interface IWeChatOfficialAccountClient
{
    /// <summary>
    /// 获取二维码
    /// </summary>
    /// <returns></returns>
    Task<string> GetQrCodeAsync(GetQrCodeRequestModel input);
}