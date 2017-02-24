using System;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class DelegateCommand<Object> : ICommand
    {
        #region Fields
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Constructors
        public DelegateCommand(Action<object> execute)
            :this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute,
                  Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
