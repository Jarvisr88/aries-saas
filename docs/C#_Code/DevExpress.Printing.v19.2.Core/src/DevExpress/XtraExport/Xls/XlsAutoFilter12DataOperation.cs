namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    internal class XlsAutoFilter12DataOperation : XlsAutoFilterDataOperation
    {
        protected override void WriteStringOper(BinaryWriter writer)
        {
            writer.Write((byte) base.Value.TextValue.Length);
            writer.Write(base.IsSimple ? ((byte) 1) : ((byte) 0));
            writer.Write((ushort) 0);
            writer.Write(0);
        }
    }
}

