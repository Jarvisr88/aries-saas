namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentWsBool : XlsContentBase
    {
        public override int GetSize() => 
            2;

        public override void Read(XlReader reader, int size)
        {
            short num = reader.ReadInt16();
            this.ShowPageBreaks = Convert.ToBoolean((int) (num & 1));
            this.IsDialog = Convert.ToBoolean((int) (num & 0x10));
            this.ApplyStyles = Convert.ToBoolean((int) (num & 0x20));
            this.ShowRowSumsBelow = Convert.ToBoolean((int) (num & 0x40));
            this.ShowColumnSumsRight = Convert.ToBoolean((int) (num & 0x80));
            this.FitToPage = Convert.ToBoolean((int) (num & 0x100));
            this.SynchronizeHorizontalScrolling = Convert.ToBoolean((int) (num & 0x1000));
            this.SynchronizeVerticalScrolling = Convert.ToBoolean((int) (num & 0x2000));
            this.TransitionFormulaEvaluation = Convert.ToBoolean((int) (num & 0x4000));
            this.TransitionFormulaEntry = Convert.ToBoolean((int) (num & 0x8000));
        }

        public override void Write(BinaryWriter writer)
        {
            int num = 0;
            if (this.ShowPageBreaks)
            {
                num |= 1;
            }
            if (this.IsDialog)
            {
                num |= 0x10;
            }
            if (this.ApplyStyles)
            {
                num |= 0x20;
            }
            if (this.ShowRowSumsBelow)
            {
                num |= 0x40;
            }
            if (this.ShowColumnSumsRight)
            {
                num |= 0x80;
            }
            if (this.FitToPage)
            {
                num |= 0x100;
            }
            if (this.SynchronizeHorizontalScrolling)
            {
                num |= 0x1000;
            }
            if (this.SynchronizeVerticalScrolling)
            {
                num |= 0x2000;
            }
            if (this.TransitionFormulaEvaluation)
            {
                num |= 0x4000;
            }
            if (this.TransitionFormulaEntry)
            {
                num |= 0x8000;
            }
            writer.Write((short) num);
        }

        public bool ShowPageBreaks { get; set; }

        public bool IsDialog { get; set; }

        public bool ApplyStyles { get; set; }

        public bool ShowRowSumsBelow { get; set; }

        public bool ShowColumnSumsRight { get; set; }

        public bool FitToPage { get; set; }

        public bool SynchronizeHorizontalScrolling { get; set; }

        public bool SynchronizeVerticalScrolling { get; set; }

        public bool TransitionFormulaEvaluation { get; set; }

        public bool TransitionFormulaEntry { get; set; }
    }
}

