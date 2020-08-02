using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Linq;

namespace GetThisBreadV2.Core
{

    class MessageEventListener
    {
        

        


        public async Task UwUAlarmAsync(SocketMessage message)
        {
            
            string[] theGreatFilter;
            theGreatFilter = new string[]
                {
                "owo",
                "uwu",
                "oWo",
                "OwO",
                "ÒwÓ",

                };

            if (message.Author.IsBot) { return; }

            if (message.Content.Contains($"{theGreatFilter}"))
            {
                return;
            }
            await message.Channel.SendMessageAsync($":rotating_light: You have violtaed the uwu law! Remove your uwu at once! :rotating_light:");
            return;
        }


    }
}
