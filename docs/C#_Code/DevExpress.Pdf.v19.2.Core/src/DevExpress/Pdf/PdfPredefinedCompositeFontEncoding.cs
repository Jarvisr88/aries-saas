namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfPredefinedCompositeFontEncoding : PdfCompositeFontEncoding
    {
        private static readonly List<string> horizontalEncodingNames;
        private static readonly List<string> verticalEncodingNames;
        private readonly string name;
        private readonly bool isVertical;
        private readonly Lazy<PdfCIDCMap> encoding;

        static PdfPredefinedCompositeFontEncoding()
        {
            List<string> list1 = new List<string>();
            list1.Add("GB-EUC-H");
            list1.Add("GBpc-EUC-H");
            list1.Add("GBK-EUC-H");
            list1.Add("GBKp-EUC-H");
            list1.Add("GBK2K-H");
            list1.Add("UniGB-UCS2-H");
            list1.Add("UniGB-UTF16-H");
            list1.Add("B5pc-H");
            list1.Add("HKscs-B5-H");
            list1.Add("ETen-B5-H");
            list1.Add("ETenms-B5-H");
            list1.Add("CNS-EUC-H");
            list1.Add("UniCNS-UCS2-H");
            list1.Add("UniCNS-UTF16-H");
            list1.Add("83pv-RKSJ-H");
            list1.Add("90ms-RKSJ-H");
            list1.Add("90msp-RKSJ-H");
            list1.Add("90pv-RKSJ-H");
            list1.Add("Add-RKSJ-H");
            list1.Add("EUC-H");
            list1.Add("Ext-RKSJ-H");
            list1.Add("H");
            list1.Add("UniJIS-UCS2-H");
            list1.Add("UniJIS-UCS2-HW-H");
            list1.Add("UniJIS-UTF16-H");
            list1.Add("KSC-EUC-H");
            list1.Add("KSCms-UHC-H");
            list1.Add("KSCms-UHC-HW-H");
            list1.Add("KSCpc-EUC-H");
            list1.Add("UniKS-UCS2-H");
            list1.Add("UniKS-UTF16-H");
            horizontalEncodingNames = list1;
            List<string> list2 = new List<string>();
            list2.Add("GB-EUC-V");
            list2.Add("GBpc-EUC-V");
            list2.Add("GBK-EUC-V");
            list2.Add("GBKp-EUC-V");
            list2.Add("GBK2K-V");
            list2.Add("UniGB-UCS2-V");
            list2.Add("UniGB-UTF16-V");
            list2.Add("B5pc-V");
            list2.Add("HKscs-B5-V");
            list2.Add("ETen-B5-V");
            list2.Add("ETenms-B5-V");
            list2.Add("CNS-EUC-V");
            list2.Add("UniCNS-UCS2-V");
            list2.Add("UniCNS-UTF16-V");
            list2.Add("90ms-RKSJ-V");
            list2.Add("90msp-RKSJ-V");
            list2.Add("Add-RKSJ-V");
            list2.Add("EUC-V");
            list2.Add("Ext-RKSJ-V");
            list2.Add("V");
            list2.Add("UniJIS-UCS2-V");
            list2.Add("UniJIS-UCS2-HW-V");
            list2.Add("UniJIS-UTF16-V");
            list2.Add("KSC-EUC-V");
            list2.Add("KSCms-UHC-V");
            list2.Add("KSCms-UHC-HW-V");
            list2.Add("UniKS-UCS2-V");
            list2.Add("UniKS-UTF16-V");
            verticalEncodingNames = list2;
        }

        internal PdfPredefinedCompositeFontEncoding(string name)
        {
            this.name = name;
            if (!horizontalEncodingNames.Contains(name))
            {
                if (verticalEncodingNames.Contains(name))
                {
                    this.isVertical = true;
                }
                else
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            this.encoding = new Lazy<PdfCIDCMap>(() => PdfCIDCMap.GetPredefined(name));
        }

        internal override short GetCID(byte[] code) => 
            this.encoding.Value.GetCID(code);

        protected internal override PdfStringCommandData GetStringData(byte[] bytes, double[] glyphOffsets) => 
            this.encoding.Value.GetStringData(bytes, glyphOffsets);

        protected internal override object Write(PdfObjectCollection objects) => 
            new PdfName(this.name);

        public string Name =>
            this.name;

        public override bool IsVertical =>
            this.isVertical;
    }
}

