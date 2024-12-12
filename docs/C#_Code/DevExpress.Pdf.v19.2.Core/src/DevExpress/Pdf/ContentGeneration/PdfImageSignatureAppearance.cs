namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.IO;

    public class PdfImageSignatureAppearance : PdfSignatureAppearance
    {
        private readonly ISignatureImageDataProvider imageDataProvider;
        private readonly PdfOrientedRectangle actualBounds;

        public PdfImageSignatureAppearance(Stream imageStream, PdfOrientedRectangle signatureBounds, int pageIndex) : base(signatureBounds.BoundingRectangle, pageIndex)
        {
            this.imageDataProvider = new PdfStreamSignatureImageDataProvider(imageStream);
            this.actualBounds = signatureBounds;
        }

        public PdfImageSignatureAppearance(byte[] imageStream, PdfOrientedRectangle signatureBounds, int pageIndex) : base(signatureBounds.BoundingRectangle, pageIndex)
        {
            this.imageDataProvider = new PdfArraySignatureImageDataProvider(imageStream);
            this.actualBounds = signatureBounds;
        }

        protected override void DrawFormContent(PdfFormCommandConstructor formConstructor)
        {
            using (PdfGraphicsDocument document = new PdfGraphicsDocument(formConstructor.DocumentCatalog, 0))
            {
                PdfXObjectResourceCache imageCache = document.ImageCache;
                this.imageDataProvider.PerformActionWithStream(delegate (Stream imageStream) {
                    if (PdfRecognizedImageInfo.DetectImage(imageStream).Type != PdfRecognizedImageFormat.Metafile)
                    {
                        this.DrawXObject(formConstructor, imageCache.AddXObject(imageStream));
                    }
                    else
                    {
                        try
                        {
                            EmfMetafile emfMetafile = EmfMetafile.Create(this.imageDataProvider.Bytes);
                            if (emfMetafile != null)
                            {
                                this.DrawXObject(formConstructor, imageCache.AddXObject(emfMetafile));
                                return;
                            }
                        }
                        catch
                        {
                        }
                        using (Image image = Image.FromStream(imageStream))
                        {
                            this.DrawXObject(formConstructor, imageCache.AddXObject(image, imageCache.ExtractSMask ? Color.Transparent : Color.White, 300));
                        }
                    }
                });
            }
        }

        private void DrawXObject(PdfFormCommandConstructor constructor, PdfXObjectCachedResource resource)
        {
            using (resource)
            {
                PdfRectangle boundingBox;
                if (this.actualBounds.Angle == 0.0)
                {
                    boundingBox = constructor.BoundingBox;
                }
                else
                {
                    PdfRectangle boundingRectangle = this.actualBounds.BoundingRectangle;
                    PdfPoint point = new PdfPoint(this.actualBounds.Left - boundingRectangle.Left, this.actualBounds.Top - boundingRectangle.Bottom);
                    PdfTransformationMatrix matrix = PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -point.X, -point.Y), PdfTransformationMatrix.CreateRotate(this.actualBounds.Angle * 57.295779513082323)), new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, point.X, point.Y));
                    constructor.ModifyTransformationMatrix(matrix);
                    boundingBox = new PdfRectangle(point.X, point.Y - this.actualBounds.Height, point.X + this.actualBounds.Width, point.Y);
                }
                double width = resource.Width;
                double height = resource.Height;
                double num3 = Math.Min((double) (boundingBox.Width / width), (double) (boundingBox.Height / height));
                double num4 = width * num3;
                double num5 = height * num3;
                double left = boundingBox.Left + ((boundingBox.Width - num4) / 2.0);
                double bottom = boundingBox.Bottom + ((boundingBox.Height - num5) / 2.0);
                resource.Draw(constructor, new PdfRectangle(left, bottom, left + num4, bottom + num5));
            }
        }

        private interface ISignatureImageDataProvider
        {
            void PerformActionWithStream(Action<Stream> action);

            byte[] Bytes { get; }
        }

        private class PdfArraySignatureImageDataProvider : PdfImageSignatureAppearance.ISignatureImageDataProvider
        {
            private readonly byte[] imageBytes;

            public PdfArraySignatureImageDataProvider(byte[] imageBytes)
            {
                this.imageBytes = imageBytes;
            }

            public void PerformActionWithStream(Action<Stream> action)
            {
                using (MemoryStream stream = new MemoryStream(this.imageBytes))
                {
                    action(stream);
                }
            }

            public byte[] Bytes =>
                this.imageBytes;
        }

        private class PdfStreamSignatureImageDataProvider : PdfImageSignatureAppearance.ISignatureImageDataProvider
        {
            private readonly Stream imageStream;

            public PdfStreamSignatureImageDataProvider(Stream imageStream)
            {
                this.imageStream = imageStream;
            }

            public void PerformActionWithStream(Action<Stream> action)
            {
                action(this.imageStream);
            }

            public byte[] Bytes
            {
                get
                {
                    byte[] buffer = new byte[this.imageStream.Length];
                    this.imageStream.Read(buffer, 0, buffer.Length);
                    this.imageStream.Position = 0L;
                    return buffer;
                }
            }
        }
    }
}

