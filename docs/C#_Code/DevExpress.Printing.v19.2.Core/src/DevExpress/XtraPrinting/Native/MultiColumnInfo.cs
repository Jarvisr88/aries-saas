namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using System;

    public class MultiColumnInfo
    {
        private int columnCount;
        private float start;
        private float end;

        [XtraSerializableProperty(0)]
        public int ColumnCount { get; set; }

        [XtraSerializableProperty(1)]
        public float Start { get; set; }

        [XtraSerializableProperty(2)]
        public float End { get; set; }
    }
}

