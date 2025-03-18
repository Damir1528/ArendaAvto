using System;
using System.Collections.Generic;
using ArendaAvto.Models;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using Tmds.DBus.Protocol;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.IO;
using System.Net.Http;

namespace ArendaAvto.ViewModels
{
	public class ShowCarsViewModel : ViewModelBase
	{

        
        // для подсчета записей
        private int _sortCar;
        public int SortCar
        {
            get => _sortCar;
            set => this.RaiseAndSetIfChanged(ref _sortCar, value);
        }
        private int _totalCar;
        public int TotalCar
        {
            get => _totalCar;
            set => this.RaiseAndSetIfChanged(ref _totalCar, value);
        }

        private bool _ButtonVisibleCars;
        public bool ButtonVisibleCars
        {
            get => _ButtonVisibleCars;
            set => this.RaiseAndSetIfChanged(ref _ButtonVisibleCars, value);
        }
        public static ShowCarsViewModel Sels;
        // создание списка для вывода
        public List<Cars> _carsList;
        public List<Cars> CarsList { get => _carsList; set => this.RaiseAndSetIfChanged(ref _carsList, value); }
        public ShowCarsViewModel()
        {
            ButtonVisibleCars = true;
            Sels = this;
            _ = LoadCarsAsync();
        }
        private async Task LoadCarsAsync()
        {
            if (MainWindowViewModel.Self != null)
            {
                CarsList = await MainWindowViewModel.Self.GetCarsAsync();
                TotalCar = CarsList.Count;
                SortCar = CarsList.Count;
                TotalCar = CarsList.Count;
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Окно", "MainWindowViewModel.Self не инициализирован.", ButtonEnum.Ok).ShowAsync();
            }
        }

        

        
        public void ToPageGlav()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }

        public void ToPageCarsAdd()
        {
            MainWindowViewModel.Self.PageContent = new CarsAdd();
        }

        //фильтрация
        private string _selectedCarsFilter = null;

        string _searchCars;
        public string SearchCars
        {
            get => _searchCars;
            set
            {
                _searchCars = value;
                AllFilters();
            }
        }

        // радио баттоны
        bool _all = true;
        public bool All { get => _all; set { this.RaiseAndSetIfChanged(ref _all, value); AllFilters(); } }
        bool _biznes = false;
        public bool Biznes { get => _biznes; set { this.RaiseAndSetIfChanged(ref _biznes, value); AllFilters(); } }
        bool _komfort = false;
        public bool Komfort { get => _komfort; set { this.RaiseAndSetIfChanged(ref _komfort, value); AllFilters(); } }
        bool _sport = false;
        public bool Sport { get => _sport; set { this.RaiseAndSetIfChanged(ref _sport, value); AllFilters(); } }

        // для сортировки

        int _selectedSort = 0;
        public int SelectedSort { get => _selectedSort; set { _selectedSort = value; AllFilters(); } }

        bool _isVisibleSort = false;
        public bool IsVisibleSort { get => _isVisibleSort; set => this.RaiseAndSetIfChanged(ref _isVisibleSort, value); }

        bool _sortUp = true;
        public bool SortUp { get => _sortUp; set { this.RaiseAndSetIfChanged(ref _sortUp, value); AllFilters(); } }

        bool _sortDown = false;
        public bool SortDown { get => _sortDown; set { this.RaiseAndSetIfChanged(ref _sortDown, value); AllFilters(); } }
        void AllFilters()
        {
            // поисковая строка
            if (!string.IsNullOrWhiteSpace(_searchCars))
            {
                CarsList = CarsList.Where(x => $"{x.Marka}".ToLower()
                                                                 .Contains(_searchCars.ToLower()))
                                          .ToList();
            }
            // радио баттоны
            if (_all)
            {

            }
            else
            {
                if (_biznes)
                {
                    CarsList = CarsList.Where(x => x.Class == "Бизнес").ToList();
                }
                if (_komfort)
                {
                    CarsList = CarsList.Where(x => x.Class == "Комфорт").ToList();
                }
                if (_sport)
                {
                    CarsList = CarsList.Where(x => x.Class == "Спорт").ToList();
                }
            }
            // сортировка
            switch (_selectedSort)
            {
                case 0:
                    IsVisibleSort = false;
                    break;

                case 1:
                    IsVisibleSort = true;
                    if (_sortDown)
                    {
                        CarsList = CarsList.OrderByDescending(x => x.Marka).ThenByDescending(x => x.Marka).ToList();
                    }
                    else
                    {
                        CarsList = CarsList.OrderBy(x => x.Marka).ThenBy(x => x.Marka).ToList();
                    }
                    break;

            }
            // галочка с ролью
            if (_photo)
            {
                // Фильтруем список, оставляя только автомобили с фотографией
                CarsList = CarsList.Where(x => !string.IsNullOrEmpty(x.Photo)).ToList();
            }

            if (_unphoto)
            {
                // Фильтруем список, оставляя только автомобили без фотографии
                CarsList = CarsList.Where(x => string.IsNullOrEmpty(x.Photo)).ToList();
            }

            // подсчет записй
            SortCar = CarsList.Count;
        }

        // с
        bool _photo = false;
        public bool Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                AllFilters();
            }
        }
        // без
        bool _unphoto = false;
        public bool UnPhoto
        {
            get => _unphoto;
            set
            {
                _unphoto = value;
                AllFilters();
            }
        }
    }
}