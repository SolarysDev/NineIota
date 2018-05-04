using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ZeroTwo.CMD
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("warn")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Warn(IGuildUser target = null, [Remainder]string reason = "you've been a bad boy")
        {
            if(target == null)
            {
                await ReplyAsync("Wait, who am I warning again?");
                return;
            }

            await target.SendMessageAsync($"Warned by **{Context.User.Username}#{Context.User.Discriminator}**. Reason: {reason}");
            await ReplyAsync($"{target.Username}#{target.Discriminator} has been warned.");
        }

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(IGuildUser target = null, [Remainder]string reason = "you've been a bad boy")
        {
            if(target == null)
            {
                await ReplyAsync("Kicking thin air is cool.");
                return;
            }

            try
            {
                await target.KickAsync(reason);
                await ReplyAsync("*and another one bites the dust*");
            }
            catch
            {
                await ReplyAsync("Shit... Check role hierarchy.");
            }
        }

        [Command("ban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(IGuildUser target = null, [Remainder]string reason = "you've been a REALLY bad boy")
        {
            if(target == null)
            {
                await ReplyAsync("Do we really want to drop the hammer on this ant, sir?");
                return;
            }

            try
            {
                await Context.Guild.AddBanAsync(target, 3, reason);
                await ReplyAsync("DEAD!");
            }
            catch
            {
                await ReplyAsync("Shit... Check role hierarchy.");
            }
        }

        [Command("softban")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Softban(IGuildUser target = null, [Remainder]string reason = "you've been a bad boy")
        {
            if(target == null)
            {
                await ReplyAsync("one sec while i softban this speck of dust");
                return;
            }
            
            try
            {
                await Context.Guild.AddBanAsync(target, 1);
                await Context.Guild.RemoveBanAsync(target);
                await ReplyAsync("at least it's not a ban :3");
            }
            catch
            {
                await ReplyAsync("Shit. Check role hierarchy.");
            }
        }

        [Command("lock")]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task ChannelLockdown()
        {
            try
            {
                var channel = Context.Guild.GetTextChannel(Context.Channel.Id);
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                await ReplyAsync(":lock:");
            }
            catch
            {
                await ReplyAsync("failed");
            }
        }

        [Command("unlock")]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task ChannelUnlock()
        {
            try
            {
                var channel = Context.Guild.GetTextChannel(Context.Channel.Id);
                await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Inherit));
                await ReplyAsync(":key:");
            }
            catch
            {
                await ReplyAsync("failed");
            }
        }

        [Command("prune")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task MassDelete(uint count)
        {
            if(count == 0)
            {
                await ReplyAsync("I'm deleting... *nothing?*");
                return;
            }

            await Context.Message.DeleteAsync();
            var deleteCount = await Context.Channel.GetMessagesAsync((int)count).FlattenAsync();

            if(Context.Channel is ITextChannel text)
            {
                await text.DeleteMessagesAsync(deleteCount);
            } else return;
        }
    }
}