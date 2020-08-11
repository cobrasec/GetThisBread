using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GetThisBread
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private ServiceProvider services;

        public CommandService Commands { get => commands; set => commands = value; }


        static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {

            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug

            });


            Commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info
            });


            //Main bot Token
            //var token = File.ReadAllText("Token.txt");

            //Dev bot token
            var token = "NzEyMTIyMjIxMTQ5MDI4Mzcy.XxjxUQ.O15vEvXXwhqLK5njWUPBtrFe0EQ";


            services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommandsAsync();
            //await client.SetGameAsync("Use b!help to get started!");
            //await client.SetStatusAsync(UserStatus.AFK);
            await client.SetGameAsync("SPACEX BABY", "https://www.youtube.com/watch?v=tSJIQftoxeU", ActivityType.Watching);
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            //Hooking into events
            client.Log += LogAsync;
            client.UserJoined += AnnounceUserJoined;
            client.UserLeft += AnnounceUserLeft;
            client.MessageReceived += HandleCommandAsync;
            client.MessageReceived += UwUAlarmAsync;
            await Task.Delay(-1);


        }


        public async Task InstallCommandsAsync()
        {
            // Discover all of the commands in this assembly and load them.
            await Commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),

                                        services: null);
        }

        public async Task HandleCommandAsync(SocketMessage messageParam)
        {

            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            //MAKE SURE TO CHANGE d! TO b! BEFORE MAIN BOT RELEASE
            if (!(message.HasStringPrefix("b!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot) return;

            var context = new CommandContext(client, message);

            var result = await Commands.ExecuteAsync(context: context, argPos: argPos, services: null);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);

        }


        public async Task AnnounceUserJoined(SocketGuildUser user)
        {

            Random rand;
            rand = new Random();
            string[] randomWelcome;
            randomWelcome = new string[]
                {
                    "Kick back and relax my friend, welcome to **The Ticklers**!",
                    $"All weapons must be left at the front door {user.Mention}!",
                    $"Hey guys the pizza is here! Oh wait... Its just {user.Mention}.",
                   $"Well well, look who decied to join in on the fun! User {user.Mention} is here!",
                   $"#PoggersFor{user.Mention}",
                   $"Weclome {user.Mention}! Come inside where its warm and share some stories!",
                   $"Guess whos back, back again, its {user.Mention}!",
                   $"Who's this nerd? Welcome though {user.Mention}!"
                };

            int randomWelcomeString = rand.Next(randomWelcome.Length);
            string randomWelcomeToPost = randomWelcome[randomWelcomeString];
            var channel = client.GetChannel(417942280377335810) as SocketTextChannel;
            await channel.SendMessageAsync(randomWelcomeToPost);
            return;
        }


        public async Task AnnounceUserLeft(SocketGuildUser user)
        {
            var channel = client.GetChannel(489175046317670412) as SocketTextChannel;
            await channel.SendMessageAsync($"Bye {user.Mention}! Hope to see you again soon!");
            return;
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }




        public async Task UwUAlarmAsync(SocketMessage message)
        {

            if (message.Author.IsBot) { return; }

            if (message.Content.Contains("owo") || message.Content.Contains("uwu") || message.Content.Contains("OwO")
                || message.Content.Contains("UwU") || message.Content.Contains("ÒwÓ") || message.Content.Contains("OvO") || message.Content.Contains("oWo")
                || message.Content.Contains("Uwu") || message.Content.Contains(" úwú ") || message.Content.Contains("u w u"))

            {
                await message.DeleteAsync();

                await message.Channel.SendMessageAsync($":rotating_light: You have violtaed the uwu law! Remove your uwu at once! :rotating_light:");
                return;
            }

        }





    }

} //The reason this line updated was because I deleted the bracket on accident...