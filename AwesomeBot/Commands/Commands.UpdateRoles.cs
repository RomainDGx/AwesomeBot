using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using System.Threading.Tasks;

namespace AwesomeBot
{
    internal partial class Commands : ApplicationCommandsModule
    {
        [SlashCommand( "update-roles", "Update the roles of users.", defaultMemberPermissions: (long)Permissions.Administrator, dmPermission: false )]
        public async Task UpdateRolesAsync( InteractionContext ctx )
        {
            await ctx.CreateResponseAsync( InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder
            {
                Content = "Not Implemented ||bruh||."
            } );
        }
    }
}
