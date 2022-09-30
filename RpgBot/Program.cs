using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using RpgBot.Modules;

namespace RpgBot
{
    class RpgBot : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;

        Dice dice = new Dice();

        static void Main(string[] args)=> new RpgBot().MainAsync().GetAwaiter().GetResult();

        public RpgBot()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, "MTAyMzIyMTA0NDI0NjQ5NTM2Ng.GGg7WW.at0MckKzHrOoc8idNt5xG7GfGWU5SHc8_Ep0fM");

            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        private Task ReadyAsync()
        {
            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message.Author.IsBot)
                return;

            int argPos = 0;
            if (message.HasStringPrefix("!",ref argPos))
            {
                int value = dice.DiceRoll(message.Content);
                if(value == -999999)
                {
                    await message.Channel.SendMessageAsync("Seu resultado contem um erro");
                }
                else
                {
                    if (value == -10000)
                    {
                        await message.Channel.SendMessageAsync("Siga o exemplo !XdX+X-X, sendo X qualquer numero");
                    }
                    else
                    {
                        await message.Channel.SendMessageAsync("Seu dado rolou " + value);
                    }
                }
            }
            else
            {
                await message.Channel.SendMessageAsync("Seu resultado contem um erro");
            }
        }
    }
}