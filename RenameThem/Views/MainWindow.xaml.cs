using System;
using System.Linq;
using System.Windows.Input;
using RenameThem.ViewModels;

namespace RenameThem.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var args = Environment.GetCommandLineArgs();
            var directory = args.Count() == 2 ? args[1] : null;

            DataContext = new MainViewModel(directory);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}