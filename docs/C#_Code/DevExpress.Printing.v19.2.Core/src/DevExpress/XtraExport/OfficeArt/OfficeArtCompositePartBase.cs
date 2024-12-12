namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal abstract class OfficeArtCompositePartBase : OfficeArtPartBase
    {
        private readonly List<OfficeArtPartBase> items = new List<OfficeArtPartBase>();

        protected OfficeArtCompositePartBase()
        {
        }

        protected internal override int GetSize()
        {
            int num = 0;
            int count = this.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.Items[i].ShouldWrite())
                {
                    num = (num + 8) + this.Items[i].GetSize();
                }
            }
            return num;
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            int count = this.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.Items[i].Write(writer);
            }
        }

        public List<OfficeArtPartBase> Items =>
            this.items;
    }
}

