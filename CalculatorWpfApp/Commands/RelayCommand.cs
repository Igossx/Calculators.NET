using System;
using System.Windows.Input;

namespace CalculatorWpfApp.Commands
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public ICommand AddNumberCommand { get; set; }

        public ICommand AddOperationCommand { get; set; }

        public ICommand ClearScreenCommand { get; set; }

        public ICommand GetResultCommand { get; set; }

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
