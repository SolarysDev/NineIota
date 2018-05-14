using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using GiphyDotNet.Manager;
using GiphyDotNet.Model;
using GiphyDotNet.Model.Parameters;
using GiphyDotNet.Tools;

namespace ZeroTwo.CMD
{
    public class Meme : ModuleBase<SocketCommandContext>
    {
        [Command("template")]
        public async Task Imgflip()
        {
            string json;
            using(var client = new WebClient())
            {
                json = client.DownloadString("https://api.imgflip.com/get_memes");
            }
            var r = new System.Random();
            var meme = (JsonConvert.DeserializeObject<dynamic>(json)).data.memes[r.Next(0, 76)].url.ToString();

            var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithImageUrl(meme);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("giphy")]
        public async Task Giphy([Remainder]string tag = null)
        {
            if(tag == null)
            {
                await ReplyAsync("You need to give me a tag!");
                return;
            }

            var GClient = new Giphy(Config.bot.Giphy);

            var gif = await GClient.RandomGif(new RandomParameter() {Tag = tag});

            var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithImageUrl(gif.Data.ImageUrl);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("prince")]
        public async Task PrinceMeme()
        {
            var embed = new EmbedBuilder()
                .WithTitle("this picture is approved!")
                .WithImageUrl("https://gyazo.com/c552bb144e7a28f0ffc78a58d9e3a43a");
            await ReplyAsync("", false, embed.Build());

        }
    }
}