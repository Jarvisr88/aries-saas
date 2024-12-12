namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Windows.Controls;

    public abstract class Control<TControl> : Control where TControl: Control<TControl>
    {
        static Control()
        {
            DependencyPropertyRegistrator<TControl>.New().OverrideDefaultStyleKey();
        }

        protected Control()
        {
        }
    }
}

