

//using UEMM.Core.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace UEMM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : IDisposable
    {
        private bool _disposed = false;

        internal readonly Code.Middleware Middleware = new();

        App()
        {
            DropIfAlreadyRunning();

#if RELEASE
            AppDomain.CurrentDomain!.UnhandledException += Code.UnhandledException.OnUnhandledException;
#endif
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
