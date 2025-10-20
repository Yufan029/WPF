using System.Windows.Input;

namespace WeatherApp.Commands
{
    /// <summary>
    /// The command base class.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Base class always return true, leave for the subclass to make further decision.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// Inherited class need to implement.
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object? parameter);

        /// <summary>
        /// Raise when excute state change.
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
