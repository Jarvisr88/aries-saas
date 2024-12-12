namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfPage : PdfPageTreeItem, IPdfContentsOwner
    {
        private PdfContents contents;
        private PdfXObjects xObjects;
        private PdfFonts fonts;
        private PdfAnnotations annotations;
        private PdfArray procSet;
        private PdfObjectCollection<PdfTransparencyGS> transparencyGSCollection;
        private PdfObjectCollection<PdfShading> shadingCollection;
        private PdfObjectCollection<PdfPattern> patternCollection;

        public PdfPage(PdfPageTreeItem parent, bool compressed) : base(parent, compressed)
        {
            this.xObjects = new PdfXObjects();
            this.fonts = new PdfFonts();
            this.annotations = new PdfAnnotations();
            this.procSet = new PdfArray();
            this.transparencyGSCollection = new PdfObjectCollection<PdfTransparencyGS>();
            this.shadingCollection = new PdfObjectCollection<PdfShading>();
            this.patternCollection = new PdfObjectCollection<PdfPattern>();
            this.procSet.Add("PDF");
            this.procSet.Add("Text");
            this.procSet.Add("ImageC");
        }

        public void AddAnnotation(PdfAnnotation annotation)
        {
            this.annotations.AddUnique(annotation);
        }

        public void AddPattern(PdfPattern pattern)
        {
            this.patternCollection.AddUnique(pattern);
        }

        public void AddShading(PdfShading shading)
        {
            this.shadingCollection.AddUnique(shading);
        }

        public void AddTransparencyGS(PdfTransparencyGS transparencyGS)
        {
            this.transparencyGSCollection.AddUnique(transparencyGS);
        }

        private void FillAnnotations()
        {
            if (this.annotations.Count > 0)
            {
                PdfArray array = new PdfArray();
                int num = 0;
                while (true)
                {
                    if (num >= this.annotations.Count)
                    {
                        base.Dictionary.Add("Annots", array);
                        break;
                    }
                    array.Add(this.annotations[num].Dictionary);
                    num++;
                }
            }
        }

        private void FillFonts(PdfDictionary resourceDictionary)
        {
            if (this.fonts.Count > 0)
            {
                PdfDictionary dictionary = new PdfDictionary();
                int num = 0;
                while (true)
                {
                    if (num >= this.fonts.Count)
                    {
                        resourceDictionary.Add("Font", dictionary);
                        break;
                    }
                    dictionary.Add(this.fonts[num].Name, this.fonts[num].Dictionary);
                    num++;
                }
            }
        }

        private void FillProcSets(PdfDictionary resourceDictionary)
        {
            if (this.procSet.Count > 0)
            {
                resourceDictionary.Add("ProcSet", this.procSet);
            }
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", new PdfName("Page"));
            PdfDictionary resourceDictionary = new PdfDictionary();
            this.FillFonts(resourceDictionary);
            resourceDictionary.AddIfNotNull("XObject", this.xObjects.CreateDictionary());
            resourceDictionary.AddIfNotNull("ExtGState", this.transparencyGSCollection.CreateDictionary());
            resourceDictionary.AddIfNotNull("Shading", this.shadingCollection.CreateDictionary());
            resourceDictionary.AddIfNotNull("Pattern", this.patternCollection.CreateDictionary());
            this.FillProcSets(resourceDictionary);
            base.Dictionary.Add("Resources", resourceDictionary);
            this.FillAnnotations();
            base.Dictionary.Add("Contents", this.contents.InnerObject);
            base.FillUp();
        }

        public void InitializeContents(PdfContents contents)
        {
            this.contents ??= contents;
        }

        PdfFonts IPdfContentsOwner.Fonts =>
            this.fonts;

        public PdfArray ProcSet =>
            this.procSet;

        public PdfXObjects XObjects =>
            this.xObjects;
    }
}

