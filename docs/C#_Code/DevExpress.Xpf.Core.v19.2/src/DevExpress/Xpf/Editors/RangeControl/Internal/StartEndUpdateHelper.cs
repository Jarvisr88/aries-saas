namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.RangeControl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StartEndUpdateHelper
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty StartValueProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty EndValueProperty;

        public StartEndUpdateHelper(DevExpress.Xpf.Editors.RangeControl.RangeControl rangeControl, DependencyProperty startProperty, DependencyProperty endProperty)
        {
            this.RangeControl = rangeControl;
            this.StartValueProperty = startProperty;
            this.EndValueProperty = endProperty;
        }

        public void Update<T>(StartEndUpdateSource updateSource) where T: struct, IComparable
        {
            if ((this.StartValue != null) && (!this.StartValue.IsInfinity && !Equals(this.StartValue.RealValue, this.RangeControl.GetValue(this.StartValueProperty))))
            {
                this.RangeControl.SetCurrentValue(this.StartValueProperty, this.StartValue.RealValue);
            }
            if ((this.EndValue != null) && (!this.EndValue.IsInfinity && !Equals(this.EndValue.RealValue, this.RangeControl.GetValue(this.EndValueProperty))))
            {
                this.RangeControl.SetCurrentValue(this.EndValueProperty, this.EndValue.RealValue);
            }
        }

        private DevExpress.Xpf.Editors.RangeControl.RangeControl RangeControl { get; set; }

        public IComparableObjectWrapper StartValue { get; set; }

        public IComparableObjectWrapper EndValue { get; set; }
    }
}

