using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;
using UEMM.Views;
using UEMM.Code;
using Lepo.i18n.DependencyInjection;
using UEMM.ViewModels;
using UEMM.Services;
using Lepo.i18n.Yaml;
using System.Windows.Threading;
using UEMM.Views.Pages;


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

                _ = b.FromYaml(Assembly.GetExecutingAssembly(), "Assets.Strings.en_US.yml", new CultureInfo("en-US"));
                //_ = b.FromJson(Assembly.GetExecutingAssembly(), "Resources.Translations-en-US.json", new CultureInfo("en-US"));

                //var cul = new CultureInfo((JsonConvert.DeserializeObject<LangInfo>(File.ReadAllText(@"CultureInfos.json")).CultureInfo));
                var cul = new CultureInfo("en-US");
                b.SetCulture(cul);

                //Application.Current.Resources["MainDirection"] = cul.EnglishName == "English (United States)" ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
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
            _ = services.AddSingleton<Dashboard>();
            _ = services.AddSingleton<DashboardViewModel>();


            _ = services.AddTransient<MainWindow>();
         _ = services.AddTransient<Dashboard>();

        }).Build();


        App()
        {
            DropIfAlreadyRunning();

#if RELEASE
            AppDomain.CurrentDomain!.UnhandledException += Code.UnhandledException.OnUnhandledException;
#endif
            
            //Languages.Load();
            
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
            _host.Start();

            Middleware.Initialize();

            base.OnStartup(e);
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            Dispose();

            base.OnExit(e);

            await _host.StopAsync();

            _host.Dispose();
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
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }

    }
}
