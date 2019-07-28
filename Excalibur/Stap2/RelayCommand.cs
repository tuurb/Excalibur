using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Excalibur.Stap2
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? (() => true);
        }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute();
        }

        void ICommand.Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
