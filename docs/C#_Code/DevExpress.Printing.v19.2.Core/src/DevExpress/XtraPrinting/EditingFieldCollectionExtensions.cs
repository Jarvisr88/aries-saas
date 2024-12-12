namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class EditingFieldCollectionExtensions
    {
        private static int GetFieldIndex(EditingFieldCollection collection, long[] prevPages)
        {
            long pageID = -1L;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].PageID != pageID)
                {
                    pageID = collection[i].PageID;
                    if (Array.IndexOf<long>(prevPages, pageID) < 0)
                    {
                        return i;
                    }
                }
            }
            return collection.Count;
        }

        public static IList<EditingField> GetPageEditingFields(this EditingFieldCollection collection, Page page)
        {
            List<EditingField> list = new List<EditingField>();
            foreach (EditingField field in collection)
            {
                if (field.PageID == page.ID)
                {
                    list.Add(field);
                }
            }
            return list;
        }

        public static void InsertFields(this EditingFieldCollection collection, long[] prevPages, IList<EditingField> fields)
        {
            int num = (prevPages.Length != 0) ? GetFieldIndex(collection, prevPages) : 0;
            for (int i = 0; i < fields.Count; i++)
            {
                collection.Insert(num + i, fields[i]);
            }
        }
    }
}

