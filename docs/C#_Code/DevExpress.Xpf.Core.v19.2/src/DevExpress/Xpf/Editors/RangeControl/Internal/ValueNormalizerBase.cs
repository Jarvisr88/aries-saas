namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;

    public abstract class ValueNormalizerBase : IValueNormalizer
    {
        protected ValueNormalizerBase()
        {
        }

        public abstract double GetComparableValue(object realValue);
        public abstract object GetRealValue(double comparable);
    }
}

