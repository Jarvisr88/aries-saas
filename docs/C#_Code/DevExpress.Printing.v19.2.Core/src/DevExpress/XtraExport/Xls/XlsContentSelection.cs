namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentSelection : XlsContentBase
    {
        public override int GetSize() => 
            9 + (this.SelectedCells.Count * 6);

        private byte PaneTypeToCode(XlViewPaneType paneType)
        {
            switch (paneType)
            {
                case XlViewPaneType.BottomLeft:
                    return 2;

                case XlViewPaneType.BottomRight:
                    return 0;

                case XlViewPaneType.TopRight:
                    return 1;
            }
            return 3;
        }

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.PaneTypeToCode(this.ViewPane));
            writer.Write((ushort) this.ActiveCell.Row);
            writer.Write((ushort) this.ActiveCell.Column);
            writer.Write((short) this.ActiveCellIndex);
            int count = this.SelectedCells.Count;
            writer.Write((ushort) count);
            for (int i = 0; i < count; i++)
            {
                this.SelectedCells[i].Write(writer);
            }
        }

        public XlViewPaneType ViewPane { get; set; }

        public XlCellPosition ActiveCell { get; set; }

        public int ActiveCellIndex { get; set; }

        public List<XlsRefU> SelectedCells { get; } = new List<XlsRefU>()
    }
}

