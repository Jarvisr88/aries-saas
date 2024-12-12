namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Core.Native.Serialization;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentStreamingDeserializationCollection : DocumentDeserializationCollection
    {
        private SerializedPageDataList list;

        public DocumentStreamingDeserializationCollection(Document document) : this(document, predicate1)
        {
            Predicate<int> predicate1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Predicate<int> local1 = <>c.<>9__1_0;
                predicate1 = <>c.<>9__1_0 = _ => true;
            }
        }

        public void AddSerializedPageDataList(SerializedPageDataList list)
        {
            this.list = list;
            base.Add(new XtraObjectInfo("SerializedPageDataList", list));
        }

        protected override Page CreatePage(int index, int pageCount) => 
            new PartiallyDeserializedPage(this.list[index].PageData) { ID = this.list[index].PageID };

        protected override void CreatePages(int pageCount)
        {
            PartiallyDeserializedDocument document = base.Document as PartiallyDeserializedDocument;
            if (document != null)
            {
                document.UpdatePages(pageCount);
            }
            else
            {
                base.CreatePages(pageCount);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentStreamingDeserializationCollection.<>c <>9 = new DocumentStreamingDeserializationCollection.<>c();
            public static Predicate<int> <>9__1_0;

            internal bool <.ctor>b__1_0(int _) => 
                true;
        }
    }
}

