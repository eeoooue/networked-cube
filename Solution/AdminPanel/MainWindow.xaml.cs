﻿using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.ResetCubeStrategies;
using LibCubeIntegration.ShuffleCubeStrategies;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IResetCubeStrategy ResetStrategy = new ResetCubeViaAPIStrategy("CubeProxy");
        private IShuffleCubeStrategy ShuffleStrategy = new ShuffleCubeViaAPIStrategy("CubeProxy");

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            await ResetStrategy.ResetCubeAsync();
        }

        private async void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            await ShuffleStrategy.ShuffleCubeAsync();
        }
    }
}