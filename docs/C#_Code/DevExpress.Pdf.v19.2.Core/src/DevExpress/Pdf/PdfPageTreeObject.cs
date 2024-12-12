namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfPageTreeObject : PdfObject
    {
        internal const string ParentDictionaryKey = "Parent";
        private const string resourcesDictionaryKey = "Resources";
        private const string mediaBoxDictionaryKey = "MediaBox";
        private const string cropBoxDictionaryKey = "CropBox";
        private const string rotateDictionaryKey = "Rotate";
        private readonly PdfDocumentCatalog documentCatalog;
        private PdfResources resources;
        private PdfObjectReference resourcesReference;
        private PdfPageTreeNode parent;
        private PdfRectangle mediaBox;
        private PdfRectangle cropBox;
        private int rotate;

        protected PdfPageTreeObject(PdfPageTreeNode parent, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            PdfObjectCollection objects = dictionary.Objects;
            this.documentCatalog = objects.DocumentCatalog;
            this.parent = parent;
            this.resources = dictionary.GetResources("Resources", (parent == null) ? null : parent.resources, true, true);
            this.mediaBox = dictionary.GetRectangle("MediaBox");
            this.cropBox = dictionary.GetRectangle("CropBox");
            PdfObjectReference objectReference = dictionary.GetObjectReference("Parent");
            bool flag = ReferenceEquals(parent, null);
            if (!flag)
            {
                this.mediaBox ??= parent.mediaBox;
                this.cropBox ??= parent.cropBox;
            }
            else if (objectReference != null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if ((this.mediaBox != null) && ((this.mediaBox.Width < 1.0) && (this.mediaBox.Height < 1.0)))
            {
                this.mediaBox = PdfPaperSize.Letter;
            }
            int? integer = dictionary.GetInteger("Rotate");
            this.rotate = ((int) Math.Round((double) (((double) NormalizeRotate((integer != null) ? integer.GetValueOrDefault() : (flag ? 0 : parent.rotate))) / 90.0))) * 90;
            this.UpdateBoxes();
        }

        protected PdfPageTreeObject(PdfDocumentCatalog documentCatalog, PdfRectangle mediaBox, PdfRectangle cropBox, int rotate)
        {
            this.documentCatalog = documentCatalog;
            this.mediaBox = mediaBox;
            this.cropBox = cropBox;
            this.Rotate = rotate;
            this.CheckBox(cropBox, PdfCoreStringId.MsgIncorrectPageCropBox);
            this.resources = new PdfResources(documentCatalog, true);
        }

        protected void CheckBox(PdfRectangle box, PdfCoreStringId messageId)
        {
            if ((box != null) && !this.mediaBox.Contains(box))
            {
                throw new ArgumentOutOfRangeException(PdfCoreLocalizer.GetString(messageId));
            }
        }

        internal static bool CheckRotate(int rotate) => 
            (rotate == 0) || ((rotate == 90) || ((rotate == 180) || (rotate == 270)));

        protected internal virtual void FlushPageData(PdfObjectCollection objects)
        {
            this.resourcesReference = objects.AddObject((PdfObject) this.resources);
            this.resources = null;
        }

        internal static int NormalizeRotate(int rotate)
        {
            int num = -360;
            if (rotate < 0)
            {
                num = 360;
            }
            while (((rotate < 0) && (num > 0)) || ((rotate > 270) && (num < 0)))
            {
                rotate += num;
            }
            return rotate;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            if (this.parent == null)
            {
                dictionary.Add("MediaBox", this.mediaBox);
                dictionary.Add("CropBox", this.cropBox, this.mediaBox);
                dictionary.Add("Rotate", this.rotate, 0);
            }
            else
            {
                PdfPageTreeNode pageNode = objects.DocumentCatalog.Pages.GetPageNode(objects, false);
                dictionary.Add("Parent", new PdfObjectReference(pageNode.ObjectNumber));
                dictionary.Add("MediaBox", this.mediaBox, pageNode.mediaBox);
                dictionary.Add("CropBox", this.CropBox, pageNode.CropBox);
                dictionary.Add("Rotate", this.rotate, pageNode.Rotate);
            }
            if (this.resourcesReference == null)
            {
                dictionary.Add("Resources", this.resources);
            }
            else
            {
                dictionary.Add("Resources", this.resourcesReference);
            }
            return dictionary;
        }

        protected virtual void UpdateBoxes()
        {
            PdfRectangle cropBox = this.CropBox;
            if ((this.mediaBox != null) && ((cropBox != null) && !this.mediaBox.Contains(cropBox)))
            {
                this.cropBox = this.mediaBox.Trim(cropBox);
            }
        }

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;

        internal PdfResources Resources
        {
            get
            {
                this.resources ??= new PdfResources(this.documentCatalog, false);
                return this.resources;
            }
        }

        internal PdfPageTreeNode Parent
        {
            get => 
                this.parent;
            set => 
                this.parent = value;
        }

        public PdfRectangle MediaBox
        {
            get => 
                this.mediaBox;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                this.mediaBox = value;
                this.UpdateBoxes();
            }
        }

        public PdfRectangle CropBox
        {
            get => 
                this.cropBox ?? this.mediaBox;
            set
            {
                this.CheckBox(value, PdfCoreStringId.MsgIncorrectPageCropBox);
                this.cropBox = value;
            }
        }

        public int Rotate
        {
            get => 
                this.rotate;
            set
            {
                value = NormalizeRotate(value);
                if (!CheckRotate(value))
                {
                    throw new ArgumentOutOfRangeException("Rotate", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPageRotate));
                }
                this.rotate = value;
            }
        }
    }
}

