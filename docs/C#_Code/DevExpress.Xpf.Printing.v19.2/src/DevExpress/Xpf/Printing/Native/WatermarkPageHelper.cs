namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.ReportServer.ServiceModel.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;

    public static class WatermarkPageHelper
    {
        public static Page CopyPage(Page source)
        {
            PrintingSystemXmlSerializer serializer = new PrintingSystemXmlSerializer();
            using (MemoryStream stream = new MemoryStream())
            {
                Page[] pages = new Page[] { source };
                source.Document.SerializeCore(stream, serializer, ContinuousExportInfo.Empty, pages);
                stream.Position = 0L;
                DeserializedPrintingSystem container = new DeserializedPrintingSystem();
                container.ReplaceService<IBrickPagePairFactory>(new FakeBrickPagePairFactory());
                container.Document.Deserialize(stream, new PrintingSystemXmlSerializer());
                return container.Document.Pages[0];
            }
        }

        public static Watermark CopyWatermark(Watermark source)
        {
            Watermark watermark = new Watermark();
            watermark.CopyFrom(source);
            return watermark;
        }

        public static XpfWatermark CopyXpfWatermark(Watermark source)
        {
            XpfWatermark watermark = new XpfWatermark();
            watermark.CopyFrom(source);
            return watermark;
        }

        public static Page CreatePageStub(XtraPageSettingsBase pageSettings)
        {
            PSPage page = new PSPage(pageSettings.Data);
            new PrintingSystem().Document.Pages.Add(page);
            return page;
        }

        private class FakeBrickPagePairFactory : IBrickPagePairFactory
        {
            BrickPagePair IBrickPagePairFactory.CreateBrickPagePair(int[] brickIndexes, int pageIndex) => 
                new FakeBrickPagePair(brickIndexes, pageIndex);

            private class FakeBrickPagePair : BrickPagePair
            {
                public FakeBrickPagePair(int[] brickIndices, int pageIndex) : base(brickIndices, pageIndex, (long) pageIndex)
                {
                }
            }
        }
    }
}

