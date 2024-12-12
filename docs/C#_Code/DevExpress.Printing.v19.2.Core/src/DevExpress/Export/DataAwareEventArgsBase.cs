namespace DevExpress.Export
{
    using System;
    using System.Runtime.CompilerServices;

    public class DataAwareEventArgsBase
    {
        public DataAwareEventArgsBase()
        {
            this.Handled = false;
        }

        public int DataSourceRowIndex { get; set; }

        public int DocumentRow { get; set; }

        public int RowHandle { get; set; }

        public object DataSourceOwner { get; set; }

        public bool Handled { get; set; }
    }
}

