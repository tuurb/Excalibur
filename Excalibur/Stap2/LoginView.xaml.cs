using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Excalibur.Stap2
{
    public partial class LoginView : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _status;
        private int _selectedSleepTime;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
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


        public LoginView()
        {
            InitializeComponent();
            DataContext = this;

            AvailableSleepTimes = new List<int>(Enumerable.Range(0, 10));
            SelectedSleepTime = 2;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
