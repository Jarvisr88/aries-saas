namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ClipboardRow : IClipboardRow
    {
        private int indent;
        private object[] rowCells;

        public ClipboardRow(object[] rowCells) : this(rowCells, 0)
        {
        }

        public ClipboardRow(object[] rowCells, int indent)
        {
            this.rowCells = rowCells;
            this.indent = indent;
        }

        public Type[] GetCellTypes()
        {
            Func<object, Type> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<object, Type> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = x => x?.GetType();
            }
            return this.rowCells.Select<object, Type>(selector).ToArray<Type>();
        }

        public object[] Cells =>
            this.rowCells;

        public int Indent =>
            this.indent;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClipboardRow.<>c <>9 = new ClipboardRow.<>c();
            public static Func<object, Type> <>9__6_0;

            internal Type <GetCellTypes>b__6_0(object x) => 
                x?.GetType();
        }
    }
}

