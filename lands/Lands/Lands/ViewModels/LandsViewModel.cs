
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel:BaseViewModel
    {
        #region Services
        private ApiService _apiService;
        #endregion

        #region Variables
        private ObservableCollection<Country> _countries;
        #endregion

        #region Propiedades
        public ObservableCollection<Country> Countries
        {
            get
            {
                return _countries;
            }

            set
            {
                SetValue(ref this._countries, value);
            }
        }
        #endregion

        #region Constructor
        public LandsViewModel()
        {
            this._apiService = new ApiService();
            this.LoadCountries();
        }
        #endregion

        #region Metodos
        private async void LoadCountries()
        {
            //http://restcountries.eu/rest/v2/all
            var conectionstatus= await _apiService.CheckConnection();

            if (conectionstatus.IsSuccess)
            {
                var response = await this._apiService.GetList<Country>("http://restcountries.eu"
                                                                      , "/rest"
                                                                      , "/v2/all");

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                    return;
                }

                var lista = (List<Country>)response.Result;
                this.Countries = new ObservableCollection<Country>(lista);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", conectionstatus.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }
        }
        #endregion
    }
}
