using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SuxrobGM.Sdk.Wpf.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action execute;
       
        public DelegateCommand(Action actionDelegate, Predicate<object> predicate)
        {
            this.canExecute = predicate;
            this.execute = actionDelegate;
        }

        public DelegateCommand(Action actionDelegate)
        {
            this.execute = actionDelegate;
            this.canExecute = null;
        }

        public event EventHandler CanExecuteChanged;
        /*{
            add { CommandManager.Add(value); }
            remove { CommandManager.Remove(value); }
        }*/

        public void Execute(object parameter = null)
        {
            this.execute();
        }

        public bool CanExecute(object parameter = null)
        {
            if (canExecute == null)
                return true;
            return this.canExecute(parameter);
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> canExecute;
        private readonly Action<T> execute;

        public DelegateCommand(Action<T> actionDelegate, Predicate<T> predicate)
        {
            this.canExecute = predicate;
            this.execute = actionDelegate;
        }

        public DelegateCommand(Action<T> actionDelegate)
        {
            this.execute = actionDelegate;
            this.canExecute = null;
        }

        public event EventHandler CanExecuteChanged;
        /*{
            add { CommandManager.Add(value); }
            remove { CommandManager.Remove(value); }
        }*/

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            return this.canExecute((T)parameter);
        }
    }
}
