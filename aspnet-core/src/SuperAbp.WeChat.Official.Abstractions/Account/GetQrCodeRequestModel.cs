using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Official.Account;

public record GetQrCodeRequestModel(
    [property: JsonPropertyName("action_info")] GetQrCodeRequestModel.Action ActionInfo,
    [property: JsonPropertyName("expire_seconds")] int ExpireSeconds = 60,
    [property: JsonPropertyName("action_name")] string ActionName = "QR_SCENE")
{
    public record Action([property: JsonPropertyName("scene")] Action.SceneModel Scene)
    {
        public record SceneModel(
            [property: JsonPropertyName("scene_id")]
            int? SceneId = null,
            [property: JsonPropertyName("scene_str")]
            string? SceneString = null);
    }
}