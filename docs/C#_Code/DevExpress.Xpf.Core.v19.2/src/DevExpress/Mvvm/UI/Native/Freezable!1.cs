namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Windows;

    public abstract class Freezable<TFreezable> : Freezable where TFreezable: Freezable<TFreezable>, new()
    {
        protected Freezable()
        {
        }

        protected override Freezable CreateInstanceCore() => 
            Activator.CreateInstance<TFreezable>();
    }
}

