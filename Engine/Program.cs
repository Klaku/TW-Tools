using Engine;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "TwHelper Engine";
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<HangfireService>();
        services.AddHostedService<WindowsBackgroundService>();
    })
    .Build();

await host.RunAsync();
