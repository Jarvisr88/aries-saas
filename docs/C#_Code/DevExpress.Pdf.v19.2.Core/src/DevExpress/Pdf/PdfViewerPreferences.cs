namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfViewerPreferences
    {
        private const string hideToolbarDictionaryKey = "HideToolbar";
        private const string hideMenubarDictionaryKey = "HideMenubar";
        private const string hideWindowUIDictionaryKey = "HideWindowUI";
        private const string fitWindowDictionaryKey = "FitWindow";
        private const string centerWindowDictionaryKey = "CenterWindow";
        private const string displayDocTitleDictionaryKey = "DisplayDocTitle";
        private const string nonFullScreenPageModeDictionaryKey = "NonFullScreenPageMode";
        private const string directionDictionaryKey = "Direction";
        private const string viewAreaDictionaryKey = "ViewArea";
        private const string viewClipDictionaryKey = "ViewClip";
        private const string printAreaDictionaryKey = "PrintArea";
        private const string printClipDictionaryKey = "PrintClip";
        private const string printScalingDictionaryKey = "PrintScaling";
        private const string printModeDictionaryKey = "Duplex";
        private const string pickTrayByPDFSizeDictionaryKey = "PickTrayByPDFSize";
        private const string printPageRangeDictionaryKey = "PrintPageRange";
        private const string printNumCopiesDictionaryKey = "NumCopies";
        private readonly bool hideToolbar;
        private readonly bool hideMenubar;
        private readonly bool hideWindowUI;
        private readonly bool fitWindow;
        private readonly bool centerWindow;
        private readonly bool displayDocTitle;
        private readonly PdfNonFullScreenPageMode nonFullScreenPageMode;
        private readonly PdfDirection direction;
        private readonly PdfViewArea viewArea;
        private readonly PdfViewArea viewClip;
        private readonly PdfViewArea printArea;
        private readonly PdfViewArea printClip;
        private readonly PdfPrintScaling printScaling;
        private readonly PdfPrintMode printMode;
        private readonly bool pickTrayByPDFSize;
        private readonly List<PdfPrintPageRange> printPageRange;
        private readonly int printNumCopies;

        internal PdfViewerPreferences()
        {
            this.printPageRange = new List<PdfPrintPageRange>();
            this.direction = PdfDirection.RightToLeft;
        }

        internal PdfViewerPreferences(PdfReaderDictionary dictionary)
        {
            object obj2;
            this.printPageRange = new List<PdfPrintPageRange>();
            bool? boolean = dictionary.GetBoolean("HideToolbar", true);
            this.hideToolbar = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("HideMenubar", true);
            this.hideMenubar = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("HideWindowUI", true);
            this.hideWindowUI = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("FitWindow", true);
            this.fitWindow = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("CenterWindow", true);
            this.centerWindow = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("DisplayDocTitle", true);
            this.displayDocTitle = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.nonFullScreenPageMode = dictionary.GetEnumName<PdfNonFullScreenPageMode>("NonFullScreenPageMode");
            this.direction = dictionary.GetEnumName<PdfDirection>("Direction");
            this.viewArea = dictionary.GetEnumName<PdfViewArea>("ViewArea");
            this.viewClip = dictionary.GetEnumName<PdfViewArea>("ViewClip");
            this.printArea = dictionary.GetEnumName<PdfViewArea>("PrintArea");
            this.printClip = dictionary.GetEnumName<PdfViewArea>("PrintClip");
            if (!dictionary.TryGetValue("PrintScaling", out obj2))
            {
                this.printScaling = PdfPrintScaling.AppDefault;
            }
            else
            {
                obj2 = dictionary.Objects.TryResolve(obj2, null);
                if (obj2 == null)
                {
                    this.printScaling = PdfPrintScaling.AppDefault;
                }
                else
                {
                    PdfName name = obj2 as PdfName;
                    if (name != null)
                    {
                        this.printScaling = PdfEnumToStringConverter.Parse<PdfPrintScaling>(name.Name, true);
                    }
                    else
                    {
                        byte[] buffer = obj2 as byte[];
                        if (buffer != null)
                        {
                            this.printScaling = PdfEnumToStringConverter.Parse<PdfPrintScaling>(PdfDocumentReader.ConvertToString(buffer), true);
                        }
                        else
                        {
                            if (!(obj2 as bool))
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.printScaling = ((bool) obj2) ? PdfPrintScaling.AppDefault : PdfPrintScaling.None;
                        }
                    }
                }
            }
            this.printMode = dictionary.GetEnumName<PdfPrintMode>("Duplex");
            boolean = dictionary.GetBoolean("PickTrayByPDFSize");
            this.pickTrayByPDFSize = (boolean != null) ? boolean.GetValueOrDefault() : true;
            IList<object> array = dictionary.GetArray("PrintPageRange");
            if (array != null)
            {
                int count = array.Count;
                if ((count % 2) > 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                count /= 2;
                int num2 = 0;
                int num3 = 0;
                while (num2 < count)
                {
                    int? nullable2 = array[num3++] as int?;
                    int? nullable3 = array[num3++] as int?;
                    this.printPageRange.Add(new PdfPrintPageRange(nullable2.GetValueOrDefault(1), nullable3.GetValueOrDefault(1)));
                    num2++;
                }
            }
            int? integer = dictionary.GetInteger("NumCopies");
            this.printNumCopies = (integer != null) ? integer.GetValueOrDefault() : 1;
        }

        internal PdfDictionary Write()
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            dictionary.Add("HideToolbar", this.hideToolbar, false);
            dictionary.Add("HideMenubar", this.hideMenubar, false);
            dictionary.Add("HideWindowUI", this.hideWindowUI, false);
            dictionary.Add("FitWindow", this.fitWindow, false);
            dictionary.Add("CenterWindow", this.centerWindow, false);
            dictionary.Add("DisplayDocTitle", this.displayDocTitle, false);
            dictionary.AddEnumName<PdfNonFullScreenPageMode>("NonFullScreenPageMode", this.nonFullScreenPageMode);
            dictionary.AddEnumName<PdfDirection>("Direction", this.direction);
            dictionary.AddEnumName<PdfViewArea>("ViewArea", this.viewArea);
            dictionary.AddEnumName<PdfViewArea>("ViewClip", this.viewClip);
            dictionary.AddEnumName<PdfViewArea>("PrintArea", this.printArea);
            dictionary.AddEnumName<PdfViewArea>("PrintClip", this.printClip);
            dictionary.AddEnumName<PdfPrintScaling>("PrintScaling", this.printScaling);
            dictionary.AddEnumName<PdfPrintMode>("Duplex", this.printMode);
            dictionary.Add("PickTrayByPDFSize", this.pickTrayByPDFSize, true);
            if (this.printPageRange.Count > 0)
            {
                List<object> list = new List<object>();
                foreach (PdfPrintPageRange range in this.printPageRange)
                {
                    list.Add(range.Start);
                    list.Add(range.End);
                }
                dictionary.Add("PrintPageRange", list);
            }
            dictionary.Add("NumCopies", this.printNumCopies, 1);
            return dictionary;
        }

        public bool HideToolbar =>
            this.hideToolbar;

        public bool HideMenubar =>
            this.hideMenubar;

        public bool HideWindowUI =>
            this.hideWindowUI;

        public bool FitWindow =>
            this.fitWindow;

        public bool CenterWindow =>
            this.centerWindow;

        public bool DisplayDocTitle =>
            this.displayDocTitle;

        public PdfNonFullScreenPageMode NonFullScreenPageMode =>
            this.nonFullScreenPageMode;

        public PdfDirection Direction =>
            this.direction;

        public PdfViewArea ViewArea =>
            this.viewArea;

        public PdfViewArea ViewClip =>
            this.viewClip;

        public PdfViewArea PrintArea =>
            this.printArea;

        public PdfViewArea PrintClip =>
            this.printClip;

        public PdfPrintScaling PrintScaling =>
            this.printScaling;

        public PdfPrintMode PrintMode =>
            this.printMode;

        public bool PickTrayByPDFSize =>
            this.pickTrayByPDFSize;

        public IList<PdfPrintPageRange> PrintPageRange =>
            this.printPageRange;

        public int PrintNumCopies =>
            this.printNumCopies;
    }
}

