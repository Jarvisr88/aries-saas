namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using System;

    public class DXWebEmptyControlCollection : DXWebControlCollection
    {
        public DXWebEmptyControlCollection(DXWebControlBase owner) : base(owner)
        {
        }

        public override void Add(DXWebControlBase child)
        {
            throw new NotSupportedException();
        }

        public override void AddAt(int index, DXWebControlBase child)
        {
            throw new NotSupportedException();
        }
    }
}

