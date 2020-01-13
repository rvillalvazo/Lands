
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;

    public class CountryItemViewModel:Country
    {
        #region Comandos
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        public async void SelectLand()
        {
            MainViewModel.GetInstance().Country = new CountryViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountryPage());
        }
        #endregion
    }
}
