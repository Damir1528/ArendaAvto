using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ArendaAvto.ViewModels;

namespace ArendaAvto;

public partial class ShowStatistic : UserControl
{
    public ShowStatistic()
    {
        InitializeComponent();
        DataContext = new ShowStatisticViewModel();
    }
}