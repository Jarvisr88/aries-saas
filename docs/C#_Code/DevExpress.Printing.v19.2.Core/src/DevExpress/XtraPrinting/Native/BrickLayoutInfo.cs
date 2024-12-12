namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using System;

    public class BrickLayoutInfo : IXtraSupportCreateContentPropertyValue
    {
        private DevExpress.XtraPrinting.Brick brick;
        private RectangleDF rect;

        public BrickLayoutInfo();
        public BrickLayoutInfo(DevExpress.XtraPrinting.Brick brick, RectangleDF rect);
        object IXtraSupportCreateContentPropertyValue.Create(XtraItemEventArgs e);

        [XtraSerializableProperty(XtraSerializationVisibility.Content, true, false, false, 3, XtraSerializationFlags.Cached)]
        public DevExpress.XtraPrinting.Brick Brick { get; set; }

        [XtraSerializableProperty]
        public RectangleDF Rect { get; set; }
    }
}

