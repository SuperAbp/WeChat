namespace SuperAbp.WeChat.MiniProgram.Account;

public interface IWeChatMiniProgramAccountClient
{
    Task<string?> CodeToSessionAsync(string code);
}