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
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            client.Log += LogAsync;
            await Task.Delay(-1);







        }


        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into Command Handler
            client.MessageReceived += HandleCommandAsync;
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


            // Hook into the UserJoined event
            client.UserJoined += HandleUserJoinedAsync;
            client.UserLeft += AnnounceUserLeft;
        }


        public async Task HandleUserJoinedAsync (SocketGuildUser user) //Welcomes new user
        {
            var channel = client.GetChannel(417942280377335810) as SocketTextChannel;
            await channel.SendMessageAsync($"#PoggersFor{user.Mention}!");

        }


        public async Task AnnounceUserLeft(SocketGuildUser user)
        {
            var channel = client.GetChannel(489175046317670412) as SocketTextChannel;
            await channel.SendMessageAsync($"Bye {user.Mention}! Hope to see you again soon!");
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }





    }



}








