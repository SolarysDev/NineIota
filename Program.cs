using System;
using System.Threading.Tasks;

using Discord;

using Lambda;

namespace ZeroTwo
{
    class Program
    {
        private LambdaClient _client;

        private static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        private async Task MainAsync(string[] args)
        {
            _client = new LambdaClient(new LambdaConfig
            {
                TotalShards = 1,
                IgnoreBots = true,
                IgnoreDMs = true,
                HelpCommand = HelpCommandFormat.Override,
                LogGuildUpdates = true,
                OwnerId = 268081508068229120,
                OverrideReady = true,
                OverrideConnected = true,
                LogLevel = LogSeverity.Error
            });

            _client.ShardConnected += shard =>
            {
                Console.WriteLine($"Shard {shard.ShardId} connected.");
                return Task.CompletedTask;
            };

            _client.ShardReady += async shard =>
            {
                Console.WriteLine($"Shard {shard.ShardId} ready.");
                await Task.Run(async () =>
                {
                    for (;;)
                    {
                        await Task.Delay(180000);
                        if (Config.Flags.PushToBotlist)
                            throw new NotImplementedException();
                        if (Config.Flags.UpdatePresence)
                            await shard.SetActivityAsync(new Game($"{shard.Guilds.Count} servers on shard {shard.ShardId} | >>", ActivityType.Watching));
                    }
                });
            };

            _client.Log += log =>
            {
                Console.WriteLine(log.ToString());
                return Task.CompletedTask;
            };

            var mainHandler = new LambdaHandler(_client);
            await mainHandler.InitializeAsync(">>");

            await _client.LoginAsync(TokenType.Bot, Config.Keys.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
