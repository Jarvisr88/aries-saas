namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseFilterRangeAttributeProxy : FilterAttributeProxy
    {
        protected BaseFilterRangeAttributeProxy()
        {
        }

        public object MinOrMinMember { get; set; }

        public object MaxOrMaxMember { get; set; }

        public string FromName { get; set; }

        public string ToName { get; set; }
    }
}

