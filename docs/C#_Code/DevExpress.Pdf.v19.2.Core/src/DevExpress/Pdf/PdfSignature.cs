namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;

    public class PdfSignature : PdfObject
    {
        internal const string ContentsDictionaryKey = "Contents";
        private const string filterDictionaryKey = "Filter";
        private const string subFilterDictionaryKey = "SubFilter";
        private const string byteRangeDictionaryKey = "ByteRange";
        private const string nameDictionaryKey = "Name";
        private const string signingTimeDictionaryKey = "M";
        private const string locationDictionaryKey = "Location";
        private const string reasonDictionaryKey = "Reason";
        private const string contactInfoDictionaryKey = "ContactInfo";
        private readonly X509Certificate2 certificate;
        private readonly string filter;
        private readonly string subFilter;
        private readonly PdfSignatureReference[] reference;
        private readonly int? alteredPagesCount;
        private readonly int? alteredInteractiveFormFieldsCount;
        private readonly int? filledInInteractiveFormFieldsCount;
        private readonly int? handlerVersion;
        private readonly bool shouldUseReference;
        private readonly PdfSignatureBuildProperties buildProperties;
        private PdfPlaceholder contentsPlaceHolder;
        private PdfPlaceholder byteRangePlaceHolder;
        private byte[] contents;
        private PdfSignatureByteRange[] byteRange;
        private string name;
        private DateTimeOffset? signingTime;
        private string location;
        private string reason;
        private string contactInfo;
        private PdfSignatureAppearance appearance;
        private PdfAnnotationFlags annotationFlags;

        internal PdfSignature(PdfReaderDictionary dictionary)
        {
            this.annotationFlags = PdfAnnotationFlags.Locked | PdfAnnotationFlags.Print;
            this.filter = dictionary.GetName("Filter");
            this.subFilter = dictionary.GetName("SubFilter");
            this.contents = dictionary.GetBytes("Contents");
            IList<object> array = dictionary.GetArray("ByteRange");
            if (string.IsNullOrEmpty(this.filter) || ((this.contents == null) || (array == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int count = array.Count;
            if ((count < 4) || ((count % 2) != 0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            count /= 2;
            this.byteRange = new PdfSignatureByteRange[count];
            int index = 0;
            int num3 = 0;
            while (index < count)
            {
                int start = PdfDocumentReader.ConvertToInteger(array[num3++]);
                int length = PdfDocumentReader.ConvertToInteger(array[num3++]);
                if ((start < 0) || (length < 0))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.byteRange[index] = new PdfSignatureByteRange(start, length);
                index++;
            }
            IList<object> list2 = dictionary.GetArray("Reference");
            if (list2 != null)
            {
                PdfObjectCollection objects = dictionary.Objects;
                int num6 = list2.Count;
                this.reference = new PdfSignatureReference[num6];
                for (int i = 0; i < num6; i++)
                {
                    PdfReaderDictionary dictionary3 = objects.TryResolve(list2[i], null) as PdfReaderDictionary;
                    if (dictionary3 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.reference[i] = PdfSignatureReference.Create(dictionary3);
                }
            }
            IList<object> list3 = dictionary.GetArray("Changes");
            if (list3 != null)
            {
                if (list3.Count != 3)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.alteredPagesCount = new int?(PdfDocumentReader.ConvertToInteger(list3[0]));
                this.alteredInteractiveFormFieldsCount = new int?(PdfDocumentReader.ConvertToInteger(list3[1]));
                this.filledInInteractiveFormFieldsCount = new int?(PdfDocumentReader.ConvertToInteger(list3[2]));
            }
            this.name = dictionary.GetString("Name");
            this.signingTime = dictionary.GetDate("M");
            this.location = dictionary.GetString("Location");
            this.reason = dictionary.GetString("Reason");
            this.contactInfo = dictionary.GetString("ContactInfo");
            this.handlerVersion = dictionary.GetInteger("R");
            int? integer = dictionary.GetInteger("V");
            if (integer != null)
            {
                int num8 = integer.Value;
                if (num8 == 0)
                {
                    this.shouldUseReference = false;
                }
                else if (num8 != 1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else
                {
                    this.shouldUseReference = true;
                }
            }
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Prop_Build");
            if (dictionary2 != null)
            {
                this.buildProperties = new PdfSignatureBuildProperties(dictionary2);
            }
        }

        public PdfSignature(X509Certificate2 certificate)
        {
            this.annotationFlags = PdfAnnotationFlags.Locked | PdfAnnotationFlags.Print;
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }
            this.certificate = certificate;
            this.filter = "Adobe.PPKMS";
            this.subFilter = "adbe.pkcs7.sha1";
        }

        public PdfSignature(X509Certificate2 certificate, byte[] imageData, int pageNumber, PdfOrientedRectangle signatureBounds) : this(certificate)
        {
            ValidateParameters(imageData, pageNumber, signatureBounds);
            this.appearance = PdfSignatureAppearance.Create(imageData, signatureBounds, pageNumber - 1);
        }

        public PdfSignature(X509Certificate2 certificate, Stream imageData, int pageNumber, PdfOrientedRectangle signatureBounds) : this(certificate)
        {
            ValidateParameters(imageData, pageNumber, signatureBounds);
            this.appearance = PdfSignatureAppearance.Create(imageData, signatureBounds, pageNumber - 1);
        }

        public PdfSignature(X509Certificate2 certificate, byte[] imageData, int pageNumber, PdfRectangle signatureBounds) : this(certificate)
        {
            ValidateParameters(imageData, pageNumber, signatureBounds);
            this.appearance = PdfSignatureAppearance.Create(imageData, new PdfOrientedRectangle(signatureBounds.TopLeft, signatureBounds.Width, signatureBounds.Height), pageNumber - 1);
        }

        public PdfSignature(X509Certificate2 certificate, Stream imageData, int pageNumber, PdfRectangle signatureBounds) : this(certificate)
        {
            ValidateParameters(imageData, pageNumber, signatureBounds);
            this.appearance = PdfSignatureAppearance.Create(imageData, new PdfOrientedRectangle(signatureBounds.TopLeft, signatureBounds.Width, signatureBounds.Height), pageNumber - 1);
        }

        internal void PatchStream(PdfDocumentStream stream)
        {
            if ((this.contentsPlaceHolder != null) && (this.byteRangePlaceHolder != null))
            {
                int offset = this.contentsPlaceHolder.Offset;
                int length = this.contentsPlaceHolder.Length;
                int start = offset + length;
                this.byteRange = new PdfSignatureByteRange[] { new PdfSignatureByteRange(0, offset), new PdfSignatureByteRange(start, ((int) stream.Length) - start) };
                stream.Patch((long) this.byteRangePlaceHolder.Offset, new PdfWritableSignatureByteRangeArray(this.byteRange));
                using (PdfUnsignedDocumentStream stream2 = new PdfUnsignedDocumentStream(stream, (long) offset, length))
                {
                    using (SHA1 sha = SHA1.Create())
                    {
                        this.contents = this.SignData(sha.ComputeHash(stream2));
                    }
                }
                stream.Patch((long) offset, this.contents);
            }
        }

        private byte[] SignData(byte[] hash)
        {
            bool silent = !Environment.UserInteractive || (HttpContextAccessor.Current != null);
            SignedCms cms = new SignedCms(new ContentInfo(hash));
            cms.ComputeSignature(new CmsSigner(this.certificate), silent);
            return cms.Encode();
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Filter", this.filter);
            dictionary.AddName("SubFilter", this.subFilter);
            this.contentsPlaceHolder = new PdfPlaceholder((this.SignData(new byte[20]).Length * 2) + 2);
            dictionary.Add("Contents", this.contentsPlaceHolder);
            this.byteRangePlaceHolder = new PdfPlaceholder(0x24);
            dictionary.Add("ByteRange", this.byteRangePlaceHolder);
            if (!string.IsNullOrEmpty(this.name))
            {
                dictionary.Add("Name", this.name);
            }
            this.signingTime = new DateTimeOffset?(DateTimeOffset.Now);
            dictionary.Add("M", this.signingTime);
            if (!string.IsNullOrEmpty(this.location))
            {
                dictionary.Add("Location", this.location);
            }
            if (!string.IsNullOrEmpty(this.reason))
            {
                dictionary.Add("Reason", this.reason);
            }
            if (!string.IsNullOrEmpty(this.contactInfo))
            {
                dictionary.Add("ContactInfo", this.contactInfo);
            }
            return dictionary;
        }

        private static void ValidateParameters(object imageData, int pageNumber, object signatureBounds)
        {
            if (imageData == null)
            {
                throw new ArgumentNullException("imageData");
            }
            if (signatureBounds == null)
            {
                throw new ArgumentNullException("signatureBounds");
            }
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgPageNumberShouldBePositive), "signatureBounds");
            }
        }

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public DateTimeOffset? SigningTime =>
            this.signingTime;

        public string Location
        {
            get => 
                this.location;
            set => 
                this.location = value;
        }

        public string Reason
        {
            get => 
                this.reason;
            set => 
                this.reason = value;
        }

        public string ContactInfo
        {
            get => 
                this.contactInfo;
            set => 
                this.contactInfo = value;
        }

        public PdfAnnotationFlags AnnotationFlags
        {
            get => 
                this.annotationFlags;
            set => 
                this.annotationFlags = value;
        }

        internal string Filter =>
            this.filter;

        internal string SubFilter =>
            this.subFilter;

        internal byte[] Contents =>
            this.contents;

        internal PdfSignatureByteRange[] ByteRange =>
            this.byteRange;

        internal PdfSignatureReference[] Reference =>
            this.reference;

        internal int? AlteredPagesCount =>
            this.alteredPagesCount;

        internal int? AlteredInteractiveFormFieldsCount =>
            this.alteredInteractiveFormFieldsCount;

        internal int? FilledInInteractiveFormFieldsCount =>
            this.filledInInteractiveFormFieldsCount;

        internal int? HandlerVersion =>
            this.handlerVersion;

        internal bool ShouldUseReference =>
            this.shouldUseReference;

        internal PdfSignatureBuildProperties BuildProperties =>
            this.buildProperties;

        internal PdfSignatureAppearance Appearance
        {
            get => 
                this.appearance;
            set => 
                this.appearance = value;
        }
    }
}

