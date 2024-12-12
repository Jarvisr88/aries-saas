namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    public class PdfCompositeClip : PdfGraphicsClip
    {
        private readonly IList<PdfGraphicsClip> clipList;

        private PdfCompositeClip(IList<PdfGraphicsClip> clipList)
        {
            this.clipList = clipList;
        }

        public PdfCompositeClip(PdfGraphicsClip first, PdfGraphicsClip second) : this(list1)
        {
            List<PdfGraphicsClip> list1 = new List<PdfGraphicsClip>(2);
            list1.Add(first);
            list1.Add(second);
        }

        public override void Apply(PdfGraphicsCommandConstructor constructor)
        {
            foreach (PdfGraphicsClip clip in this.clipList)
            {
                clip.Apply(constructor);
            }
        }

        public override PdfGraphicsClip ApplyTransform(Matrix matrix)
        {
            IList<PdfGraphicsClip> clipList = new List<PdfGraphicsClip>(this.clipList.Count);
            foreach (PdfGraphicsClip clip in this.clipList)
            {
                clipList.Add(clip.ApplyTransform(matrix));
            }
            return new PdfCompositeClip(clipList);
        }

        public override RectangleF GetBounds(Matrix boundsTransform)
        {
            RectangleF bounds = this.clipList[0].GetBounds(boundsTransform);
            foreach (PdfGraphicsClip clip in this.clipList.Skip<PdfGraphicsClip>(1))
            {
                bounds = RectangleF.Intersect(bounds, clip.GetBounds(boundsTransform));
            }
            return bounds;
        }

        public override PdfGraphicsClip Intersect(PdfGraphicsClip clip)
        {
            this.clipList.Add(clip);
            return this;
        }
    }
}

