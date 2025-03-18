using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ArendaAvto.ViewModels;

namespace ArendaAvto;

public partial class ShowCars : UserControl
{
    public ShowCars()
    {
        InitializeComponent();
        DataContext = new ShowCarsViewModel();
    }
}