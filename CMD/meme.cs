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
                .WithColor(new Color(255, 0, 0))
                .WithImageUrl("https://did-you-know-that-the-last.letters-of-the-alphabet-are.xyz/lKIdrkPW.png");
            await ReplyAsync("", false, embed.Build());

        }

        [Command("asktrump")]
        public async Task AskTrump()
        {
            string json;
            using (var client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync("https://api.whatdoestrumpthink.com/api/v1/quotes");
            }

            var responses = JsonConvert.DeserializeObject<dynamic>(json).messages.personalized;
            var r = new Random();

            var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithTitle("What does Trump Think?")
                .WithDescription($"{Context.User.Mention} {responses[r.Next(0, 572)].ToString()}")
                .WithImageUrl("http://coldfrontmag.com/wp-content/uploads/2016/11/Angry-Trump.jpg")
                .Build();
            await ReplyAsync("", false, embed);
        }
    }
}