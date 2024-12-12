namespace DevExpress.Export.Binary
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BinaryHyperlinkCompositeMoniker : BinaryHyperlinkMonikerBase
    {
        private readonly List<BinaryHyperlinkMonikerBase> items;

        public BinaryHyperlinkCompositeMoniker() : base(BinaryHyperlinkMonikerFactory.CLSID_CompositeMoniker)
        {
            this.items = new List<BinaryHyperlinkMonikerBase>();
        }

        public static BinaryHyperlinkCompositeMoniker FromStream(XlReader reader)
        {
            BinaryHyperlinkCompositeMoniker moniker = new BinaryHyperlinkCompositeMoniker();
            moniker.Read(reader);
            return moniker;
        }

        public override int GetSize()
        {
            int num = 0;
            for (int i = 0; i < this.items.Count; i++)
            {
                num += this.items[i].GetSize();
            }
            return ((base.GetSize() + 4) + num);
        }

        protected void Read(XlReader reader)
        {
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                BinaryHyperlinkMonikerBase item = BinaryHyperlinkMonikerFactory.Create(reader);
                this.items.Add(item);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            int count = this.items.Count;
            writer.Write(count);
            for (int i = 0; i < count; i++)
            {
                this.items[i].Write(writer);
            }
        }

        public List<BinaryHyperlinkMonikerBase> Items =>
            this.items;
    }
}

