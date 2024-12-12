namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsDxfN12List : XlsDxfN
    {
        private readonly XfProperties extProperties = new XfProperties();

        internal override void AddExtProperty(XfPropBase prop)
        {
            this.extProperties.Add(prop);
        }

        public override short GetSize()
        {
            if (this.IsEmpty)
            {
                return 0;
            }
            int size = base.GetSize();
            if (this.extProperties.Count > 0)
            {
                size += this.extProperties.GetSize() + 4;
            }
            return (short) size;
        }

        public override void Read(BinaryReader reader)
        {
        }

        internal override void SetIsEmpty(bool value)
        {
            this.IsEmpty = value;
        }

        public override void Write(BinaryWriter writer)
        {
            if (!this.IsEmpty)
            {
                base.Write(writer);
                if (this.extProperties.Count > 0)
                {
                    writer.Write((ushort) 0);
                    writer.Write((ushort) 0xffff);
                    this.extProperties.Write(writer);
                }
            }
        }

        public bool IsEmpty { get; set; }

        public XfProperties ExtProperties =>
            this.extProperties;
    }
}

