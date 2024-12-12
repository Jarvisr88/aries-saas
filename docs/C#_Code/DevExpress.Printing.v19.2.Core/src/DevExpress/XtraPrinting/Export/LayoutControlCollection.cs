namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Collections.Generic;

    public class LayoutControlCollection : List<ILayoutControl>
    {
        public LayoutControlCollection()
        {
        }

        public LayoutControlCollection(IEnumerable<ILayoutControl> collection) : base(collection)
        {
        }

        public void Add(ILayoutControl value)
        {
            if (value != null)
            {
                base.Add(value);
            }
        }

        public void AddRange(ILayoutControl[] values)
        {
            foreach (ILayoutControl control in values)
            {
                this.Add(control);
            }
        }
    }
}

