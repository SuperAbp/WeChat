using System.Text.Json.Serialization;
using static SuperAbp.WeChat.Pay.WeChatPayCreateOrderRequestModel;
using static SuperAbp.WeChat.Pay.WeChatPayCreateOrderRequestModel.OrderDetail;
using static SuperAbp.WeChat.Pay.WeChatPayCreateOrderRequestModel.OrderSceneInfo;

namespace SuperAbp.WeChat.Pay;

/// <summary>
/// 下单
/// </summary>
/// <param name="ApplicationId">应用ID</param>
/// <param name="MerchantId">商户号</param>
/// <param name="Description">描述</param>
/// <param name="OrderNumber">订单号</param>
/// <param name="NotificationUrl">通知地址</param>
/// <param name="Amount">金额</param>
/// <param name="ExpireTime">交易结束时间</param>
/// <param name="Attach">附加数据</param>
/// <param name="GoodsTag">订单优惠标记</param>
/// <param name="SupportInvoice">电子发票入口开放标识</param>
/// <param name="Detail">优惠功能</param>
/// <param name="SceneInfo">场景信息</param>
/// <param name="SettleInfo">结算信息</param>
public record WeChatPayCreateOrderRequestModel(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("out_trade_no")] string OrderNumber,
    [property: JsonPropertyName("notify_url")] string NotificationUrl,
    [property: JsonPropertyName("amount")] WeChatPayCreateOrderRequestModel.OrderAmount Amount,
    [property: JsonPropertyName("time_expire")] DateTime? ExpireTime = null,
    [property: JsonPropertyName("attach")] string? Attach = null,
    [property: JsonPropertyName("goods_tag")] string? GoodsTag = null,
    [property: JsonPropertyName("support_fapiao")] bool? SupportInvoice = null,
    [property: JsonPropertyName("detail")] WeChatPayCreateOrderRequestModel.OrderDetail? Detail = null,
    [property: JsonPropertyName("scene_info")] WeChatPayCreateOrderRequestModel.OrderSceneInfo? SceneInfo = null,
    [property: JsonPropertyName("settle_info")] WeChatPayCreateOrderRequestModel.OrderSettleInfo? SettleInfo = null)
{
    [JsonPropertyName("appid")]
    public string? ApplicationId { get; set; }
    [JsonPropertyName("mchid")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// 订单金额
    /// </summary>
    /// <param name="Total">总金额</param>
    /// <param name="Currency">货币类型</param>
    public record OrderAmount(int Total, string Currency = "CNY");

    /// <summary>
    /// 优惠功能
    /// </summary>
    /// <param name="CostPrice">订单原价</param>
    /// <param name="InvoiceId">商品小票ID</param>
    /// <param name="GoodsDetail">单品列表</param>
    public record OrderDetail(
        [property: JsonPropertyName("cost_price")] int CostPrice,
        [property: JsonPropertyName("invoice_id")] string InvoiceId,
        [property: JsonPropertyName("goods_detail")] OrderDetail.OrderGoodsDetail[] GoodsDetail)
    {
        /// <summary>
        /// 单品列表
        /// </summary>
        /// <param name="MerchantGoodsId">商户侧商品编码</param>
        /// <param name="Quantity">商品数量</param>
        /// <param name="UnitPrice">商品单价</param>
        /// <param name="WeChatPayGoodsId">微信支付商品编码</param>
        /// <param name="GoodsName">商品名称</param>
        public record OrderGoodsDetail(
            [property: JsonPropertyName("merchant_goods_id")] string MerchantGoodsId,
            [property: JsonPropertyName("quantity")] int Quantity,
            [property: JsonPropertyName("unit_price")] int UnitPrice,
            [property: JsonPropertyName("wechatpay_goods_id")] string? WeChatPayGoodsId = null,
            [property: JsonPropertyName("goods_name")] string? GoodsName = null);
    }

    /// <summary>
    /// 场景信息
    /// </summary>
    /// <param name="PayerClientIp">用户终端IP</param>
    /// <param name="DeviceId">商户端设备号</param>
    /// <param name="StoreInfo">商户门店信息</param>
    public record OrderSceneInfo(
        [property: JsonPropertyName("payer_client_ip")] string PayerClientIp,
        [property: JsonPropertyName("device_id")] string? DeviceId = null,
        [property: JsonPropertyName("store_info")] OrderSceneInfo.OrderStoreInfo? StoreInfo = null)
    {
        /// <summary>
        /// 商户门店信息
        /// </summary>
        /// <param name="Id">门店编号</param>
        /// <param name="Name">门店名称</param>
        /// <param name="AreaCode">地区编码</param>
        /// <param name="Address">详细地址</param>
        public record OrderStoreInfo(
            [property: JsonPropertyName("id")] string Id,
            [property: JsonPropertyName("name")] string? Name = null,
            [property: JsonPropertyName("area_code")] string? AreaCode = null,
            [property: JsonPropertyName("address")] string? Address = null);
    }

    /// <summary>
    /// 结算信息
    /// </summary>
    /// <param name="ProfitSharing">是否指定分账</param>
    public record OrderSettleInfo(bool ProfitSharing);
}