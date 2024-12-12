namespace DevExpress.Data
{
    using System;

    public class UnboundSourceValuePushedEventArgs : EventArgs
    {
        private int _RowIndex;
        private string _Name;
        private object _Tag;
        private object _Value;

        protected internal UnboundSourceValuePushedEventArgs();
        protected internal UnboundSourceValuePushedEventArgs(int rowIndex, string name, object tag, object value);
        internal void Init(int rowIndex, string name, object tag, object value);
        internal void UnInit();

        public int RowIndex { get; }

        public string PropertyName { get; }

        public object Tag { get; }

        public object Value { get; }
    }
}

