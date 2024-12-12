namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.Reflection;

    public interface IBookmarkNodeCollection : IList, ICollection, IEnumerable
    {
        void Add(BookmarkNode item);

        BookmarkNode this[int index] { get; }
    }
}

