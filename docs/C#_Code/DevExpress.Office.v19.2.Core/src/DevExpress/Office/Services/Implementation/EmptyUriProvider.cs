namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public class EmptyUriProvider : IUriProvider
    {
        private int counter;

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            if (string.IsNullOrEmpty(styleText))
            {
                return string.Empty;
            }
            this.counter++;
            return $"css{this.counter}.css";
        }

        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            this.counter++;
            return $"img{this.counter}.img";
        }
    }
}

