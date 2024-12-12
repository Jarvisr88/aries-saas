namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class PdfNumberTreeNode<T> where T: class
    {
        private const string numsKey = "Nums";

        private static int ConvertToInt(object key)
        {
            if (!(key is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (int) key;
        }

        internal static PdfDeferredSortedDictionary<int, T> Parse(PdfReaderDictionary dictionary, PdfCreateTreeElementAction<T> createElement, bool checkElementCount)
        {
            try
            {
                PdfCreateTreeKeyAction<int> createKey = <>c<T>.<>9__2_0;
                if (<>c<T>.<>9__2_0 == null)
                {
                    PdfCreateTreeKeyAction<int> local1 = <>c<T>.<>9__2_0;
                    createKey = <>c<T>.<>9__2_0 = k => PdfNumberTreeNode<T>.ConvertToInt(k);
                }
                return PdfElementTreeNode<int, T>.Parse(dictionary, createKey, createElement, "Nums", checkElementCount);
            }
            catch
            {
                return null;
            }
        }

        internal static PdfWriterDictionary Write(PdfObjectCollection objects, PdfDeferredSortedDictionary<int, T> dictionary, Func<PdfObjectCollection, T, object> writeAction)
        {
            PdfConvertToKeyTreeElementAction<int> convertToKeyAction = <>c<T>.<>9__3_0;
            if (<>c<T>.<>9__3_0 == null)
            {
                PdfConvertToKeyTreeElementAction<int> local1 = <>c<T>.<>9__3_0;
                convertToKeyAction = <>c<T>.<>9__3_0 = (PdfConvertToKeyTreeElementAction<int>) (k => k);
            }
            return PdfElementTreeNode<int, T>.Write(objects, "Nums", dictionary, convertToKeyAction, writeAction);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfNumberTreeNode<T>.<>c <>9;
            public static PdfCreateTreeKeyAction<int> <>9__2_0;
            public static PdfConvertToKeyTreeElementAction<int> <>9__3_0;

            static <>c()
            {
                PdfNumberTreeNode<T>.<>c.<>9 = new PdfNumberTreeNode<T>.<>c();
            }

            internal int <Parse>b__2_0(object k) => 
                PdfNumberTreeNode<T>.ConvertToInt(k);

            internal object <Write>b__3_0(int k) => 
                k;
        }
    }
}

