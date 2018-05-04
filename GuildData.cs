using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace ZeroTwo
{
    public static class GuildData
    {
        public static List<GuildConstruct> frick;

        public struct GuildConstruct
        {
            public ulong ID;
            public string WelcomeMessage;
        }
    }
}