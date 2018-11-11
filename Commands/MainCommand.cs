<<<<<<< HEAD
﻿using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    /// <summary>
    /// ICommand implementations
    /// </summary>
    class MainCommand : ICommand
    {
        private Predicate<object> canExecute;
        private Action<object> execute;

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
=======
﻿using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    /// <summary>
    /// Simple ICommand implementation.
    /// </summary>
    class MainCommand : ICommand
    {
        private Predicate<object> canExecute;
        private Action<object> execute;

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
>>>>>>> origin/master
