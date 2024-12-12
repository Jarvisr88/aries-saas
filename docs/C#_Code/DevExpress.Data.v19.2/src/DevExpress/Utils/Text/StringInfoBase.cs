namespace DevExpress.Utils.Text
{
    using DevExpress.Utils;
    using DevExpress.Utils.Text.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class StringInfoBase
    {
        private List<StringBlock> blocks;
        private List<Rectangle> blockBounds;
        private Rectangle bounds;
        private object context;
        internal bool simpleString;
        private string sourceString;
        private Rectangle originalBounds;

        public StringInfoBase() : this(null, null, string.Empty)
        {
        }

        public StringInfoBase(List<StringBlock> blocks, List<Rectangle> blockBounds, string sourceString)
        {
            this.simpleString = false;
            this.originalBounds = Rectangle.Empty;
            this.sourceString = sourceString;
            this.blocks = blocks;
            this.blockBounds = blockBounds;
            this.bounds = Rectangle.Empty;
            this.AllowBaselineAlignment = true;
            this.HyperlinkSettings = new DevExpress.Utils.Text.HyperlinkSettings();
            this.AllowPartiallyVisibleRows = true;
        }

        public virtual void Assign(StringInfoBase info)
        {
            if (this.Blocks == null)
            {
                this.blocks = new List<StringBlock>();
                this.blockBounds = new List<Rectangle>();
            }
            this.Blocks.Clear();
            this.BlocksBounds.Clear();
            this.sourceString = info.SourceString;
            this.bounds = info.Bounds;
            this.simpleString = info.simpleString;
            this.OriginalBounds = info.OriginalBounds;
            this.AllowBaselineAlignment = info.AllowBaselineAlignment;
            for (int i = 0; i < ((info.Blocks == null) ? 0 : info.Blocks.Count); i++)
            {
                this.Blocks.Add(info.Blocks[i]);
                this.BlocksBounds.Add(info.BlocksBounds[i]);
            }
            this.LinesBounds = null;
            if (info.LinesBounds != null)
            {
                this.LinesBounds = new List<Rectangle>();
                for (int j = 0; j < info.LinesBounds.Count; j++)
                {
                    this.LinesBounds.Add(info.LinesBounds[j]);
                }
            }
            this.AllowPartiallyVisibleRows = info.AllowPartiallyVisibleRows;
        }

        public void Assign(List<StringBlock> blocks, List<Rectangle> blockBounds, string sourceString)
        {
            this.sourceString = sourceString;
            this.blocks = blocks;
            this.blockBounds = blockBounds;
        }

        internal int GetBlocksWidth(int startIndex, int count) => 
            (count >= 2) ? (this.BlocksBounds[(startIndex + count) - 1].Right - this.BlocksBounds[startIndex].Left) : this.BlocksBounds[startIndex].Width;

        public abstract bool GetIsEllipsisTrimming();
        internal int GetLineBlockCount(int startIndex)
        {
            int num = 0;
            int lineNumber = 0;
            int num3 = startIndex;
            while (num3 < this.Blocks.Count)
            {
                if (num3 == startIndex)
                {
                    lineNumber = this.Blocks[num3].LineNumber;
                }
                else if (this.Blocks[num3].LineNumber != lineNumber)
                {
                    break;
                }
                num3++;
                num++;
            }
            return num;
        }

        public StringBlock GetLinkByPoint(Point pt)
        {
            if (this.Blocks != null)
            {
                for (int i = 0; i < this.Blocks.Count; i++)
                {
                    if ((this.Blocks[i].Type == StringBlockType.Link) && this.BlocksBounds[i].Contains(pt))
                    {
                        return this.Blocks[i];
                    }
                }
            }
            return null;
        }

        public abstract StringFormat GetStringFormat();
        public void Offset(int x, int y)
        {
            if (!this.originalBounds.IsEmpty)
            {
                this.originalBounds.Offset(x, y);
                this.SetBounds(new Rectangle(this.Bounds.X + x, this.Bounds.Y + y, this.Bounds.Width, this.Bounds.Height));
                if (this.BlocksBounds != null)
                {
                    for (int i = 0; i < this.BlocksBounds.Count; i++)
                    {
                        Rectangle rectangle2 = this.BlocksBounds[i];
                        rectangle2.Offset(x, y);
                        this.BlocksBounds[i] = rectangle2;
                    }
                    if (this.LinesBounds != null)
                    {
                        for (int j = 0; j < this.LinesBounds.Count; j++)
                        {
                            Rectangle rectangle3 = this.LinesBounds[j];
                            rectangle3.Offset(x, y);
                            this.LinesBounds[j] = rectangle3;
                        }
                    }
                }
            }
        }

        public void SetBounds(Rectangle bounds)
        {
            this.bounds = bounds;
        }

        protected void SetBoundsLocation(Point location)
        {
            this.bounds.Location = location;
        }

        public void SetLocation(Point pt)
        {
            this.SetLocation(pt.X, pt.Y);
        }

        public void SetLocation(int x, int y)
        {
            int num = x - this.Bounds.X;
            this.Offset(num, y - this.Bounds.Y);
            this.SetBounds(new Rectangle(x, y, this.Bounds.Width, this.Bounds.Height));
        }

        protected void SetSimpleStringInfo(string text, Rectangle bounds)
        {
            this.simpleString = true;
            this.sourceString = text;
            this.bounds = bounds;
            this.OriginalBounds = bounds;
        }

        protected internal void UpdateLocation(Point pt)
        {
            if (this.BlocksBounds != null)
            {
                Rectangle rectangle;
                Point point = new Point(pt.X - this.Bounds.X, pt.Y - this.Bounds.Y);
                for (int i = 0; i < this.BlocksBounds.Count; i++)
                {
                    rectangle = this.BlocksBounds[i];
                    this.BlocksBounds[i] = new Rectangle(new Point(this.BlocksBounds[i].X + point.X, this.BlocksBounds[i].Y + point.Y), rectangle.Size);
                }
                if (this.LinesBounds != null)
                {
                    for (int j = 0; j < this.LinesBounds.Count; j++)
                    {
                        rectangle = this.LinesBounds[j];
                        this.LinesBounds[j] = new Rectangle(new Point(this.LinesBounds[j].X + point.X, this.LinesBounds[j].Y + point.Y), rectangle.Size);
                    }
                }
            }
        }

        internal void UpdateXLocation(HorzAlignment align, Rectangle bounds)
        {
            if (StringPainterBase.IsValidSize(bounds) && ((align != HorzAlignment.Near) && ((align != HorzAlignment.Default) && ((this.Blocks != null) && (this.BlocksBounds != null)))))
            {
                int startIndex = 0;
                while (startIndex < this.Blocks.Count)
                {
                    int lineBlockCount = this.GetLineBlockCount(startIndex);
                    int blocksWidth = this.GetBlocksWidth(startIndex, lineBlockCount);
                    int num4 = StringPainterBase.CalcXByAlignment(bounds, blocksWidth, align);
                    int num5 = startIndex;
                    while (true)
                    {
                        if (num5 >= (startIndex + lineBlockCount))
                        {
                            startIndex += lineBlockCount;
                            break;
                        }
                        Rectangle rectangle = this.BlocksBounds[num5];
                        rectangle.X = num4;
                        this.BlocksBounds[num5] = rectangle;
                        num4 += rectangle.Width;
                        num5++;
                    }
                }
            }
        }

        public DevExpress.Utils.Text.HyperlinkSettings HyperlinkSettings { get; set; }

        public bool AllowBaselineAlignment { get; set; }

        public bool AllowPartiallyVisibleRows { get; set; }

        public bool DisableWrapNonFitText { get; set; }

        public Rectangle OriginalBounds
        {
            get => 
                this.originalBounds;
            set => 
                this.originalBounds = value;
        }

        public bool SimpleString =>
            this.simpleString;

        public object Context
        {
            get => 
                this.context;
            set => 
                this.context = value;
        }

        public bool RoundTextHeight { get; set; }

        public abstract bool RightToLeft { get; }

        public abstract DevExpress.Utils.WordWrap WordWrap { get; }

        public abstract VertAlignment VAlignment { get; }

        public abstract HorzAlignment HAlignment { get; }

        public abstract System.Drawing.Font Font { get; }

        public bool IsEmpty =>
            (this.Blocks == null) || (this.Blocks.Count == 0);

        public Rectangle Bounds =>
            this.bounds;

        public string SourceString =>
            this.sourceString;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<StringBlock> Blocks =>
            this.blocks;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<Rectangle> BlocksBounds =>
            this.blockBounds;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<Rectangle> LinesBounds { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HasScripts { get; internal set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HasImages { get; internal set; }
    }
}

