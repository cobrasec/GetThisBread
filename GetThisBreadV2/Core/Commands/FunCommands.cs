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

        public async Task Message()
        {
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
            await Context.Channel.SendMessageAsync("Aww you shouldn't have :heart:");
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



        [Command("Argument"), Summary("Sends a new ice breaker for everyone to yell at each other")]
        public async Task Argument()
        {
            Random rand;
            rand = new Random();
            string[] Argument;
            Argument = new string[]
                {
                    "iPhone or Android?", //0
                    "Game of Thrones or Lord of the Rings?", //1
                    "Windows or Mac?", //2
                    "Will add more, give me a second..."
                };
            int randomArgument = rand.Next(Argument.Length);
            string randomArgumentToPost = Argument[randomArgument];
            await Context.Channel.SendMessageAsync(randomArgumentToPost);

        }


        

    }




}