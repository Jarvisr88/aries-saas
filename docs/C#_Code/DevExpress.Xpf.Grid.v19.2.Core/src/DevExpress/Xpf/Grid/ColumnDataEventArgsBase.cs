namespace DevExpress.Xpf.Grid
{
    using System;

    public class ColumnDataEventArgsBase : EventArgs
    {
        private ColumnBase column;
        private object _value;
        private bool isGetAction = true;

        protected internal ColumnDataEventArgsBase(ColumnBase column, object _value, bool isGetAction)
        {
            this.column = column;
            this._value = _value;
            this.isGetAction = isGetAction;
        }

        public ColumnBase Column =>
            this.column;

        public bool IsGetData =>
            this.isGetAction;

        public bool IsSetData =>
            !this.IsGetData;

        public object Value
        {
            get => 
                this._value;
            set => 
                this._value = value;
        }
    }
}

