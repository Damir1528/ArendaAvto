using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ArendaAvto.Models;

namespace ArendaAvto.ViewModels
{
	public class ShowArendaViewModel : ViewModelBase
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
        public List<Arenda> _arendaList;
        public List<Arenda> ArendaList { get => _arendaList; set => this.RaiseAndSetIfChanged(ref _arendaList, value); }
        public ShowArendaViewModel()
        {
            _ = LoadArendaAsync();
        }
        private async Task LoadArendaAsync()
        {
            if (MainWindowViewModel.Self != null)
            {
                ArendaList = await MainWindowViewModel.Self.GetArendsAsync();
                TotalCar = ArendaList.Count;
                SortCar = ArendaList.Count;
                TotalCar = ArendaList.Count;
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
                ArendaList = ArendaList.Where(x => $"{x.Client.Firstname}".ToLower()
                                                                 .Contains(_searchClient.ToLower()))
                                          .ToList();
            }
            //радио баттоны
            if (_all)
            {

            }
            else
            {
                if (_biznes)
                {
                                    ArendaList = ArendaList
                        .Where(x =>
                        {
                            DateTime dataVidachi, dataVozvrat;
                            bool isDataVidachiValid = DateTime.TryParse(x.DataVidachi, out dataVidachi);
                            bool isDataVozvratValid = DateTime.TryParse(x.DataVozvrat, out dataVozvrat);
                            if (isDataVidachiValid && isDataVozvratValid)
                            {
                                return (dataVozvrat - dataVidachi).TotalDays <= 1;
                            }
                            return false; 
                        })
                        .ToList();
                }
                if (_komfort)
                {
                                        ArendaList = ArendaList
                        .Where(x =>
                        {
                            DateTime dataVidachi, dataVozvrat;
                            bool isDataVidachiValid = DateTime.TryParse(x.DataVidachi, out dataVidachi);
                            bool isDataVozvratValid = DateTime.TryParse(x.DataVozvrat, out dataVozvrat);

                            if (isDataVidachiValid && isDataVozvratValid)
                            {
                                double daysDifference = (dataVozvrat - dataVidachi).TotalDays;
                                return daysDifference > 3 && daysDifference < 7; 
                            }
                            return false; 
                        })
                        .ToList();
                }
                if (_sport)
                {
                    ArendaList = ArendaList
                        .Where(x =>
                        {
                            DateTime dataVidachi, dataVozvrat;
                            bool isDataVidachiValid = DateTime.TryParse(x.DataVidachi, out dataVidachi);
                            bool isDataVozvratValid = DateTime.TryParse(x.DataVozvrat, out dataVozvrat);
                            if (isDataVidachiValid && isDataVozvratValid)
                            {
                                return (dataVozvrat - dataVidachi).TotalDays > 7;
                            }
                            return false;
                        })
                        .ToList();
                }
            }
            //сортировка
            switch (_selectedSort)
            {
                case 0:
                    IsVisibleSort = false;
                    break;

                case 1:
                    IsVisibleSort = true;
                    if (_sortDown)
                    {
                        ArendaList = ArendaList.OrderByDescending(x => x.Client.Secondname).ThenByDescending(x => x.Client.Secondname).ToList();
                    }
                    else
                    {
                        ArendaList = ArendaList.OrderBy(x => x.Client.Secondname).ThenBy(x => x.Client.Secondname).ToList();
                    }
                    break;

            }
            //подсчет записй
            SortCar = ArendaList.Count;
        }
    }
}