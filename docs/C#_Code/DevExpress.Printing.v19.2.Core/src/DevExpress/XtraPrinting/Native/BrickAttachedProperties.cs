namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public static class BrickAttachedProperties
    {
        public static readonly AttachedProperty<string> SortData = AttachedPropertyBase.Register<string>("Brick.SortData");
        public static readonly AttachedProperty<object> Value = AttachedPropertyBase.Register<object>("Brick.Value");
        public static readonly AttachedProperty<object> DrillDownKey = AttachedPropertyBase.Register<object>("Brick.DrillDownKey");
        public static readonly AttachedProperty<string> Url = AttachedPropertyBase.Register<string>("Brick.Url");
        public static readonly AttachedProperty<string> Hint = AttachedPropertyBase.Register<string>("Brick.Hint");
        public static readonly AttachedProperty<string> Id = AttachedPropertyBase.Register<string>("Brick.Id");
        public static readonly AttachedProperty<string> NavigationRef = AttachedPropertyBase.Register<string>("Brick.NavigationRef");
        public static readonly AttachedProperty<string> NavigationId = AttachedPropertyBase.Register<string>("Brick.NavigationId");
        public static readonly AttachedProperty<long> NavigationPageId = AttachedPropertyBase.Register<long>("Brick.NavigationPageId");
        public static readonly AttachedProperty<int> RowIndex = AttachedPropertyBase.Register<int>("RowIndex");
        public static readonly AttachedProperty<int> ParentID = AttachedPropertyBase.Register<int>("ParentID");
        public static readonly AttachedProperty<string> Target = AttachedPropertyBase.Register<string>("Brick.Target");
        public static readonly AttachedProperty<string> AnchorName = AttachedPropertyBase.Register<string>("Brick.AnchorName");
        public static readonly AttachedProperty<BrickPagePair> NavigationPair = AttachedPropertyBase.Register<BrickPagePair>("Brick.NavigationPair");
        public static readonly AttachedProperty<DevExpress.XtraPrinting.BookmarkInfo> BookmarkInfo = AttachedPropertyBase.Register<DevExpress.XtraPrinting.BookmarkInfo>("Brick.BookmarkInfo");
        public static readonly AttachedProperty<object> MergeValue = AttachedPropertyBase.Register<object>("Brick.MergeValue");
        public static readonly AttachedProperty<bool> SummaryInProgress = AttachedPropertyBase.Register<bool>("Brick.SummaryInProgress");
        public static readonly AttachedProperty<float> Angle = AttachedPropertyBase.Register<float>("Brick.Angle");
        public static readonly AttachedProperty<string> XlsxFormatString = AttachedPropertyBase.Register<string>("Brick.XlsxFormatString");
        public static readonly AttachedProperty<DevExpress.Emf.EmfMetafile> EmfMetafile = AttachedPropertyBase.Register<DevExpress.Emf.EmfMetafile>("Brick.EmfMetafile");
        public static readonly AttachedProperty<DevExpress.XtraPrinting.Native.MergeDirection> MergeDirection = AttachedPropertyBase.Register<DevExpress.XtraPrinting.Native.MergeDirection>("Brick.MergeDirection");
        public static readonly AttachedProperty<int> ContainerId = AttachedPropertyBase.Register<int>("Brick.ContainerId");
        public static readonly AttachedProperty<Color> TransparentColor = AttachedPropertyBase.Register<Color>("TransparentColor");
        public static readonly AttachedProperty<int> ColumnIndex = AttachedPropertyBase.Register<int>("ColumnIndex");
    }
}

