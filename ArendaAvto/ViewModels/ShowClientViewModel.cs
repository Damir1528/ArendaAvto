using System;
using System.Collections.Generic;
using ArendaAvto.Models;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace ArendaAvto.ViewModels
{
	public class ShowClientViewModel : ViewModelBase
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
        // создание списка для вывода
        public List<Client> _clientList;
        public List<Client> ClientList { get => _clientList; set => this.RaiseAndSetIfChanged(ref _clientList, value); }
        public ShowClientViewModel()
        {
            _ = LoadClientAsync();
        }
        private async Task LoadClientAsync()
        {
            if (MainWindowViewModel.Self != null)
            {
                ClientList = await MainWindowViewModel.Self.GetClientsAsync();
                TotalCar = ClientList.Count;
                SortCar = ClientList.Count;
                TotalCar = ClientList.Count;
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

        //фильтрация
        private string _selectedCarsFilter = null;

        string _searchClient;
        public string SearchCient
        {
            get => _searchClient;
            set
            {
                _searchClient = value;
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
            if (!string.IsNullOrWhiteSpace(_searchClient))
            {
                ClientList = ClientList.Where(x => $"{x.Firstname}".ToLower()
                                                                 .Contains(_searchClient.ToLower()))
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
                    ClientList = ClientList.Where(x => x.Bonus <= 100).ToList();
                }
                if (_komfort)
                {
                    ClientList = ClientList.Where(x => x.Bonus <= 500).ToList();
                }
                if (_sport)
                {
                    ClientList = ClientList.Where(x => x.Bonus > 500).ToList();
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
                        ClientList = ClientList.OrderByDescending(x => x.Secondname).ThenByDescending(x => x.Secondname).ToList();
                    }
                    else
                    {
                        ClientList = ClientList.OrderBy(x => x.Secondname).ThenBy(x => x.Secondname).ToList();
                    }
                    break;

            }
            // подсчет записй
            SortCar = ClientList.Count;
        }
    }
}