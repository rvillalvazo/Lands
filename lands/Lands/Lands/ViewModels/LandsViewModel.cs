
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Models;
    using Services;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using System.Linq;

    public class LandsViewModel:BaseViewModel
    {
        #region Services
        private ApiService _apiService;
        #endregion

        #region Variables
        private ObservableCollection<Country> _countries;
        private bool _isRefreshing = false;
        private string _filter = String.Empty;
        private List<Country> _countriesList=null;
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

        public bool IsRefreshing
        {
            get
            {
                return this._isRefreshing;
            }
            set
            {
                SetValue(ref this._isRefreshing, value);
            }
        }

        public string Filter
        {
            get
            {
                return this._filter;
            }
            set
            {
                SetValue(ref this._filter, value);
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

            this.IsRefreshing = true;

            var conectionstatus= await _apiService.CheckConnection();

            if (conectionstatus.IsSuccess)
            {
                var response = await this._apiService.GetList<Country>("http://restcountries.eu"
                                                                      , "/rest"
                                                                      , "/v2/all");

                if (!response.IsSuccess)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                    return;
                }

                this._countriesList = (List<Country>)response.Result;
                this.Countries = new ObservableCollection<Country>(this._countriesList);
                this.IsRefreshing = false;
            }
            else
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", conectionstatus.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCountries);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        public async void Search()
        {
            if(String.IsNullOrEmpty(this._filter))
            {
                this.Countries = new ObservableCollection<Country>(this._countriesList);
            }
            else
            {
                this.Countries = new ObservableCollection<Country>(this._countriesList.Where(country=> country.Name.ToLower().Contains(this._filter.ToLower())));
            }
        }
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        public async void SelectLand()
        {
            //await Application.Current.MainPage.DisplayAlert("País Seleccionado", "", "Accept");

            //return;
        }
        #endregion
    }
}
