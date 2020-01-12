using System;

namespace Lands.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;

    public class LoginViewModel :  BaseViewModel
    {
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
                SetValue(ref this._password,value);
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
            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "rvillalvazo@hotmail.com";
            this.Password = "hotmail";
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
                await Application.Current.MainPage.DisplayAlert("Error", "Debe capturar un Email", "Accept");
                return;
            }
            else if (String.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe capturar un password", "Accept");
                return;
            }
            else
            {
                this.IsRunning = true;
                this.IsEnabled = false;

                if (this.Email.Equals("rvillalvazo@hotmail.com") && this.Password.Equals("hotmail"))
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    this.Email = String.Empty;
                    this.Password = String.Empty;

                    MainViewModel.GetInstance().Lands = new LandsViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
                }
                else
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    this.Password = String.Empty;

                    await Application.Current.MainPage.DisplayAlert("Error", "Email o password incorrecto.", "Accept");
                    return;
                }
            }
        }
        #endregion
    }
}
