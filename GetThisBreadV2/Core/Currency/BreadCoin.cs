using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetThisBreadV2.Core.Currency
{
    public class BreadCoin : ModuleBase<ShardedCommandContext>
    {
        [Group("BreadCoin"), Alias("coin"), Summary("Manage the gorups for BreadCoin")]
        public class BreadCoinGroup : ModuleBase<ShardedCommandContext>
        {
            [Command(""), Alias("me", "my"), Summary("Shows a user their BreadCoin balance")]
            public async Task Coin()
            {

            }

            [Command("Give"), Alias("gift"), Summary("Give a specified user BreadCoins")]
            public async Task Give(IUser User = null, int Amount = 0)
            {
                //BreadCoin give <parameters> 1000
                //group cmd user amount

                //Checks
                //Does user have permissions?
                //Does the user have enough BreadCoin?
                if (User == null)
                {
                    //The executer has not mentioned a user
                    await Context.Channel.SendMessageAsync(":x: Sorry but you didn't mention a user to give your BreadCoin to! Please use **Bread give <@user> <amount>**");
                    return;
                }
                //At this point, we made sure that the user was pinged
                if (User.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Sorry but bots can't use me! So no need to give BreadCoin to other bots!");
                    return;
                }

                //Also at this point we haev figured that the user has pinged a user and said user is not a bot
                if (Amount == 0)
                {
                    await Context.Channel.SendMessageAsync($":x: You didn't specify an amount of BreadCoin that I need to give to {User.Username}!");
                    return;
                }

                //Okay so user isn't a bot and executer has the right amount of coins
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync($":x: You do not have admin permissions to run this command!");
                    return;
                }

                //Execution
                //Calculations
                //Telling the user what they have gotten
                await Context.Channel.SendMessageAsync($":tada: {User.Mention} you have recieved ${Amount}BreadCoins from {Context.User.Username}!");





                //Saving the data
                //Save the data to DB
                //Save a file




            }
            
        }

    }
}
