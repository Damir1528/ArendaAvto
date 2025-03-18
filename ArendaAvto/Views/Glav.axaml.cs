using ArendaAvto.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArendaAvto;

public partial class Glav : UserControl
{
    public Glav()
    {
        InitializeComponent();
        DataContext = new GlavViewModel();
    }
}