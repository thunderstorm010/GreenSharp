using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using GreenSharp.Attributes;
using GreenSharp.Contexts;
using GreenSharp.Database;
using GreenSharp.Database.Entities;
using Microsoft.Extensions.Logging;

namespace GreenSharp
{
    public class CommandHandler
    {
        
        
        public readonly Dictionary<MethodInfo, (String?, Boolean)> FunctionDict =
            new Dictionary<MethodInfo, (String?, Boolean)>();
        public static DiscordClient Client { get; private set; }
        public CommandHandler(DiscordClient client)
        {
            Client = client;
            Task.Delay(150);
            HandleAllCommands();
        }
        
        
        public void HandleAllCommands()
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
            foreach (var methodInfo in dict)
            {
                if (!methodInfo.IsStatic)
                {
                    Client.Logger.Log(LogLevel.Error,  $"CommandHandler, Method {methodInfo.DeclaringType}.{methodInfo.Name} is not static. Skipping method.",DateTime.Now);
                    Client.Logger.Log(LogLevel.Warning, $"CommandHandler, Failed to load command named {methodInfo.DeclaringType}.{methodInfo.Name}", DateTime.Now);
                    continue;
                }
                var select =
                    (CommandAttribute) methodInfo.GetCustomAttributes()
                        .First(attribute => attribute is CommandAttribute);
                FunctionDict.Add(methodInfo, (select.Alias, select.AllowDMs));
                Client.Logger.LogInformation($"CommandHandler, Loaded command named {methodInfo.Name}, custom alias {select.Alias ?? "undefined"}");
                
            }
            Client.Logger.Log(LogLevel.Information, "CommandHandler, Loaded all commands.");
            Client.MessageCreated += MessageReceived;
        }
        

      
        private Task MessageReceived(MessageCreateEventArgs args)
        {
            var config = BotConfig.New();
            foreach ((var method, (String? name, Boolean isGuildCommand)) in FunctionDict)
            {
                Task.Run(async() =>
                {
                    String prefix;
                    if (!isGuildCommand)
                    {
                        if (args.Message.Channel.Guild == null) return;
                        
                        
                        var guildId = (Int64) args.Message.Channel.GuildId;
                        var asyncTableQuery = config.Utils.Connection.Table<Guild>()
                            .Where(pred => guildId == pred.Id);
                        prefix = await asyncTableQuery.FirstOrDefaultAsync() == null
                            ? "g!"
                            : (await asyncTableQuery.FirstAsync()).CustomPrefix ?? "g!";
                    }
                    else
                    {
                        prefix = "g!";
                    }
                    String alias = name ?? method.Name;
                    var ctx = new MRCommandCtx(args.Message, Client, alias, prefix, config);
                    if (args.Message.Content.StartsWith($"{prefix}{alias}") ||
                        args.Message.Content.StartsWith($"<@!{Client.CurrentUser.Id}> {alias}"))
                    {
                        try
                        {
                            var invoke = (Task) method.Invoke(null,
                                new Object?[] {ctx})!;
                            await invoke;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                });
            }

            return Task.CompletedTask;
        }
        }
        
    }
