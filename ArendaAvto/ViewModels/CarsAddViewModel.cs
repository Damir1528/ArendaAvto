using System;
using System.Collections.Generic;
using ArendaAvto.Models;
using MsBox.Avalonia.ViewModels.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReactiveUI;
using System.Threading.Tasks;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;

namespace ArendaAvto.ViewModels
{
	public class CarsAddViewModel : ViewModelBase
	{
        private readonly Supabase.Client supabase;

        private Cars _newCar;
        public Cars NewCar
        {
            get => _newCar;
            set => this.RaiseAndSetIfChanged(ref _newCar, value, nameof(NewCar));
        }

        //public ICommand AddCarCommand { get; }

        public CarsAddViewModel()
        {
            supabase = new Supabase.Client("https://hprxdibkvadmmsvxyavr.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhwcnhkaWJrdmFkbW1zdnh5YXZyIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjU2MDY5NzMsImV4cCI6MjA0MTE4Mjk3M30.zSV2quIa7GnDGsMyvwQEhiAeehri9_JyIpwqYto91A8");
            NewCar = new Cars();
            //AddCarCommand = new RelayCommand(async (param) => await AddCarAsync());
        }
        public void ToPageGlav()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }
        public async Task AddCarAsync()
        {
            try
            {
                // Убедитесь, что Id не установлен, чтобы база данных могла сгенерировать его
                NewCar.Id = 0; // или просто не устанавливайте Id, если это возможно

                await supabase.From<Cars>().Upsert(NewCar);
                await MessageBoxManager.GetMessageBoxStandard("Окно", "Запись успешно добавлена", ButtonEnum.Ok).ShowAsync();
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("Окно", "Ошибка добавления записи", ButtonEnum.Ok).ShowAsync();
            }
        }

    }
}