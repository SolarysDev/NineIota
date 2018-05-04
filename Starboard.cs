using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Discord;
using Discord.WebSocket;
using Discord.Commands; 

namespace ZeroTwo
{
    public static class Starboard
    {
        public static List<StarEntry> GuildDir {get; set;}

        static Starboard()
        {
            if(!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if(!File.Exists("Resources/star.json"))
            {
                GuildDir = new List<StarEntry>();
                string json = JsonConvert.SerializeObject(GuildDir, Formatting.Indented);
                File.WriteAllText("Resources/star.json", json);
            }
            else
            {
                string json = File.ReadAllText("Resources/star.json");
                GuildDir = JsonConvert.DeserializeObject<List<StarEntry>>(json);
            }
        }

        public static void SaveState()
        {
            string json = JsonConvert.SerializeObject(GuildDir, Formatting.Indented);
            File.WriteAllText("Resources/star.json", json);
        }
    }

    public struct StarEntry
    {
        public ulong GuildID {get; set;}
        public ulong ChannelID {get; set;}
        public List<ulong> StarredMessages {get; set;}
    }

    public class StarboardFrontEnd : ModuleBase<SocketCommandContext>
    {
        [Command("star")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        public async Task Star()
        {
            if(Starboard.GuildDir.Exists(b => b.GuildID == Context.Guild.Id))
            {
                var f = Starboard.GuildDir.Single(ff => ff.GuildID == Context.Guild.Id);
                await ReplyAsync($"you need to >>unstar <${f.ChannelID}> first");
                return;
            }

           var o = new OverwritePermissions(sendMessages: PermValue.Deny);
           var z = new OverwritePermissions(sendMessages: PermValue.Allow);

           await Context.Guild.GetTextChannel(Context.Channel.Id).AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, o);
           await Context.Guild.GetTextChannel(Context.Channel.Id).AddPermissionOverwriteAsync(Context.Client.CurrentUser, z);

           var s = new StarEntry();
           s.GuildID = Context.Guild.Id;
           s.ChannelID = Context.Channel.Id;
           Starboard.GuildDir.Add(s);
           await ReplyAsync("starred!");
           Starboard.SaveState();
        }

        [Command("unstar")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        public async Task UnStar()
        {
            var r = Starboard.GuildDir.Single(s => s.GuildID == Context.Guild.Id);
            if(r.ChannelID != Context.Channel.Id)
            {
                await ReplyAsync($"you need to >>unstar in <#{r.ChannelID}>");
                return;
            }
            Starboard.GuildDir.Remove(r);
            await Context.Guild.GetChannel(Context.Channel.Id).DeleteAsync();
            Starboard.SaveState();
        }
    }
}