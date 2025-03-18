using ArendaAvto.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArendaAvto;

public partial class CarsAdd : UserControl
{
    public CarsAdd()
    {
        InitializeComponent();
        DataContext = new CarsAddViewModel();
    }
}