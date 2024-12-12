namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    internal class PartiallyDeserializedPageList : PageList
    {
        public PartiallyDeserializedPageList(PartiallyDeserializedDocument document) : base(document, new PartiallyDeserializedInnerPageList(document))
        {
        }

        protected override long[] GetPageIds(int length)
        {
            PartiallyDeserializedDocument document = (PartiallyDeserializedDocument) base.Document;
            long[] numArray = new long[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = document.Storage.PageDataList.PageIds[i];
            }
            return numArray;
        }

        protected override void ValidatePage(Page page)
        {
        }
    }
}

