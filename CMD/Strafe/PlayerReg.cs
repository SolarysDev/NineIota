using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ZeroTwo.CMD.Strafe
{
    public class PlayerReg : ModuleBase<SocketCommandContext>
    {
        [Command("reg")]
        public async Task RegisterPlayer()
        {
            if(Context.Guild.Id != 378801748749451264) return;
            if (!(Context.Channel is SocketGuildChannel c)) return;
            var user = c.Guild.GetUser(Context.User.Id);
            var role = c.Guild.GetRole(466639518565400588);
            await user.AddRoleAsync(role);

            var embed = new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithThumbnailUrl("noot")
                .WithTitle("Ready!")
                .WithDescription(
                    "You now have the competitor role! Make sure to [register for the tournament](https://docs.google.com/forms/d/e/1FAIpQLSf7GDM3r8lJMG-upTWBk5_2TsXTMsrhmWCNG8dRMdfAVT4K-Q/viewform)")
                .Build();
            await ReplyAsync("", false, embed);
        }
    }
}