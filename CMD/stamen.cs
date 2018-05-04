using System;
using System.Threading.Tasks;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotsList.Api;
using System.Diagnostics;

namespace ZeroTwo.CMD
{
    public class Stamen : ModuleBase<SocketCommandContext>
    {
        [Command("stamen")]
        public async Task StamenHandler(string action = "", string args = "")
        {
            int CheckResult = Array.IndexOf(Stamens.List, Context.User.Id.ToString());

            if(CheckResult == -1)
            {
                await ReplyAsync("You don't have access to Stamen actions. DM Solarys#0556 if you think this is a mistake.");
                return;
            }

            switch(action)
            {
                case "list":
                    string StamenList = ListAllStamens();
                    await ReplyAsync($"List of all Stamens (by userID) \n```\n{StamenList}\n```");
                    break;

                case "resolve":
                    var ResolvedOutput = ResolveByID(Convert.ToUInt64(args)).Build();
                    await ReplyAsync("", false, ResolvedOutput);
                    break;

                case "push":
                    DBLPush();
                    break;

		        case "ram":
			        RAM();
			        break;

                case "member":
                    var m = MemberCount();
                    await ReplyAsync($"Serving {m} members.");
                    break;

                case "save":
                    StarSave();
                    await ReplyAsync("saved!");
                    break;

                default:
                    await ReplyAsync("Invalid action");
                    break;
            }
            
        }

        private string ListAllStamens()
        {
            string listed = "";
            foreach(string stamen in Stamens.List)
            {
                listed += String.Format("\n{0}", stamen);
            }
            return listed;
        }
        
        private EmbedBuilder ResolveByID(ulong ID)
        {
            try
            {
                var user = Context.Client.GetUser(ID);
                return new EmbedBuilder()
                            .WithColor(new Color(255, 0, 0))
                            .WithTitle($"Resolved information for **{user.Username}#{user.Discriminator}**")
                            .WithThumbnailUrl(user.GetAvatarUrl())
                            .AddField("ID", user.Id)
                            .AddField("Creation Date", user.CreatedAt)
                            .AddField("Account Type", user.IsBot ? "Bot" : "User")
                            .AddField("Current Activity", user.Activity == null? "Idle" : $"{user.Activity}");
            }
            catch
            {
                return new EmbedBuilder()
                .WithColor(new Color(255, 0, 0))
                .WithTitle("FAILED")
                .WithDescription("Provided ID was invalid or isn't in any of my servers.");
            }
        }

        private async void DBLPush()
        {
            var DBLClient = new AuthDiscordBotListApi(Context.Client.CurrentUser.Id, Config.bot.DBLKey);
            await DBLClient.UpdateStats(Context.Client.Guilds.Count);
            await ReplyAsync("DBL stats successfully pushed.");
        }

	    private async void RAM()
	    {
		    var RamUsage = Process.GetCurrentProcess().PrivateMemorySize64;

		    await ReplyAsync($"Current ram usage is {RamUsage / 1048576}MB.");
	    }

        private int MemberCount()
        {
            int members = 0;
            foreach(SocketGuild g in Context.Client.Guilds)
            {
                members += g.MemberCount;
            }
            return members;
        }

        private void StarSave()
        {
            Starboard.SaveState();
        }
    }
}