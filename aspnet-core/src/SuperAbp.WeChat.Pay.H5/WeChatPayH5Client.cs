using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperAbp.WeChat.Pay;
using Volo.Abp.Json;

namespace SuperAbp.WeChat.Pay.H5;

public class WeChatPayH5Client(
    IJsonSerializer serializer,
    IOptionsMonitor<WeChatPayOptions> options,
    ILoggerFactory logger)
    : WeChatPayClient(options, serializer, logger)
{
    public override async Task<string> CreateOrderAsync(WeChatPayCreateOrderRequestModel input)
    {
        if (String.IsNullOrWhiteSpace(input.ApplicationId))
        {
            input.ApplicationId = Options.ApplicationId;
        }
        if (String.IsNullOrWhiteSpace(input.MerchantId))
        {
            input.MerchantId = Options.MerchantId;
        }

        var body = Serializer.Serialize(input);
        var response = await PostAsync(body, "/v3/pay/transactions/h5");
        var content = response.Content
                      ?? throw new WeChatPayRequestException("Response Content为空");
        if (response.IsSuccessStatusCode)
        {
            var data = Serializer.Deserialize<WeChatPayH5CreateOrderResponseModel>(content);
            return data.Url;
        }
        else
        {
            var data = Serializer.Deserialize<WeChatPayErrorResponseModel>(content);
            throw new WeChatPayException(Serializer.Serialize(data));
        }
    }
}