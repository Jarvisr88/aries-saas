namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class BinaryHyperlinkAntiMoniker : BinaryHyperlinkMonikerBase
    {
        private int count;

        public BinaryHyperlinkAntiMoniker() : base(BinaryHyperlinkMonikerFactory.CLSID_AntiMoniker)
        {
        }

        public static BinaryHyperlinkAntiMoniker FromStream(XlReader reader)
        {
            BinaryHyperlinkAntiMoniker moniker = new BinaryHyperlinkAntiMoniker();
            moniker.Read(reader);
            return moniker;
        }

        public override int GetSize() => 
            base.GetSize() + 4;

        protected void Read(XlReader reader)
        {
            this.Count = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.count);
        }

        public int Count
        {
            get => 
                this.count;
            set
            {
                if ((value < 0) || (value > 0x100000))
                {
                    throw new ArgumentOutOfRangeException("Count value out of range 0...1048576");
                }
                this.count = value;
            }
        }
    }
}

