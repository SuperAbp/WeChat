using System.Text.Json.Nodes;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace SuperAbp.WeChat.Official.Account;

/// <summary>
/// 账户管理
/// </summary>
public class WeChatOfficialAccountClient(
    IOptionsMonitor<WeChatOfficialOptions> options,
    ILoggerFactory loggerFactory,
    IJsonSerializer serializer,
    IDistributedCache<string> cache)
    : WeChatOfficialClient(options.CurrentValue, loggerFactory, cache), IWeChatOfficialAccountClient, ITransientDependency
{
    protected IJsonSerializer Serializer = serializer;

    public async Task<string> GetQrCodeAsync(GetQrCodeRequestModel input)
    {
        var token = await GetAccessTokenAsync();
        var response = await PostAsync("/cgi-bin/qrcode/create?access_token=" + token,
            Serializer.Serialize(input));
        var content = response.Content
                      ?? throw new WeChatOfficialRequestException("Response Content为空");
        var jobj = JsonNode.Parse(content);
        if (jobj?["ticket"] is null)
        {
            throw new WeChatOfficialException("获取Ticket失败：" + content);
        }

        var ticket = jobj["ticket"]!.ToString();
        return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + HttpUtility.UrlEncode(ticket);
    }
}