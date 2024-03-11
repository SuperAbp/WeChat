using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.Pay;

public class SuperAbpWeChatPayModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<WeChatPayOptions>(configuration.GetSection("SuperAbpWeChat:Pay"));

        Configure<JsonSerializerOptions>(options =>
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }
}