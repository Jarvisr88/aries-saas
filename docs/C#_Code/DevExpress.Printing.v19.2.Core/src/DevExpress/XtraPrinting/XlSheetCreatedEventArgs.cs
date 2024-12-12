namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlSheetCreatedEventArgs : EventArgs
    {
        private string sheetName;

        public XlSheetCreatedEventArgs(int index, string sheetName)
        {
            this.Index = index;
            this.sheetName = sheetName;
        }

        public string SheetName
        {
            get => 
                this.sheetName;
            set => 
                this.sheetName = value;
        }

        public int Index { get; private set; }
    }
}

