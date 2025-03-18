using System;
using System.Collections.Generic;
using ArendaAvto.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Supabase.Interfaces;

namespace ArendaAvto.ViewModels
{
    public class ShowTOViewModel : ViewModelBase
    {

        private readonly Supabase.Client _supabase; // Предполагается, что у вас есть клиент Supabase

        public List<CarMaintenanceInfo> CarsNeedingMaintenance { get; set; }

        public ShowTOViewModel(Supabase.Client supabase)
        {
            _supabase = supabase;
            CarsNeedingMaintenance = new List<CarMaintenanceInfo>();
            LoadCarsAsync();
        }

        private async Task LoadCarsAsync()
        {
            var cars = await GetCarsAsync();
            foreach (var car in cars)
            {
                // Здесь можно добавить логику для определения, нуждается ли машина в обслуживании
                // Например, если состояние машины не "хорошее"
                if (car.Sostoyanie != "Хорошее")
                {
                    CarsNeedingMaintenance.Add(new CarMaintenanceInfo
                    {
                        CarId = car.Id,
                        Comment = $"Машина {car.Marka} {car.Model} требует обслуживания."
                    });
                }
            }
        }

        public async Task<List<Cars>> GetCarsAsync()
        {
            try
            {
                // Подключение к Supabase
                await _supabase.InitializeAsync();
                // Выполнение запроса к таблице Cars
                var response = await _supabase.From<Cars>().Get();
                // Проверка на наличие ошибок
                if (response == null || response.Models == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }
                return response.Models; // Возвращаем список автомобилей
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<Cars>(); // Возвращаем пустой список в случае ошибки
            }
        }
        public void ToPageGlav()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }
        // Здесь можно добавить другие методы, если это необходимо
    }

    public class CarMaintenanceInfo
    {
        public int CarId { get; set; }
        public string Comment { get; set; }
    }
}


