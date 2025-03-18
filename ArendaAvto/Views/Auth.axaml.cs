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
        GenerateCaptcha();
    }
    private string captcha;
    private void GenerateCaptcha()
    {
        captcha = GenerateRandomString(6); // ���������� ������ ������ 6 ��������
        captchaText.Text = captcha; // ���������� ����� �� ������
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder result = new StringBuilder();
        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }
        return result.ToString();
    }
    public async void autorization(object sender, RoutedEventArgs e)
    {
        AuthViewModel page1 = new AuthViewModel();
        page1.prover = false;
        string login2 = login.Text;
        string password2 = password.Text;
        page1.autorization = true;
        if (login2 == "admin" && password2 == "admin1" && captchaInput.Text == captcha)
        {
            ShowMessage.Text = "�� ������� ��������������! \n ��������� ��������� ������";
            //EnterButton1.IsEnabled = true;
            //EnterButton1.IsVisible = true;
            ChangeMessageAdd(login2);
        }
        else if (captchaInput.Text != captcha)
        {
            await MessageBoxManager.GetMessageBoxStandard("����", "�� ����� �������� �����", ButtonEnum.Ok).ShowAsync();
            EnterButton.IsEnabled = false; // ��������� ������ �����������
            await Task.Delay(10000); // ���� 10 ������
            EnterButton.IsEnabled = true; // ������������ ������
            GenerateCaptcha(); // ���������� ����� �����
            captchaInput.Clear(); // ������� ���� ����� �����
            return;
        }
        else
        {
            ShowMessage.Text = "�������� ����� ��� ������";
            EnterButton.IsEnabled = false;
            EnterButton.IsVisible = false;
        }
    }

    private async void ChangeMessageAdd(string role)
    {
        await MessageBoxManager.GetMessageBoxStandard("����", "�� ������� ��������������", ButtonEnum.Ok).ShowAsync();
        await Task.Delay(4000);
        MainWindowViewModel.Self.PageContent = new Glav();
    }
}