using Lambda;

using Discord.Commands;
using Discord;

using System;
using System.Threading.Tasks;

using ZeroTwo.Common;

namespace ZeroTwo.Modules
{
    public class InfoMofule : LambdaModule
    {
        [Command("help")]
        [Alias("commands", "?")]
        public async Task HelpCommand()
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Zero)
                .WithTitle("commands!")
                .WithDescription("having any issues? [get help here!](https://discord.gg/sJMCW8n)")
                .AddField("Exposition Dump", "`help` - Gets you this DM. \n`info` - Hear my life story.");
            await Context.User.SendMessageAsync("", false, embed.Build());
        }

        [Command("info")]
        public async Task BotInfo()
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Zero)
                .WithTitle("君今が僕のダーリンだ!")
                .WithDescription(
                    "I've got best girl status, an amazing bust, and a giant robot! Could you ask for more?")
                .AddField("Server Count", Context.Client.Guilds.Count, true)
                .AddField("Built on...",
                    ".NET Core 2.0, C#, Discord.Net + the [Lambda Framework](https://github.com/SolarysDev/Lambda)",
                    true)
                .AddField("Support Server Link", "[here you go!](https://discord.gg/sJMCW8n)")
                .AddField("Github Repository", "[boop!](https://github.com/SolarysDev/NineIota/tree/rewrite)");
            await ReplyAsync("", false, embed.Build());
        }
    }
}
