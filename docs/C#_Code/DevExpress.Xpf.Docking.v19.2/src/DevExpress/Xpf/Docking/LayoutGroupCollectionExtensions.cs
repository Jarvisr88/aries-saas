namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Internal;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class LayoutGroupCollectionExtensions
    {
        public static void Purge(this LayoutGroupCollection collection)
        {
            foreach (LayoutGroup group in collection.ToArray<LayoutGroup>())
            {
                if (!group.ShouldStayInTree())
                {
                    PlaceHolderHelper.ClearPlaceHolder(group);
                    collection.Remove(group);
                }
            }
        }
    }
}

