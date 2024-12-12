namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfSignatureReference : PdfObject
    {
        private readonly PdfSignatureTransformMethod transformMethod;
        private readonly string digestMethod;

        private PdfSignatureReference(PdfSignatureTransformMethod transformMethod, string digestMethod, int number) : base(number)
        {
            this.transformMethod = transformMethod;
            this.digestMethod = digestMethod;
        }

        public static PdfSignatureReference Create(PdfReaderDictionary dictionary)
        {
            PdfSignatureTransformMethod method;
            string name = dictionary.GetName("TransformMethod");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Data");
            if (string.IsNullOrEmpty(name))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if ((dictionary2 != null) && (dictionary2.Number != dictionary2.Objects.DocumentCatalog.ObjectNumber))
            {
                return null;
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("TransformParams");
            if (name == "DocMDP")
            {
                method = new PdfDocumentMDPSignatureTransformMethod(dictionary3);
            }
            else if (name != "UR")
            {
                if (name != "FieldMDP")
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                }
                method = new PdfFieldMDPSignatureTransformMethod(dictionary3);
            }
            else
            {
                PdfUsageRightsSignatureTransformMethod method2 = new PdfUsageRightsSignatureTransformMethod(dictionary3);
                IList<PdfAnnotationUsageRight> annotationUsageRights = method2.AnnotationUsageRights;
                if (annotationUsageRights != null)
                {
                    foreach (PdfAnnotationUsageRight right in annotationUsageRights)
                    {
                        if ((right == PdfAnnotationUsageRight.Online) || (right == PdfAnnotationUsageRight.SummaryView))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
                IList<PdfInteractiveFormFieldUsageRight> interactiveFormFieldUsageRights = method2.InteractiveFormFieldUsageRights;
                if (interactiveFormFieldUsageRights != null)
                {
                    foreach (PdfInteractiveFormFieldUsageRight right2 in interactiveFormFieldUsageRights)
                    {
                        if ((right2 == PdfInteractiveFormFieldUsageRight.BarcodePlaintext) || (right2 == PdfInteractiveFormFieldUsageRight.Online))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
                method = method2;
            }
            string text1 = dictionary.GetName("DigestMethod");
            string digestMethod = text1;
            if (text1 == null)
            {
                string local1 = text1;
                digestMethod = "MD5";
            }
            return new PdfSignatureReference(method, digestMethod, dictionary.Number);
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            new PdfWriterDictionary(objects);

        public PdfSignatureTransformMethod TransformMethod =>
            this.transformMethod;

        public string DigestMethod =>
            this.digestMethod;
    }
}

