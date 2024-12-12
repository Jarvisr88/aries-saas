namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class IComparableObjectWrapper : IComparable
    {
        public IComparableObjectWrapper(IComparable value, object realValue, bool isInfinity)
        {
            this.Value = value;
            this.RealValue = realValue;
            this.IsInfinity = isInfinity;
        }

        public int CompareTo(object obj)
        {
            IComparableObjectWrapper wrapper = obj as IComparableObjectWrapper;
            return ((wrapper == null) ? this.Value.CompareTo(obj) : this.Value.CompareTo(wrapper.Value));
        }

        public IComparable Value { get; private set; }

        public object RealValue { get; private set; }

        public bool IsInfinity { get; private set; }
    }
}

