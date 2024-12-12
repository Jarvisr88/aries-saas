namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using DevExpress.Text.Fonts.DirectWrite.CCW;
    using System;
    using System.Collections.Generic;

    public class DirectWriteFontEngine : DXFontEngine
    {
        private readonly Lazy<DirectWriteSystemFontCollection> systemFontCollection;
        private readonly DWriteFontCollectionLoader collectionLoader = new DWriteFontCollectionLoader();
        private readonly DirectWriteFontMeasurer measurer;
        private DWriteFactory factory = DWriteStandardInterop.CreateDwriteFactory(false);

        public DirectWriteFontEngine()
        {
            this.factory.RegisterFontCollectionLoader(this.collectionLoader);
            this.measurer = new DirectWriteFontMeasurer(this.factory);
            this.systemFontCollection = new Lazy<DirectWriteSystemFontCollection>(() => new DirectWriteSystemFontCollection(this.factory, this.measurer, this.factory.GetSystemFontCollection(true)), false);
        }

        public override IDXPrivateFontCollection CreatePrivateFontCollection(IEnumerable<byte[]> fontFiles) => 
            new DirectWritePrivateFontCollection(this.factory, this.measurer, this.collectionLoader, fontFiles);

        public override void Dispose()
        {
            if (this.systemFontCollection.IsValueCreated)
            {
                this.systemFontCollection.Value.Dispose();
            }
            if (this.factory != null)
            {
                this.factory.UnregisterFontCollectionLoader(this.collectionLoader);
                this.factory.Dispose();
                this.factory = null;
            }
        }

        public override IDXSystemFontCollection SystemFontCollection =>
            this.systemFontCollection.Value;
    }
}

