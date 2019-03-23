using System;
using System.Collections.ObjectModel;

namespace SuxrobGM.Sdk.Wpf.Commands
{
    public static class CommandManager
    {
        private static ObservableCollection<Action> actionCommandsList = new ObservableCollection<Action>();
        public static void Add(Action raiseCanExecuteChangedAction)
        {
            actionCommandsList.Add(raiseCanExecuteChangedAction);
        }
        
        public static void Remove(Action raiseCanExecuteChangedAction)
        {
            actionCommandsList.Remove(raiseCanExecuteChangedAction);
        }
    }

    public static class CommandManager<T>
    {
        private static ObservableCollection<Action<T>> actionCommandsList = new ObservableCollection<Action<T>>();
        public static void Add(Action<T> raiseCanExecuteChangedAction)
        {
            actionCommandsList.Add(raiseCanExecuteChangedAction);
        }

        public static void Remove(Action<T> raiseCanExecuteChangedAction)
        {
            actionCommandsList.Remove(raiseCanExecuteChangedAction);
        }
    }
}
