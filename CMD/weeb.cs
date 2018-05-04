using System;
using System.Threading.Tasks;
using System.Net.Http;
using Discord;
using Discord.Commands;
using MALAPI;

namespace ZeroTwo.CMD
{
    public class Weeb : ModuleBase<SocketCommandContext>
    {
        [Command("mal")]
        public async Task MyAnimeList_GetShow([Remainder]string query = "")
        {
            if(query == "")
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("MyAnimeList - Search Anime")
                    .WithDescription("`>>mal <anime name>` \n¯\\_(ツ)_/¯");
                await ReplyAsync("", false, embed.Build());
                return;
            }

            try
            {
                var mal = new API(Config.bot.malUser, Config.bot.malKey);
                var output = await mal.SearchController.SearchForAnimeAsync(query);
                
                var anime = output.Entries[0];

                var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithTitle($"{anime.EnglishTitle}")
                .WithThumbnailUrl(anime.ImageLink)
                .WithDescription(anime.Synopsis.Replace("<br />", null).Replace("&quot;", null).Replace("&#039;", "'").Replace("[i]", "*").Replace("[/i]", "*").Replace("&mdash;", " -- ").Replace("&Ouml;", "O").Replace("&Ouml;", "O").Replace("&rsquo;", "'"))
                .AddField("Episode Count", anime.EpisodesCount)
                .AddField("Airing Dates", String.Format("Started: {0}, {1}", anime.StartDateStr, anime.EndDate.Ticks == 0 ? "Still Airing" : $"Ended: {anime.EndDateStr}"))
                .AddField("MAL Score", anime.Score)
                .WithFooter("If this isn't what you were looking for, use the Japanese title.");

                await ReplyAsync("", false, embed.Build());
            }
            catch
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("HECK!")
                    .WithDescription("MyAnimeList returned a lemon.")
                    .WithThumbnailUrl("http://images.clipartpanda.com/lemon-clip-art-nicubunu_Lemon.png");

                await ReplyAsync("", false, embed.Build());
            }
        }

        [Command("manga")]
        public async Task MyAnimeList_GetManga([Remainder]string query = "")
        {
            if(query == "")
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("MyAnimeList - Search Manga")
                    .WithDescription("`>>manga <manga name>` \n¯\\_(ツ)_/¯");
                await ReplyAsync("", false, embed.Build());

                return;
            }

            try
            {
                var mml = new API(Config.bot.malUser, Config.bot.malKey);
                var output = await mml.SearchController.SearchForMangaAsync(query);
                var manga = output.Entries[0];

                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithThumbnailUrl(manga.ImageLink)
                    .WithTitle(manga.EnglishTitle)
                    .WithDescription(manga.Synopsis.Replace("<br />", null).Replace("&quot;", null).Replace("&#039;", "'").Replace("[i]", "*").Replace("[/i]", "*").Replace("&mdash;", " -- ").Replace("&Ouml;", "O").Replace("&rsquo;", "'"))
                    .AddField("Current Length", $"{manga.Volumes} Volumes, {manga.Chapters} Chapters")
                    .AddField("Printing Dates", String.Format("Started: {0}, {1}", manga.StartDateStr, manga.EndDate.Ticks == 0 ? "Still Running" : $"Ended/Pulled {manga.EndDateStr}"))
                    .AddField("MAL Score", manga.Score)
                    .WithFooter("If this isn't what you were looking for, use the Japanese title.");
                await ReplyAsync("", false, embed.Build());
            }
            catch
            {
                var embed = new EmbedBuilder()
                    .WithColor(new Color(255, 0, 0))
                    .WithTitle("HECK!")
                    .WithDescription("MyAnimeList returned a lemon.")
                    .WithThumbnailUrl("http://images.clipartpanda.com/lemon-clip-art-nicubunu_Lemon.png");

                await ReplyAsync("", false, embed.Build());
            }
        }
    }
}