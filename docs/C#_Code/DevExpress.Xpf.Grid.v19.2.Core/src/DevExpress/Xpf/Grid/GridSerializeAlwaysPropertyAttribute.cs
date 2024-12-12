namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using System;

    public class GridSerializeAlwaysPropertyAttribute : XtraSerializablePropertyId
    {
        public const int IdSerializeAlways = 2;

        public GridSerializeAlwaysPropertyAttribute() : base(2)
        {
        }
    }
}

