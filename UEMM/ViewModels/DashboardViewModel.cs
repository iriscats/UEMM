using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using Wpf.Ui.Controls;

namespace UEMM.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }
}
