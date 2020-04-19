using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetThisBreadV2.Core.Commands
{
    public class GameOfTheWeek : ModuleBase
    {
        [Command("Game"), Summary("Send a random game from the list weekly by issuing the command weekly")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Game()
        {
            EmbedBuilder Embed = new EmbedBuilder();


            Random rand;
            rand = new Random();
            string[] randomGameMessage;
            randomGameMessage = new string[]
                {
                    "Tom Clancy's Rainbow Six Siege! \n Get it here! \n https://store.steampowered.com/app/312670/", //0
                    "Minecraft! \n Get it here! \n https://www.minecraft.net/en-us/", //1
                    "Dirt Rally 2.0! \n Get it here \n https://store.steampowered.com/app/690790/", //2
                    "Strange Brigade! \n Get it here! \n https://store.steampowered.com/app/312670/",  //3
                    "Civ5! \n Get it here! \n https://store.steampowered.com/app/312670/", //4
                    "Left 4 Dead 2! \n Get it here! \n https://store.steampowered.com/app/550/", //5
                    "Payday 2! \n Get it here! \n https://store.steampowered.com/app/218620/", //6
                    "Payday: The Hiest!\n Get it here! \n https://store.steampowered.com/app/24240/", //7
                    "Ori and the Blind Forest! \n Get it here! \n https://store.steampowered.com/app/387290/", //8
                    "Portal 2! \n Get it here! \n https://store.steampowered.com/app/620/", //9
                };

            int gameMessage = rand.Next(randomGameMessage.Length);
            string gameMessageToPost = randomGameMessage[gameMessage];
            Embed.WithDescription(gameMessageToPost);
            Embed.WithColor(0, 137, 255);




            await Context.Channel.SendMessageAsync("Game of the week is...", false, Embed.Build());
        }








    }
}
