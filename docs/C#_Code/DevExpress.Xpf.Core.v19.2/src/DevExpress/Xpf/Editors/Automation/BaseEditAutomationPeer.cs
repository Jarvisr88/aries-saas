namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public abstract class BaseEditAutomationPeer : FrameworkElementAutomationPeer, IValueProvider
    {
        protected BaseEditAutomationPeer(BaseEdit element) : base(element)
        {
        }

        protected override string GetClassNameCore() => 
            this.Editor.GetType().Name;

        protected override string GetHelpTextCore()
        {
            if (string.IsNullOrEmpty(AutomationProperties.GetHelpText(base.Owner)))
            {
                ToolTipContentControl toolTip = ((FrameworkElement) base.Owner).ToolTip as ToolTipContentControl;
                if (toolTip != null)
                {
                    string helpText = CreatePeerForElement(toolTip).GetHelpText();
                    if (!string.IsNullOrEmpty(helpText))
                    {
                        return helpText;
                    }
                }
            }
            return base.GetHelpTextCore();
        }

        protected override string GetNameCore()
        {
            string nameCore = base.GetNameCore();
            return (string.IsNullOrEmpty(nameCore) ? this.Editor.GetPlainText() : nameCore);
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Value) ? null : this.GetValueInterface();

        protected virtual string GetValue() => 
            this.GetValueCore();

        protected virtual string GetValueCore() => 
            (this.Editor.EditValue == null) ? string.Empty : this.Editor.EditValue.ToString();

        private object GetValueInterface() => 
            this;

        protected override bool HasKeyboardFocusCore()
        {
            Func<FrameworkElement, bool> evaluator = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<FrameworkElement, bool> local1 = <>c.<>9__14_0;
                evaluator = <>c.<>9__14_0 = x => x.IsFocused;
            }
            return this.EditCore.Return<FrameworkElement, bool>(evaluator, (<>c.<>9__14_1 ??= () => false));
        }

        protected override bool IsKeyboardFocusableCore()
        {
            Func<FrameworkElement, bool> evaluator = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<FrameworkElement, bool> local1 = <>c.<>9__13_0;
                evaluator = <>c.<>9__13_0 = x => x.Focusable;
            }
            return this.EditCore.Return<FrameworkElement, bool>(evaluator, (<>c.<>9__13_1 ??= () => false));
        }

        protected override void SetFocusCore()
        {
            Action<FrameworkElement> action = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Action<FrameworkElement> local1 = <>c.<>9__12_0;
                action = <>c.<>9__12_0 = x => x.Focus();
            }
            this.EditCore.Do<FrameworkElement>(action);
        }

        protected virtual void SetValue(string value)
        {
            if (!base.IsEnabled() || this.Editor.IsReadOnly)
            {
                throw new ElementNotEnabledException();
            }
            this.SetValueCore(value);
        }

        protected virtual void SetValueCore(string value)
        {
            BaseEditHelper.SetCurrentValue(this.Editor, BaseEdit.EditValueProperty, value);
        }

        void IValueProvider.SetValue(string value)
        {
            this.SetValue(value);
        }

        protected BaseEdit Editor =>
            base.Owner as BaseEdit;

        protected FrameworkElement EditCore
        {
            get
            {
                Func<BaseEdit, FrameworkElement> evaluator = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<BaseEdit, FrameworkElement> local1 = <>c.<>9__4_0;
                    evaluator = <>c.<>9__4_0 = x => x.EditCore;
                }
                return this.Editor.With<BaseEdit, FrameworkElement>(evaluator);
            }
        }

        bool IValueProvider.IsReadOnly =>
            this.Editor.IsReadOnly;

        string IValueProvider.Value =>
            this.GetValue();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseEditAutomationPeer.<>c <>9 = new BaseEditAutomationPeer.<>c();
            public static Func<BaseEdit, FrameworkElement> <>9__4_0;
            public static Action<FrameworkElement> <>9__12_0;
            public static Func<FrameworkElement, bool> <>9__13_0;
            public static Func<bool> <>9__13_1;
            public static Func<FrameworkElement, bool> <>9__14_0;
            public static Func<bool> <>9__14_1;

            internal FrameworkElement <get_EditCore>b__4_0(BaseEdit x) => 
                x.EditCore;

            internal bool <HasKeyboardFocusCore>b__14_0(FrameworkElement x) => 
                x.IsFocused;

            internal bool <HasKeyboardFocusCore>b__14_1() => 
                false;

            internal bool <IsKeyboardFocusableCore>b__13_0(FrameworkElement x) => 
                x.Focusable;

            internal bool <IsKeyboardFocusableCore>b__13_1() => 
                false;

            internal void <SetFocusCore>b__12_0(FrameworkElement x)
            {
                x.Focus();
            }
        }
    }
}

