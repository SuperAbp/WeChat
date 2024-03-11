using Microsoft.Extensions.DependencyInjection;
using SuperAbp.WeChat.Pay;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.Pay.Native;

[DependsOn(typeof(SuperAbpWeChatPayModule))]
public class SuperAbpWeChatPayNativeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWeChatPayClient, WeChatPayNativeClient>();
    }
}