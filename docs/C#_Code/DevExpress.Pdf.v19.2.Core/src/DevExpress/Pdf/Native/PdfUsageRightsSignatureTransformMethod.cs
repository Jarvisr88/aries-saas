namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfUsageRightsSignatureTransformMethod : PdfSignatureTransformMethod
    {
        private readonly bool allowFullSave;
        private readonly string message;
        private readonly IList<PdfAnnotationUsageRight> annotationUsageRights;
        private readonly IList<PdfInteractiveFormFieldUsageRight> interactiveFormFieldUsageRights;
        private readonly bool allowModifySignature;
        private readonly IList<PdfEmbeddedFileUsageRight> embeddedFileUsageRights;
        private readonly bool restrictOtherPermissions;

        public PdfUsageRightsSignatureTransformMethod(PdfReaderDictionary dictionary) : base(dictionary)
        {
            if (dictionary != null)
            {
                this.allowFullSave = ParseSinglePermission(dictionary, "Document", "FullSave");
                this.message = dictionary.GetString("Msg");
                Func<object, PdfAnnotationUsageRight> create = <>c.<>9__25_0;
                if (<>c.<>9__25_0 == null)
                {
                    Func<object, PdfAnnotationUsageRight> local1 = <>c.<>9__25_0;
                    create = <>c.<>9__25_0 = value => ConvertToEnum<PdfAnnotationUsageRight>(value);
                }
                this.annotationUsageRights = dictionary.GetArray<PdfAnnotationUsageRight>("Annots", create);
                Func<object, PdfInteractiveFormFieldUsageRight> func2 = <>c.<>9__25_1;
                if (<>c.<>9__25_1 == null)
                {
                    Func<object, PdfInteractiveFormFieldUsageRight> local2 = <>c.<>9__25_1;
                    func2 = <>c.<>9__25_1 = value => ConvertToEnum<PdfInteractiveFormFieldUsageRight>(value);
                }
                this.interactiveFormFieldUsageRights = dictionary.GetArray<PdfInteractiveFormFieldUsageRight>("Form", func2);
                this.allowModifySignature = ParseSinglePermission(dictionary, "Signature", "Modify");
                Func<object, PdfEmbeddedFileUsageRight> func3 = <>c.<>9__25_2;
                if (<>c.<>9__25_2 == null)
                {
                    Func<object, PdfEmbeddedFileUsageRight> local3 = <>c.<>9__25_2;
                    func3 = <>c.<>9__25_2 = value => ConvertToEnum<PdfEmbeddedFileUsageRight>(value);
                }
                this.embeddedFileUsageRights = dictionary.GetArray<PdfEmbeddedFileUsageRight>("EF", func3);
                bool? boolean = dictionary.GetBoolean("P");
                this.restrictOtherPermissions = (boolean != null) ? boolean.GetValueOrDefault() : false;
            }
        }

        private static T ConvertToEnum<T>(object value) where T: struct
        {
            PdfName name = value as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfEnumToStringConverter.Parse<T>(name.Name, true);
        }

        private static bool ParseSinglePermission(PdfReaderDictionary dictionary, string key, string expectedValue)
        {
            IList<object> array = dictionary.GetArray(key);
            if (array == null)
            {
                return false;
            }
            if (array.Count != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfName name = array[0] as PdfName;
            if ((name == null) || (name.Name != expectedValue))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return true;
        }

        public bool AllowFullSave =>
            this.allowFullSave;

        public string Message =>
            this.message;

        public IList<PdfAnnotationUsageRight> AnnotationUsageRights =>
            this.annotationUsageRights;

        public IList<PdfInteractiveFormFieldUsageRight> InteractiveFormFieldUsageRights =>
            this.interactiveFormFieldUsageRights;

        public bool AllowModifySignature =>
            this.allowModifySignature;

        public IList<PdfEmbeddedFileUsageRight> EmbeddedFileUsageRights =>
            this.embeddedFileUsageRights;

        public bool RestrictOtherPermissions =>
            this.restrictOtherPermissions;

        protected override string ValidVersion =>
            "2.2";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfUsageRightsSignatureTransformMethod.<>c <>9 = new PdfUsageRightsSignatureTransformMethod.<>c();
            public static Func<object, PdfAnnotationUsageRight> <>9__25_0;
            public static Func<object, PdfInteractiveFormFieldUsageRight> <>9__25_1;
            public static Func<object, PdfEmbeddedFileUsageRight> <>9__25_2;

            internal PdfAnnotationUsageRight <.ctor>b__25_0(object value) => 
                PdfUsageRightsSignatureTransformMethod.ConvertToEnum<PdfAnnotationUsageRight>(value);

            internal PdfInteractiveFormFieldUsageRight <.ctor>b__25_1(object value) => 
                PdfUsageRightsSignatureTransformMethod.ConvertToEnum<PdfInteractiveFormFieldUsageRight>(value);

            internal PdfEmbeddedFileUsageRight <.ctor>b__25_2(object value) => 
                PdfUsageRightsSignatureTransformMethod.ConvertToEnum<PdfEmbeddedFileUsageRight>(value);
        }
    }
}

