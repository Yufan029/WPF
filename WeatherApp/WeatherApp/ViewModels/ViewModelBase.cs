using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherApp.ViewModels
{
    /// <summary>
    /// ViewModel base class
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set the property value and raise the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="original">The property original value.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">The property name.</param>
        protected void SetProperty<T>(ref T original, T value, [CallerMemberName] string? propertyName = null)
        {
            // No need to assign if equal.
            if (Equals(original, value))
            {
                return;
            }

            original = value;

            // Raise the PropertyChanged event in order to update UI.
            OnPropertyChanged(propertyName);
        }
    }
}
