namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;

    public class NumericValueNormalizer : ValueNormalizerBase
    {
        public override double GetComparableValue(object realValue) => 
            Convert.ToDouble(realValue);

        public override object GetRealValue(double comparable) => 
            comparable;
    }
}

