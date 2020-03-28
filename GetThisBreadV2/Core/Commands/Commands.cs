using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;

namespace GetThisBread.Core.Commands

    //Commands for staff in the discord.
{

    public class Staff : ModuleBase


    {
        [Command("userinfo"), Summary("Returns the users profile info")]
        [Alias("user")]
        [RequireUserPermission(Discord.GuildPermission.KickMembers)]
        public async Task UserInfo(SocketGuildUser user = null)
        {

            EmbedBuilder Embed = new EmbedBuilder();

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: Whoops, you didn't provide a username or a mention! **Use b! user <@user>**");
                return;
            }
            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: Sorry but I cannot provide info for a bots as they are not users!");
                return;
            }

            //Embed for userinfo

            var userCreate = user.CreatedAt.LocalDateTime.ToString("dddd, dd MMMM yyyy");

            Embed.AddField("Profile created on:", "\n" + userCreate, true);
            Embed.AddField("User dicriminator:", "\n" + user.Discriminator, false);
            Embed.AddField("Users status:", user.Status, true);
            Embed.WithColor(17, 0, 255);
            Embed.WithFooter($"Action was performed by {Context.User.Username}.");
            Embed.WithTitle($"{user.Username}'s info has been provided.");

            Embed.WithFields();
            Embed.WithThumbnailUrl(user.GetAvatarUrl());

            await Context.Channel.SendMessageAsync("yoink", false, Embed.Build());


        }

        [Command("Purge"), Remarks("Purge the messages in the channel")]
        [Alias("purge")]
        [RequireUserPermission(Discord.GuildPermission.ManageMessages)]

        public async Task Clear(int amountOfMessagesToDelete)
        {
            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete + 1).FlattenAsync());

            await Context.Channel.SendMessageAsync("Messages have been purged!");

        }




        [Command("UserPurge"), Summary("Purges a specified users messages")]
        [Alias("userp", "userdel")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Clear(SocketGuildUser user = null, int amountOfMessagesToDelete = 20)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user to purge! @ them or type their username!");
                return;
            }



            if (user == Context.User)
                amountOfMessagesToDelete++; //Because it will count the purge command as a message

            var messages = await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete).FlattenAsync();

            var result = messages.Where(x => x.Author.Id == user.Id && x.CreatedAt >= DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(14)));

            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(result);

            await Context.Channel.SendMessageAsync("Users messages have been purged!");

            

        }

        [Command("Kick"), Summary("Kicks the user specified")]
        [Alias("kick", "k")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser user = null, [Remainder] string reason = "Reason not provided.")
        {

            EmbedBuilder Embed = new EmbedBuilder();
            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user!");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: If you want to kick bots do it manually! *This is a security feature*");
                return;
            }

            if (reason == "Reason not provided.")
            {
                await Context.Channel.SendMessageAsync(":x: You didn't provide a reason!");
                return;
            }

            //If the user was mentioned and isn't a bot this will execute.

            await user.KickAsync();

            //Shows the date the user was kicked on
            var date = Context.Message.CreatedAt.ToString("dddd, dd MMMM yyyy");

            //This is the embed for the kick
            Embed.WithTitle("User kicked information.");
            Embed.AddField("Username:", user.Username, true);
            Embed.AddField("Kicked by:", Context.User.Mention, true);
            Embed.AddField("Kicked on:", date, true);
            Embed.AddField("Reason:", reason, true);
            Embed.WithColor(255, 30, 6);
            Embed.WithThumbnailUrl(user.GetAvatarUrl());

            await Context.Channel.SendMessageAsync($"{user.Username} was kicked! Providing info of the kick...", false, Embed.Build());


            await ((ISocketMessageChannel)user.GetOrCreateDMChannelAsync()).SendMessageAsync("Sorry but you were kicked. Please DM a mod if you believe this was wrong.");

        }


        [Command("Ban"), Summary("Bans the user specified")]
        [Alias("ban", "b")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser user = null)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user. Please mention them or type their name.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: You can't ban a bot. If you want it gone you can kick it or remove it manually.");
                return;
            }


            Random rand;
            rand = new Random();
            string[] randomBanMessage;
            randomBanMessage = new string[]
                {
                    "Ooo that had to hurt.", //0
                    "Yikes man, shouldn't have done that.", //1
                    "Again? Thought the rules stated that. Tsk Tsk.", //2
                    "Hoo ha you just got hit by, you've been struck by, a smooth criminal.", //3
                    "You hear that? *cannon thunders in the distance*" //4
                };

            await user.BanAsync();
            int banMessage = rand.Next(randomBanMessage.Length);
            string banMessageToPost = randomBanMessage[banMessage];
            await Context.Channel.SendMessageAsync(banMessageToPost);

            //await ((ISocketMessageChannel)client.GetChannel(593216013793886208)).SendMessageAsync("User was banned." + Context.User.GetAvatarUrl());


        }

        [Command("Mute"), Summary("Mute a specified user")]
        [Alias("mute", "M")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Mute(SocketGuildUser user = null)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: Hey! You didn't specify a user to mute! Please mention them or type their name with discriminator.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: Sorry but you can't mute bots.");
                return;
            }

            


            ulong roleID = 610640079823568933;
            var role = Context.Guild.GetRole(roleID);
            await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync($":white_check_mark: User {user.Mention} has been muted.");

            //working on DMing the user letting them know they were muted. Will put this with the ban and kick commands once I figure it out.

            //var userDM = await Context.User.GetOrCreateDMChannelAsync();
           // await userDM.SendMessageAsync("Sorry but you were muted by a mod or admin.");


            //Working on logging the muted users in a respected channel.
            var channel = Context.Guild.GetChannelAsync(613090719044861952);

            await Context.Channel.SendMessageAsync((channel) + $"User {user.Mention} was muted.");


           // await ((ISocketMessageChannel)client.GetChannel(610592394383196173)).SendMessageAsync($"User {user.Username} was muted." + Context.User.GetAvatarUrl());


        }


        [Command("Unmute"), Summary("Unmute a specified user.")]
        [Alias("unmute", "um")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task UnMute(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($":x:{user.Mention} You didn't specify a user to unmute! Please @ them or type in their name.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: Sory but bots can't be affected by both mute and unmute.");
                return;
            }

            if (Context.Message.Content.Contains("Thing"))
            {
                await Context.Channel.SendMessageAsync("No u");
            }

            ulong roleID = 610640079823568933;
            var role = Context.Guild.GetRole(roleID);
            await user.RemoveRoleAsync(role);
            await Context.Channel.SendMessageAsync($":white_check_mark: User {user.Mention} has been unmuted.");

            ulong grantRole = 418240237123272704;
            var roleGrant = Context.Guild.GetRole(grantRole);
            await user.AddRoleAsync(roleGrant);


        }



        
    }
   



}












    



