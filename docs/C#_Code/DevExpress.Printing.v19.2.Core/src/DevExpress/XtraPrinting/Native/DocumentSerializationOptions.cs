namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;

    public class DocumentSerializationOptions : IXtraSortableProperties, IXtraSupportShouldSerialize, IXtraSupportDeserializeCollectionItem
    {
        private int pageCount;
        private Document document;
        public EventHandler<ShouldSerializeEventArgs> ShouldSerialize;
        public EventHandler PageCountChanged;

        public DocumentSerializationOptions(Document document);
        public DocumentSerializationOptions(Document document, int pageCount);
        public static void AddImageEntryToCache(XtraItemEventArgs e);
        bool IXtraSortableProperties.ShouldSortProperties();
        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e);
        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e);
        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName);
        public static BookmarkNode GetRootBookmark(Document document);
        private void RaisePageCountChanged();
        private bool? RaiseShouldSerialize(string propertyName);
        private bool ShouldSerializePageBackColor();

        [XtraSerializableProperty(10), DefaultValue("")]
        public string Culture { get; set; }

        [XtraSerializableProperty(11), DefaultValue(false)]
        public bool RightToLeftLayout { get; set; }

        [XtraSerializableProperty(0)]
        public string Name { get; set; }

        [XtraSerializableProperty(1)]
        public int PageCount { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 5, XtraSerializationFlags.Cached)]
        public ICollection SharedStyles { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 6, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex | XtraSerializationFlags.Cached)]
        public ICollection SharedImages { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 7, XtraSerializationFlags.Cached)]
        public ICollection SharedBricks { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 12, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public ICollection EditingFields { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 14, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public ICollection ImageResources { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Content, 8)]
        public DevExpress.XtraPrinting.Drawing.Watermark Watermark { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Content, 9)]
        public virtual BookmarkNode RootBookmark { get; }

        [XtraSerializableProperty(2)]
        public Color PageBackColor { get; set; }

        [XtraSerializableProperty(3), DefaultValue(true)]
        public bool ContinuousPageNumbering { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 4, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public ICollection PageData { get; }

        [XtraSerializableProperty(13), DefaultValue(false)]
        public bool IsPageCollectionModified { get; set; }
    }
}

