using System;
using SQLite;

namespace GreenSharp.Database.Entities
{
    [Table(name: "users")]
    public class User
    {
        [PrimaryKey]
        [Unique] 
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("balance")] 
        public Int64 Balance { get; set; }
    }
}