using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ArendaAvto.ViewModels;

namespace ArendaAvto;

public partial class ShowArenda : UserControl
{
    public ShowArenda()
    {
        InitializeComponent();
        DataContext = new ShowArendaViewModel();
    }
}