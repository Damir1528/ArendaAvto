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

        private readonly Supabase.Client _supabase; // ��������������, ��� � ��� ���� ������ Supabase

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
                // ����� ����� �������� ������ ��� �����������, ��������� �� ������ � ������������
                // ��������, ���� ��������� ������ �� "�������"
                if (car.Sostoyanie != "�������")
                {
                    CarsNeedingMaintenance.Add(new CarMaintenanceInfo
                    {
                        CarId = car.Id,
                        Comment = $"������ {car.Marka} {car.Model} ������� ������������."
                    });
                }
            }
        }

        public async Task<List<Cars>> GetCarsAsync()
        {
            try
            {
                // ����������� � Supabase
                await _supabase.InitializeAsync();
                // ���������� ������� � ������� Cars
                var response = await _supabase.From<Cars>().Get();
                // �������� �� ������� ������
                if (response == null || response.Models == null)
                {
                    throw new Exception($"������ ��������� ������: {response}");
                }
                return response.Models; // ���������� ������ �����������
            }
            catch (Exception ex)
            {
                Console.WriteLine($"��������� ������: {ex.Message}");
                return new List<Cars>(); // ���������� ������ ������ � ������ ������
            }
        }
        public void ToPageGlav()
        {
            MainWindowViewModel.Self.PageContent = new Glav();
        }
        // ����� ����� �������� ������ ������, ���� ��� ����������
    }

    public class CarMaintenanceInfo
    {
        public int CarId { get; set; }
        public string Comment { get; set; }
    }
}


