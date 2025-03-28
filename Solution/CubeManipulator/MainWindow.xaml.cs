using System.Windows;

namespace CubeManipulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new CubeManipulatorViewModel(); //Get ViewModel
        }
    }
}