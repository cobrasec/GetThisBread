using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;

namespace GetThisBread.Core.Commands

{


    public class Admin : ModuleBase


    {

        [Command("Userinfo"), Summary("Returns the users info")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            EmbedBuilder Embed = new EmbedBuilder();


            var userInfo = user ?? Context.Client.CurrentUser;
            //await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}#{userInfo.GetAvatarUrl()}");

            // Embed.WithDescription("User Name: " + userInfo.Username + "\n" + "Discriminator: " + "#" + userInfo.Discriminator + "\n" +
            //"Profile Created on: " + userInfo.CreatedAt);




            //Don't mind these. They are here for testing.
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
        public async Task Kick(SocketGuildUser user)
        {
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

            //await Context.Channel.SendMessageAsync("User was given the boot. Git rekt.");

            //await ((ISocketMessageChannel)user.GetChannel(593250389621473290)).SendMessageAsync("User was kicked." + Context.User.GetAvatarUrl());

        }


        [Command("Ban"), Summary("Bans the user specified")]
        [Alias("ban", "b")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser user)
        {
            Random rand;
            rand = new Random();
            string[] randomBanMessage;
            randomBanMessage = new string[]
                {
                    "Ooo that has to hurt.", //0
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

        //[Command("Unban"), Summary("Unbans the user specified")]
        //public async Task UnBan(Sokcet user)
        //{
        //await user.UnBan();
        //}








    }



}




    



