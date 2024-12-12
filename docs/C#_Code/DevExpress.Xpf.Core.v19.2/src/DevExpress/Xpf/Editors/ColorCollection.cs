namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;

    public class ColorCollection : List<Color>
    {
        internal ColorCollection()
        {
        }

        public ColorCollection(IEnumerable<Color> source) : base(source)
        {
        }
    }
}

