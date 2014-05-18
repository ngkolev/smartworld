using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartWorld.UI.ViewModel
{
    public class RelayCommand : ICommand
    {
        #region Private Fields

        private Action action;
        private Func<bool> canExecute;

        #endregion

        #region Constructor

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.action = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action execute)
            : this(execute, null)
        {

        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute();
            else return true;
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            if (CanExecute(null) && action != null)
            {
                action();
            }
        }

        #endregion
    }

    public class RelayCommand<T> : ICommand
    {
        #region Private Fields

        private Action<T> action;
        private Func<T, bool> canExecute;

        #endregion

        #region Constructor

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.action = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute((T)parameter);
            else return true;
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            if (CanExecute(null) && action != null)
            {
                action((T)parameter);
            }
        }

        #endregion
    }
}
