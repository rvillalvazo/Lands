namespace Lands.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        #region Propiedades
        public static string Accept
        {
            get
            {
                return Resource.Accept;
            }
        }

        public static string Error
        {
            get
            {
                return Resource.Error;
            }
        }

        public static string EmailValidation
        {
            get
            {
                return Resource.EmailValidation;
            }
        }

        public static string PasswordValidation
        {
            get
            {
                return Resource.PasswordValidation;
            }
        }

        public static string EmailPlaceholder
        {
            get
            {
                return Resource.EmailPlaceholder;
            }
        }

        public static string RememberMe
        {
            get
            {
                return Resource.RememberMe;
            }
        }
        #endregion

        #region Constructor
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        #endregion
    }
}
