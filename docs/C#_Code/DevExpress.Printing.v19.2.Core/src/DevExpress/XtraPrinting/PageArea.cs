namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [TypeConverter("DevExpress.XtraPrinting.Design.PageAreaConverter,DevExpress.XtraPrinting.v19.2.Design")]
    public class PageArea
    {
        protected StringCollection fContent;
        protected BrickList bricks;
        protected System.Drawing.Font fFont;
        protected BrickAlignment fLineAlignment;
        protected Image[] images;
        private BrickGraphics graph;

        public PageArea()
        {
            this.fLineAlignment = BrickAlignment.Near;
            this.Initialize();
        }

        public PageArea(string[] content, System.Drawing.Font font, BrickAlignment lineAlignment)
        {
            this.fLineAlignment = BrickAlignment.Near;
            this.Initialize();
            this.fContent.AddRange(content);
            this.fLineAlignment = lineAlignment;
            this.SetFont(font);
        }

        private void AddBricks(BrickGraphics graph, Image[] images)
        {
            this.bricks.AddRange(this.CreateBricks(graph, images));
        }

        public void CreateArea(BrickGraphics graph, Image[] images)
        {
            try
            {
                this.AddBricks(graph, images);
                foreach (Brick brick in this.bricks)
                {
                    graph.DrawBrick(brick);
                }
            }
            finally
            {
                this.bricks.Clear();
            }
        }

        private Brick CreateBrick(string s, BrickAlignment alignment, BrickAlignment lineAligment)
        {
            PageTableBrick brick = this.CreateTable(s);
            if ((brick == null) || (brick.Rows.Count <= 0))
            {
                brick = null;
            }
            else
            {
                brick.Alignment = alignment;
                brick.LineAlignment = lineAligment;
            }
            return brick;
        }

        private IList<Brick> CreateBricks(BrickGraphics graph, Image[] images)
        {
            IList<Brick> list2;
            this.images = images;
            this.graph = graph;
            List<Brick> list = new List<Brick>();
            try
            {
                BrickAlignment[] alignmentArray = new BrickAlignment[] { BrickAlignment.Near, BrickAlignment.Center, BrickAlignment.Far };
                int index = 0;
                while (true)
                {
                    if (index >= Math.Min(3, this.fContent.Count))
                    {
                        list2 = list;
                        break;
                    }
                    Brick item = this.CreateBrick(this.fContent[index], alignmentArray[index], this.fLineAlignment);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                    index++;
                }
            }
            finally
            {
                graph = null;
                images = null;
            }
            return list2;
        }

        private Brick CreateCell(string s) => 
            this.CreateCell(s, false);

        private Brick CreateCell(string s, bool isEmptyStringAllowed)
        {
            if ((s == null) || ((s.Length == 0) && !isEmptyStringAllowed))
            {
                return null;
            }
            Match match = Regex.Match(s, @"\[Image (?<val>\d+)\]");
            if (!match.Success)
            {
                PageInfoBrick brick2 = new PageInfoBrick {
                    Sides = BorderSide.None,
                    Font = this.Font,
                    AutoWidth = true,
                    PageInfo = this.ToPageInfo(s),
                    Rect = new RectangleF(PointF.Empty, this.graph.Measurer.MeasureString("gM", this.Font, this.GetPageUnit()))
                };
                brick2.Format = this.ToStringFormat(brick2.PageInfo, s);
                brick2.Style.BackColor = Color.Transparent;
                return brick2;
            }
            int index = Convert.ToInt32(match.Groups["val"].Value, 10);
            if (index >= this.images.Length)
            {
                return null;
            }
            PageImageBrick brick = new PageImageBrick {
                Image = this.images[index],
                Sides = BorderSide.None
            };
            brick.Rect = new RectangleF(0f, 0f, (float) brick.Image.Width, (float) brick.Image.Height);
            brick.Style.BackColor = Color.Transparent;
            return brick;
        }

        private TableRow CreateRow(string s)
        {
            TableRow row = new TableRow();
            if (string.IsNullOrEmpty(s))
            {
                Brick item = this.CreateCell(s, true);
                if (item != null)
                {
                    row.Bricks.Add(item);
                }
            }
            else
            {
                foreach (string str in new Regex(@"(\[[^\[]*\])").Split(s))
                {
                    Brick item = this.CreateCell(str);
                    if (item != null)
                    {
                        row.Bricks.Add(item);
                    }
                }
            }
            return row;
        }

        private PageTableBrick CreateTable(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            PageTableBrick brick = new PageTableBrick();
            foreach (string str in Regex.Split(s, "\r\n"))
            {
                TableRow row = this.CreateRow(str);
                if ((row != null) && (row.Bricks.Count > 0))
                {
                    brick.Rows.Add(row);
                }
            }
            return brick;
        }

        public IList<Brick> GetBricks(BrickGraphics graph, Image[] images) => 
            this.CreateBricks(graph, images);

        private static PageInfo GetPageInfoUsingLocalizer(string s, XtraLocalizer<PreviewStringId> localizer) => 
            !s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageNumber)) ? (!s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageNumberOfTotal)) ? (!s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageTotal)) ? ((s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageTime)) || s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageDate))) ? PageInfo.DateTime : (!s.Equals(localizer.GetLocalizedString(PreviewStringId.PageInfo_PageUserName)) ? PageInfo.None : PageInfo.UserName)) : PageInfo.Total) : PageInfo.NumberOfTotal) : PageInfo.Number;

        private GraphicsUnit GetPageUnit() => 
            (this.graph != null) ? this.graph.PageUnit : GraphicsUnit.Pixel;

        protected virtual void Initialize()
        {
            this.fContent = new StringCollection();
            this.bricks = new BrickList();
        }

        internal SizeF MeasureArea(BrickGraphics graph, Image[] images)
        {
            SizeF size;
            try
            {
                this.AddBricks(graph, images);
                foreach (Brick brick in this.bricks)
                {
                    brick.PerformLayout(graph.PrintingSystem);
                }
                size = this.bricks.Bounds.Size;
            }
            finally
            {
                this.bricks.Clear();
            }
            return size;
        }

        private void SetFont(System.Drawing.Font value)
        {
            this.fFont = (System.Drawing.Font) value.Clone();
        }

        public bool ShouldSerialize() => 
            (this.fContent.Count > 0) || (!this.Font.Equals(this.DefaultFont) || (this.fLineAlignment != BrickAlignment.Near));

        private PageInfo ToPageInfo(string s)
        {
            PageInfo pageInfoUsingLocalizer = GetPageInfoUsingLocalizer(s, PreviewLocalizer.Active);
            return ((pageInfoUsingLocalizer != PageInfo.None) ? pageInfoUsingLocalizer : GetPageInfoUsingLocalizer(s, PreviewLocalizer.Default));
        }

        private string ToStringFormat(string info) => 
            (info.Equals(PreviewLocalizer.GetString(PreviewStringId.PageInfo_PageDate)) || info.Equals(PreviewLocalizer.Default.GetLocalizedString(PreviewStringId.PageInfo_PageDate))) ? "{0:d}" : "{0:t}";

        private string ToStringFormat(PageInfo pageInfo, string info) => 
            pageInfo.Equals(PageInfo.DateTime) ? this.ToStringFormat(info) : (pageInfo.Equals(PageInfo.NumberOfTotal) ? PreviewLocalizer.GetString(PreviewStringId.SB_PageInfo) : (pageInfo.Equals(PageInfo.None) ? info : ""));

        private object XtraCreateContentItem(XtraItemEventArgs e) => 
            e.Item.Value;

        [Description("Provides access to a collection of strings, representing the content of a page header or page footer."), XtraSerializableProperty(XtraSerializationVisibility.SimpleCollection, true, true, true)]
        public StringCollection Content =>
            this.fContent;

        [Description("Gets or sets a value specifying which edge (top, center or bottom) of the page area its content should be aligned."), XtraSerializableProperty]
        public BrickAlignment LineAlignment
        {
            get => 
                this.fLineAlignment;
            set => 
                this.fLineAlignment = value;
        }

        [Description("Gets or sets the font used to draw the page area's text."), XtraSerializableProperty]
        public System.Drawing.Font Font
        {
            get
            {
                if (this.fFont == null)
                {
                    this.SetFont(this.DefaultFont);
                }
                return this.fFont;
            }
            set => 
                this.SetFont(value);
        }

        private System.Drawing.Font DefaultFont =>
            Control.DefaultFont;
    }
}

