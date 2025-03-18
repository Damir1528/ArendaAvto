using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ArendaAvto.ViewModels;

namespace ArendaAvto;

public partial class ShowClient : UserControl
{
    public ShowClient()
    {
        InitializeComponent();
        DataContext = new ShowClientViewModel();
    }
}