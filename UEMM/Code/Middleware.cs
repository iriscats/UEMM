

using System;
using System.Reflection;
using System.Windows;

namespace UEMM.Code
{
    /// <summary>
    /// A class to separate additional functions from <see cref="Application"/>.
    /// </summary>
    internal class Middleware : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// Application settings.
        /// </summary>
        public readonly Core.Settings.Manager Settings = new();

        /// <summary>
        /// Custom shortcuts.
        /// </summary>
        //public readonly Core.Input.Shortcuts Shortcuts = new();

        /// <summary>
        /// Game instance.
        /// </summary>
        public readonly Core.Game.GameInstance GameInstance = new();

        ~Middleware()
        {
            Dispose();
        }

        public bool Initialize()
        {
            SetLanguage(Settings.Options.Language);

            // Shortcuts.Initialize(Application.Current.MainWindow!);
            //
            // if (!String.IsNullOrEmpty(Settings.Options.GameRootDirectory))
            //     GameInstance.Fetch(Settings.Options.GameRootDirectory);

            return true;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine(
                $"INFO | {typeof(Middleware)} disposed, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                "UEMM");
#endif
            //Lepo.i18n.Translator.Flush();
            // Dispose middleware resources.
        }

        public void SetLanguage(string language)
        {
            language = language.Trim();

            // Validate
            switch (language)
            {
                default:
                    // Lepo.i18n.Translator.SetLanguage(
                    //     Assembly.GetExecutingAssembly(),
                    //     "en_US",
                    //     "UEMM.Assets.Strings.en_US.yml",
                    //     false
               //     );
                    break;
            }
        }
    }
}