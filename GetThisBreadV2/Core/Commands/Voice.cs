using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace GetThisBreadV2.Core.Commands
{
    public class Voice : ModuleBase



    {
        [Command("play", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {

            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in voice channel to use this command."); return; }

            var audioClient = await channel.ConnectAsync();

        }


    }
}
