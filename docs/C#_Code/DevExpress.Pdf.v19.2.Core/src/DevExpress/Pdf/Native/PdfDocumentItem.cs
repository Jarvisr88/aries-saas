namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfDocumentItem
    {
        private readonly int objectGeneration;
        private int objectNumber;

        protected PdfDocumentItem(int number, int generation)
        {
            this.objectNumber = number;
            this.objectGeneration = generation;
        }

        protected internal int ObjectNumber
        {
            get => 
                this.objectNumber;
            set => 
                this.objectNumber = value;
        }

        protected internal int ObjectGeneration =>
            this.objectGeneration;
    }
}

