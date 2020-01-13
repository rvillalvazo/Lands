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
        #region Propiedades
        public CountryItemViewModel Country
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CountryViewModel(CountryItemViewModel country)
        {
            this.Country = country;
        }
        #endregion
    }
}
