namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class PaginationItemLinkControlBaseAutomationPeer : FrameworkElementAutomationPeer, IValueProvider
    {
        private readonly SpinEdit editor;

        public PaginationItemLinkControlBaseAutomationPeer(FrameworkElement owner) : base(owner)
        {
            this.editor = LayoutHelper.FindElementByType<SpinEdit>(owner);
        }

        protected override string GetAutomationIdCore() => 
            base.Owner.GetType().Name;

        protected override string GetNameCore() => 
            "Pagination";

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Value) ? base.GetPattern(patternInterface) : this;

        public void SetValue(string value)
        {
            if (!this.editor.IsEnabled || this.editor.IsReadOnly)
            {
                throw new ElementNotEnabledException();
            }
            this.editor.EditValue = value;
        }

        public string Value =>
            this.editor.EditValue.ToString();

        public bool IsReadOnly =>
            this.editor.IsReadOnly;
    }
}

