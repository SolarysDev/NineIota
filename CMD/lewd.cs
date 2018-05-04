using System;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using DiscordBotsList.Api;

namespace ZeroTwo.CMD
{
    public class Lewd : ModuleBase<SocketCommandContext>
    {
        [Command("neko")]
        public async Task NekosLife(string tag = "-h")
        {
            if(Context.Channel is ITextChannel t)
            {
                if(!t.IsNsfw)
                {
                    await ReplyAsync("keep the lewds in the nsfw channels!");
                    return;
                }

                if(tag == "-h")
                {
                    var failembed = new EmbedBuilder()
                        .WithColor(new Color(255, 0, 0))
                        .WithTitle("Valid Tags (pick one)")
                        .WithDescription("`cum, les, meow, tickle, lewd, feed, bj, nsfw_neko_gif, poke, anal, slap, pussy, lizard, classic, kuni, pat, kiss, neko, cuddle, fox_girl, boobs, Random_hentai_gif, hug`");
                    
                    await ReplyAsync("", false, failembed.Build());
                    return;
                }
                string json;
                using(var client = new WebClient())
                {
                    json = await client.DownloadStringTaskAsync(new Uri($"https://nekos.life/api/v2/img/{tag}"));
                }
                
                try
                {
                    var lewd = JsonConvert.DeserializeObject<dynamic>(json);

                    var embed = new EmbedBuilder()
                        .WithColor(new Color(255, 0, 0))
                        .WithImageUrl(lewd.url.ToString());

                    await ReplyAsync("", false, embed.Build());
                }
                catch
                {
                    var err = new EmbedBuilder()
                        .WithColor(new Color(255, 0, 0))
                        .WithTitle("FAILED")
                        .WithDescription("Either the api is down or you used an invalid tag. Run the command again without a tag to get the list of valid ones.");

                    await ReplyAsync("", false, err.Build());
                }
            }
        }
    }
}