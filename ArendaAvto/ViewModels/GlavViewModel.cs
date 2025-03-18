using System;
using System.Collections.Generic;
using ReactiveUI;

namespace ArendaAvto.ViewModels
{
	public class GlavViewModel : ViewModelBase
	{
        public void ToPageShowCars()
        {
            MainWindowViewModel.Self.PageContent = new ShowCars();
        }
        public void ToPageShowArenda()
        {
            MainWindowViewModel.Self.PageContent = new ShowArenda();
        }
        public void ToPageShowClient()
        {
            MainWindowViewModel.Self.PageContent = new ShowClient();
        }
        public void ToPageShowStatistic()
        {
            MainWindowViewModel.Self.PageContent = new ShowStatistic();
        }
        public void ToPageShowTO()
        {
            MainWindowViewModel.Self.PageContent = new ShowTO();
        }
    }
}