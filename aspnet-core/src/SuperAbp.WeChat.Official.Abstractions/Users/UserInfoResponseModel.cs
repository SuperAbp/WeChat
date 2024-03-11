using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Official.Users;

public class UserInfoResponseModel
{
    [JsonPropertyName("subscribe")]
    public virtual long Subscribe { get; set; }

    /// <summary>
    /// 微信用户在此场景下的唯一Id
    /// </summary>
    [JsonPropertyName("openid")]
    public virtual string? OpenId { get; set; }

    [JsonPropertyName("language")]
    public virtual string? Language { get; set; }

    [JsonPropertyName("subscribe_time")]
    public virtual long SubscribeTime { get; set; }

    /// <summary>
    /// 微信用户唯一Id
    /// </summary>
    [JsonPropertyName("unionid")]
    public virtual string? UnionId { get; set; }

    [JsonPropertyName("remark")]
    public virtual string? Remark { get; set; }

    [JsonPropertyName("groupid")]
    public virtual long Groupid { get; set; }

    [JsonPropertyName("tagid_list")]
    public virtual long[]? TagidList { get; set; }

    [JsonPropertyName("subscribe_scene")]
    public virtual string? SubscribeScene { get; set; }

    [JsonPropertyName("qr_scene")]
    public virtual long QrScene { get; set; }

    [JsonPropertyName("qr_scene_str")]
    public virtual string? QrSceneStr { get; set; }
}