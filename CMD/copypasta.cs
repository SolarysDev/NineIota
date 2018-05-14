using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ZeroTwo.CMD
{
    public class CopyPasta : ModuleBase<SocketCommandContext>
    {
        [Command("work")]
        public async Task WorkPasta(string lang1 = null, string lang2 = null, string lang3 = null, string lang4 = null)
        {
            if(lang1 == null || lang2 == null || lang3 == null || lang4 == null)
            {
                await ReplyAsync("```\n>>work <language 1> <language 2> <language 3> <language 4>\n```");
                return;
            }

            await ReplyAsync($"My work is {lang1} development, sometimes {lang2}, sometimes {lang3}, sometimes {lang4} - mostly {lang1} tho");
        }
    }
}