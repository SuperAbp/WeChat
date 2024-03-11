using Microsoft.Extensions.DependencyInjection;
using SuperAbp.WeChat.Pay;
using Volo.Abp.Modularity;

namespace SuperAbp.WeChat.Pay.H5;

[DependsOn(typeof(SuperAbpWeChatPayModule))]
public class SuperAbpWeChatPayH5Module : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWeChatPayClient, WeChatPayH5Client>();
    }
}