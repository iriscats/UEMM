

using UEMM.Services;
using UEMM.ViewModels.Pages;
using UEMM.ViewModels.Windows;
using UEMM.Views.Pages;
using UEMM.Views.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lepo.i18n.DependencyInjection;
using Lepo.i18n.Json;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using Wpf.Ui;
using System.Globalization;
using Newtonsoft.Json;
using UEMM.Models;

namespace UEMM;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            _ = services.AddStringLocalizer(b =>
            {
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Assets.Langs.Translations-zh-CN.json", new CultureInfo("zh-CN"));
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Assets.Langs.Translations-en-US.json", new CultureInfo("en-US"));

                var cul = new CultureInfo((JsonConvert.DeserializeObject<LangInfo>(File.ReadAllText(@"Settings.json")).CultureInfo));
                b.SetCulture(cul);
      
            });


            _ = services.AddHostedService<ApplicationHostService>();

            // Page resolver service
            _ = services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            _ = services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            _ = services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            _ = services.AddSingleton<INavigationService, NavigationService>();

            // Main window with navigation
            _ = services.AddSingleton<INavigationWindow, MainWindow>();
            _ = services.AddSingleton<MainWindowViewModel>();

            _ = services.AddSingleton<DashboardPage>();
            _ = services.AddSingleton<DashboardViewModel>();
            _ = services.AddSingleton<SettingsPage>();
            _ = services.AddSingleton<SettingsViewModel>();
        }).Build();


    internal readonly Code.Middleware Middleware = new();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();

        Middleware.Initialize();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}
