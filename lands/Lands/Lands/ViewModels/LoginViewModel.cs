using System;

namespace Lands.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;
    using Services;
    using Helpers;

    public class LoginViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService _apiService;
        #endregion

        #region Variables
        private string _email = String.Empty;
        private string _password = String.Empty;
        private bool _isRunning = false;
        private bool _isEnabled = false;
        #endregion

        #region Propiedades
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                SetValue(ref this._email, value);
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetValue(ref this._password, value);
            }
        }
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                SetValue(ref this._isRunning, value);
            }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                SetValue(ref this._isEnabled, value);
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this._apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "rvillalvazo@hotmail.com";
            this.Password = "u5tuj0p0kLands";
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (String.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailValidation, Languages.Accept);
                return;
            }
            else if (String.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordValidation, Languages.Accept);
                return;
            }
            else
            {
                this.IsRunning = true;
                this.IsEnabled = false;

                var connection = await this._apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message,Languages.Accept);
                    return;
                }

                //http://localhost/Lands/API 
                //http://localhost:2909/

                var token = await this._apiService.GetToken("http://192.168.0.6:2909/Lands/API", this.Email, this.Password);

                if (token == null)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(Languages.Error, "Something was wrong.", Languages.Accept);
                    return;
                }

                if (String.IsNullOrEmpty(token.AccessToken))
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(Languages.Error, token.ErrorDescription, Languages.Accept);

                    this.Password = String.Empty;

                    return;
                }

                this.IsRunning = false;
                this.IsEnabled = true;

                this.Email = String.Empty;
                this.Password = String.Empty;

                var mainVM = MainViewModel.GetInstance();
                mainVM.Token = token;
                mainVM.Lands = new LandsViewModel();

                await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            }
        }
        #endregion
    }
}
