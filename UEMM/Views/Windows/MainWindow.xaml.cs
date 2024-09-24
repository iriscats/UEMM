using System.Diagnostics;
using UEMM.ViewModels.Windows;


namespace UEMM.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        // ViewModel = viewModel;
        // DataContext = this;
        //
        // SystemThemeWatcher.Watch(this);
        //
        // InitializeComponent();
        // SetPageService(pageService);
        //
        // navigationService.SetNavigationControl(RootNavigation);
    }

    /// <summary>
    /// Raises the closed event.
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        Process.GetCurrentProcess().Close();
    }


}