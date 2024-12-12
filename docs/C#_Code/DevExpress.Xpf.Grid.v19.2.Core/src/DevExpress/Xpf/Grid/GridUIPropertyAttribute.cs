namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using System;

    public class GridUIPropertyAttribute : XtraSerializablePropertyId
    {
        public const int IdUI = 1;

        public GridUIPropertyAttribute() : base(1)
        {
        }
    }
}

