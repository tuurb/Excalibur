using System.Reactive.Disposables;
using ReactiveUI;

namespace Excalibur.Stap3
{
    public partial class LoginView : IViewFor<LoginViewModel>
    {
        public LoginViewModel ViewModel { get; set; }
        
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as LoginViewModel;
        }

        public LoginView()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();

            this.WhenActivated(disposables => 
            {
                this.Bind(ViewModel, vm => vm.Username, view => view.UsernameText.Text).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Password, view => view.PasswordText.Text).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.SelectedSleepTime, view => view.SleepTimeSelection.SelectedItem).DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.AvailableSleepTimes, v => v.SleepTimeSelection.ItemsSource).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Status, view => view.StatusText.Text).DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.LoginCommand, view => view.LoginButton).DisposeWith(disposables);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, view => view.CancelButton).DisposeWith(disposables);
            });
        }
    }
}
