namespace UEMM.Models;

public class AppConfig
{
    public string ConfigurationsFolder { get; set; }

    public string AppPropertiesFileName { get; set; }
    
    public string CultureInfo { get; set; } = default!;
}