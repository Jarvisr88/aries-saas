namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

    public class PdfSignatureEditorPresenter
    {
        private readonly IPdfSignatureEditorView view;
        private readonly PdfSignatureOptions options;

        public PdfSignatureEditorPresenter(PdfSignatureOptions options, IPdfSignatureEditorView view)
        {
            this.view = view;
            this.options = options;
            this.InitializeCertificateItems();
            view.Reason = options.Reason;
            view.ContactInfo = options.ContactInfo;
            view.Location = options.Location;
            view.SelectedCertificateItemChanged += (o, e) => this.InvalidateTextEditorsisEnabled();
            this.InvalidateTextEditorsisEnabled();
            view.Submit += (o, e) => this.OnSubmit();
        }

        private static ICertificateItem FindCertificateItem(List<ICertificateItem> certificateItems, X509Certificate2 certificate)
        {
            ICertificateItem item2;
            using (List<ICertificateItem>.Enumerator enumerator = certificateItems.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ICertificateItem current = enumerator.Current;
                        if (!(current is CertificateItem) || !Equals(certificate, ((CertificateItem) current).Certificate))
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return NoneCertificateItem.Instance;
                    }
                    break;
                }
            }
            return item2;
        }

        private void InitializeCertificateItems()
        {
            Converter<object, ICertificateItem> converter = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Converter<object, ICertificateItem> local1 = <>c.<>9__3_0;
                converter = <>c.<>9__3_0 = x => new CertificateItem((X509Certificate2) x);
            }
            List<ICertificateItem> items = new List<ICertificateItem>(SelectCertificates().ConvertAll<ICertificateItem>(converter));
            items.Insert(0, NoneCertificateItem.Instance);
            this.view.FillCertificateItems(items);
            this.view.SelectedCertificateItem = (this.options.Certificate != null) ? FindCertificateItem(items, this.options.Certificate) : NoneCertificateItem.Instance;
        }

        private void InvalidateTextEditorsisEnabled()
        {
            this.view.EnableTextEditors(!ReferenceEquals(this.view.SelectedCertificateItem, NoneCertificateItem.Instance));
        }

        private void OnSubmit()
        {
            if (ReferenceEquals(this.view.SelectedCertificateItem, NoneCertificateItem.Instance))
            {
                this.options.Certificate = null;
            }
            else
            {
                CertificateItem selectedCertificateItem = (CertificateItem) this.view.SelectedCertificateItem;
                this.options.Certificate = selectedCertificateItem.Certificate;
                this.options.Reason = this.view.Reason;
                this.options.Location = this.view.Location;
                this.options.ContactInfo = this.view.ContactInfo;
            }
        }

        private static X509Certificate2Collection SelectCertificates()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly);
            return store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, true).Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfSignatureEditorPresenter.<>c <>9 = new PdfSignatureEditorPresenter.<>c();
            public static Converter<object, ICertificateItem> <>9__3_0;

            internal ICertificateItem <InitializeCertificateItems>b__3_0(object x) => 
                new CertificateItem((X509Certificate2) x);
        }
    }
}

