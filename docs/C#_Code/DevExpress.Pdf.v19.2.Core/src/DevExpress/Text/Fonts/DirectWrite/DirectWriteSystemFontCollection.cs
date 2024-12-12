namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class DirectWriteSystemFontCollection : DirectWriteFontCollection, IDXSystemFontCollection, IDXFontCollection
    {
        private const string defaultFontFamily = "Arial";
        private readonly Lazy<DXFontFamily[]> families;
        private readonly DWriteFontCollection collection;
        private readonly DWriteFactory factory;

        public DirectWriteSystemFontCollection(DWriteFactory factory, DirectWriteFontMeasurer measurer, DWriteFontCollection collection) : base(factory, measurer)
        {
            this.collection = collection;
            this.factory = factory;
            this.families = new Lazy<DXFontFamily[]>(new Func<DXFontFamily[]>(this.CreateFamilies));
            base.DefaultFontFamily = "Arial";
        }

        public override void Dispose()
        {
            if (this.families.IsValueCreated)
            {
                foreach (DXFontFamily family in this.families.Value)
                {
                    family.Dispose();
                }
            }
            this.collection.Dispose();
        }

        public override IReadOnlyList<IDXFontFamily> Families =>
            this.families.Value;

        protected override DWriteFontCollection Collection =>
            this.collection;
    }
}

