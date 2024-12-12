namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;

    internal class ButtonInfoBaseAutomationPeer : AutomationPeer
    {
        protected ButtonInfoBaseAutomationPeer(ButtonInfoBase owner)
        {
            this.Owner = owner;
        }

        protected override string GetAcceleratorKeyCore() => 
            AutomationProperties.GetAcceleratorKey(this.Owner);

        protected override string GetAccessKeyCore() => 
            AutomationProperties.GetAccessKey(this.Owner);

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Button;

        protected override string GetAutomationIdCore() => 
            AutomationProperties.GetAutomationId(this.Owner);

        protected override Rect GetBoundingRectangleCore() => 
            new Rect(0.0, 0.0, 0.0, 0.0);

        protected override List<AutomationPeer> GetChildrenCore() => 
            null;

        protected override string GetClassNameCore() => 
            "ButtonInfo";

        protected override Point GetClickablePointCore() => 
            new Point(0.0, 0.0);

        protected override string GetHelpTextCore() => 
            AutomationProperties.GetHelpText(this.Owner);

        protected override string GetItemStatusCore() => 
            AutomationProperties.GetName(this.Owner);

        protected override string GetItemTypeCore() => 
            AutomationProperties.GetAutomationId(this.Owner);

        protected override AutomationPeer GetLabeledByCore() => 
            null;

        protected override string GetNameCore() => 
            AutomationProperties.GetName(this.Owner);

        protected override AutomationOrientation GetOrientationCore() => 
            AutomationOrientation.Horizontal;

        public override object GetPattern(PatternInterface patternInterface) => 
            null;

        protected override bool HasKeyboardFocusCore() => 
            false;

        protected override bool IsContentElementCore() => 
            false;

        protected override bool IsControlElementCore() => 
            false;

        protected override bool IsEnabledCore() => 
            this.Owner.IsEnabled;

        protected override bool IsKeyboardFocusableCore() => 
            false;

        protected override bool IsOffscreenCore() => 
            false;

        protected override bool IsPasswordCore() => 
            false;

        protected override bool IsRequiredForFormCore() => 
            false;

        protected override void SetFocusCore()
        {
        }

        protected ButtonInfoBase Owner { get; private set; }
    }
}

