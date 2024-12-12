namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class StackBase
    {
        protected List<object> list;

        public StackBase();
        public void Clear();
        public object Peek();
        public object Pop();
        public void Push(object obj);

        public int Count { get; }

        public bool IsEmpty { get; }

        protected int LastIndex { get; }
    }
}

