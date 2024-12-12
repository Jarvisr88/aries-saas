namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [TemplateVisualState(Name="Normal", GroupName="CommonStates"), TemplateVisualState(Name="MouseOver", GroupName="CommonStates"), TemplateVisualState(Name="Pressed", GroupName="CommonStates"), TemplateVisualState(Name="Disabled", GroupName="CommonStates")]
    public class ControlBoxButtonBorder : ContentControl
    {
        static ControlBoxButtonBorder()
        {
            new DependencyPropertyRegistrator<ControlBoxButtonBorder>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public ControlBoxButtonBorder()
        {
            base.Focusable = false;
        }
    }
}

