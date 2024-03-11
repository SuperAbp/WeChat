﻿namespace SuperAbp.WeChat.Official;

public class WeChatOfficialOptions
{
    /// <summary>
    /// AppId
    /// </summary>
    public string ApplicationId { get; set; } = default!;

    public string Secret { get; set; } = default!;

    public string Url { get; set; } = default!;
}