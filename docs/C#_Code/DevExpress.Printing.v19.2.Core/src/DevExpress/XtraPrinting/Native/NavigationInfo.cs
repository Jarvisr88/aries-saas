namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class NavigationInfo
    {
        private BrickPagePairCollection bookmarkBricks;
        private BrickPagePairCollection navigationBricks;
        private Dictionary<string, BrickPagePair> targets;
        private Dictionary<string, IList<Brick>> links;

        public NavigationInfo();
        public void Clear();
        public void UpdateTargets(Page page, int[] indices, VisualBrick brick, RectangleF brickBounds);

        public BrickPagePairCollection NavigationBricks { get; }

        public BrickPagePairCollection BookmarkBricks { get; }

        public IDictionary<string, BrickPagePair> Targets { get; }

        public IDictionary<string, IList<Brick>> Links { get; }
    }
}

