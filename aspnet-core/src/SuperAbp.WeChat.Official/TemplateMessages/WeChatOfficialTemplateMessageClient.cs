using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace SuperAbp.WeChat.Official.TemplateMessages;

public class WeChatOfficialTemplateMessageClient(
    IOptionsMonitor<WeChatOfficialOptions> options,
    ILoggerFactory logger,
    IJsonSerializer serializer,
    IDistributedCache<string> cache)
    : WeChatOfficialClient(options.CurrentValue, logger, cache), IWeChatOfficialTemplateMessageClient, ITransientDependency
{
    protected IJsonSerializer Serializer = serializer;

    public async Task<long> SendMessageAsync(SendMessageRequestModel input)
    {
        var accessToken = await GetAccessTokenAsync();
        var response = await PostAsync($"/cgi-bin/message/template/send?access_token={accessToken}", Serializer.Serialize(input));
        var content = response.Content
            ?? throw new WeChatOfficialRequestException("Response Content为空");
        var model = Serializer.Deserialize<TemplateMessageResponseModel>(content);
        if (model.ErrorCode != 0)
        {
            throw new WeChatOfficialException("消息发送失败：" + model.ErrorMessage);
        }

        return model.MessageId;
    }
}