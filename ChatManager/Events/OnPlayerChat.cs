﻿using ChatManager.Utils;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;

namespace ChatManager.Events;

public class OnPlayerChat
{
    
    public static HookResult Run(CCSPlayerController? player, CommandInfo info)
    {

        string message = info.GetArg(1);
        
        if (player == null || !player.IsValid || player.IsBot) return HookResult.Handled;
        if (string.IsNullOrEmpty(message)) return HookResult.Handled;

        string steamId = new SteamID(player.SteamID).SteamId64.ToString();
        
        string deadStatus = "";
        string playerTeam = "";
        string playerName = player.PlayerName;

        if (ChatManager._config != null && ChatManager._config.GeneralSettings.AdBlockingOnChatAndPlayerNames)
        {
            playerName = Utils.Helpers.FilterAds(playerName);
        }
            
        if (ChatManager._config != null && ChatManager._config.GeneralSettings.BlockBannedWordsInChat)
        {
            message = Utils.Helpers.FilterAds(Utils.Helpers.ReplaceBannedWords(message));
        }
        
        if (ChatManager._config != null && ChatManager._config.GeneralSettings.LoggingMessages)
        {
            Task.Run(() =>
            {
                Server.NextFrame(() => Discord.Send(player, message, "Message"));
            });  
        }
        
        return HookResult.Continue;

    }
    
}