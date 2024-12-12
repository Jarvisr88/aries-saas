namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class FieldNameWrapper
    {
        public FieldNameWrapper(IDataColumnInfo info) : this(info.FieldName, info.Caption)
        {
        }

        public FieldNameWrapper(string fieldName, string caption)
        {
            this.FieldName = fieldName;
            this.Caption = caption;
        }

        public static IList<FieldNameWrapper> Create(IDataColumnInfo info)
        {
            Func<IDataColumnInfo, FieldNameWrapper> selector = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<IDataColumnInfo, FieldNameWrapper> local1 = <>c.<>9__10_0;
                selector = <>c.<>9__10_0 = x => new FieldNameWrapper(x);
            }
            return info.Columns.Select<IDataColumnInfo, FieldNameWrapper>(selector).ToList<FieldNameWrapper>();
        }

        public override string ToString() => 
            this.Caption;

        public string FieldName { get; private set; }

        public string Caption { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FieldNameWrapper.<>c <>9 = new FieldNameWrapper.<>c();
            public static Func<IDataColumnInfo, FieldNameWrapper> <>9__10_0;

            internal FieldNameWrapper <Create>b__10_0(IDataColumnInfo x) => 
                new FieldNameWrapper(x);
        }
    }
}

