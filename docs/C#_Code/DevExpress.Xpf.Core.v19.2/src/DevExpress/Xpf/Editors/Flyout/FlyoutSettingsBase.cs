namespace DevExpress.Xpf.Editors.Flyout
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Flyout.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public abstract class FlyoutSettingsBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static FlyoutSettingsBase()
        {
            Type type = typeof(FlyoutSettingsBase);
        }

        protected FlyoutSettingsBase()
        {
        }

        public virtual void Apply(FlyoutPositionCalculator calculator, FlyoutBase flyout)
        {
        }

        public virtual FlyoutAnimatorBase CreateAnimator() => 
            new FlyoutAnimator();

        public abstract FlyoutPositionCalculator CreatePositionCalculator();
        public abstract FlyoutBase.FlyoutStrategy CreateStrategy();
        public virtual IndicatorDirection GetIndicatorDirection(FlyoutPlacement placement) => 
            IndicatorDirection.None;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            this.RaisePropertyChanged(e.Property.Name);
        }

        public virtual void OnPropertyChanged(FlyoutBase flyout, PropertyChangedEventArgs e)
        {
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged.Do<PropertyChangedEventHandler>(x => x(this, new PropertyChangedEventArgs(propertyName)));
        }
    }
}

