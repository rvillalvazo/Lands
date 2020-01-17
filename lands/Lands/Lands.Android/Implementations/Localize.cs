[assembly: Xamarin.Forms.Dependency(typeof(Lands.Droid.Implementations.Localize))]
namespace Lands.Droid.Implementations
{
    using System.Globalization;
    using System.Threading;
    using Helpers;
    using Interfaces;

    public class Localize : ILocalize
    {
        #region Metodos
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_","-"));

            CultureInfo ci = null;

            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch(CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));

                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    ci = new CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;

            switch(androidLanguage)
            {
                case "ms-BN":
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;
                case "in-ID":
                    netLanguage = "id-ID";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
            }

            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platformCulture)
        {
            var netLanguage = platformCulture.LanguageCode;

            switch(platformCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH";
                    break;
            }

            return netLanguage;
        }
        #endregion
    }
}