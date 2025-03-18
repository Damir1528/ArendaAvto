using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ArendaAvto.Models;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using Tmds.DBus.Protocol;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel;
using ArendaAvto.Models;
using System.Runtime.CompilerServices;
using Intersoft.Crosslight;
using Supabase.Gotrue;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using User = ArendaAvto.Models.User;

namespace ArendaAvto.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {

        private string _userName;
        private string _password;
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public List<User> _userList;
        public List<User> UserList { get => _userList; set => this.RaiseAndSetIfChanged(ref _userList, value); }
        public AuthViewModel()
        {
            _ = LoadUsersAsync();
            EnterButtonEnabled = true;
            EnterButtonCommand = ReactiveCommand.CreateFromTask(ExecuteLogin);
            ToShowCommand = ReactiveCommand.Create(ExecuteToShow);

        }
        private async Task LoadUsersAsync()
        {
            if (MainWindowViewModel.Self != null)
            {
                UserList = await MainWindowViewModel.Self.GetUsersAsync();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Окно", "MainWindowViewModel.Self не инициализирован.", ButtonEnum.Ok).ShowAsync();
            }
        }
        private bool _enterButtonEnabled;
        public bool EnterButtonEnabled
        {
            get => _enterButtonEnabled;
            set => this.RaiseAndSetIfChanged(ref _enterButtonEnabled, value);
        }
        private bool _enterButtonVisible;
        public bool EnterButtonVisible
        {
            get => _enterButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _enterButtonVisible, value);
        }
        private string _captchaText;
        public string captchaText
        {
            get => _captchaText;
            set => this.RaiseAndSetIfChanged(ref _captchaText, value);
        }

        private string _captcha;
        public string captcha
        {
            get => _captcha;
            set => this.RaiseAndSetIfChanged(ref _captcha, value);
        }
        public ICommand EnterButtonCommand { get; }


        public ICommand ToShowCommand { get; }

        private void ExecuteToShow()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }
        public async Task ExecuteLogin()
        {
            EnterButtonEnabled = true; 
            EnterButtonVisible = false;
            var user = UserList.FirstOrDefault(u => u.Name == UserName && u.Password == Password);
            if (user != null && captchaText == captcha)
            {
                // Проверяем RoleId для авторизованного пользователя
                if (user.RoleId == 1)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Окно", "Вы авторизовались под администратором", ButtonEnum.Ok).ShowAsync();
                    MainWindowViewModel.Self.PageContent = new Glav();
                    GlavViewModel.Selg.ButtonVisibleGlav = true;
                    ShowCarsViewModel.Sels.ButtonVisibleCars = true;
                }
                else if (user.RoleId == 2)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Окно", "Вы авторизовались под пользователем", ButtonEnum.Ok).ShowAsync();
                    MainWindowViewModel.Self.PageContent = new Glav();
                    GlavViewModel.Selg.ButtonVisibleGlav = false;
                    ShowCarsViewModel.Sels.ButtonVisibleCars = false;
                }
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Окно", "Вы не верно ввели капчу", ButtonEnum.Ok).ShowAsync();
                EnterButtonEnabled = false; 
                EnterButtonVisible = true;
                GenerateCaptcha();
                await Task.Delay(10000); 
                EnterButtonEnabled = true; 
            }
        }
        private void GenerateCaptcha()
        {
            captcha = GenerateRandomString(6); 
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

    }
}