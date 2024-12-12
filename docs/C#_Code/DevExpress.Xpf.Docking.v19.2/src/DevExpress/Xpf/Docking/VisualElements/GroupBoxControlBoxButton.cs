namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplateVisualState(Name="Expanded", GroupName="ExpandedStates"), TemplateVisualState(Name="Collapsed", GroupName="ExpandedStates")]
    public class GroupBoxControlBoxButton : DevExpress.Xpf.Docking.VisualElements.ControlBoxButton
    {
        public static readonly DependencyProperty IsExpandedProperty;

        static GroupBoxControlBoxButton()
        {
            new DependencyPropertyRegistrator<GroupBoxControlBoxButton>().Register<bool>("IsExpanded", ref IsExpandedProperty, false, (dObj, ea) => ((GroupBoxControlBoxButton) dObj).OnIsExpandedChanged((bool) ea.NewValue), null);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Group != null)
            {
                BindingHelper.SetBinding(this, IsExpandedProperty, this.Group, "IsExpanded");
            }
            this.UpdateToolTip();
        }

        protected virtual void OnIsExpandedChanged(bool isExpanded)
        {
            this.UpdateVisualState();
            this.UpdateToolTip();
        }

        protected virtual void UpdateToolTip()
        {
            object toolTip = BaseControlBoxControl.GetToolTip(this.IsExpanded ? DockingStringId.MenuItemCollapseGroup : DockingStringId.MenuItemExpandGroup);
            this.AttachToolTip(toolTip);
        }

        protected override void UpdateVisualState()
        {
            base.UpdateVisualState();
            string stateName = this.IsExpanded ? "Expanded" : "Collapsed";
            VisualStateManager.GoToState(this, stateName, false);
            if (base.PartGlyphControl != null)
            {
                VisualStateManager.GoToState(base.PartGlyphControl, stateName, false);
            }
        }

        public bool IsExpanded
        {
            get => 
                (bool) base.GetValue(IsExpandedProperty);
            set => 
                base.SetValue(IsExpandedProperty, value);
        }

        private LayoutGroup Group =>
            base.Item as LayoutGroup;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupBoxControlBoxButton.<>c <>9 = new GroupBoxControlBoxButton.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((GroupBoxControlBoxButton) dObj).OnIsExpandedChanged((bool) ea.NewValue);
            }
        }
    }
}

