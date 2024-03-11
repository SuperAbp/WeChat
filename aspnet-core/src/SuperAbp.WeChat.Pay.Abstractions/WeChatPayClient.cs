using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.Json;
using System.Security.Cryptography.X509Certificates;

namespace SuperAbp.WeChat.Pay;

public abstract class WeChatPayClient(
    IOptionsMonitor<WeChatPayOptions> options,
    IJsonSerializer serializer,
    ILoggerFactory logger)
    : IWeChatPayClient
{
    protected WeChatPayOptions Options { get; } = options.CurrentValue;
    protected IJsonSerializer Serializer { get; } = serializer;
    private readonly ILogger<WeChatPayClient> _logger = logger.CreateLogger<WeChatPayClient>();

    public bool CheckSign(string key, string data, string signature)
    {
        var cert = new X509Certificate2(Encoding.UTF8.GetBytes(key));
        var rsaParam = cert.GetRSAPublicKey().ExportParameters(false);
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(rsaParam);
        return rsa.VerifyData(Encoding.UTF8.GetBytes(data), Convert.FromBase64String(signature), HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);
    }

    public abstract Task<string> CreateOrderAsync(WeChatPayCreateOrderRequestModel input);

    /// <summary>
    /// 订单查询
    /// </summary>
    /// <param name="transactionId">微信订单号</param>
    /// <returns></returns>
    public virtual async Task<WeChatPayQueryOrderResponseModel> QueryOrderByIdAsync(string transactionId)
    {
        return await QueryOrderByUrlAsync($"/v3/pay/transactions/id/{transactionId}?mchid={Options.MerchantId}");
    }

    /// <summary>
    /// 订单查询
    /// </summary>
    /// <param name="orderNumber">商户订单号</param>
    /// <returns></returns>
    public virtual async Task<WeChatPayQueryOrderResponseModel> QueryOrderAsync(string orderNumber)
    {
        return await QueryOrderByUrlAsync(
            $"/v3/pay/transactions/out-trade-no/{orderNumber}?mchid={Options.MerchantId}");
    }

    protected virtual async Task<WeChatPayQueryOrderResponseModel> QueryOrderByUrlAsync(string url)
    {
        var response = await GetAsync(url);
        var content = response.Content
                      ?? throw new WeChatPayRequestException("Response Content为空");
        if (response.IsSuccessStatusCode)
        {
            var data = Serializer.Deserialize<WeChatPayQueryOrderResponseModel>(content);
            return data;
        }
        else
        {
            var data = Serializer.Deserialize<WeChatPayErrorResponseModel>(content);
            throw new WeChatPayException(Serializer.Serialize(data));
        }
    }

    public async Task<WeChatPayCertificatesResponseModel.Data> GetCertificatesAsync()
    {
        var response = await GetAsync("/v3/certificates");
        var content = response.Content
                      ?? throw new WeChatPayRequestException("Response Content为空");
        if (response.IsSuccessStatusCode)
        {
            var data = Serializer.Deserialize<WeChatPayCertificatesResponseModel>(content);
            return data.Datas.OrderByDescending(d => d.EffectiveTime).First();
        }
        else
        {
            var data = Serializer.Deserialize<WeChatPayErrorResponseModel>(content);
            throw new WeChatPayException(Serializer.Serialize(data));
        }
    }

    /// <summary>
    /// POST请求
    /// </summary>
    /// <param name="body">参数</param>
    /// <param name="uri">地址</param>
    /// <returns></returns>
    protected virtual async Task<RestResponse> PostAsync(string body, string uri)
    {
        var options = new RestClientOptions("https://api.mch.weixin.qq.com")
        {
            Authenticator = new WeChatPayAuthenticator(Options.MerchantId, "POST",
                uri, body, Options.CertificateSerialNumber, Options.CertificatePath)
        };

        using var client = new RestClient(options);
        var request = new RestRequest(uri)
            .AddStringBody(body, DataFormat.Json);
        var response = await client.ExecutePostAsync(request);
        _logger.LogInformation($"【微信支付】携带【{body}】请求【{uri}】响应【{response.Content}】");
        return response;
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="uri">地址</param>
    /// <returns></returns>
    protected virtual async Task<RestResponse> GetAsync(string uri)
    {
        var options = new RestClientOptions(Options.Url)
        {
            Authenticator = new WeChatPayAuthenticator(Options.MerchantId, "GET",
                uri, String.Empty, Options.CertificateSerialNumber, Options.CertificatePath)
        };

        using var client = new RestClient(options);
        var request = new RestRequest(uri);
        var response = await client.ExecuteGetAsync(request);
        _logger.LogInformation($"【微信支付】请求【{uri}】响应【{response.Content}】");
        return response;
    }
}