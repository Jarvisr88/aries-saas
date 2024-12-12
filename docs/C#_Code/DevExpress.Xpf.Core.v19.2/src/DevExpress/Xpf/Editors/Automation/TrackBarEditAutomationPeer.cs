namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;

    public class TrackBarEditAutomationPeer : RangeBaseEditAutomationPeer
    {
        public TrackBarEditAutomationPeer(TrackBarEdit element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Slider;

        public override object GetPattern(PatternInterface patternInterface) => 
            !this.Editor.IsRange ? base.GetPattern(patternInterface) : ((patternInterface == PatternInterface.Value) ? this : null);

        protected override string GetValue() => 
            !this.Editor.IsRange ? base.GetValue() : this.GetValueCore(this.Editor.SelectionStart, this.Editor.SelectionEnd);

        protected virtual string GetValueCore(double selectionStart, double selectionEnd) => 
            $"selection start equals {selectionStart}, selection end equals {selectionEnd}";

        protected override bool HasKeyboardFocusCore() => 
            this.Editor.IsFocused;

        protected override bool IsKeyboardFocusableCore() => 
            this.Editor.Focusable;

        internal void RaiseSelectionEndPropertyChangedEvent(double oldValue, double newValue)
        {
            base.RaisePropertyChangedEvent(ValuePatternIdentifiers.ValueProperty, this.GetValueCore(this.Editor.SelectionStart, oldValue), this.GetValueCore(this.Editor.SelectionStart, newValue));
        }

        internal void RaiseSelectionStartPropertyChangedEvent(double oldValue, double newValue)
        {
            base.RaisePropertyChangedEvent(ValuePatternIdentifiers.ValueProperty, this.GetValueCore(oldValue, this.Editor.SelectionEnd), this.GetValueCore(newValue, this.Editor.SelectionEnd));
        }

        protected override void SetFocusCore()
        {
            this.Editor.Focus();
        }

        protected override void SetValue(string value)
        {
            if (!this.Editor.IsRange)
            {
                base.SetValue(value);
            }
        }

        protected TrackBarEdit Editor =>
            base.Editor as TrackBarEdit;
    }
}

