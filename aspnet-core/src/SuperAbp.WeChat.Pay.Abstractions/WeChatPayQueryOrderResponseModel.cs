using System.Text.Json.Serialization;
using System;

namespace SuperAbp.WeChat.Pay;

public partial class WeChatPayQueryOrderResponseModel
{
    [JsonPropertyName("appid")]
    public string Appid { get; set; } = default!;

    [JsonPropertyName("attach")]
    public string Attach { get; set; } = default!;

    [JsonPropertyName("bank_type")]
    public string BankType { get; set; } = default!;

    [JsonPropertyName("mchid")]
    public string MerchantId { get; set; } = default!;

    [JsonPropertyName("out_trade_no")]
    public string MerchantNumber { get; set; } = default!;

    [JsonPropertyName("success_time")]
    public DateTimeOffset SuccessTime { get; set; }

    [JsonPropertyName("trade_state")]
    public string TradeState { get; set; } = default!;

    [JsonPropertyName("trade_state_desc")]
    public string TradeStateDesc { get; set; } = default!;

    [JsonPropertyName("trade_type")]
    public string TradeType { get; set; } = default!;

    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;

    [JsonPropertyName("payer")]
    public OrderPayer Payer { get; set; } = default!;

    [JsonPropertyName("amount")]
    public OrderAmount Amount { get; set; } = default!;

    public partial class OrderAmount
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;

        [JsonPropertyName("payer_currency")]
        public string PayerCurrency { get; set; } = default!;

        [JsonPropertyName("payer_total")]
        public long PayerTotal { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class OrderPayer
    {
        [JsonPropertyName("openid")]
        public string Openid { get; set; } = default!;
    }
}