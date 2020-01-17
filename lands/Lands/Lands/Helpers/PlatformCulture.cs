namespace Lands.Helpers
{
    using System;

    public class PlatformCulture 
    {
        #region Propiedades
        public string PlatformString
        {
            get;
            private set;
        }

        public string LanguageCode
        {
            get;
            private set;
        }

        public string LocaleCode
        {
            get;
            private set;
        }
        #endregion
        #region Constructor
        public PlatformCulture(string platformCultureString)
        {

            if (String.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier","platformCultureString");
            }

            PlatformString = platformCultureString.Replace("_","-");

            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if(dashIndex>0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return PlatformString;
        }
        #endregion
    }
}
