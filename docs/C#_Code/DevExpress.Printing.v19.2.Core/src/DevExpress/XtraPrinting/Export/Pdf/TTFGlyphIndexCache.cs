namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;

    internal class TTFGlyphIndexCache
    {
        private HashSet<ushort> innerList = new HashSet<ushort>();

        public bool Add(ushort glyphIndex)
        {
            if (this.innerList.Contains(glyphIndex))
            {
                return false;
            }
            this.innerList.Add(glyphIndex);
            return true;
        }

        public void Clear()
        {
            this.innerList.Clear();
        }

        public int Count =>
            this.innerList.Count;

        public ushort[] ToArray
        {
            get
            {
                List<ushort> list = new List<ushort>(this.innerList);
                list.Sort();
                return list.ToArray();
            }
        }
    }
}

