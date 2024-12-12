namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data;
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    public sealed class ExpressionEditorModel : BindableBase
    {
        internal ExpressionEditorModel(IDataColumnInfo column)
        {
            this.<Column>k__BackingField = column;
        }

        public string Expression { get; set; }

        public IDataColumnInfo Column { get; }
    }
}

