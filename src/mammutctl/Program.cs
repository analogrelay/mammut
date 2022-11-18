using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Hosting;

new HostBuilder()
    .RunCommandLineApplicationAsync<Application>(args);

public class Application
{
    
}