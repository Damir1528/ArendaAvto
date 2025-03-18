using ArendaAvto.ViewModels;
using Avalonia.Controls;

namespace ArendaAvto.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}