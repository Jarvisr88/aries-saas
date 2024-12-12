namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsCondFmtIconThreshold : XlsCondFmtValueObject
    {
        public override int GetSize() => 
            base.GetSize() + 5;

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.EqualPass ? ((byte) 1) : ((byte) 0));
            writer.Write(0);
        }

        public bool EqualPass { get; set; }
    }
}

