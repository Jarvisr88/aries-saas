namespace DevExpress.Mvvm.UI.Interactivity
{
    using System;

    public abstract class TriggerBase<T> : TriggerBase where T: DependencyObject
    {
        protected TriggerBase() : base(typeof(T))
        {
        }

        protected T AssociatedObject =>
            (T) base.AssociatedObject;
    }
}

