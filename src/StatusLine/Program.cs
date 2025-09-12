using Microsoft.Extensions.DependencyInjection;
using StatusLine;
using StatusLine.ClaudeCode;
using StatusLine.Plugins;

// Set UTF-8 encoding for console output
var input = Helper.Initialize();
var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));
StatuslineInput? claude = null;

try
{
    claude = StatuslineInput.FromJson(input);

    await SessionUtil.UpdateAsync(claude, cts.Token);

}
catch (ArgumentException)
{
    // ignore
}
catch (Exception e)
{
    e.Write();
}

// Configure DI container with factory to set Input
await using var provider = new ServiceCollection()
    .AddTransient<IPlugin>(_ => new ProjectPlugin { Input = claude })
    .AddTransient<IPlugin>(_ => new GitPlugin { Input = claude })
    .AddTransient<IPlugin>(_ => new TokenPlugin { Input = claude })
    .BuildServiceProvider();

// Execute plugins using DI
var plugins = provider.GetServices<IPlugin>().ToArray();
for (var i = 0; i < plugins.Length; i++)
{
    try
    {
        plugins[i].TextArray.Write();

        // Add separator except for the last plugin
        if (i < plugins.Length - 1)
        {
            Helper.SplitWrite();
        }
    }
    catch (Exception e)
    {
        e.Write();
    }
}