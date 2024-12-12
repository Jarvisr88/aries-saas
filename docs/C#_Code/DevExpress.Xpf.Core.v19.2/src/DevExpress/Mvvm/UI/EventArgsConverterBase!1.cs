namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Windows;

    public abstract class EventArgsConverterBase<TArgs> : DependencyObject, IEventArgsConverter
    {
        protected EventArgsConverterBase()
        {
        }

        protected abstract object Convert(object sender, TArgs args);
        object IEventArgsConverter.Convert(object sender, object args) => 
            (args is TArgs) ? this.Convert(sender, (TArgs) args) : null;
    }
}

