namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfXObject : PdfDocumentStreamObject
    {
        private string name;

        protected PdfXObject(string name, bool compressed) : base(compressed)
        {
            this.name = name;
        }

        public override void AddToDictionary(PdfDictionary dictionary)
        {
            dictionary.Add(this.Name, base.Stream);
        }

        public override void FillUp()
        {
            base.Attributes.Add("Type", "XObject");
            base.Attributes.Add("Name", this.name);
            base.FillUp();
        }

        public string Name =>
            this.name;
    }
}

