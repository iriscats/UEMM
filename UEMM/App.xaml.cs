﻿//using UEMM.Core.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using UEMM.Code;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UEMM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : IDisposable
    {
        private bool _disposed = false;

        internal readonly Code.Middleware Middleware = new();

        private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            _ = services.AddStringLocalizer(b =>
            {
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Resources.Translations-fa-IR.json", new CultureInfo("fa-IR"));
                _ = b.FromJson(Assembly.GetExecutingAssembly(), "Resources.Translations-en-US.json", new CultureInfo("en-US"));
                var cul = new CultureInfo((JsonConvert.DeserializeObject<LanInfo>(File.ReadAllText(@"CultureInfos.json")).CultureInfo));
                b.SetCulture(cul);
                Application.Current.Resources["MainDirection"] = cul.EnglishName == "English (United States)" ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
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
        _ = services.AddTransient<DataPage>();
        _ = services.AddTransient<DataViewModel>();
        _ = services.AddSingleton<SettingsPage>();
        _ = services.AddSingleton<SettingsViewModel>();
    }).Build();



        App()
        {
            DropIfAlreadyRunning();

#if RELEASE
            AppDomain.CurrentDomain!.UnhandledException += Code.UnhandledException.OnUnhandledException;
#endif
            
            Languages.Load();
            
        }

        ~App()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"INFO | {typeof(App)} disposed, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "UEMM");
#endif
            Middleware.Dispose();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Middleware.Initialize();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Dispose();

            base.OnExit(e);
        }

        protected void DropIfAlreadyRunning()
        {
            var proc = Process.GetCurrentProcess();

            if (Process.GetProcesses().Count(p => p.ProcessName == proc.ProcessName) < 2)
                return;

            // Process name is different from main window title
            //var baseInstance = User32.FindWindow(null, "Cyberpunk 2077 Mod Manager");
            // if (baseInstance != IntPtr.Zero)
            //{
            // Set focus if alrady running.
            //  User32.ShowWindow(baseInstance, 1);
            //  User32.SetForegroundWindow(baseInstance);
            // }

            // Close current
            Shutdown();
        }
    }
}
