using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using GreenSharp.Attributes;
using GreenSharp.Contexts;
using GreenSharp.Database;

namespace GreenSharp.Commands
{
    public class Yardım
    {
        [Command("yardım","Yardım alırsınız.")]
        public static async Task Yardim(MRCommandCtx ctx)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblyTypes = currentAssembly.GetTypes();
            var tmp1 = assemblyTypes
                .Select(type => type.GetMethods());
            var methods = new List<MethodInfo>();
            foreach (var methodInfos in tmp1)
            {
                methods.AddRange(methodInfos);
            }
            var dict = methods.Where(method => method.GetCustomAttributes().Any(attr => attr is CommandAttribute));
            var embedBuilder = new DiscordEmbedBuilder();
            embedBuilder
                .WithTitle("Yardım")
                .WithColor(DiscordColor.Blue);
            String description;
            var descriptionBuilder = new StringBuilder();
            var prefix = $"{(BotConfig.New().GetGuildAsync((Int64) ctx.Channel.GuildId).Result == null ? "g!" : BotConfig.New().GetGuildAsync((Int64) ctx.Channel.GuildId).Result!.CustomPrefix ?? "g!")}";
            descriptionBuilder.AppendLine("Prefix: " + prefix);
            descriptionBuilder.Append("\n");
            foreach (var methodInfo in dict)
            {
                var attributes =
                    (CommandAttribute) methodInfo.GetCustomAttributes()
                        .First(attribute => attribute is CommandAttribute);
                
                description = attributes.Description ?? "*Açıklama yok.*";
                String alias = attributes.Alias ?? methodInfo.Name;
                descriptionBuilder.AppendLine($"**{alias}** :: {description}");
                
            }
            description = descriptionBuilder.ToString();
            embedBuilder.WithDescription(description);
            var builder = embedBuilder.Build();
            await ctx.ReplyAsync(embed: builder);
        }
    }
}