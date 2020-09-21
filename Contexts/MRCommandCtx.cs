using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using GreenSharp.Database;

namespace GreenSharp.Contexts
{
    public class MRCommandCtx
    {
        public DiscordMessage Message { get; }

        public DiscordChannel Channel => Message.Channel;

        public DiscordClient Client { get; }
        
        public String Alias { get; }
        
        public String Prefix { get; }
        
        public BotConfig Config { get; }

        public MRCommandCtx(DiscordMessage message, DiscordClient client, String @alias, String prefix, BotConfig config)
        {
            Message = message;
            Client = client;
            Alias = alias;
            Prefix = prefix;
            Config = config;
        }

        public String? StringArguments
        {
            get
            {
                String content = Message.Content;
                if (content.StartsWith($"<@!{Client.CurrentUser.Id}>"))
                {
                    content = content.Remove(0, Client.CurrentUser.Mention.Length + 2);
                    Console.WriteLine(content);
                }
                else
                {
                    content = content.Remove(0, Prefix.Length);
                }

                return content.Length == 0 ? null : content.Remove(0, Alias.Length + 1);
            }
        }

        public async Task<DiscordMessage> ReplyAsync(String? text = null, Boolean isTts = false, DiscordEmbed? embed = null, IEnumerable<IMention> mentions = null) 
            => await Message.Channel.SendMessageAsync(text, isTts, embed, mentions);

        
        
        public async Task<String> GetPrefix()
        {
            var config = BotConfig.New();
            if (GetGuild() == null) return "g!";
            var guildId = (Int64) GetGuild().Id;
            if (await config.GetGuildAsync(guildId) == null)
                return "g!";
            return (await config.GetGuildAsync(guildId)).CustomPrefix ?? "g!";
        }

        public DiscordGuild? GetGuild() => Message.Channel.Guild ?? null;
    }
}