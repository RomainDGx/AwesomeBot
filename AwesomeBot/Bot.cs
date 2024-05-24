using DisCatSharp.ApplicationCommands;
using DisCatSharp.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AwesomeBot
{
    internal class Bot : DiscordHostedService
    {
        public Bot( IConfiguration config,
                    ILogger<DiscordHostedService> logger,
                    IServiceProvider serviceProvider,
                    IHostApplicationLifetime applicationLifetime,
                    string configBotSection = "DisCatSharp" )
            : base( config, logger, serviceProvider, applicationLifetime, configBotSection )
        {
        }

        protected override Task PreConnectAsync()
        {
            Client.RegisterEventHandlers( Assembly.GetExecutingAssembly() );
            return base.PreConnectAsync();
        }

        protected override async Task PostConnectAsync()
        {
            var appCommands = Client.UseApplicationCommands();
            appCommands.RegisterGuildCommands<Commands>( Configuration.GetValue<ulong>( "DisCatSharp:Discord:GuildId" ) );
            foreach( var (guildId, commands) in appCommands.GuildCommands )
            {
                await Client.BulkOverwriteGuildApplicationCommandsAsync( guildId, commands );
            }
            await base.PostConnectAsync();
        }
    }
}
