namespace UEMM.Code;

internal static class Global
{
    public static string Version =>
        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString();

    public static App App => Application.Current as App ??
                             throw new NullReferenceException(
                                 "ERROR | The main application instance does not exist.");
    
    
    
    
}