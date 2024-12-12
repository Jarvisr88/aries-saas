namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class BookmarkInfo
    {
        public static readonly BookmarkInfo Empty = new BookmarkInfo(NullBrickOwner.Instance, string.Empty, null);
        private string bookmark;
        private BookmarkInfo bookmarkParentInfo;
        private IBrickOwner brickOwner;

        public BookmarkInfo()
        {
            this.bookmark = string.Empty;
        }

        public BookmarkInfo(IBrickOwner brickOwner, string bookmark, BookmarkInfo bookmarkParentInfo)
        {
            this.bookmark = string.Empty;
            this.brickOwner = brickOwner;
            this.bookmark = bookmark;
            this.bookmarkParentInfo = bookmarkParentInfo;
        }

        public string Bookmark =>
            this.bookmark;

        public IBrickOwner BrickOwner =>
            this.brickOwner;

        public BookmarkInfo ParentInfo
        {
            get => 
                this.bookmarkParentInfo;
            set => 
                this.bookmarkParentInfo = value;
        }

        public VisualBrick ParentBrick { get; internal set; }

        public bool HasBookmark =>
            !string.IsNullOrEmpty(this.Bookmark);
    }
}

