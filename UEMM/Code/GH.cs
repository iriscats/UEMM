using Avalonia.Threading;

namespace UEMM.Code;

/// <summary>
/// Static global hook.
/// </summary>
internal static class GH
{
    /// <summary>
    /// Main application version.
    /// </summary>
    public static string Version =>
        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString() ?? "1.0.0";

    /// <summary>
    /// Application settings.
    /// </summary>
   // public static Core.Settings.Manager Settings => (Application.Current as App)!.Middleware.Settings;

    /// <summary>
    /// Application settings.
    /// </summary>
    //public static Core.Game.IGame Game => (Application.Current as App)!.Middleware.GameInstance;

    /// <summary>
    /// Returns the instance of the current <see cref="System.Windows.Application"/>.
    /// </summary>
    public static App App => Application.Current as App ??
                             throw new NullReferenceException(
                                 "ERROR | The main application instance does not exist.");

    /// <summary>
    /// Changes <see cref="Lepo.i18n.Translator.Current"/> language.
    /// </summary>
    /// <param name="language">Language code.</param>
    // public static void SetLanguage(string language) =>
    //     (Application.Current as App)!.Middleware.SetLanguage(language);


    /// <summary>
    /// Synchronously executes delegated action on UI thread.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    //public static void Dispatch(Action callback) => Application.Current.de.Invoke(callback);

    /// <summary>
    /// Asynchronously executes delegated action on UI thread.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    // public static DispatcherOperation DispatchAsync(Action callback) =>
    //     Application.de.InvokeAsync(callback);
}