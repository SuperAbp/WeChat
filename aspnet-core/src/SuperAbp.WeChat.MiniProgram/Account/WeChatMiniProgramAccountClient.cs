using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace SuperAbp.WeChat.MiniProgram.Account;

public class WeChatMiniProgramAccountClient(
    IJsonSerializer serializer,
    IOptionsMonitor<WeChatMiniProgramOptions> options,
    ILogger<WeChatMiniProgramAccountClient> logger)
    : IWeChatMiniProgramAccountClient, ITransientDependency
{
    protected ILogger<WeChatMiniProgramAccountClient> Logger { get; } = logger;
    protected IJsonSerializer Serializer { get; } = serializer;
    protected WeChatMiniProgramOptions Options { get; } = options.CurrentValue;

    public virtual async Task<string?> CodeToSessionAsync(string code)
    {
        var options = new RestClientOptions(Options.Url);

        using var client = new RestClient(options);
        var uri =
            $"/sns/jscode2session?appid={Options.ApplicationId}&secret={Options.Secret}&js_code={code}&grant_type=authorization_code";
        var request = new RestRequest(uri);
        var response = await client.ExecuteGetAsync(request);

        Logger.LogInformation($"【微信小程序】请求【{uri}】响应【{response.Content}】");
        if (String.IsNullOrWhiteSpace(response.Content))
        {
            throw new WeChatMiniProgramRequestException("CodeToSession失败，Response Content为空");
        }
        var model = Serializer.Deserialize<CodeToSessionResponseModel>(response.Content);
        if (model.ErrorCode != 0)
        {
            throw new WeChatMiniProgramException(model.ErrorMessage);
        }

        return model.UnionId;
    }
}