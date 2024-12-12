namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Collections;

    public static class ClosedListIterator
    {
        public static void Iterate(IList list, IClosedListVisitor visitor)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                visitor.VisitElement(list[((i - 1) + count) % count], list[i], i);
            }
        }
    }
}

