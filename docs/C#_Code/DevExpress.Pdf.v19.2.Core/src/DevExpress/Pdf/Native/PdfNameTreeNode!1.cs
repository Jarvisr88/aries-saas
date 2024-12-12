namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public static class PdfNameTreeNode<T> where T: class
    {
        private const string namesKey = "Names";
        private static readonly PdfNameTreeEncoding encoding;

        static PdfNameTreeNode()
        {
            PdfNameTreeNode<T>.encoding = new PdfNameTreeEncoding();
        }

        private static object ConvertFromName(string value) => 
            PdfNameTreeNode<T>.encoding.GetBytes(value);

        private static string ConvertToName(object key)
        {
            byte[] bytes = key as byte[];
            if (bytes != null)
            {
                return PdfNameTreeNode<T>.encoding.GetString(bytes, 0, bytes.Length);
            }
            PdfName name = key as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return name.Name;
        }

        internal static PdfDeferredSortedDictionary<string, T> Parse(PdfReaderDictionary dictionary, PdfCreateTreeElementAction<T> createElement)
        {
            PdfCreateTreeKeyAction<string> createKey = <>c<T>.<>9__4_0;
            if (<>c<T>.<>9__4_0 == null)
            {
                PdfCreateTreeKeyAction<string> local1 = <>c<T>.<>9__4_0;
                createKey = <>c<T>.<>9__4_0 = k => PdfNameTreeNode<T>.ConvertToName(k);
            }
            return PdfElementTreeNode<string, T>.Parse(dictionary, createKey, createElement, "Names", true);
        }

        internal static PdfWriterDictionary Write(PdfObjectCollection objects, PdfDeferredSortedDictionary<string, T> dictionary)
        {
            PdfConvertToKeyTreeElementAction<string> convertToKeyAction = <>c<T>.<>9__5_0;
            if (<>c<T>.<>9__5_0 == null)
            {
                PdfConvertToKeyTreeElementAction<string> local1 = <>c<T>.<>9__5_0;
                convertToKeyAction = <>c<T>.<>9__5_0 = k => PdfNameTreeNode<T>.ConvertFromName(k);
            }
            return PdfElementTreeNode<string, T>.Write(objects, "Names", dictionary, convertToKeyAction, null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfNameTreeNode<T>.<>c <>9;
            public static PdfCreateTreeKeyAction<string> <>9__4_0;
            public static PdfConvertToKeyTreeElementAction<string> <>9__5_0;

            static <>c()
            {
                PdfNameTreeNode<T>.<>c.<>9 = new PdfNameTreeNode<T>.<>c();
            }

            internal string <Parse>b__4_0(object k) => 
                PdfNameTreeNode<T>.ConvertToName(k);

            internal object <Write>b__5_0(string k) => 
                PdfNameTreeNode<T>.ConvertFromName(k);
        }
    }
}

