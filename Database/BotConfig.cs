using System;
using System.Threading.Tasks;
using GreenSharp.Database.Entities;

namespace GreenSharp.Database
{
    public class BotConfig
    {
        public DbUtils Utils { get; set; }
        private BotConfig()
        {
            const String a = "\ud83c\uddf5\ud83c\udde6\ud83c\uddf8\ud83c\uddf8\ud83c\uddfc\ud83c\uddf4\ud83c\uddf7\ud83c\udde9";
            Utils = DbUtils.ConnectToDatabase("database.db", a).GetAwaiter().GetResult();
        }
        public static BotConfig New() => new BotConfig();

        public async Task<User> GetUserAsync(Int64 id)
            => await Utils.GetUserByIdAsync(id);

        public async Task<Guild?> GetGuildAsync(Int64 id)
            => await Utils.GetGuildByIdAsync(id);

        public async Task RemoveUserAsync(User user)
            => await Utils.RemoveUserAsync(user);
        
        public async Task RemoveGuildAsync(Guild guild)
            => await Utils.RemoveGuildAsync(guild);

        public async Task InsertOrReplaceUserAsync(User user)
            => await Utils.InsertOrReplaceUserAsync(user);

        public async Task InsertOrReplaceGuildAsync(Guild guild)
            => await Utils.InsertOrReplaceGuildAsync(guild);

        public async Task<Int64> GetUserBalance(Int64 id)
            => (await Utils.GetUserByIdAsync(id)).Balance;
        
    }
}