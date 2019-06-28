using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;


namespace GetThisBread.Core.Commands.Embed
{


    public class Admin : ModuleBase
    {
        [Command("botInfo"), Summary("Author of bot")]
        [Alias("info")]
        public async Task Embed()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("GetThisBread");
            Embed.WithColor(68, 63, 209);
            Embed.WithFooter("Thank you for enjoying my bot!", Context.User.GetAvatarUrl());
            Embed.WithDescription("Hello and welcome to GetThisBread! The everything bot! \n " +
                "Bot prefix is `Bread` \n" +
                 "To use commands type `Bread` then the command name. \n " +
                 "List of commands \n" +
                 "```Admin: \n Purge \n UserPurge \n Userinfo```");


            Embed.WithThumbnailUrl("https://cdn.discordapp.com/avatars/551269352624750592/a02feb9cf7776f4a782db0253a8a6339.png?size=128");




            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }

}