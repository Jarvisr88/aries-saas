namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;

    public class TextEditAutomationPeer : BaseEditAutomationPeer
    {
        public TextEditAutomationPeer(TextEditBase element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Edit;

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface == PatternInterface.Scroll) ? this.GetScrollInterface() : ((patternInterface != PatternInterface.Text) ? base.GetPattern(patternInterface) : this.GetTextInterface());

        private object GetPatternFromEditBox(PatternInterface pi)
        {
            Func<TextEditBase, FrameworkElement> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<TextEditBase, FrameworkElement> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.EditCore;
            }
            return this.Editor.With<TextEditBase, FrameworkElement>(evaluator).With<FrameworkElement, AutomationPeer>(new Func<FrameworkElement, AutomationPeer>(UIElementAutomationPeer.CreatePeerForElement)).With<AutomationPeer, object>(x => x.GetPattern(pi));
        }

        private object GetScrollInterface() => 
            this.GetPatternFromEditBox(PatternInterface.Scroll);

        private object GetTextInterface() => 
            this.GetPatternFromEditBox(PatternInterface.Text);

        protected TextEditBase Editor =>
            base.Editor as TextEditBase;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEditAutomationPeer.<>c <>9 = new TextEditAutomationPeer.<>c();
            public static Func<TextEditBase, FrameworkElement> <>9__5_0;

            internal FrameworkElement <GetPatternFromEditBox>b__5_0(TextEditBase x) => 
                x.EditCore;
        }
    }
}

