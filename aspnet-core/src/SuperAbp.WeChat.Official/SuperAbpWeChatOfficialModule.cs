using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.Official;

[DependsOn(typeof(SuperAbpWeChatOfficialAbstractionsModule))]
public class SuperAbpWeChatOfficialModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<WeChatOfficialOptions>(configuration.GetSection("SuperAbpWeChat:Official"));

        Configure<JsonSerializerOptions>(options =>
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }
}