using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace GreenSharp
{
    class Program
    {
        public static DiscordClient _discord;

        private static void Main(String[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        

        private static async Task MainAsync(String[] args)
        {
            try
            {
                
                
                _discord = new DiscordClient(new DiscordConfiguration
                {
                    Token = Environment.GetEnvironmentVariable("BOT_TOKEN"),
                    TokenType = TokenType.Bot
                })!;

                _discord.Ready += async eventArgs =>
                {
                    await eventArgs.Client.UpdateStatusAsync(new DiscordActivity("g!yardım yazmanı", ActivityType.ListeningTo));
                };

            }
            catch (Exception ex) {
              Console.WriteLine(ex);  
            }
            
            var guild = _discord.GetGuildAsync(752609702273089556).Result;
            var channel = guild.GetChannel(755667599706161214);
            var messages = channel.GetMessagesAsync();    
            if (messages.Result.Any())
                await channel.DeleteMessagesAsync(messages.Result);
           
            await _discord.ConnectAsync();
            new CommandHandler(_discord);
            
            await Task.Delay(-1);
            
        }
        
        
    }
}