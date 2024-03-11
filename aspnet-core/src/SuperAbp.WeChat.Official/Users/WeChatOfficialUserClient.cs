using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace SuperAbp.WeChat.Official.Users;

/// <summary>
/// 用户管理
/// </summary>
public class WeChatOfficialUserClient(
    IOptionsMonitor<WeChatOfficialOptions> options,
    ILoggerFactory logger,
    IJsonSerializer serializer,
    IDistributedCache<string> cache)
    : WeChatOfficialClient(options.CurrentValue, logger, cache), IWeChatOfficialUserClient, ITransientDependency
{
    public async Task<UserInfoResponseModel> GetUserInfoAsync(string openId, string lang = "zh_CN")
    {
        var accessToken = await GetAccessTokenAsync();
        var response = await GetAsync($"/cgi-bin/user/info?access_token={accessToken}&openid={openId}&lang={lang}");
        var content = response.Content
                      ?? throw new WeChatOfficialRequestException("Response Content为空");
        var jobj = JsonNode.Parse(content);
        if (jobj?["subscribe"] is null)
        {
            throw new WeChatOfficialException("获取用户信息失败：" + content);
        }

        var model = serializer.Deserialize<UserInfoResponseModel>(content);
        return model;
    }

    public async Task<IReadOnlyList<UserInfoResponseModel>> GetUserInfosAsync(GetUserInfosRequestModel input)
    {
        var accessToken = await GetAccessTokenAsync();

        var response = await PostAsync($"/cgi-bin/user/info/batchget?access_token={accessToken}", serializer.Serialize(input));
        var content = response.Content
                      ?? throw new WeChatOfficialRequestException("Response Content为空");
        var jobj = JsonNode.Parse(content);
        if (jobj?["errcode"] is not null)
        {
            throw new WeChatOfficialException("获取用户信息失败：" + content);
        }

        var model = serializer.Deserialize<List<UserInfoResponseModel>>(content);
        return model;
    }
}