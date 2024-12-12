namespace DevExpress.Data
{
    using System;

    public class VisibleIndexHeightInfo
    {
        private static readonly int[] zeroHeight;
        private static readonly int[] selfHeight;
        private int[][] map;
        private bool dirty;
        private bool allowFixedGroups;
        private VisibleIndexCollection source;

        static VisibleIndexHeightInfo();
        public VisibleIndexHeightInfo(VisibleIndexCollection source);
        public void Calculate();
        public bool IsSelfHeight(int[] height);
        public bool IsZeroHeight(int[] height);
        public void Reset();

        public int[][] Map { get; }

        public bool AllowFixedGroups { get; set; }
    }
}

