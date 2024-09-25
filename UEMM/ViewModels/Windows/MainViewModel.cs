using System.Collections.Generic;
using UEMM.Models;

namespace UEMM.ViewModels.Windows;

public class MainViewModel : ObservableObject
{
    public List<ModInfo> People { get; } = new()
    {
        new ModInfo()
        {
            Name = "text"
        },
        new ModInfo()
        {
            Name = "text"
        },
        new ModInfo()
        {
            Name = "text"
        }
    };

    public MainViewModel()
    {
    }
}