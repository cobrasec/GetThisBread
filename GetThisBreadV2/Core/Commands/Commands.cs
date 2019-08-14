using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;
using Discord.Rest;

namespace GetThisBread.Core.Commands


{

    public class Admin : ModuleBase


    {


        [Command("Userinfo"), Summary("Returns the users info")]
        [Alias("user", "whois")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            EmbedBuilder Embed = new EmbedBuilder();

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync("Sorry but I cannot proivide the info for this user as it is a bot!");
                return;
            }


            var userInfo = user ?? Context.Client.CurrentUser;

            //Embed for the Users info. 
            Embed.AddField("Profile created on.", " " + userInfo.CreatedAt);
            Embed.AddField("User Discriminator.", "\n" + "#" + userInfo.Discriminator);
            Embed.AddField("Users Status.", userInfo.Status);
            Embed.AddField("User name (if they have a nick.)", userInfo.Username);


            Embed.WithThumbnailUrl("" + userInfo.GetAvatarUrl());
            Embed.WithColor(17, 0, 255);
            Embed.WithFooter("");

            await Context.Channel.SendMessageAsync("Here ya go!", false, Embed.Build());
            //await Context.Channel.SendMessageAsync("Here ya go!");

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
        public async Task Clear(SocketGuildUser user, int amountOfMessagesToDelete = 100)
        {
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
        public async Task Kick(SocketGuildUser user = null)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync("You didn't specifie a user. Please @ them or type in their name manually with discriminator number.");
                return;
            }

            Random rand;
            rand = new Random();
            string[] randomKickMessage;
            randomKickMessage = new string[]
                {
                    "User was kicked. Bye bye.", //0
                    "User has been given the boot. Git rekt.", //1
                    "Beep boop, user destroyed.", //2
                    "Another one bites the dust.", //3
                    "Thou has yeeteth thy user from kingdom." //4
                };


            await user.KickAsync();
            int KickMessage = rand.Next(randomKickMessage.Length);
            string messageToPost = randomKickMessage[KickMessage];
            await Context.Channel.SendMessageAsync(messageToPost);


           // await ((ISocketMessageChannel)user.GetOrCreateDMChannelAsync()).SendMessageAsync("Sorry but you were kicked. Please DM a mod if you believe this was wrong.");

        }


        [Command("Ban"), Summary("Bans the user specified")]
        [Alias("ban", "b")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser user = null)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specifie a user. Please mention them or type their name.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: You can't ban a bot. If you want it gone you can kick it or remove it manually.");
                return;
            }

            //test webhook

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
                await Context.Channel.SendMessageAsync(":x: Hey you didn't specifie a user to mute! Please mention them or type their name with discriminator.");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: Hey sorry but you can't mute bots.");
                return;
            }

            


            ulong roleID = 610640079823568933;
            var role = Context.Guild.GetRole(roleID);
            await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync($":white_check_mark: User {user.Username} has been muted.");

            ulong roleRemove = 418240237123272704;
            var removeRole = Context.Guild.GetRole(roleRemove);
            await user.RemoveRoleAsync(removeRole);

            //ulong channelID = 610592394383196173;
            // var channel = Context.Guild.GetChannelAsync(channelID);

            //await Context.Channel.SendMessageAsync($"User {user.Username} was muted." + (channel));

            //await ((ISocketMessageChannel)client.GetChannel(610592394383196173)).SendMessageAsync($"User {user.Username} was muted." + Context.User.GetAvatarUrl());


        }


        [Command("Unmute"), Summary("Unmute a specified user.")]
        [Alias("unmute", "um")]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task UnMute(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specifie a user to unmute! Please @ them or type in their name.");
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
            await Context.Channel.SendMessageAsync($":white_check_mark: User {user.Username} has been unmuted.");

            ulong grantRole = 418240237123272704;
            var roleGrant = Context.Guild.GetRole(grantRole);
            await user.AddRoleAsync(roleGrant);


        }



        
    }
   



}












    



