using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ZeroTwo.Commands
{
    public class BotHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            await ReplyAsync("No help for you. \n**psst, use >>commands**");
        }

        [Command("commands")]
        public async Task CommandList()
        {
            await ReplyAsync("Check DMs");
            await Context.User.SendMessageAsync("Alrighty, here's my command list." + 
                                                "\n------------------" +
                                                "\n`commands` - Gets you this DM." +
                                                "\n`modcommands` Gets you the list of moderation/server-management commands. You'll need to be able to view the audit log." +
                                                "\n`whereami` - Gets you info on the current server." +
                                                "\n`botinfo` - Learn my life story." +
                                                "\n`choose` - Self explanatory." +
                                                "\n`8ball` - Self explanatory." +
                                                "\n`osu` - Gets stats for the specified osu! player." +
                                                "\n`fortnite` - Gets stats for the specified Fortnite player." +
                                                "\n`mal` - Gets an anime from MyAnimeList based on the provided name" +
                                                "\n`manga` Same as above, but for manga." +
                                                "\n`doggo` - Gets you a fluffy doggo pic." +
                                                "\n`template` - A useless meme command that I left in for the memes. Get bases to make memes out of, I guess." +
                                                "\n`giphy` - Search the massive stores of Giphy's ~~normie~~ vault." +
                                                "\n`neko` - ***mmmmmmm*** (NSFW)" +
                                                "\n`work` - Generates a good copypasta from Discord Bot List" +
                                                "\n`prince` - I approve of this picture!" +
                                                "\n`asktrump` - What does Trump think?" +
                                                "\n------------------");
        }

        [Command("modcommands")]
        public async Task ModCommands()
        {
            var author = Context.Guild.GetUser(Context.User.Id);
            if(!author.GuildPermissions.Has(GuildPermission.ViewAuditLog))
            {
                await ReplyAsync("You need to be able to view the Audit Log.");
                return;
            }

            await ReplyAsync("Check your DMs");
            await Context.User.SendMessageAsync("***whoosh!***" + 
                                                "\n**Moderation Commands** (You will be ignored if you/I don't have the listed permission)" +
                                                "\n`warn` - Self explanatory. Requires (user) Kick." +
                                                "\n`kick` - Self explanatory. Requires (both) Kick." +
                                                "\n`ban` - Self explanatory. Requires (both) Ban." +
                                                "\n`softban` - Bans the user and immediately unbans them, but deletes every message they sent in the last 24 hours. Requires Kick Members (user) and Ban Members (bot)" +
                                                "\n`prune` - Mass-deletes the specified amount of messages. Requires (both) Manage Messages." +
                                                "\n`lock` - Denies \"Send Messages\" to `@everyone`. Requires (both) Manage Channels" +
                                                "\n`unlock` - Reverses the above. Also requires (both) Manage Channels." +
                                                "\n`star` - Enables Starboard for your server, in the channel you used the command in. Requires (both) Manage Channels" +
                                                "\n`unstar` - Disables Starboard for your server, and deletes the channel used for Starboard. Requires (both) Manage Channels." +
                                                "\n------------------");
        }
    }
}