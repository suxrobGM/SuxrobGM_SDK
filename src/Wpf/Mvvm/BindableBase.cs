using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuxrobGM.Sdk.Wpf.Mvvm
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
