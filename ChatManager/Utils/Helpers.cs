using System.Text.RegularExpressions;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Entities;
using Newtonsoft.Json.Linq;

namespace ChatManager.Utils;

public class Helpers
{

    public static string setTeamName(int teamNum)
    {

        string teamName = "";

        switch (teamNum)
        {
            case 0:
                teamName = $"{ChatManager._config?.TeamTags.NoneSyntax}";
                break;
            case 1:
                teamName = $"{ChatManager._config?.TeamTags.SpecSyntax}";
                break;
            case 2:
                teamName = $"{ChatManager._config?.TeamTags.TeamTSyntax}";
                break;
            case 3:
                teamName = $"{ChatManager._config?.TeamTags.TeamCtSyntax}";
                break;
        }

        return teamName;

    }

    public static string ReplaceBannedWords(string target)
    {

        List<string>? bannedWords = ChatManager._config?.BannedWords;

        if (bannedWords != null && bannedWords.Any())
        {
            
            foreach (var bannedWord in bannedWords)
            {
                string pattern = $@"\b{Regex.Escape(bannedWord)}\b";
                if (Regex.IsMatch(target, pattern, RegexOptions.IgnoreCase))
                {
                    target = Regex.Replace(target, pattern, "****", RegexOptions.IgnoreCase);
                }
            }
            
        }

        return target;

    }

    public static string FilterAds(string target)
    {
        // Регулярные выражения для различных типов ссылок и IP-адресов
        string ipRegexPattern = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
        string generalUrlPattern = @"(?:https?:\/\/)?(?:www\.)?[\w\-]+\.[\w\-]+(?:\/\S*)?";
        string neverlosePattern = @"(?:https?:\/\/)?(?:www\.)?neverlose\.cc\/market\/item\?id=\w+";
        string marketPattern = @"(?:https?:\/\/)?(?:www\.)?market\/item\?id=\w+";
        string itemPattern = @"(?:https?:\/\/)?(?:www\.)?item\?id=\w+";
        string idPattern = @"(?:https?:\/\/)?(?:www\.)?\?id=\w+";

        // Регулярные выражения для социальных сетей (Facebook, Twitter, Instagram и т.д.)
        string socialMediaPattern = @"(?:https?:\/\/)?(?:www\.)?(?:facebook|twitter|instagram|tiktok|vk|ok|youtube|telegram|whatsapp|viber)\.com(?:\/\S*)?";

        // Регулярные выражения для попыток обхода фильтров (например, добавление пробелов, точек и подчеркиваний)
        string obfuscationPattern = @"(?:https?\s*[:.]\s*\/{2})?(?:www\.)?[\w\-]+\s*[\.:]\s*[\w\-]+(\s*[\.:]\s*[a-z]{2,})(\s*\/\S*)?";

        // Дополнительные выражения для вариаций ссылок
        string partialNeverlosePattern = @"(?:neverlose\.cc\/market\/item\?id=\w+)";
        string partialMarketPattern = @"(?:market\/item\?id=\w+)";
        string partialItemPattern = @"(?:item\?id=\w+)";
        string partialIdPattern = @"(?:\?id=\w+)";

        // Объединение всех регулярных выражений в один список
        List<string> patterns = new List<string>
    {
        ipRegexPattern,
        generalUrlPattern,
        neverlosePattern,
        marketPattern,
        itemPattern,
        idPattern,
        socialMediaPattern,
        obfuscationPattern,
        partialNeverlosePattern,
        partialMarketPattern,
        partialItemPattern,
        partialIdPattern
    };

        // Применение всех регулярных выражений к строке target
        foreach (var pattern in patterns)
        {
            target = Regex.Replace(target, pattern, "****", RegexOptions.IgnoreCase);
        }

        return target;
    }

}