using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

using GetThisBread.Core.Commands;



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
                LogLevel = LogSeverity.Debug
            });



            var token = File.ReadAllText("Token.txt");



            services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommandsAsync();
            await client.SetGameAsync("Use b!help to get started!");
            await client.SetStatusAsync(UserStatus.AFK);
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            //Hooking into events
            client.Log += LogAsync;
            client.UserJoined += AnnounceUserJoined;
            client.UserLeft += AnnounceUserLeft;
            client.MessageReceived += HandleCommandAsync;
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
            // Don't process the command if it was a System Message

            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins

            int argPos = 0;

            // Determine if the message is a command, based on if it starts with '!' or a mention prefix


            if (!(message.HasStringPrefix("b!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot) return;

            // Command Context

            var context = new CommandContext(client, message);

            // Execute the command.

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
                    "Kick back is here and relax my friend, welcome to **The Ticklers**!",
                    $"All weapons must be left at the front door {user.Mention}!",
                    $"Hey guys the pizza is here! Oh wait... Its just {user.Mention}.",
                   $"Well well, look who decied to join in on the fun! User {user.Mention} is here!",
                   $"#PoggersFor{user.Mention}",
                   $""
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

       


    }



}








