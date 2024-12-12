namespace DevExpress.Pdf.Native
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;

    public static class PdfEmbeddedResourceProvider
    {
        public static Stream GetDecompressedEmbeddedResourceStream(string resourceName)
        {
            MemoryStream destination = new MemoryStream();
            using (DeflateStream stream2 = new DeflateStream(GetEmbeddedResourceStream(resourceName), CompressionMode.Decompress))
            {
                stream2.CopyTo(destination);
            }
            destination.Position = 0L;
            return destination;
        }

        public static Stream GetEmbeddedResourceStream(string resourceName) => 
            GetExecutingAssembly().GetManifestResourceStream("DevExpress.Pdf." + resourceName);

        private static Assembly GetExecutingAssembly() => 
            typeof(PdfEmbeddedResourceProvider).GetAssembly();
    }
}

