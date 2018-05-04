using System.Threading.Tasks;
using System.Linq;
using Discord;
using Discord.Commands;
using Fortnite.Net;

namespace ZeroTwo.CMD
{
    public class Frotnut : ModuleBase<SocketCommandContext>
    {
        [Command("fortnite")]
        public async Task Fortnite_GetPlayer(string platform, [Remainder]string player)
        {
            if(platform != "pc" && platform != "xbl" && platform != "ps4")
            {
                await ReplyAsync("The valid platform tags are [pc, xbl, ps4]");
                return;
            }

            var FClient = new FortniteClient(Config.bot.FortniteKey);

            try
            {
                var Player = FClient.GetProfile(platform, player);
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle($"Player stats for {Player.EpicUserHandle}")
                    .WithThumbnailUrl("https://i1.wp.com/mentalmars.com/wp-content/uploads/2017/07/Fortnite-Logo-Big.jpg?fit=400%2C400&ssl=1")
                    .AddField("Platform", Player.PlatformNameLong)
                    .AddField("KD", $"{Player.LifeTimeStats.KD}", true)
                    .AddField("Top 25's", Player.LifeTimeStats.Top25s, true)
                    .AddField("Top 5's", Player.LifeTimeStats.Top5s, true)
                    .AddField("Top 3's", Player.LifeTimeStats.Top3s, true)
                    .AddField("Wins | Win Percentage", $"{Player.LifeTimeStats.Wins} | {Player.LifeTimeStats.WinPercentage}", true);

                await ReplyAsync("", false, embed.Build());
            }
            catch (System.Exception e)
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("FAILED")
                    .WithThumbnailUrl("https://cdn0.woolworths.media/content/wowproductimages/large/259514.jpg")
                    .WithDescription("I got a friggin lemon!");

                await ReplyAsync("", false, embed.Build());
            }
        }
    }
}