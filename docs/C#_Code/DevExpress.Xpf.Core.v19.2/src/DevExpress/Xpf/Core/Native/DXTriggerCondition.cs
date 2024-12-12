namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class DXTriggerCondition : FrameworkElement
    {
        public static readonly DependencyProperty ActualValueProperty;
        private BindingSource bindingSource;
        private FrameworkElement templatedParent;

        public event EventHandler ActualValueChanged;

        static DXTriggerCondition();
        public DXTriggerCondition(UIElement owner, DXCondition condition);
        public void ActivateCondition();
        private void CreateBinding(System.Windows.Data.Binding binding);
        private UIElement GetElementByName(string elementName);
        private object GetSource(BindingSource bindingSource);
        private object GetSourceByRelativeSource(RelativeSource relativeSource);
        private void OnActualValueChanged();
        private void RaiseActualValueChanged();
        private void SetBinding();

        public object ActualValue { get; set; }

        public UIElement Owner { get; private set; }

        public System.Windows.Data.Binding Binding { get; private set; }

        public object TriggerValue { get; private set; }

        public bool IsEntryCondition { get; }

        protected internal FrameworkElement TemplatedParent { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTriggerCondition.<>c <>9;

            static <>c();
            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

