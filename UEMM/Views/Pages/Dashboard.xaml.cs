﻿

using UEMM.Code;
using Lepo.i18n;
using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;
using UEMM.ViewModels;
using UEMM.ViewModels.Pages;

namespace UEMM.Views.Pages
{
    internal class DashboardData : ViewData
    {
        private int _installedMods = 0;
        public int InstalledMods
        {
            get => _installedMods;
            set => UpdateProperty(ref _installedMods, value, nameof(InstalledMods));
        }

        private string _gameVersion = Translator.String("global.unknown");
        public string GameVersion
        {
            get => _gameVersion;
            set => UpdateProperty(ref _gameVersion, value, nameof(GameVersion));
        }

        private string _managerVersion = Translator.String("global.unknown");
        public string ManagerVersion
        {
            get => _managerVersion;
            set => UpdateProperty(ref _managerVersion, value, nameof(ManagerVersion));
        }

    }

    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard  : INavigableView<DashboardViewModel>
    {
        internal DashboardData DashboardDataStack { get; } = new();

        public DashboardViewModel ViewModel =>  new DashboardViewModel();

        public Dashboard()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            DataContext = DashboardDataStack;

            DashboardDataStack.InstalledMods = 0;
            DashboardDataStack.ManagerVersion = GH.Version;

            if (!String.IsNullOrEmpty(GH.Game.ProductVersion))
                DashboardDataStack.GameVersion = GH.Game.ProductVersion;
        }

        private void ButtonAction_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Wpf.Ui.Controls.CardAction control) return;

            switch (control.Tag.ToString())
            {
                case "list":
                    GH.Navigate("list");
                    break;

                case "add":
                    GH.Navigate("install");
                    break;

                case "help":
                    GH.Navigate("help");
                    break;
            }
        }
    }
}
