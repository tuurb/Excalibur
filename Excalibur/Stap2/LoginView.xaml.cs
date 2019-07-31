namespace Excalibur.Stap2
{
    public partial class LoginView
    {
        public LoginViewModel ViewModel => (LoginViewModel)DataContext;

        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.Status = ViewModel.IsLoginGeslaagd 
                ? $"{ViewModel.Username} is succesvol ingelogd" 
                : $"Inloggen van gebruiker {ViewModel.Username} is niet geslaagd";
        }
    }
}
