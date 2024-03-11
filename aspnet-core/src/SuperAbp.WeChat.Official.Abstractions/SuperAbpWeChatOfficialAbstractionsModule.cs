using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.Official;

[DependsOn(typeof(AbpJsonModule))]
public class SuperAbpWeChatOfficialAbstractionsModule : AbpModule
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