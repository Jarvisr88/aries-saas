namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class Appearance : Freezable
    {
        public static readonly DependencyProperty NormalProperty;
        public static readonly DependencyProperty ActiveProperty;

        static Appearance()
        {
            DependencyPropertyRegistrator<Appearance> registrator = new DependencyPropertyRegistrator<Appearance>();
            registrator.Register<AppearanceObject>("Normal", ref NormalProperty, null, (d, e) => ((Appearance) d).OnAppearanceObjectChanged((AppearanceObject) e.NewValue), null);
            registrator.Register<AppearanceObject>("Active", ref ActiveProperty, null, (d, e) => ((Appearance) d).OnAppearanceObjectChanged((AppearanceObject) e.NewValue), null);
        }

        public Appearance()
        {
            this.Normal = new AppearanceObject();
            this.Active = new AppearanceObject();
        }

        protected override Freezable CreateInstanceCore() => 
            new Appearance();

        protected virtual void OnAppearanceObjectChanged(AppearanceObject appearanceObject)
        {
            if (appearanceObject != null)
            {
                appearanceObject.Appearance = this;
            }
        }

        protected internal virtual void OnAppearanceObjectPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.OnAppearanceObjectPropertyChanged(e);
            }
        }

        public AppearanceObject Normal
        {
            get => 
                (AppearanceObject) base.GetValue(NormalProperty);
            set => 
                base.SetValue(NormalProperty, value);
        }

        public AppearanceObject Active
        {
            get => 
                (AppearanceObject) base.GetValue(ActiveProperty);
            set => 
                base.SetValue(ActiveProperty, value);
        }

        protected internal BaseLayoutItem Owner { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Appearance.<>c <>9 = new Appearance.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Appearance) d).OnAppearanceObjectChanged((AppearanceObject) e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Appearance) d).OnAppearanceObjectChanged((AppearanceObject) e.NewValue);
            }
        }
    }
}

