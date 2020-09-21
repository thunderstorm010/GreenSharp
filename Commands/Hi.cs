using System;
using System.Threading.Tasks;
using GreenSharp.Attributes;
using GreenSharp.Contexts;

namespace GreenSharp.Commands
{
    public class Hi
    {
        [Command("ping")]
        public static async Task Command(MRCommandCtx args)
            => await args.ReplyAsync("Ping: " + args.Client.Ping);
    }
}