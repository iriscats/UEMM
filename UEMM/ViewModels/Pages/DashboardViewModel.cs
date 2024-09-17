

namespace UEMM.ViewModels.Pages;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty]
    private int _installMod = 0;


    [ObservableProperty]
    private int _totalMod = 0;


    [ObservableProperty]
    private string _gameVersion = "unknown";


    [ObservableProperty]
    private string _frameworkVersion = "unknown";
}
