namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ControlBox : FrameworkElement
    {
        public static readonly DependencyProperty HotButtonProperty;
        public static readonly DependencyProperty PressedButtonProperty;
        public static readonly DependencyProperty MDIButtonMinimizeTemplateProperty;
        public static readonly DependencyProperty MDIButtonRestoreTemplateProperty;
        public static readonly DependencyProperty MDIButtonCloseTemplateProperty;
        public static readonly DependencyProperty MDIButtonBorderStyleProperty;

        static ControlBox()
        {
            DependencyPropertyRegistrator<ControlBox> registrator = new DependencyPropertyRegistrator<ControlBox>();
            registrator.RegisterAttachedInherited<HitTestType>("HotButton", ref HotButtonProperty, HitTestType.Undefined, null, null);
            registrator.RegisterAttachedInherited<HitTestType>("PressedButton", ref PressedButtonProperty, HitTestType.Undefined, null, null);
            registrator.Register<DataTemplate>("MDIButtonMinimizeTemplate", ref MDIButtonMinimizeTemplateProperty, null, (dObj, e) => ((ControlBox) dObj).OnMDIButtonMinimizeChanged((DataTemplate) e.NewValue), null);
            registrator.Register<DataTemplate>("MDIButtonRestoreTemplate", ref MDIButtonRestoreTemplateProperty, null, (dObj, e) => ((ControlBox) dObj).OnMDIButtonRestoreChanged((DataTemplate) e.NewValue), null);
            registrator.Register<DataTemplate>("MDIButtonCloseTemplate", ref MDIButtonCloseTemplateProperty, null, (dObj, e) => ((ControlBox) dObj).OnMDIButtonCloseChanged((DataTemplate) e.NewValue), null);
            registrator.Register<Style>("MDIButtonBorderStyle", ref MDIButtonBorderStyleProperty, null, (dObj, e) => ((ControlBox) dObj).OnMDIButtonBorderStyleChanged((Style) e.NewValue), null);
        }

        public ControlBox()
        {
            base.Loaded += new RoutedEventHandler(this.ControlBox_Loaded);
        }

        private void ControlBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnMDIButtonCloseChanged(this.MDIButtonCloseTemplate);
            this.OnMDIButtonRestoreChanged(this.MDIButtonRestoreTemplate);
            this.OnMDIButtonMinimizeChanged(this.MDIButtonMinimizeTemplate);
            this.OnMDIButtonBorderStyleChanged(this.MDIButtonBorderStyle);
        }

        private BarMDIButtonItem GetBarItem(MDIMenuBar.ItemType type)
        {
            DockLayoutManager manager = DockLayoutManager.Ensure(this, false);
            return (((manager == null) || manager.IsDisposing) ? null : manager.MDIController.MDIMenuBar.GetBarItem(type));
        }

        public static HitTestType GetHotButton(DependencyObject obj) => 
            (HitTestType) obj.GetValue(HotButtonProperty);

        public static HitTestType GetPressedButton(DependencyObject obj) => 
            (HitTestType) obj.GetValue(PressedButtonProperty);

        protected virtual void OnMDIButtonBorderStyleChanged(Style value)
        {
            DockLayoutManager manager = DockLayoutManager.Ensure(this, false);
            if ((manager != null) && !manager.IsDisposing)
            {
                manager.MDIController.MDIMenuBar.UpdateMDIButtonBorderStyle(manager, value);
            }
        }

        protected virtual void OnMDIButtonCloseChanged(DataTemplate value)
        {
            this.PrepareMDIButton(this.GetBarItem(MDIMenuBar.ItemType.Close), value);
        }

        protected virtual void OnMDIButtonMinimizeChanged(DataTemplate value)
        {
            this.PrepareMDIButton(this.GetBarItem(MDIMenuBar.ItemType.Minimize), value);
        }

        protected virtual void OnMDIButtonRestoreChanged(DataTemplate value)
        {
            this.PrepareMDIButton(this.GetBarItem(MDIMenuBar.ItemType.Restore), value);
        }

        protected virtual void PrepareMDIButton(BarMDIButtonItem barItem, DataTemplate value)
        {
            if (barItem != null)
            {
                barItem.GlyphTemplate = value;
            }
        }

        public static void SetHotButton(DependencyObject obj, HitTestType value)
        {
            obj.SetValue(HotButtonProperty, value);
        }

        public static void SetPressedButton(DependencyObject obj, HitTestType value)
        {
            obj.SetValue(PressedButtonProperty, value);
        }

        public Style MDIButtonBorderStyle
        {
            get => 
                (Style) base.GetValue(MDIButtonBorderStyleProperty);
            set => 
                base.SetValue(MDIButtonBorderStyleProperty, value);
        }

        public DataTemplate MDIButtonCloseTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MDIButtonCloseTemplateProperty);
            set => 
                base.SetValue(MDIButtonCloseTemplateProperty, value);
        }

        public DataTemplate MDIButtonMinimizeTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MDIButtonMinimizeTemplateProperty);
            set => 
                base.SetValue(MDIButtonMinimizeTemplateProperty, value);
        }

        public DataTemplate MDIButtonRestoreTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MDIButtonRestoreTemplateProperty);
            set => 
                base.SetValue(MDIButtonRestoreTemplateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ControlBox.<>c <>9 = new ControlBox.<>c();

            internal void <.cctor>b__6_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ControlBox) dObj).OnMDIButtonMinimizeChanged((DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__6_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ControlBox) dObj).OnMDIButtonRestoreChanged((DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__6_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ControlBox) dObj).OnMDIButtonCloseChanged((DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__6_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ControlBox) dObj).OnMDIButtonBorderStyleChanged((Style) e.NewValue);
            }
        }
    }
}

