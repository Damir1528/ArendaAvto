using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ArendaAvto.Models;
using ReactiveUI;
using Avalonia.Controls;
using System.Linq;

namespace ArendaAvto.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        UserControl _pageContent;

        public UserControl PageContent
        {
            get => _pageContent;
            set => this.RaiseAndSetIfChanged(ref _pageContent, value);
            
        }

        public static MainWindowViewModel Self;
        public readonly Supabase.Client _supabase; // Добавляем поле для клиента Supabase

        public  MainWindowViewModel()
        {
            Self = this;
            string url = "https://hprxdibkvadmmsvxyavr.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhwcnhkaWJrdmFkbW1zdnh5YXZyIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjU2MDY5NzMsImV4cCI6MjA0MTE4Mjk3M30.zSV2quIa7GnDGsMyvwQEhiAeehri9_JyIpwqYto91A8";

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
            {
                // Обработка ошибки: ключи окружения не найдены
                throw new Exception("SUPABASE_URL or SUPABASE_KEY environment variables are not set.");
            }
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };
            _supabase = new Supabase.Client(url, key, options);
            InitializeAsync().ConfigureAwait(false); // Запускаем асинхронную инициализацию
            //PageContent = new ShowCars();
            PageContent = new Auth();

        }

        private async Task InitializeAsync()
        {
            try
            {
                await _supabase.InitializeAsync();
                Console.WriteLine("Supabase initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Supabase: {ex.Message}");
            }
        }
        // User
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                // Подключение к Supabase
                await _supabase.InitializeAsync();
                // Выполнение запроса к таблице User
                var response = await _supabase.From<User>().Get();
                // Проверка на наличие ошибок
                if (response == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }
                var users = response.Models; // Возвращаем список пользователей

                // Загрузка связанных объектов
                var user1 = await GetRolesAsync();

                // Загрузка связанных объектов по идентификаторам
                foreach (var user in users)
                {
                    // Находим соответствующий объект Car по CarId
                    user.role = user1.FirstOrDefault(role => role.Id == user.RoleId);
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<User>(); // Возвращаем пустой список в случае ошибки
            }
        }
        // Role
        public async Task<List<Role>> GetRolesAsync()
        {
            try
            {
                // Подключение к Supabase
                await _supabase.InitializeAsync();
                // Выполнение запроса к таблице User
                var response = await _supabase.From<Role>().Get();
                // Проверка на наличие ошибок
                if (response == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }
                return response.Models; // Возвращаем список пользователей
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<Role>(); // Возвращаем пустой список в случае ошибки
            }
        }
        // Cars
        public async Task<List<Cars>> GetCarsAsync()
        {
            try
            {
                // Подключение к Supabase
                await _supabase.InitializeAsync();
                // Выполнение запроса к таблице User
                var response = await _supabase.From<Cars>().Get();
                // Проверка на наличие ошибок
                if (response == null && response.Models == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }
                return response.Models; // Возвращаем список пользователей
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<Cars>(); // Возвращаем пустой список в случае ошибки
            }
        }
        // Client
        public async Task<List<Client>> GetClientsAsync()
        {
            try
            {
                // Подключение к Supabase
                await _supabase.InitializeAsync();
                // Выполнение запроса к таблице User
                var response = await _supabase.From<Client>().Get();
                // Проверка на наличие ошибок
                if (response == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }
                return response.Models; // Возвращаем список пользователей
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<Client>(); // Возвращаем пустой список в случае ошибки
            }
        }

        // Arenda
        public async Task<List<Arenda>> GetArendsAsync()
        {
            try
            {
                await _supabase.InitializeAsync();
                var response = await _supabase.From<Arenda>().Get();
                if (response == null || response.Models == null)
                {
                    throw new Exception($"Ошибка получения данных: {response}");
                }

                var arendas = response.Models;

                // Загрузка связанных объектов
                var cars = await GetCarsAsync();
                var clients = await GetClientsAsync();

                // Загрузка связанных объектов по идентификаторам
                foreach (var arenda in arendas)
                {
                    // Находим соответствующий объект Car по CarId
                    arenda.Car = cars.FirstOrDefault(car => car.Id == arenda.CarId);
                    // Находим соответствующий объект Client по ClientId
                    arenda.Client = clients.FirstOrDefault(client => client.Id == arenda.ClientId);
                }

                return arendas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return new List<Arenda>();
            }
        }


    }
}
