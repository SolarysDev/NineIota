using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ZeroTwo.CMD
{
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Command("whereami")]
        public async Task ServerInfo()
        {
            var Guild = Context.Guild;
            var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithTitle($"Welcome to {Guild.Name}")
                .WithThumbnailUrl(Guild.IconUrl)
                .AddField("Member Count", Guild.MemberCount)
                .AddField("Owner", $"{Guild.Owner.Username}#{Guild.Owner.Discriminator} ({Guild.Owner.Mention})")
                .AddField("Verification Level", Guild.VerificationLevel)
                .AddField("Region", Guild.VoiceRegionId)
                .AddField("Creation Date", Guild.CreatedAt)
                .AddField("Join Date", Guild.GetUser(Context.User.Id).JoinedAt);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("botinfo")]
        public async Task BotInfo()
        {
            var embed = new EmbedBuilder()
                .WithTitle($"\"Oh, I get it. You're a pervert!\"")
                .WithDescription("I'm a badass, fanservice-ridden waifu with a giant robot! Seriously, could you want for more!? \nIf you like using this bot, please [vote here](https://discordbots.org/bot/424445724348907520?) to help me reach every server in sight!")
                .WithThumbnailUrl("https://pbs.twimg.com/media/DV3RLO-X0AAnhX-.jpg")
                .WithColor(new Color(255, 0, 0))
                .AddField("Creator", "Solarys#0556")
                .AddField("Server Count", Context.Client.Guilds.Count)
                .AddField("Framework and Language", "Discord.NET, with .NET Core and C#")
                .AddField("Support Server Link", "https://discord.gg/sJMCW8n", true)
                .AddField("Repository Link", "not yet!", true)
                .WithFooter("wheee!");
            
            await ReplyAsync("", false, embed.Build());
        }
    }
}