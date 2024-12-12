namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Windows.Automation.Peers;

    public class ToolTipContentControlAutomationPeer : FrameworkElementAutomationPeer
    {
        public ToolTipContentControlAutomationPeer(ToolTipContentControl owner) : base(owner)
        {
        }

        protected override string GetHelpTextCore()
        {
            BaseValidationError content = this.Owner.Content as BaseValidationError;
            if (content != null)
            {
                object errorContent = content.ErrorContent;
                if (errorContent != null)
                {
                    return errorContent.ToString();
                }
                object local1 = errorContent;
            }
            return null;
        }

        private ToolTipContentControl Owner =>
            (ToolTipContentControl) base.Owner;
    }
}

