using DisCatSharp;
using DisCatSharp.Enums;
using DisCatSharp.EventArgs;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AwesomeBot
{
    [EventHandler]
    public class EventHandler
    {
        readonly IConfiguration _configuration;

        public EventHandler( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        [Event( DiscordEvent.MessageCreated )]
        public async Task RespondMotherAsync( DiscordClient _, MessageCreateEventArgs e )
        {
            if( e.Author.Id == _configuration.GetValue<ulong>( "DisCatSharp:Discord:RespondMotherAuthorId" ) )
            {
                var content = e.Message.Content.ToLower();
                if( content.Contains( "mère" ) || content.Contains( "mere" ) || content.Contains( "maman" ) )
                {
                    await e.Message.RespondAsync( builder => builder.WithContent( $"Ta mère <@{e.Author.Id}> !" ) );
                }
            }
        }
    }
}
