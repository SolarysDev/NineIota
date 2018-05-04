using System;
using System.Threading.Tasks;
using System.Net;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

public class Doggo : ModuleBase<SocketCommandContext>
{
    [Command("doggo")]
    public async Task DoggoCommand()
    {
        string doggo = GetDoggo();
        await ReplyAsync("", false, new EmbedBuilder()
            .WithColor(new Color(255, 0, 0))
            .WithImageUrl(doggo)
            .Build());
    }

    private static string GetDoggo()
    {
        string json;
        using(WebClient client = new WebClient())
        {
            json = client.DownloadString("https://dog.ceo/api/breeds/image/random");
        }
        string imageURL = (JsonConvert.DeserializeObject<dynamic>(json)).message.ToString();
        return imageURL;
    }
}