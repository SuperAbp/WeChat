using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace SuperAbp.WeChat.Pay;

/// <summary>
/// 微信支付授权
/// </summary>
public class WeChatPayAuthenticator : AuthenticatorBase
{
    public WeChatPayAuthenticator(string merchantId, string method, string uri, string body, string serialNo, string certificatePath)
        : base(Sign(merchantId, method, uri, body, serialNo, certificatePath))
    {
    }

    private static string Sign(string merchantId, string method, string uri, string body, string serialNo, string certificatePath)
    {
        long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        string random = Path.GetRandomFileName();
        string message = $"{method}\n{uri}\n{timestamp}\n{random}\n{body}\n";
        string signature = Sign(message, certificatePath, merchantId);
        return $"WECHATPAY2-SHA256-RSA2048 mchid=\"{merchantId}\",nonce_str=\"{random}\",timestamp=\"{timestamp}\",serial_no=\"{serialNo}\",signature=\"{signature}\"";
    }

    private static string Sign(string source, string certificatePath, string key)
    {
        var cert = new X509Certificate2(certificatePath, key);
        var rsa = cert.GetRSAPrivateKey() ?? throw new Exception("无法识别的响应");
        var res = Convert.ToBase64String(rsa.SignData(Encoding.UTF8.GetBytes(source), HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1));
        return res;
    }

    protected override ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        => new(new HeaderParameter(KnownHeaders.Authorization, accessToken));
}