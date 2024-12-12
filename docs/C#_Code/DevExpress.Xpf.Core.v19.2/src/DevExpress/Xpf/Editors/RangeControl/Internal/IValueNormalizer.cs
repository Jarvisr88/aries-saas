namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;

    public interface IValueNormalizer
    {
        double GetComparableValue(object realValue);
        object GetRealValue(double comparable);
    }
}

