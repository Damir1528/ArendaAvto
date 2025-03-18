using ArendaAvto.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Text;
using System.Threading.Tasks;
using System;
using ArendaAvto.Models;
using Intersoft.Crosslight;

namespace ArendaAvto;

public partial class Auth : UserControl
{
    private AuthViewModel _captchaViewModel;
    public Auth()
    {
        InitializeComponent();
        DataContext = new AuthViewModel();
    }
}