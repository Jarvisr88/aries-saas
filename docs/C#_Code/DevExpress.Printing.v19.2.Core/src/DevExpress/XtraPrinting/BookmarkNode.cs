namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Core.Native;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class BookmarkNode : StorableObjectBase, IBookmarkNode, IXtraPartlyDeserializable, IXtraSupportShouldSerializeCollectionItem, IXtraSupportDeserializeCollectionItem
    {
        private string text;
        private BrickPagePair pair;
        private readonly IBookmarkNodeCollection nodes;

        public BookmarkNode()
        {
            this.text = string.Empty;
            this.nodes = this.CreateBookmarkNodeCollection();
        }

        public BookmarkNode(string text) : this(text, BrickPagePair.Empty)
        {
        }

        public BookmarkNode(string text, BrickPagePair bpPair) : this()
        {
            this.Text = text;
            this.Pair = bpPair;
        }

        public BookmarkNode(string text, DevExpress.XtraPrinting.Brick brick, DevExpress.XtraPrinting.Page page) : this(text, BrickPagePair.Create(brick, page))
        {
        }

        protected virtual IBookmarkNodeCollection CreateBookmarkNodeCollection() => 
            new BookmarkNodeCollection();

        [Obsolete("Use CreateBookmarkNodeCollection instead")]
        protected virtual IBookmarkNodeCollection CreateBookmarkodeCollection() => 
            this.CreateBookmarkNodeCollection();

        void IXtraPartlyDeserializable.Deserialize(object rootObject, IXtraPropertyCollection properties)
        {
            this.pair = ((Document) rootObject).CreateBrickPagePair(properties["PageIndex"], properties["Indices"]);
        }

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "Nodes") ? null : new BookmarkNode(string.Empty, null, null);

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "Nodes")
            {
                this.Nodes.Add((BookmarkNode) e.Item.Value);
            }
        }

        bool IXtraSupportShouldSerializeCollectionItem.ShouldSerializeCollectionItem(XtraItemEventArgs e) => 
            ((BookmarkNode) e.Item.Value).IsValid((Document) e.RootObject);

        protected internal virtual int GetPageRangeIndex(int[] indices) => 
            Array.BinarySearch<int>(indices, this.PageIndex);

        internal virtual bool IsValid(Document document) => 
            (this.PageIndex == -1) || (this.Pair.GetPage(document.Pages) != null);

        [Description("Gets the collection of child bookmarks for the current bookmark. This collection is used when creating a hierarchical document map."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.DeserializeCollectionItemBeforeCallSetIndex)]
        public IBookmarkNodeCollection Nodes =>
            this.nodes;

        [Description("Gets the text of a bookmark node."), XtraSerializableProperty]
        public string Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        [Description("Provides access to the brick-page pair, associated with the current bookmark.")]
        public BrickPagePair Pair
        {
            get => 
                this.pair;
            internal set => 
                this.pair = value;
        }

        [Obsolete("Use the Page.GetBrickByIndices method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.XtraPrinting.Brick Brick
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        [DefaultValue(""), XtraSerializableProperty, EditorBrowsable(EditorBrowsableState.Never)]
        public string Indices =>
            this.pair.Indices;

        [DefaultValue(-1), XtraSerializableProperty]
        public int PageIndex =>
            this.pair.PageIndex;

        [Obsolete("Use the PageIndex property instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.XtraPrinting.Page Page
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        int IBookmarkNode.PageIndex =>
            this.PageIndex;

        string IBookmarkNode.Text =>
            this.Text;

        IEnumerable<IBookmarkNode> IBookmarkNode.Nodes =>
            this.Nodes.Cast<IBookmarkNode>();
    }
}

