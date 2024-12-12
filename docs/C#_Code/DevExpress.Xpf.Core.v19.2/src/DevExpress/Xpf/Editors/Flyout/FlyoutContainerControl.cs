namespace DevExpress.Xpf.Editors.Flyout
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Flyout.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class FlyoutContainerControl : ContentControl, INotifyPropertyChanged
    {
        private FlyoutBase flyout;

        public event PropertyChangedEventHandler PropertyChanged;

        public FlyoutContainerControl()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateOwner();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateOwner();
        }

        protected virtual void UpdateOwner()
        {
            if (this.Flyout == null)
            {
                this.Flyout = (FlyoutBase) base.GetValue(FlyoutBase.FlyoutProperty);
            }
        }

        public FlyoutBase Flyout
        {
            get => 
                this.flyout;
            protected set
            {
                if (!ReferenceEquals(this.flyout, value))
                {
                    this.flyout = value;
                    this.PropertyChanged.Do<PropertyChangedEventHandler>(x => x(this, new PropertyChangedEventArgs("Flyout")));
                }
            }
        }
    }
}

