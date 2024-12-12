namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.CompilerServices;

    public static class PdfRenderingSettings
    {
        private static bool useExternalDctDecoder = true;

        public static bool UseExternalDctDecoder
        {
            get => 
                useExternalDctDecoder;
            set => 
                useExternalDctDecoder = value;
        }

        [Obsolete("This API is now obsolete.")]
        public static string ExternalDctDecoderModulePath { get; set; }
    }
}

