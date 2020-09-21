using System.Threading.Tasks;
using DSharpPlus.Entities;
using GreenSharp.Attributes;
using GreenSharp.Contexts;

namespace GreenSharp.Commands
{
    public class PizzaYe
    {
        [Command("lahmacunye", "Lahmacun yersiniz.")]
        public static async Task LahmacunYe(MRCommandCtx ctx)
        {
            await ctx.ReplyAsync(embed: new DiscordEmbedBuilder()
            {
                Color = Optional.FromValue(DiscordColor.Red),
                Title = "Lahmacun Yediniz",
                ImageUrl = "https://image.shutterstock.com/image-photo/turkish-dishes-lahmacun-pizzas-lemon-600w-630271100.jpg"
            });
        }
        
        [Command("pizzaye", "Pizza yersiniz")]
        public static async Task PizzaYeA(MRCommandCtx ctx)
        {
            await ctx.ReplyAsync(embed: new DiscordEmbedBuilder()
            {
                Color = Optional.FromValue(DiscordColor.Red),
                Title = "Pizza Yediniz",
                ImageUrl = "https://cdn.ye-mek.net/App_UI/Img/out/650/2020/04/karisik-pizza-resimli-yemek-tarifi(20).jpg?w=650&h=487"
            });
        }
        
        [Command("tantuniye", "Tantuni ye")]
        public static async Task TantuniYe(MRCommandCtx ctx)
        {
            await ctx.ReplyAsync(embed: new DiscordEmbedBuilder
            {
                Color = Optional.FromValue(DiscordColor.Red),
                Title = "Tantuni Yediniz",
                ImageUrl = "https://i4.hurimg.com/i/hurriyet/75/750x422/5de50d0e7af50728c0967485.jpg"
            });
        }

        [Command("sigaraiç", "Sigara içersiniz.")]
        public static async Task SigaraIc(MRCommandCtx ctx) =>
            await ctx.ReplyAsync(embed: new DiscordEmbedBuilder
                {Title = "günah",Description = "verdiğin paraya yazk"});
        

        [Command("dustikioyna", "Dust 2 oynarsınız.")]
        public static async Task Dust2(MRCommandCtx ctx)
        {
            await ctx.ReplyAsync("Dust 2 trash bruh");
        }
        
        
    }
}