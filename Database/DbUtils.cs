using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GreenSharp.Database.Entities;
using SQLite;

namespace GreenSharp.Database
{
    public class DbUtils
    {
        public SQLiteAsyncConnection Connection { get; set; }

        private DbUtils()
        {}

        public DbUtils(SQLiteAsyncConnection connection)
        {
            Connection = connection;
        }

        public static async Task<DbUtils> ConnectToDatabase(String filename, String key)
        {
            var dbUtils = new DbUtils
            {
                Connection = new SQLiteAsyncConnection(new SQLiteConnectionString(filename, default, key: key))
            };
            await dbUtils.CreateTablesAsync();
            return dbUtils;
        }

        public async Task CreateTablesAsync()
        {
            var infos0 = await Connection.GetTableInfoAsync("guilds");
            var infos1 = await Connection.GetTableInfoAsync("users");
            if (infos1.Count.Equals(0))
            {
                await Connection.CreateTableAsync<User>();
            }

            if (infos0.Count.Equals(0))
            {
                await Connection.CreateTableAsync<Guild>();
            }
        }

        public async Task<User> GetUserByIdAsync(Int64 id)
            => await Connection.Table<User>().FirstWhereAsync(user => user.Id.Equals(id));

        public async Task<Guild?> GetGuildByIdAsync(Int64 id)
            => await Connection.Table<Guild>().FirstWhereAsync(guild => guild.Id.Equals(id));

        public async Task InsertOrReplaceUserAsync(User user)
            => await Connection.InsertOrReplaceAsync(user);

        public async Task InsertOrReplaceGuildAsync(Guild guild)
            => await Connection.InsertOrReplaceAsync(guild);

        public async Task RemoveUserAsync(User user)
            => await Connection.Table<User>().DeleteAsync(user1 => user.Id.Equals(user1.Id));

        public async Task RemoveGuildAsync(Guild guild)
            => await Connection.Table<Guild>().DeleteAsync(guild1 => guild.Id.Equals(guild1.Id));
    }
    public static class Extensions
    {
        public static async Task<T> FirstWhereAsync<T>(this AsyncTableQuery<T> query,
            Expression<Func<T, Boolean>> expr)
            where T : new()
        {
            return await query.Where(expr).FirstOrDefaultAsync();
        }
    }
}


