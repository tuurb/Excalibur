using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using DynamicData;
using ReactiveUI;

namespace Excalibur.Stap3
{
    public class LoginViewModel : ReactiveObject
    {
        private string _username;
        private string _password;
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

        public int SelectedSleepTime
        {
            get => _selectedSleepTime;
            set => this.RaiseAndSetIfChanged(ref _selectedSleepTime, value);
        }

        private readonly ObservableAsPropertyHelper<string> _status;
        public string Status => _status.Value;

        public ReactiveCommand<Unit, bool> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public ObservableCollection<int> AvailableSleepTimes { get; } = new ObservableCollection<int>();

        public LoginViewModel()
        {
            var loginManager = new LoginManager();

            AvailableSleepTimes.AddRange(Enumerable.Range(0, 10));
            SelectedSleepTime = AvailableSleepTimes[2];

            var loginEnabled = this.WhenAnyValue(
                vm => vm.Username,
                vm => vm.Password,
                (u, p) => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p));

            LoginCommand = ReactiveCommand.CreateFromObservable(
                () => loginManager.Login(Username, Password, SelectedSleepTime)
                    .ToObservable()
                    .TakeUntil(CancelCommand),
                loginEnabled);

            CancelCommand = ReactiveCommand.Create(
                () => { },
                LoginCommand.IsExecuting);

            var statusMessages = LoginCommand.Select(success => success ? "Logged in" : "Unable to login")
                .Merge(LoginCommand.ThrownExceptions.Select(ex => $"Error: {ex.Message}"))
                .Merge(CancelCommand.Select(_ => "Cancelled"))
                .Merge(loginEnabled.Select(_ => ""))
                .Merge(LoginCommand.IsExecuting.Where(x => x).Select(_ => ""));

            _status = statusMessages.ToProperty(this, vm => vm.Status);
        }
    }
}