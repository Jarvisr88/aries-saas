namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class GalleryBarItemLinkControl : BarItemLinkControl
    {
        public static readonly DependencyProperty HasTopBorderProperty = HasTopBorderPropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey HasTopBorderPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasTopBorder", typeof(bool), typeof(GalleryBarItemLinkControl), new PropertyMetadata(true));
        public static readonly DependencyProperty HasBottomBorderProperty = HasBottomBorderPropertyKey.DependencyProperty;
        private static readonly DependencyPropertyKey HasBottomBorderPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasBottomBorder", typeof(bool), typeof(GalleryBarItemLinkControl), new PropertyMetadata(true));

        public GalleryBarItemLinkControl(GalleryBarItemLink link) : base(link)
        {
            base.DefaultStyleKey = typeof(GalleryBarItemLinkControl);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        protected internal override bool GetIsSelectable() => 
            true;

        protected object GetTemplateFromProvider(DependencyProperty prop) => 
            base.GetValue(prop);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GalleryControl = base.GetTemplateChild("PART_GalleryControl") as DevExpress.Xpf.Bars.GalleryControl;
            Action<DevExpress.Xpf.Bars.GalleryControl> action = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Action<DevExpress.Xpf.Bars.GalleryControl> local1 = <>c.<>9__21_0;
                action = <>c.<>9__21_0 = x => x.AllowCyclicNavigation = false;
            }
            this.GalleryControl.Do<DevExpress.Xpf.Bars.GalleryControl>(action);
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.UpdateBorderVisibility();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.UpdateBorderVisibility();
        }

        protected void UpdateBorderVisibility()
        {
            if (base.LinksControl != null)
            {
                bool flag = false;
                bool flag2 = false;
                int index = base.LinksControl.ItemLinks.IndexOf(this.GalleryLink);
                for (int i = 0; i < base.LinksControl.ItemLinks.Count; i++)
                {
                    BarItemLinkBase base2 = base.LinksControl.ItemLinks[i];
                    if ((i < index) && base2.ActualIsVisible)
                    {
                        flag = true;
                    }
                    if ((i > index) && base2.ActualIsVisible)
                    {
                        flag2 = true;
                    }
                }
                this.HasTopBorder = flag;
                this.HasBottomBorder = flag2;
            }
        }

        protected internal override void UpdateTemplateByContainerType(LinkContainerType type)
        {
            base.UpdateTemplateByContainerType(type);
            base.Template = (ControlTemplate) this.GetTemplateFromProvider(BarItemLinkControlTemplateProvider.TemplateInMenuProperty);
        }

        public bool HasTopBorder
        {
            get => 
                (bool) base.GetValue(HasTopBorderProperty);
            private set => 
                base.SetValue(HasTopBorderPropertyKey, value);
        }

        public bool HasBottomBorder
        {
            get => 
                (bool) base.GetValue(HasBottomBorderProperty);
            private set => 
                base.SetValue(HasBottomBorderPropertyKey, value);
        }

        public GalleryBarItemLink GalleryLink =>
            base.Link as GalleryBarItemLink;

        public DevExpress.Xpf.Bars.GalleryControl GalleryControl { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GalleryBarItemLinkControl.<>c <>9 = new GalleryBarItemLinkControl.<>c();
            public static Action<GalleryControl> <>9__21_0;

            internal void <OnApplyTemplate>b__21_0(GalleryControl x)
            {
                x.AllowCyclicNavigation = false;
            }
        }
    }
}

