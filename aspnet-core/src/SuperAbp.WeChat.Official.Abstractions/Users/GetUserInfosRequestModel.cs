using System.Text.Json.Serialization;

namespace SuperAbp.WeChat.Official.Users;

public record GetUserInfosRequestModel([property: JsonPropertyName("user_list")] IReadOnlyList<GetUserInfosRequestModel.UserList> Users)
{
    public record UserList(string OpenId, string Lang = "zh_CN");
}