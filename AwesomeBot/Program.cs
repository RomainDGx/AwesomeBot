using DisCatSharp.Hosting.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AwesomeBot
{
    internal class Program
    {
        static async Task Main( string[] args )
        {
            var host = CreateHostBuilder( args ).Build();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder( string[] args )
        {
            return Host.CreateDefaultBuilder( args )
                .UseConsoleLifetime()
                .ConfigureAppConfiguration( c => c
                    .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true )
                    .AddJsonFile( "appsettings.Development.json", optional: true, reloadOnChange: true )
                    .AddEnvironmentVariables()
                    .AddCommandLine( args ) )
                .ConfigureServices( s => s.AddDiscordHostedService<Bot>() );
        }
    }
}
