using Discord.WebSocket;
using System;
using System.Reflection;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;
using System.IO;

namespace GetThisBread.Core.Commands.Fun
{


    public class Everyone : ModuleBase
    {

        //Please add new aguments to the Argument string if you have any ideas. 
        [Command("Argument"), Summary("Sends a new ice breaker for everyone to yell at each other")]
        public async Task Argument()
        {
            Random rand;
            rand = new Random();
            string[] Argument;
            Argument = new string[]
                {
                    "iPhone or Android?",
                    "Game of Thrones or Lord of the Rings?",
                    "Windows or Mac?",
                    "Discord or Skype?",
                    "Pokemon or Digimon?",
                    "Laptop or Desktop?",

                };
            int randomArgument = rand.Next(Argument.Length);
            string randomArgumentToPost = Argument[randomArgument];
            await Context.Channel.SendMessageAsync(randomArgumentToPost);

        }

        [Command("8Ball"), Summary("Ask the 8ball how your day will be or maybe.... your life!")]
        [Alias("8ball", "ball", "8ball,", "8Ball,", "magic")]
        public async Task MagicBall([Remainder]string args = null)
        {
            Random rand;
            rand = new Random();
            string[] magicBall;
            magicBall = new string[]
                {
                    ":8ball: It is certain.",
                    ":8ball: It is decidedly so.",
                    ":8ball: Without a doubt.",
                    ":8ball: Yes, definitely.",
                    ":8ball: You may reply on it.",
                    ":8ball: As I see it, yes.",
                    ":8ball: Most likely.",
                    ":8ball: Outlook good.",
                    ":8ball: Yes.",
                    ":8ball: Signs point to yes.",
                    ":8ball: Reply hazy, try again.",
                    ":8ball: Ask again later",
                    ":8ball: Better not tell you now.",
                    ":8ball: Cannot predict now.",
                    ":8ball: Concentrate and ask again.",
                    ":8ball: Don't count on it.",
                    ":8ball: My reply is no.",
                    ":8ball: My Sources say no.",
                    ":8ball: Outlook not so good.",
                    ":8ball: Very doubtful."
                };
            int randomBall = rand.Next(magicBall.Length);
            string randomBallToPost = magicBall[randomBall];
            await Context.Channel.SendMessageAsync(randomBallToPost);
        }

        //Please add anything to the wouldRather string if you have any ideas. 
        [Command("WYR"), Summary("Bot will ask Would you rather questions to users.")]
        [Alias("Would you rather", "wyr")]
        public async Task WYR([Remainder] string args = null)
        {
            Random rand;
            rand = new Random();
            string[] wouldRather;
            wouldRather = new string[]
                {
                    "Would you rather the aliens that make first contact be robotic or orangic?",
                    "Would you rather lose the ability to read or the ability to speak?",
                    "Would you rather have a golden voice or silver tongue?",
                    "Would you rather always be 10 minutes late or 20 minutes early?",
                    "Would you rather know the history or every object you touched or be able to talk to animals?",
                    "Would you rather have all traffic lights turn green or never have to stand in line again?",
                    "Would you rather be the first to explore a planet or be the invetor of a drug that cures a deadly disease?",

                };
            int wyRather = rand.Next(wouldRather.Length);
            string wouldYouRatherToPost = wouldRather[wyRather];
            await Context.Channel.SendMessageAsync(wouldYouRatherToPost);

        }


        [Command("appreciate"), Summary("We appreciate you!")]

        public async Task Appreciate(SocketGuildUser user = null)
        {
            var userInfo = Context.User.Username;

            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"**{userInfo}** just wanted to let everyone know that they appreciate the group here! <:gucci:687026528000671770>");
                return;
            }
            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync("I see you wanted to appreciate a bot, sadly they can't feel the love you give them. Unless they were coded to feel love... That'd be weird. Should try a freind or a user you know though!");
                return;
            }
            await Context.Channel.SendMessageAsync($"**{userInfo}** sends their appreciation **{user.Username}**'s way, how sweet! <:gucci:687026528000671770>");
        }


        [Command("help")]
        public async Task Help()
        {

            await Context.Channel.TriggerTypingAsync();
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(66, 215, 245);
            Embed.WithTitle("GetThisBread (The not so smart AI)");
            Embed.WithDescription("Haha! I see you have read my rich presence! That is very good as you are a good observer. \n"
                + "So, as with any other help command you'd expect a list right? Well, I'm different. My owner made me the not so smart AI so I'm going to talk like I'm a person and I hope that's okay. \n"
                + "So, without further ado let me introduce myself. Hi there my name is GetThisBread or just Bread for short. Most of my commands require an input as I'm still learning. My owner is still adding new commands for you and me to find out so this is gonna be fun. \n"
                + "Most of my commands are going to be 'hidden' as my owner likes to put it, he never told me why. Anywho on to the commands!");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());

            await Task.Delay(2000);
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(2000);
            await Context.Channel.SendMessageAsync("Looking through my storage unit here... It's in here somewhere... AH! Here it is!");
            await Task.Delay(5000);
            Embed.WithColor(169, 38, 255);
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(2000);
            await Context.Channel.SendMessageAsync("Tada! The command almanac. Consult this for whenever you're confused. Sadly I think a few pages are missing so you'll have to figure some out. Actually wait... think I have a missing page here..." +
                "Looks like it sayssss.. `b!almanac`! Must have got torn during a move, hmm.  Looks like its just a quick access to the almanac so that's neat. *glad I don't have to say my intro everytime*. Anywho, have fun! Gonna go clean this out now...");
            await Task.Delay(2000);
            Embed.WithTitle("Command Almanac.");
            Embed.WithDescription("The command almanac is where all the commands will be stored, use b!almanac to have quick access to this at anytime you need it. ");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());


        }

        [Command("almanac"), Summary("List of commands that can be used as a public user")]
        public async Task Almanac()
        {
           
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(169, 38, 255);
            embed.WithTitle("Command Almanac.");
            embed.WithDescription("The command almanac is where all the commands will be stored. Use b!almanac to have quick access to this at anytime you need it.");
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/705301311012470818/705301339177222204/SpellBook01_03.png");

            var field = new EmbedFieldBuilder();
            field.WithName("**appreciate**");
            field.WithValue("This command is here so you can appreciate others! I begged and pleaded with my owner to make this for me. So please use it while you can! Sadly won't work on other bots since they aren't coded with love like I am, but users it will work great!");
            field.WithIsInline(true);
            var field2 = new EmbedFieldBuilder();
            field2.WithName("**WYR**");
            field2.WithValue("WYR stands for Would You Rather. Some of these are pretty fun from what I read on them. They help break the tension in the room and cause some conversaion.");
            field2.WithIsInline(true);
            var field3 = new EmbedFieldBuilder();
            field3.WithName("**Argument**");
            field3.WithValue("This one is pretty fun! Apparnetly I came built with an argument command. Never knew I had that. Anyway, when you run this, you get an argument prompt with two questions X or Y, and then you argue about it! Though some can lead to high tentions, so please stay civil when running it.");
            field3.WithIsInline(false);
            embed.WithFields(field, field2, field3);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


    }




}