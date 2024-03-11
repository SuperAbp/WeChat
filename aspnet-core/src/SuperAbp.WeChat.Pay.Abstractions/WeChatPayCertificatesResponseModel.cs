using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;
using System;

namespace SuperAbp.WeChat.Pay;

public class WeChatPayCertificatesResponseModel
{
    [JsonPropertyName("data")]
    public Data[] Datas { get; set; } = default!;

    public class Data
    {
        [JsonPropertyName("effective_time")]
        public DateTimeOffset EffectiveTime { get; set; }

        [JsonPropertyName("encrypt_certificate")]
        public Certificate Certificate { get; set; } = default!;

        [JsonPropertyName("expire_time")]
        public DateTimeOffset ExpireTime { get; set; }

        [JsonPropertyName("serial_no")]
        public string SerialNo { get; set; } = default!;
    }

    public class Certificate
    {
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; } = default!;

        [JsonPropertyName("associated_data")]
        public string AssociatedData { get; set; } = default!;

        [JsonPropertyName("ciphertext")]
        public string Ciphertext { get; set; } = default!;

        [JsonPropertyName("nonce")]
        public string Nonce { get; set; } = default!;
    }
}