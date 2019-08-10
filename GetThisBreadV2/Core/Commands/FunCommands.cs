using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;

namespace GetThisBread.Core.Commands.Fun
{


    public class Everyone : ModuleBase
    {


        [Command("Bread"), Summary("We love bread!")]
        [Alias("love")]

        public async Task Message(IUser user = null)
        {

            var userInfo = user ?? Context.Client.CurrentUser;
            Random rand;
            rand = new Random();
            string[] randomImage;
            randomImage = new string[]
                {
                    "love1.png", //0
                    "love2.png", //1
                    "love3.png", //2
                    "love4.png", //3


                };


            int randomLoveImage = rand.Next(randomImage.Length);
            string loveImageToPost = randomImage[randomLoveImage];
            //Need to make it ping the specified user while also sending the command runners name. Work in progress.
            await Context.Channel.SendMessageAsync($"{userInfo.Username} sends love to @{userInfo.Username}");
            await Context.Channel.SendFileAsync(loveImageToPost);





        }

        [Command("Trailer"), Summary("Awesome trailer!")]
        [Alias("t", "trailer", "vid")]
        public async Task Trailer()
        {
            await Context.Channel.SendMessageAsync("Server trailer can be found here!");
            await Context.Channel.SendMessageAsync("https://www.youtube.com/watch?v=pQkjlvPjHpk&feature=youtu.be");

        }

        [Command("Mikigae"), Summary("Miki is p gae")]
        [Alias("gae", "Miki")]
        public async Task Mikigae()
        {
            await Context.Channel.SendMessageAsync("Miki is p gae tbh");
        }

        [Command("No u"), Summary("Replys No U'")]
        [Alias("no u", "no U", "NO u", "nO u")]
        public async Task NoU()
        {

         
                await Context.Channel.SendMessageAsync("No u");

            

        }

        [Command("Furret"), Summary("Furret gang gang")]
        [Alias("furret", "furretgang")]
        public async Task Furret()
        {
            await Context.Channel.SendMessageAsync("Furret is awesome!");
            await Context.Channel.SendMessageAsync("http://pm1.narvii.com/6218/8cb12e26ce3cb61112d676d846b83efe4449f388_00.jpg");




        }


        [Command("Kevin"), Summary("Dunno why his steam is this way")]
        [Alias("kevin", "wildo")]
        public async Task Wildo()
        {
            await Context.Channel.SendMessageAsync("Why is Kevin this way?");
            await Context.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/360916041850814465/553422660856446976/unknown.png");
        }


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
                    "Will add more please bear with me."
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
        //Testing out emoji posting, still a work in progress. 
        [Command("Test"), Summary("Testing the bots emoji")]
        public async Task Test()
        {
            Random rand;
            rand = new Random();
            string[] emojiPost;
            emojiPost = new string[]
                {
                    ":609770219098865677:"
                };
            int postEmoji = rand.Next(emojiPost.Length);
            string emojiToPost = emojiPost[postEmoji];
            await Context.Channel.SendMessageAsync(emojiToPost);

        }




    }




}