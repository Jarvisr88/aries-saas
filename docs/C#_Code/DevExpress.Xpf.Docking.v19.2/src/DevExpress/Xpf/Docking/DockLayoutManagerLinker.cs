namespace DevExpress.Xpf.Docking
{
    using System;

    public static class DockLayoutManagerLinker
    {
        public static void Link(DockLayoutManager first, DockLayoutManager second)
        {
            if ((first != null) && ((second != null) && !ReferenceEquals(first, second)))
            {
                if (!first.Linked.Contains(second))
                {
                    first.Linked.Add(second);
                }
                if (!second.Linked.Contains(first))
                {
                    second.Linked.Add(first);
                }
            }
        }

        public static void Unlink(DockLayoutManager first, DockLayoutManager second)
        {
            if ((first != null) && ((second != null) && !ReferenceEquals(first, second)))
            {
                if (first.Linked.Contains(second))
                {
                    first.Linked.Remove(second);
                }
                if (second.Linked.Contains(first))
                {
                    second.Linked.Remove(first);
                }
            }
        }
    }
}

