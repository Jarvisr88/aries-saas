namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class IComparableWrapper : IComparable
    {
        public IComparableWrapper(IComparable value, bool isInfinity, bool isNaN = false)
        {
            this.Value = value;
            this.IsInfinity = isInfinity;
            this.IsNaN = isNaN;
        }

        public int CompareTo(object obj)
        {
            if (this.IsNaN)
            {
                return 0;
            }
            IComparableWrapper wrapper = obj as IComparableWrapper;
            return ((wrapper == null) ? this.Value.CompareTo(obj) : (wrapper.IsNaN ? 0 : this.Value.CompareTo(wrapper.Value)));
        }

        public IComparable Value { get; private set; }

        public bool IsInfinity { get; private set; }

        public bool IsNaN { get; private set; }
    }
}

