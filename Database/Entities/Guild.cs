using System;
using SQLite;

namespace GreenSharp.Database.Entities
{
    [Table("guilds")]
    public class Guild
    {
        [PrimaryKey]
        [Unique]
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("custom_prefix")]
        public String? CustomPrefix { get; set; }
        
        
    }
}