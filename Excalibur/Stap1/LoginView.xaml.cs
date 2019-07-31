using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Excalibur.Stap1
{
    public partial class LoginView : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _status;
        private int _selectedSleepTime;
        private bool _processingLogin;
        private bool _isLoginCanceled;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoginButtonIsEnabled));
                if (_username == "error")
                {
                    throw new System.ApplicationException("Username niet toegestaan!");
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoginButtonIsEnabled));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public List<int> AvailableSleepTimes { get; }

        public int SelectedSleepTime
        {
            get => _selectedSleepTime;
            set
            {
                _selectedSleepTime = value;
                OnPropertyChanged();
            }
        }

        public bool LoginButtonIsEnabled
        {
            get
            {
                if (!_processingLogin && !string.IsNullOrEmpty(Username) && !string.IsNullOrWhiteSpace(Password))
                {
                    return true;
                }

                Status = null;
                return false;
            }
        }

        public bool CancelButtonIsEnabled => _processingLogin && !_isLoginCanceled;


        public LoginView()
        {
            InitializeComponent();
            DataContext = this;

            AvailableSleepTimes = new List<int>(Enumerable.Range(0, 10));
            SelectedSleepTime = 2;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Status = null;

            _processingLogin = true;
            OnPropertyChanged(nameof(LoginButtonIsEnabled));
            OnPropertyChanged(nameof(CancelButtonIsEnabled));

            await Task.Delay(SelectedSleepTime * 1000);

            _processingLogin = false;
            OnPropertyChanged(nameof(LoginButtonIsEnabled));
            OnPropertyChanged(nameof(CancelButtonIsEnabled));

            if (!_isLoginCanceled)
            {
                Status = Username == Password ? "Inloggen is gelukt" : "Inloggen is mislukt";
            }

            _isLoginCanceled = false;
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Status = "Inloggen is afgebroken";
            _isLoginCanceled = true;
            OnPropertyChanged(nameof(CancelButtonIsEnabled));
        }
    }
}
