namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandSupBook : XlsSourceCommandBase
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (this.IsSelfReferencing)
            {
                contentBuilder.SelfRefBookIndex = contentBuilder.SupBookCount;
            }
            contentBuilder.SupBookCount++;
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            reader.ReadUInt16();
            ushort num = reader.ReadUInt16();
            this.IsSelfReferencing = num == 0x401;
            int count = base.Size - 4;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public bool IsSelfReferencing { get; private set; }
    }
}

