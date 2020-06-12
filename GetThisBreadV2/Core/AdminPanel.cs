using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Sharpdactyl.Models.Client;
using Sharpdactyl;
using Sharpdactyl.Models.Node;
using Sharpdactyl.Models.User;

namespace GetThisBreadV2.Core
{
    /* 
      Welcome to the admin panel for whom ever sees this. Everything is pretty much self explanatory and isn't that hard to figure out. 
      I'll be adding comment blocks to the areas that are important so you always have a note to fall back on.*/

    public class AdminPanel : ModuleBase
    {

        [Command("AdminP")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task AdminP()
        {
            //Varibles for the embed.
            var say = "This command allows you to use the command say from console.";
            var restart = "This command allows you to restart the server in the event of a freeze. It will kill the server then start it back up.";
            var whitelistA = "Add a user to the whitelist.";
            var whitelistR = "Remove a user from the whitelist.";
            var usage = "Shows the usage of the server.";
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/705301311012470818/706400165761908806/en_craft_64_2019_02_21_06_54_30_UTC.png");
            embed.AddField("**Announcement**", say, false);
            embed.AddField("**Restart**", restart, true);
            embed.AddField("**WhitelistA**", whitelistA, false);
            embed.AddField("**WhitelistR**", whitelistR, true);
            embed.AddField("**Usage**", usage, false);
            embed.WithColor(Color.Magenta);
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("Announcement")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Announcement([Remainder] string say = null)
        {
            /*Connecting to the admin panel via API, after PClient the two strings are the things that will tell the bot to connect to the API. First one is the panel URL, second is the API key. 
             * (API KEY IS VERY IMPORTANT DO NOT SHARE. IF I SEE YOU SHARING IT I WILL REMOVE ACCESS) 
             */
            PClient pClient = new PClient("https://panel.unboundnetwork.net/", "2lKW7ehzEKJnGXPeAv0bTqU1QmGgRrGrLwtrWBDE9xzXF7ED");
            if (pClient.PostCMDCommand("51df9751", $"say {say}"))
            {
                await Context.Channel.SendMessageAsync(":white_check_mark: Command sent and executed! Info of the command is provided below.");
                await Task.Delay(2000);
                await Context.Channel.SendMessageAsync("Building embed info...");
                var date = Context.Message.CreatedAt.ToString("dddd, dd MMMM yyyy");
                await Task.Delay(2000);
                EmbedBuilder embed = new EmbedBuilder();
                embed.AddField("Date executed:", date, true);
                embed.AddField("Executed by:", Context.User.Mention, true);
                embed.AddField("Announcement text:", say, true);
                embed.WithColor(252, 3, 115);
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }

            else
            {
                await Context.Channel.SendMessageAsync("Command failed to send.");
            }

            if (say == null)
            {
                await Context.Channel.SendMessageAsync(":x: No text was detected! Please add text for it to send!");
                return;
            }

        }

        //Restart the server in the event of a freeze. This will soon be later be removed or locked when memory leaks get fixed.

        [Command("restart")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task serverRestart()
        
        {
         
            PClient srvClient = new PClient("https://panel.unboundnetwork.net/", "2lKW7ehzEKJnGXPeAv0bTqU1QmGgRrGrLwtrWBDE9xzXF7ED");
            if (srvClient.SendSignal("51df9751", PowerSettings.kill))
            {
                await Context.Channel.SendMessageAsync("Server was killed!");
                await Task.Delay(2000);
                await Context.Channel.SendMessageAsync("Sending restart signal...");
                await Task.Delay(6000);
                await Context.Channel.SendMessageAsync("Restart signal received!");
                srvClient.SendSignal("51df9751", PowerSettings.start);
                await Task.Delay(4000);
                //Remove this once spigot updates.
                await Context.Channel.SendMessageAsync("**Server will take an additional 20 seconds on start up due to Spigot wanting to update**");
                await Context.Channel.SendMessageAsync("Server is starting!");
                await Task.Delay(13000);
                await Context.Channel.SendMessageAsync(":white_check_mark: Server is up and running. This concludes my report logging.");
                return;
            }


        }

        //Add a user to the white list.

        [Command("whitelistA")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task whiteListAdd([Remainder] string usrName = null)
        {
            PClient srvCleint = new PClient("https://panel.unboundnetwork.net/", "2lKW7ehzEKJnGXPeAv0bTqU1QmGgRrGrLwtrWBDE9xzXF7ED");

            if (usrName == null)
            {
                await Context.Channel.SendMessageAsync(":x: Username wasn't inputted. Please type the full username when you whitelist!");
                return;
            }

            if (srvCleint.PostCMDCommand("51df9751", $"whitelist add {usrName}"))
            {
                await Context.Channel.SendMessageAsync(":white_check_mark: User was added to the whitelist!");
                return;
            }

            
        }

        //Remove a user from the whitelist.

        [Command("whitelistR")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task whiteListRemove([Remainder] string usrName = null)
        {
            PClient srvCleint = new PClient("https://panel.unboundnetwork.net/", "2lKW7ehzEKJnGXPeAv0bTqU1QmGgRrGrLwtrWBDE9xzXF7ED");

            if (usrName == null)
            {
                await Context.Channel.SendMessageAsync(":x: Username wasn't inputted. Please type the full username when you whitelist!");
                return;
            }

            if (srvCleint.PostCMDCommand("51df9751", $"whitelist remove {usrName}"))
            {
                await Context.Channel.SendMessageAsync(":white_check_mark: User was removed from the whitelist!");
                return;
            }


        }

        //Get the usage of the server.

        [Command("Usage")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task usageList()
        {
            
            PClient srvCleint = new PClient("https://panel.unboundnetwork.net/", "2lKW7ehzEKJnGXPeAv0bTqU1QmGgRrGrLwtrWBDE9xzXF7ED");
            ServerDatum srv = srvCleint.GetServerById("51df9751");
            ServerUtil srvU = srvCleint.GetServerUsage(srv.Attributes.Identifier);

            var cpu = $"%{srvU.Attributes.Cpu.Current}";
            var mem = $"{srvU.Attributes.Memory.Current}MB";
            var disk = $"%{srvU.Attributes.Disk.Current}";
            var state = $"{srvU.Attributes.State}";
            

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Server Usage.");
            embed.WithColor(Color.DarkGrey);
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/705301311012470818/706713848668880957/en_craft_79_2019_02_21_06_54_30_UTC.png");
            embed.AddField("Current CPU usage:", cpu, true);
            embed.AddField("Current Memory usage:", mem, true);
            embed.AddField("Current Server state:", state, true);
            embed.AddField("Current Disk usage", disk, true);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        

    } 
}
