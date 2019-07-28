using System.Windows;
using ReactiveUI;

namespace Excalibur
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Splat.Locator.CurrentMutable.RegisterViewsForViewModels(GetType().Assembly);
        }
    }
}
