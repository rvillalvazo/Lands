
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    public class MainViewModel
    {
        #region Propiedades
        public List<Country> CountriesList
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public LandsViewModel Lands        
        {
            get;
            set;
        }

        public CountryViewModel Country
        {
            get;
            set;
        }
        #endregion

        #region Contructores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                instance = new MainViewModel();

            return instance;
        }
        #endregion
    }
}
