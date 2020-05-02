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
    /* Not yet implemented will be soon its going green for now

    public class AdminPanel : ModuleBase
    {
        [Command("AdminP")]
        public async Task AdminP()
        {
            PClient pClient = new PClient("HOSTNAME", "APIKEY");
            foreach (ServerDatum server in pClient.GetServers())
            {
                Console.WriteLine(server.Attributes.Name, server.Attributes.Identifier);
            }
            EmbedBuilder embed = new EmbedBuilder();
            EmbedFieldBuilder field = new EmbedFieldBuilder();


        }

    } */
}
