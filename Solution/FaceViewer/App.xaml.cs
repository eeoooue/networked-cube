using Microsoft.Extensions.DependencyInjection;
using FaceViewer.ViewModels;
using LibCubeIntegration.GetCubeStrategies;
using System.Windows;

namespace FaceViewer;
public partial class App : Application
{
    readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IGetCubeStrategy, GetCubeViaApiStrategy>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}