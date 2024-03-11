using System.Text.Json.Nodes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RestSharp;
using Volo.Abp;
using Volo.Abp.Caching;

namespace SuperAbp.WeChat.Official;

public class WeChatOfficialClient(WeChatOfficialOptions options, ILoggerFactory logger, IDistributedCache<string> cache)
{
    protected WeChatOfficialOptions Options { get; } = options;
    protected ILogger<WeChatOfficialClient> Logger { get; } = logger.CreateLogger<WeChatOfficialClient>();
    protected IDistributedCache<string> Cache { get; } = cache;

    public async Task<string?> GetAccessTokenAsync()
    {
        return await Cache.GetOrAddAsync("WeChat_Official_AccessToken:" + Options.ApplicationId, async () =>
        {
            var response =
                await GetAsync(
                    $"/cgi-bin/token?grant_type=client_credential&appid={Options.ApplicationId}&secret={Options.Secret}");
            var content = response.Content
                ?? throw new WeChatOfficialRequestException("获取AccessToken失败，Response Content为空");

            var jobj = JsonNode.Parse(content);
            var accessToken = jobj?["access_token"]
                ?? throw new WeChatOfficialException(message: "获取AccessToken失败，返回：" + content);

            return accessToken.ToString();
        }, () => new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(7000)
        });
    }

    /// <summary>
    /// POST请求
    /// </summary>
    /// <param name="body">参数</param>
    /// <param name="uri">地址</param>
    /// <returns></returns>
    protected virtual async Task<RestResponse> PostAsync(string uri, string body)
    {
        var options = new RestClientOptions(Options.Url);

        using var client = new RestClient(options);
        var request = new RestRequest(uri)
            .AddStringBody(body, DataFormat.Json);
        var response = await client.ExecutePostAsync(request);
        Logger.LogInformation($"【微信公众号】携带【{body}】请求【{uri}】响应【{response.Content}】");
        return response;
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="uri">地址</param>
    /// <returns></returns>
    protected virtual async Task<RestResponse> GetAsync(string uri)
    {
        var options = new RestClientOptions(Options.Url);

        using var client = new RestClient(options);
        var request = new RestRequest(uri);
        var response = await client.ExecuteGetAsync(request);
        Logger.LogInformation($"【微信公众号】请求【{uri}】响应【{response.Content}】");
        return response;
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="uri">地址</param>
    /// <returns></returns>
    protected virtual async Task<T?> GetAsync<T>(string uri)
    {
        var options = new RestClientOptions(Options.Url);

        using var client = new RestClient(options);
        return await client.GetJsonAsync<T>(uri);
    }
}