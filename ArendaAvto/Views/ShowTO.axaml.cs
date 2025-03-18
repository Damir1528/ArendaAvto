using ArendaAvto.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArendaAvto;

public partial class ShowTO : UserControl
{
    public ShowTO()
    {
        InitializeComponent();
        DataContext = new ShowTOViewModel(MainWindowViewModel.Self._supabase);
    }

}