namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DocumentSerializationCollection : CollectionBase
    {
        public void Add(XtraObjectInfo info);
        public void Add(ContinuousExportInfo info);
        public virtual void Add(DocumentSerializationOptions options);
        public void Add(Page page, int index);
        public void AddRange(IList<Page> source, Predicate<int> predicate);
        protected XtraObjectInfo CreatePageInfo(Page page, int index);
        protected XtraObjectInfo CreatePageInfo(Page page, int index, Predicate<int> predicate);
        private static string GetPageName(int i);
    }
}

