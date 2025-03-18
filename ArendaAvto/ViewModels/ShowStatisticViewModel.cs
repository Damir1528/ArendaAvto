using System;
using System.Collections.Generic;
using ArendaAvto.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ArendaAvto.ViewModels
{
    public class ShowStatisticViewModel : ViewModelBase
    {
        private ObservableCollection<Arenda> _rentals;
        private double _averageCost;
        private double _totalCost;

        public ObservableCollection<Arenda> Rentals
        {
            get => _rentals;
            set => this.RaiseAndSetIfChanged(ref _rentals, value);
        }

        public double AverageCost
        {
            get => _averageCost;
            set => this.RaiseAndSetIfChanged(ref _averageCost, value);
        }

        public double TotalCost
        {
            get => _totalCost;
            set => this.RaiseAndSetIfChanged(ref _totalCost, value);
        }

        public ShowStatisticViewModel()
        {
            Rentals = new ObservableCollection<Arenda>();
            LoadData();
        }

        private async void LoadData()
        {
            var rentals = await GetRentalsFromDatabase();

            Rentals.Clear();
            foreach (var rental in rentals)
            {
                Rentals.Add(rental);
            }

            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            TotalCost = Rentals.Sum(r => r.Cost);
            AverageCost = Rentals.Count > 0 ? Rentals.Average(r => r.Cost) : 0;
        }

        private async Task<List<Arenda>> GetRentalsFromDatabase()
        {
            return await MainWindowViewModel.Self.GetArendsAsync();
        }

        public void ToPageGlav()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }
    }
}

