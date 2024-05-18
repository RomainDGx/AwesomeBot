using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeBot
{
    internal partial class Commands : ApplicationCommandsModule
    {
        [SlashCommand( "ping", "Makes sure the bot still alive.", dmPermission: false )]
        public async Task PingAsync( InteractionContext ctx )
        {
            var builder = new StringBuilder()
                .AppendLine( "```" )
                .AppendLine( $"Pinging {ctx.Client.CurrentUser.Username} with 32 bytes of data:" );

            await ctx.CreateResponseAsync( InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder
            {
                Content = $"{builder}```"
            } );

            int sent = 4;
            var success = new List<int>();
            var rd = new Random();

            for( int i = 0; i < sent; i++ )
            {
                int delay = rd.Next( 1, 1000 );
                if( delay > 800 )
                {
                    builder.AppendLine( "Request timed out." );
                }
                else
                {
                    success.Add( delay );
                    builder.AppendLine( $"Reply from {ctx.Client.CurrentUser.Username} bytes=32 time={delay}ms TTL=116" );
                }
                if( i < sent - 1 )
                {
                    await Task.Delay( TimeSpan.FromSeconds( 1 ) );
                    await ctx.EditResponseAsync( new DiscordWebhookBuilder { Content = $"{builder}```" } );
                }
            }

            builder.AppendLine()
                .AppendLine( $"Ping statistics for {ctx.Client.CurrentUser.Username}:" )
                .AppendLine( $"    Packets: Sent = {sent}, Received = {success.Count}, Lost = {sent - success.Count} ({(sent - success.Count) * 100 / sent}% loss)," )
                .AppendLine( "Approximate round trip times in milli-seconds:" )
                .AppendLine( $"    Minimum = {success.Min()}ms, Maximum = {success.Max()}ms, Average = {Math.Floor( success.Average() )}ms" )
                .Append( "```" );

            await ctx.EditResponseAsync( new DiscordWebhookBuilder { Content = builder.ToString() } );
        }
    }
}
