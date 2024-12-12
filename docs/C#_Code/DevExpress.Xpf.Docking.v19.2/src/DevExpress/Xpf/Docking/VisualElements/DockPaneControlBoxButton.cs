namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplateVisualState(Name="Pinned", GroupName="PinnedStates"), TemplateVisualState(Name="Unpinned", GroupName="PinnedStates")]
    public class DockPaneControlBoxButton : DevExpress.Xpf.Docking.VisualElements.ControlBoxButton
    {
        public static readonly DependencyProperty IsAutoHiddenProperty;

        static DockPaneControlBoxButton()
        {
            new DependencyPropertyRegistrator<DockPaneControlBoxButton>().Register<bool>("IsAutoHidden", ref IsAutoHiddenProperty, false, (dObj, ea) => ((DockPaneControlBoxButton) dObj).OnIsAutoHiddenChanged((bool) ea.NewValue), null);
        }

        protected virtual void OnIsAutoHiddenChanged(bool isAutoHidden)
        {
            this.UpdateVisualState();
        }

        protected override void UpdateVisualState()
        {
            base.UpdateVisualState();
            if (base.Item != null)
            {
                string stateName = (base.Item.IsPinnedTab || (!base.Item.IsAutoHidden && !base.Item.IsTabDocument)) ? "Pinned" : "Unpinned";
                string str2 = (base.Item.Parent as AutoHideGroup).Return<AutoHideGroup, Dock>((<>c.<>9__6_0 ??= x => x.DockType), (<>c.<>9__6_1 ??= () => Dock.Left)).ToString();
                VisualStateManager.GoToState(this, stateName, false);
                VisualStateManager.GoToState(this, str2, false);
                if (base.PartGlyphControl != null)
                {
                    VisualStateManager.GoToState(base.PartGlyphControl, stateName, false);
                    VisualStateManager.GoToState(base.PartGlyphControl, str2, false);
                }
            }
        }

        public bool IsAutoHidden
        {
            get => 
                (bool) base.GetValue(IsAutoHiddenProperty);
            set => 
                base.SetValue(IsAutoHiddenProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockPaneControlBoxButton.<>c <>9 = new DockPaneControlBoxButton.<>c();
            public static Func<AutoHideGroup, Dock> <>9__6_0;
            public static Func<Dock> <>9__6_1;

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((DockPaneControlBoxButton) dObj).OnIsAutoHiddenChanged((bool) ea.NewValue);
            }

            internal Dock <UpdateVisualState>b__6_0(AutoHideGroup x) => 
                x.DockType;

            internal Dock <UpdateVisualState>b__6_1() => 
                Dock.Left;
        }
    }
}

