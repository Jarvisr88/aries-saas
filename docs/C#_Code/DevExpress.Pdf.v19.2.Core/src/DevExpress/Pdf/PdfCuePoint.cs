namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCuePoint
    {
        private const string cuePointDictionaryName = "CuePoint";
        private const string timeKey = "Time";
        private const string nameKey = "Name";
        private readonly double time;
        private readonly PdfCuePointKind kind;
        private readonly string name;
        private readonly PdfAction action;

        internal PdfCuePoint(PdfReaderDictionary dictionary)
        {
            double? number = dictionary.GetNumber("Time");
            if (number == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.time = number.Value;
            string name = dictionary.GetName("Subtype");
            this.name = dictionary.GetString("Name");
            if (this.name != null)
            {
                if ((string.IsNullOrEmpty(this.name) || ((name != null) && (name != "Navigation"))) || dictionary.ContainsKey("A"))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.kind = PdfCuePointKind.Navigation;
            }
            else
            {
                this.action = dictionary.GetAction("A");
                if ((this.action == null) || ((name != null) && (name != "Event")))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.kind = PdfCuePointKind.Event;
            }
        }

        private string CuePointKindToString() => 
            (this.kind != PdfCuePointKind.Event) ? "Navigation" : "Event";

        internal PdfWriterDictionary Write(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("Type", "CuePoint");
            dictionary.Add("Time", this.time);
            dictionary.AddName("Subtype", this.CuePointKindToString());
            if (!string.IsNullOrEmpty(this.name))
            {
                dictionary.Add("Name", this.name);
            }
            else
            {
                dictionary.Add("A", this.action);
            }
            return dictionary;
        }

        public double Time =>
            this.time;

        public PdfCuePointKind Kind =>
            this.kind;

        public string Name =>
            this.name;

        public PdfAction Action =>
            this.action;
    }
}

