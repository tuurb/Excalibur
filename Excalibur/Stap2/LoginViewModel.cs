using System.Collections.Generic;
using System.Linq;

namespace Excalibur.Stap2
{
    public class LoginViewModel : BindableBase
    {
        private string _username;
        private string _password;
        private string _status;
        private int _selectedSleepTime;
        private bool _loginButtonIsEnabled;

        public string Username
        {
            get => _username;
            set
            {
                if (SetIfChanged(ref _username, value))
                {
                    SetLoginButtonIsEnabled();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetIfChanged(ref _password, value))
                {
                    SetLoginButtonIsEnabled();
                }
            }
        }

        public string Status
        {
            get => _status;
            set => SetIfChanged(ref _status, value);
        }

        public int SelectedSleepTime
        {
            get => _selectedSleepTime;
            set => SetIfChanged(ref _selectedSleepTime, value);
        }

        public bool LoginButtonIsEnabled
        {
            get => _loginButtonIsEnabled;
            set
            {
                if (SetIfChanged(ref _loginButtonIsEnabled, value))
                {
                    if (!_loginButtonIsEnabled)
                    {
                        Status = null;
                    }
                }
            }
        }

        public List<int> AvailableSleepTimes { get; }

        public bool IsLoginGeslaagd => Username == Password;

        public LoginViewModel()
        {
            AvailableSleepTimes = new List<int>(Enumerable.Range(0, 10));
            SelectedSleepTime = 2;

            LoginButtonIsEnabled = false;
        }

        private void SetLoginButtonIsEnabled()
        {
            LoginButtonIsEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}