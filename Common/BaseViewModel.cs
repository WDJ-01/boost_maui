using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Boost.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public BaseViewModel()
        {
        }
    

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (backingStore == null && value == null)
                return;

            if (backingStore != null && backingStore.Equals(value))
                return;

            backingStore = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
