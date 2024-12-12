namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class CertificateItem : ICertificateItem
    {
        public CertificateItem(X509Certificate2 cert)
        {
            this.Subject = GetName(cert.SubjectName);
            this.Issuer = GetName(cert.IssuerName);
            this.From = new DateTime?(cert.NotBefore);
            this.To = new DateTime?(cert.NotAfter);
            this.Certificate = cert;
        }

        private static string GetName(X500DistinguishedName distinguishedName)
        {
            char[] separator = new char[] { '\r', '\n' };
            foreach (string str in distinguishedName.Decode(X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.DoNotUsePlusSign).Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str.StartsWith("CN="))
                {
                    return str.Substring(3);
                }
            }
            return "";
        }

        private static string IssuerText =>
            PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignature_Issuer);

        private static string ValidRangeText =>
            PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignature_ValidRange);

        public string Subject { get; private set; }

        public string Issuer { get; private set; }

        public DateTime? From { get; private set; }

        public DateTime? To { get; private set; }

        public X509Certificate2 Certificate { get; private set; }

        public string Description
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(IssuerText);
                builder.AppendLine(this.Issuer);
                builder.AppendFormat(ValidRangeText, this.From, this.To);
                return builder.ToString();
            }
        }
    }
}

