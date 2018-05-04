using System;
using System.Threading.Tasks;
using System.Net;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace ZeroTwo.CMD
{
    public class Osu : ModuleBase<SocketCommandContext>
    {
        [Command("osu")]
        public async Task OsuGetuser(string player = "")
        {

            if(player == "")
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("osu! Command usage")
                    .WithDescription("`>>osu <player>` \n¯\\_(ツ)_/¯");

                await ReplyAsync("", false, embed.Build());
                return;
            }


            string json = "";
            using(WebClient client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(new Uri($"https://osu.ppy.sh/api/get_user?k={Config.bot.osukey}&u={player}"));
            }
            
            try
            {
                var Player = (JsonConvert.DeserializeObject<dynamic>(json))[0];

                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithThumbnailUrl(String.Format("https://a.ppy.sh/{0}", Player.user_id.ToString()))
                    .WithTitle($"Player stats for {Player.username.ToString()}")
                    .AddField("Rank", $"#{Player.pp_rank.ToString()} (#{Player.pp_country_rank.ToString()} {Player.country.ToString()})")
                    .AddField("Scores", $"{Player.count_rank_ss.ToString()} ***SS***, {Player.count_rank_s.ToString()} **S**, {Player.count_rank_a.ToString()} *A*, {Player.pp_raw.ToString()} **pp**")
                    .AddField("Misc.", $"Level {Player.level.ToString().Substring(0, 2)}, {Player.accuracy.ToString().Substring(0, 5)}% Hit Accuracy, {Player.playcount.ToString()} recorded Plays");
                await ReplyAsync("", false, embed.Build());
            }
            catch
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("FAILED")
                    .WithThumbnailUrl("http://images.clipartpanda.com/lemon-clip-art-nicubunu_Lemon.png")
                    .WithDescription("The osu! API returned a lemon.");
                await ReplyAsync("", false, embed.Build());
            }

            
        }
    }
}