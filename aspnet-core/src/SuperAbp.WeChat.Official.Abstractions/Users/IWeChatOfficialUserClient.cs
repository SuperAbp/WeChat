namespace SuperAbp.WeChat.Official.Users;

public interface IWeChatOfficialUserClient
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="openId"></param>
    /// <param name="lang">语言</param>
    /// <returns></returns>
    Task<UserInfoResponseModel> GetUserInfoAsync(string openId, string lang = "zh_CN");

    /// <summary>
    /// 批量获取用户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IReadOnlyList<UserInfoResponseModel>> GetUserInfosAsync(GetUserInfosRequestModel input);
}