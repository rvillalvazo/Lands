namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CountryViewModel:BaseViewModel
    {
        #region Variables
        private ObservableCollection<Border> _borders = new ObservableCollection<Border>();
        #endregion

        #region Propiedades
        public CountryItemViewModel Country
        {
            get;
            set;
        }

        public ObservableCollection<Border> Borders
        {
            get
            {
                return this._borders;
            }
            set
            {
                SetValue(ref this._borders,value);
            }
        }
        #endregion

        #region Constructor
        public CountryViewModel(CountryItemViewModel country)
        {
            this.Country = country;
            this.LoadBorders();
        }
        #endregion

        #region Metodos
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach(var border in this.Country.Borders)
            {
                var country = MainViewModel.GetInstance().CountriesList.Where(c => c.Alpha3Code.Equals(border)).FirstOrDefault();

                if(country!=null)
                {
                    this._borders.Add(new Border
                                            {
                                                Code =country.Alpha3Code,
                                                Name =country.Name
                                             });
                }
            }
        }
        #endregion
    }
}
