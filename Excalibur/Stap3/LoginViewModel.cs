using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace Excalibur.Stap3
{
    public class LoginViewModel : ReactiveObject
    {
        private string _username;
        private string _password;
        private string _status;
        private int _selectedSleepTime;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public List<int> AvailableSleepTimes { get; }

        public int SelectedSleepTime
        {
            get => _selectedSleepTime;
            set => this.RaiseAndSetIfChanged(ref _selectedSleepTime, value);
        }

        public LoginViewModel()
        {
            AvailableSleepTimes = new List<int>(Enumerable.Range(0, 10));
            SelectedSleepTime = 2;
        }
    }
}