namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfEnvironmentHelper
    {
        private static PdfEnvironmentHelper instance;
        private readonly bool shouldUseKerning;

        private PdfEnvironmentHelper()
        {
            System.Version version = Environment.OSVersion.Version;
            int major = version.Major;
            this.shouldUseKerning = (major > 6) || ((major == 6) && (version.Minor > 1));
        }

        public static bool ShouldUseKerning
        {
            get
            {
                instance ??= new PdfEnvironmentHelper();
                return instance.shouldUseKerning;
            }
        }
    }
}

