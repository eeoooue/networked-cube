using LibCubeIntegration.PerformMoveStrategies;
using System.Windows;
using System.Windows.Input;

namespace CubeManipulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MoveViaApiStrategy moveStrategy = new MoveViaApiStrategy("CubeProxy");

        private readonly Dictionary<Key, string> moveKeyMap = new()
        {
            { Key.U, "U" },
            { Key.L, "L" },
            { Key.B, "B" },
            { Key.M, "M" },
            { Key.F, "F" },
            { Key.R, "R" },
            { Key.D, "D" },
            { Key.S, "S" },
        };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new CubeManipulatorViewModel(); //Get ViewModel
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (moveKeyMap.TryGetValue(e.Key, out string? moveFound))
            {
                if (moveFound is string move)
                {
                    ProcessMove(move);
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            return;
        }

        private void ProcessMove(string x)
        {
            _ = moveStrategy.PerformMoveAsync(x);
        }
    }
}