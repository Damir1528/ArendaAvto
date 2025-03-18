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
        bool _prover = false;
        public bool prover
        {
            get => _prover;
            set => this.RaiseAndSetIfChanged(ref _prover, value);
        }
        bool _autorization = true;
        public bool autorization
        {
            get => _autorization;
            set => this.RaiseAndSetIfChanged(ref _autorization, value);
        }
        public bool _enabled = true;
        public bool enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _enabled, value);
            }
        }
        public List<User> _userList;
        public List<User> UserList { get => _userList; set => this.RaiseAndSetIfChanged(ref _userList, value); }
        public AuthViewModel()
        {
            _ = LoadArendaAsync();
        }
        private async Task LoadArendaAsync()
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
        //public async void ToPageOknoOrg()
        //{
        //    await Task.Delay(4000);
        //    MainWindowViewModel.Self.PageContent = new Glav();
        //}
    }
}