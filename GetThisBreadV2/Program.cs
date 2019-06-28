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

            }) ;

            client.Log += Log;




            client = new DiscordSocketClient();

            Commands = new CommandService();


            string token = "TOKEN";
             //using (var Stream = new FileStream((Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(@"\bin\Debug\netcoreapp2.1", @"Token.txt"), FileMode.Open, FileAccess.Read))
            // using (var ReadToken = new StreamReader(Stream))
             //{
                // token = ReadToken.ReadToEnd();
             //}

            services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommandsAsync();

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

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


            if (!(message.HasStringPrefix("Bread" + " ", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot) return;

            // Command Context

            var context = new CommandContext(client, message);

            // Execute the command.

            var result = await Commands.ExecuteAsync(context: context, argPos: argPos, services: null);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);


            // Hook into the UserJoined event
            //client.UserJoined += AnnounceJoinedUser;





        }

        
        public async Task AnnoucneJoineduser(SocketGuildUser user) //Welcomes new user
        {
         
            var channel = client.GetChannel(355144831422562327) as SocketTextChannel;
            await channel.SendMessageAsync($"Welcome {user.Mention} to {channel.Guild.Name}");
            
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }










    }



}
