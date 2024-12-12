namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GroupingManager
    {
        private Dictionary<object, IList<GroupingInfo>> pageGroups;
        private HashSet<object> groupKeys;

        public GroupingManager();
        public void AfterBandPrinted(long pageID, DocumentBand band);
        private static bool ArrayStartsWith(int[] array, int[] subArray);
        private void BuildPageGroups(int pageIndex, IEnumerable<DocumentBandInfo> bands);
        public void Clear();
        private static IList<DocumentBandInfo> FilterBands(IList<DocumentBandInfo> bands, Predicate<DocumentBandInfo> predicate);
        public GroupingInfo GetGroupingInfo(object groupingObject, int[] path, int pageIndex);
        public bool TryBuildPageGroups(long pageID, int pageIndex);
        public void UpdatePageGroups(int pageIndex);

        public IDictionary<long, IList<DocumentBandInfo>> PageBands { get; private set; }

        public ISet<object> GroupKeys { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupingManager.<>c <>9;
            public static Func<DocumentBandInfo, int[]> <>9__10_1;

            static <>c();
            internal int[] <TryBuildPageGroups>b__10_1(DocumentBandInfo item);
        }
    }
}

