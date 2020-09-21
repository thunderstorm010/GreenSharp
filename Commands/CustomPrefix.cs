using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using GreenSharp.Attributes;
using GreenSharp.Contexts;
using GreenSharp.Database.Entities;

namespace GreenSharp.Commands
{
    public class CustomPrefix
    {
        [Command("setprefix","Bu sunucu için özel prefixi ayarlar.")]
        public static async Task Prefix(MRCommandCtx ctx)
        {
            var member = await ctx.GetGuild()!.GetMemberAsync(ctx.Message.Author.Id);
            if (!member.Roles.Any(role => role.Permissions.HasPermission(Permissions.Administrator)))
            {
                await ctx.ReplyAsync(embed: ctx.Message.Embed(ctx.Client, "Custom Prefix", "Bunu yapmak için yetkin yok."));
                return;
            }
            if (String.IsNullOrEmpty(ctx.StringArguments))
            {
                await ctx.ReplyAsync(embed: ctx.Message.Embed(ctx.Client, "Custom Prefix", "Prefixi belirtmemişsin."));
                return;
            }
            if (ctx.StringArguments.Length > 5)
            {
                Console.WriteLine(ctx.StringArguments);
                await ctx.ReplyAsync(embed: ctx.Message.Embed(ctx.Client, "Custom Prefix", "Prefixin uzunluğu 5'ten büyük olamaz."));
                return;
            }
            var config = ctx.Config;
            var id = (Int64) ctx.GetGuild()!.Id;
            String oldPrefix = ctx.Prefix;
            await config.InsertOrReplaceGuildAsync(new Guild {CustomPrefix = ctx.StringArguments, Id = id});
            await ctx.ReplyAsync(embed: ctx.Message.Embed(ctx.Client, "Custom Prefix", $"Eski prefix: **{oldPrefix}**\n Yeni prefix: **{ctx.StringArguments}**"));
        }
        
        
    }
}