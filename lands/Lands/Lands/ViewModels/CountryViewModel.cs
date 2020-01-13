namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    public class CountryViewModel
    {
        #region Variables
        #endregion

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
