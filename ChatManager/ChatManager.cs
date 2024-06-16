using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using Helpers = ChatManager.Utils.Helpers;

namespace ChatManager;

[MinimumApiVersion(115)]
public sealed class ChatManager : BasePlugin, IPluginConfig<Config>
{

    public override string ModuleName => "ChatFilter";
    public override string ModuleAuthor => "yachty";
    public override string ModuleVersion => "1.0.0";
    private int ModuleConfigVersion => 2;
    public required Config Config { get; set; }
    public static string? _moduleDirectory;
    public static Config? _config;
    
    public override async void Load(bool hotReload)
    {
        
        base.Load(hotReload);
        Console.WriteLine("            Chat filter                       ");
        Console.WriteLine("			>> Version: " + ModuleVersion);
        Console.WriteLine("			>> Author: " + ModuleAuthor);
        Console.WriteLine(" ");
        
        _moduleDirectory = ModuleDirectory;
        
        AddCommandListener("say", Events.OnPlayerChat.Run);
        AddCommandListener("say_team", Events.OnPlayerTeamChat.Run);

    }
    
    public void OnConfigParsed(Config config)
    {

        if (config.Version < ModuleConfigVersion)
        {
            Console.WriteLine($"[ChatManager] You are using an old configuration file. Version you are using:{config.Version} - New Version: {ModuleConfigVersion}");
        }

        Config = config;
        _config = config;

    }

}