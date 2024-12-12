namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsDxfN12 : XlsDxfN12List
    {
        public override short GetSize() => 
            !base.IsEmpty ? ((short) (base.GetSize() + 4)) : 6;

        public override void Write(BinaryWriter writer)
        {
            if (base.IsEmpty)
            {
                writer.Write(0);
                writer.Write((ushort) 0);
            }
            else
            {
                int num = this.GetSize() - 4;
                writer.Write(num);
                base.Write(writer);
            }
        }
    }
}

