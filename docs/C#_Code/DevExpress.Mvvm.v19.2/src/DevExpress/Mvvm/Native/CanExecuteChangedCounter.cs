namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Windows.Input;

    public class CanExecuteChangedCounter : EventFireCounter<ICommand, EventArgs>
    {
        public CanExecuteChangedCounter(ICommand command) : base(delegate (EventHandler h) {
            command.CanExecuteChanged += h;
        }, delegate (EventHandler h) {
            command.CanExecuteChanged -= h;
        })
        {
        }
    }
}

