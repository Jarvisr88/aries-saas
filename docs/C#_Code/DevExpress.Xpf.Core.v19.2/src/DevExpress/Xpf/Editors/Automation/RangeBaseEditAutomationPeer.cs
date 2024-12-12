namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public abstract class RangeBaseEditAutomationPeer : BaseEditAutomationPeer, IRangeValueProvider
    {
        protected RangeBaseEditAutomationPeer(RangeBaseEdit element) : base(element)
        {
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.RangeValue) ? base.GetPattern(patternInterface) : this;

        internal void RaiseValuePropertyChangedEvent(double oldValue, double newValue)
        {
            base.RaisePropertyChangedEvent(RangeValuePatternIdentifiers.ValueProperty, oldValue, newValue);
        }

        void IRangeValueProvider.SetValue(double value)
        {
            this.Editor.Value = value;
        }

        protected RangeBaseEdit Editor =>
            base.Editor as RangeBaseEdit;

        bool IRangeValueProvider.IsReadOnly =>
            this.Editor.IsReadOnly;

        double IRangeValueProvider.LargeChange =>
            this.Editor.LargeStep;

        double IRangeValueProvider.Maximum =>
            this.Editor.Maximum;

        double IRangeValueProvider.Minimum =>
            this.Editor.Minimum;

        double IRangeValueProvider.SmallChange =>
            this.Editor.SmallStep;

        double IRangeValueProvider.Value =>
            this.Editor.Value;
    }
}

