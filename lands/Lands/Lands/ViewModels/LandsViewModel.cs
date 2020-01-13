
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
        private ObservableCollection<CountryItemViewModel> _countries;
        private bool _isRefreshing = false;
        private string _filter = String.Empty;
        private List<Country> _countriesList=new List<Country>();
        #endregion

        #region Propiedades
        public ObservableCollection<CountryItemViewModel> Countries
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
                this.Search();
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
                this.Countries = new ObservableCollection<CountryItemViewModel>(this.ToCountryItemViewModel());
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

        #region Metodos
        private IEnumerable<CountryItemViewModel> ToCountryItemViewModel()
        {
            return this._countriesList.Select(c => new CountryItemViewModel
            {
                Name = c.Name,
                TopLevelDomain = c.TopLevelDomain,
                Alpha2Code = c.Alpha2Code,
                Alpha3Code = c.Alpha3Code,
                CallingCodes = c.CallingCodes,
                Capital = c.Capital,
                AltSpellings = c.AltSpellings,
                Region = c.Region,
                Subregion = c.Subregion,
                Population = c.Population,
                Latlng = c.Latlng,
                Demonym = c.Demonym,
                Area = c.Area,
                Gini = c.Gini,
                Timezones = c.Timezones,
                Borders = c.Borders,
                NativeName = c.NativeName,
                NumericCode = c.NumericCode,
                Currencies = c.Currencies,
                Languages = c.Languages,
                Translations = c.Translations,
                Flag = c.Flag,
                RegionalBlocs = c.RegionalBlocs,
                Cioc = c.Cioc
            }).ToList();
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
                this.Countries = new ObservableCollection<CountryItemViewModel>(this.ToCountryItemViewModel());
            }
            else
            {
                this.Countries = new ObservableCollection<CountryItemViewModel>(this.ToCountryItemViewModel().Where(
                                                                                                                     country=> country.Name.ToLower().Contains(this._filter.ToLower())
                                                                                                                            || country.Capital.ToLower().Contains(this._filter.ToLower())));
            }
        }
        #endregion
    }
}
