namespace DevExpress.Mvvm.UI.Interactivity
{
    using System;

    public abstract class Behavior<T> : Behavior where T: DependencyObject
    {
        protected Behavior() : base(typeof(T))
        {
        }

        protected T AssociatedObject =>
            (T) base.AssociatedObject;
    }
}

