using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UEMM.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UEMM.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public MainWindow()
        {
            DataContext = this;
            //Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

            InitializeComponent();

            Loaded += (_, _) => RootNavigation.Navigate(typeof(Dashboard));
        }

        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService) => RootNavigation.SetPageService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        #endregion INavigationWindow methods
    }
}
