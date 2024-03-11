using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.MiniProgram;

[DependsOn(typeof(AbpJsonModule))]
public class SuperAbpWeChatMiniProgramModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<WeChatMiniProgramOptions>(configuration.GetSection("SuperAbpWeChat:MiniProgram"));
    }
}