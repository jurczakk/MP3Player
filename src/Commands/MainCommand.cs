using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    public class MainCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;
        
        public MainCommand(Predicate<object> _canExecute, Action<object> _execute)
        {
            canExecute = _canExecute;
            execute = _execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter) 
        {
            execute(parameter);
        } 
    }
}