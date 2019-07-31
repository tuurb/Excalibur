using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace Excalibur.Stap3
{
    public partial class LoginView : IViewFor<LoginViewModel>
    {
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as LoginViewModel;
        }

        public LoginViewModel ViewModel { get; set; }

        public LoginView()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Username, v => v.UsernameText.Text).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Password, v => v.PasswordText.Text).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.Status, v => v.StatusText.Text).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.SelectedSleepTime, v => v.SleepTimeSelection.SelectedValue).DisposeWith(disposables);
            });

        }

    }
}
