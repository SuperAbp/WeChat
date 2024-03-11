using Volo.Abp.DependencyInjection;

namespace SuperAbp.WeChat.Pay;

public interface IWeChatPayClient : ITransientDependency
{
    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="data">内容</param>
    /// <param name="signature">签名</param>
    /// <returns></returns>
    bool CheckSign(string key, string data, string signature);

    /// <summary>
    /// 下单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="WeChatPayRequestException"></exception>
    /// <exception cref="WeChatPayException"></exception>
    Task<string> CreateOrderAsync(WeChatPayCreateOrderRequestModel input);

    /// <summary>
    /// 获取证书
    /// </summary>
    /// <returns></returns>
    /// <exception cref="WeChatPayRequestException"></exception>
    /// <exception cref="WeChatPayException"></exception>
    Task<WeChatPayCertificatesResponseModel.Data> GetCertificatesAsync();

    /// <summary>
    /// 查询订单
    /// </summary>
    /// <param name="orderNumber">订单号</param>
    /// <returns></returns>
    /// <exception cref="WeChatPayRequestException"></exception>
    /// <exception cref="WeChatPayException"></exception>
    Task<WeChatPayQueryOrderResponseModel> QueryOrderAsync(string orderNumber);
}