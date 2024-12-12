namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class MultiColumn
    {
        protected MultiColumn(MultiColumn source);
        protected MultiColumn(int columnCount, float columnWidth);
        public MultiColumn(int columnCount, float columnWidth, ColumnLayout order);
        public virtual MultiColumn Clone();
        public virtual float GetColumnWidth(DocumentBand docBand);
        public virtual float GetPageWidth(float defaultValue);
        public virtual void Scale(double ratio);

        public int ColumnCount { get; private set; }

        public float ColumnWidth { get; private set; }

        public bool DownThenAcross { get; private set; }

        public bool AcrossThenDown { get; protected set; }

        public virtual bool IsAcross { get; }
    }
}

