namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class CompositeOfficeDrawingPartBase : OfficeDrawingPartBase
    {
        private readonly List<OfficeDrawingPartBase> items = new List<OfficeDrawingPartBase>();

        protected CompositeOfficeDrawingPartBase()
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

        public List<OfficeDrawingPartBase> Items =>
            this.items;
    }
}

