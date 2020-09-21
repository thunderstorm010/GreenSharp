namespace GreenSharp { public static class EmbedHelper { public static DSharpPlus.Entities.DiscordEmbed Embed(this DSharpPlus.Entities.DiscordMessage msg,DSharpPlus.DiscordClient client,System.String title,System.String description)=>new DSharpPlus.Entities.DiscordEmbedBuilder{Title=title,Description=description,Author=new DSharpPlus.Entities.DiscordEmbedBuilder.EmbedAuthor{Name=$"{msg.Author.Username}#{msg.Author.Discriminator}, ",IconUrl=msg.Author.GetAvatarUrl(DSharpPlus.ImageFormat.Auto,2048)},Footer=new DSharpPlus.Entities.DiscordEmbedBuilder.EmbedFooter{IconUrl=client.CurrentUser.GetAvatarUrl(DSharpPlus.ImageFormat.Auto,2048),Text=$"Bot: {client.CurrentUser.Username}"}};public static System.String BetterMention(this DSharpPlus.Entities.DiscordUser user) => $"<@{user.Id}>"; } }