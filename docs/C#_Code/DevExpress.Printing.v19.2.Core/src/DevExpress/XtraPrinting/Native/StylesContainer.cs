namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System.Collections.Generic;

    public class StylesContainer : ObjectContainer<BrickStyle>
    {
        public BrickStyle GetStyle(BrickStyle style);

        protected override IEqualityComparer<object> EqualityComparer { get; }
    }
}

