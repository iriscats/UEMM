using Avalonia.Interactivity;
using UEMM.Code;
using UEMM.ViewModels.Pages;


namespace UEMM.Views.Pages;

public partial class DashboardPage
{
    public DashboardViewModel ViewModel { get; }

    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        // DataContext = this;
        //
        // InitializeComponent();
    }


    private void ButtonAction_OnClick(object sender, RoutedEventArgs e)
    {
        //if (sender is not Wpf.Ui.Controls.CardAction control) return;

        //switch (control.Tag.ToString())
        {
            // case "list":
            //     GH.Navigate("list");
            //     break;
            //
            // case "add":
            //     GH.Navigate("install");
            //     break;
            //
            // case "help":
            //     GH.Navigate("help");
            //     break;
        }
    }
}