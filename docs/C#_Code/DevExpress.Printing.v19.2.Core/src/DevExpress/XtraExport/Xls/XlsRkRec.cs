namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsRkRec
    {
        private XlsRkNumber rk = new XlsRkNumber(0);

        public static XlsRkRec Read(XlReader reader) => 
            new XlsRkRec { 
                FormatIndex = reader.ReadUInt16(),
                Rk = new XlsRkNumber(reader.ReadInt32())
            };

        public void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.FormatIndex);
            writer.Write(this.Rk.GetRawValue());
        }

        public int FormatIndex { get; set; }

        public XlsRkNumber Rk
        {
            get => 
                this.rk;
            set
            {
                if (value == null)
                {
                    this.rk = new XlsRkNumber(0);
                }
                else
                {
                    this.rk = value;
                }
            }
        }
    }
}

