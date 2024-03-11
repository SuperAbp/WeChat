namespace SuperAbp.WeChat.Pay;

public class WeChatPayOptions
{
    /// <summary>
    /// Api密钥
    /// </summary>
    public string Key { get; set; } = default!;

    /// <summary>
    /// AppId
    /// </summary>
    public string ApplicationId { get; set; } = default!;

    /// <summary>
    /// 商户号
    /// </summary>
    public string MerchantId { get; set; } = default!;

    /// <summary>
    /// 证书序列号
    /// </summary>
    public string CertificateSerialNumber { get; set; } = default!;

    /// <summary>
    /// 证书路径
    /// </summary>
    public string CertificatePath { get; set; } = default!;

    public string Url { get; set; } = default!;
}