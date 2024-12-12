namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DesignFormatInfo
    {
        private ConditionalFormattingStringId nameID;
        private string formatName;
        public ConditionalFormattingStringId NameID =>
            this.nameID;
        public string FormatName =>
            this.formatName;
        public DesignFormatInfo(ConditionalFormattingStringId nameID, string formatName)
        {
            this.nameID = nameID;
            this.formatName = formatName;
        }
    }
}

