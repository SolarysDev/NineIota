using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Zero_Two.Resources;

namespace ZeroTwo.CMD
{
    public class RandomHek : ModuleBase<SocketCommandContext>
    {
        [Command("choose")]
        public async Task Choose([Remainder]string input)
        {
            string[] choices = input.Split(" ");

            if(choices.Length < 2)
            {
                await ReplyAsync("Woah there. Too many choices to comprehend.");
                return;
            }

            Random rng = new Random();
            await ReplyAsync($"I choose {choices[rng.Next(0, choices.Length)]}.");
        }

        [Command("8ball")]
        public async Task EightBall([Remainder]string question = "nani?")
        {
            Random r = new Random();
            await ReplyAsync(RandomPool.EightBall[r.Next(0, RandomPool.EightBall.Length)]);
        }
    }
}