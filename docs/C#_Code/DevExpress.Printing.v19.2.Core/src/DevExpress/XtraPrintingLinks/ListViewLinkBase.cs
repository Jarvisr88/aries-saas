namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultProperty("ListView")]
    public class ListViewLinkBase : LinkBase
    {
        private System.Windows.Forms.ListView listView;
        private ImageList imageList;
        private int offsetx;
        private BrickGraphics graph;

        public override void AddSubreport(PointF offset)
        {
            if (this.listView != null)
            {
                base.AddSubreport(offset);
            }
        }

        protected override void BeforeCreate()
        {
            if (this.ListView == null)
            {
                throw new NullReferenceException("The ListView property value must not be null");
            }
            base.BeforeCreate();
            base.ps.Graph.PageUnit = GraphicsUnit.Pixel;
            View[] views = new View[] { View.SmallIcon, View.List, View.Details };
            this.imageList = this.ViewEquals(views) ? this.listView.SmallImageList : ((this.listView.View == View.LargeIcon) ? this.listView.LargeImageList : null);
            this.offsetx = (this.imageList == null) ? 0 : this.imageList.ImageSize.Height;
            this.graph = base.ps.Graph;
        }

        protected override void CreateDetail(BrickGraphics gr)
        {
            if (this.listView.View == View.Details)
            {
                this.CreateDetails();
            }
            else
            {
                View[] views = new View[3];
                views[1] = View.SmallIcon;
                views[2] = View.List;
                if (this.ViewEquals(views))
                {
                    this.CreateIcons();
                }
            }
        }

        protected override void CreateDetailHeader(BrickGraphics gr)
        {
            if (this.listView.View == View.Details)
            {
                StringFormat source = new StringFormat(StringFormatFlags.NoWrap) {
                    LineAlignment = StringAlignment.Near
                };
                gr.DefaultBrickStyle = new BrickStyle(BorderSide.All, 1f, Color.Black, SystemColors.Control, SystemColors.ControlText, this.listView.Font, new BrickStringFormat(source));
                Rectangle empty = Rectangle.Empty;
                empty.Y = 1;
                for (int i = 0; i < this.listView.Columns.Count; i++)
                {
                    empty.Width = this.listView.Columns[i].Width;
                    empty.Height = this.listView.Font.Height + 4;
                    this.graph.DrawString(this.listView.Columns[i].Text, empty);
                    empty.Offset(this.listView.Columns[i].Width, 0);
                }
            }
        }

        private void CreateDetails()
        {
            this.graph.DefaultBrickStyle.BackColor = SystemColors.Window;
            this.graph.DefaultBrickStyle.BorderColor = SystemColors.Control;
            this.graph.DefaultBrickStyle.Sides = this.listView.GridLines ? BorderSide.All : BorderSide.None;
            for (int i = 0; i < this.listView.Items.Count; i++)
            {
                ListViewItem item = this.listView.Items[i];
                this.graph.DefaultBrickStyle.Font = item.Font;
                this.graph.DefaultBrickStyle.BackColor = item.BackColor;
                this.graph.DefaultBrickStyle.ForeColor = item.ForeColor;
                this.DrawDetailRow(item);
            }
        }

        private void CreateIcons()
        {
            this.graph.DefaultBrickStyle.BackColor = Color.Transparent;
            this.graph.DefaultBrickStyle.BorderColor = Color.Black;
            this.graph.DefaultBrickStyle.Sides = BorderSide.None;
            for (int i = 0; i < this.listView.Items.Count; i++)
            {
                ListViewItem item = this.listView.Items[i];
                this.graph.DefaultBrickStyle.Font = item.Font;
                this.graph.DefaultBrickStyle.BackColor = item.BackColor;
                this.graph.DefaultBrickStyle.ForeColor = item.ForeColor;
                PanelBrick brick = BrickFactory.CreateBrick(item);
                RectangleF rect = brick.Rect;
                this.graph.DrawBrick(brick, rect);
            }
        }

        private void DrawDetailRow(ListViewItem item)
        {
            PanelBrick brick = BrickFactory.CreateBrick(item);
            RectangleF rect = brick.Rect;
            this.graph.DrawBrick(brick, rect);
            int count = this.ListView.Columns.Count;
            for (int i = 1; i < count; i++)
            {
                rect.Offset(rect.Width, 0f);
                rect.Width = this.ListView.Columns[i].Width;
                bool flag = i < item.SubItems.Count;
                if (flag && !item.UseItemStyleForSubItems)
                {
                    ListViewItem.ListViewSubItem item2 = item.SubItems[i];
                    this.graph.DefaultBrickStyle.Font = item2.Font;
                    this.graph.DefaultBrickStyle.BackColor = item2.BackColor;
                    this.graph.DefaultBrickStyle.ForeColor = item2.ForeColor;
                }
                string text = flag ? item.SubItems[i].Text : string.Empty;
                this.graph.DrawString(text, rect);
            }
        }

        public override void SetDataObject(object data)
        {
            if (data is System.Windows.Forms.ListView)
            {
                this.listView = data as System.Windows.Forms.ListView;
            }
        }

        private bool ViewEquals(params View[] views)
        {
            foreach (View view in views)
            {
                if (this.listView.View == view)
                {
                    return true;
                }
            }
            return false;
        }

        public override System.Type PrintableObjectType =>
            typeof(System.Windows.Forms.ListView);

        [Description("Gets or sets a ListView object to be printed via the current link."), Category("Printing"), DefaultValue((string) null)]
        public System.Windows.Forms.ListView ListView
        {
            get => 
                this.listView;
            set => 
                this.listView = value;
        }

        private class BrickFactory
        {
            private const int space = 2;
            private ListViewItem item;
            private TextBrick textBrick;
            private CheckBoxBrick checkBrick;
            private ImageBrick imageBrick;
            private RectangleF textRect = RectangleF.Empty;
            private RectangleF checkRect = RectangleF.Empty;
            private RectangleF imageRect = RectangleF.Empty;
            private ImageList currentImageList;
            private PointF? offset;

            private void CalcLargeIconsLayout()
            {
                RectangleF itemRect = this.GetItemRect();
                if (!this.CanHaveImage)
                {
                    SizeF ef3 = this.CanHaveCheckBox ? this.GetCheckSize() : SizeF.Empty;
                    this.checkRect = new RectangleF(2f, (itemRect.Height - ef3.Height) / 2f, ef3.Width, ef3.Height);
                    int num = this.CanHaveCheckBox ? 4 : 0;
                    this.textRect = new RectangleF(ef3.Width + num, 0f, (itemRect.Width - (ef3.Width + (num / 2))) + 2f, itemRect.Height + 2f);
                }
                else
                {
                    Size imageSize = this.ImageSize;
                    this.imageRect = new RectangleF((itemRect.Width - imageSize.Width) / 2f, 0f, (float) imageSize.Width, (float) imageSize.Height);
                    SizeF ef2 = this.CanHaveCheckBox ? this.GetCheckSize() : SizeF.Empty;
                    this.checkRect = new RectangleF((this.imageRect.Left - ef2.Width) - 2f, this.imageRect.Bottom - ef2.Height, ef2.Width, ef2.Height);
                    this.textRect = new RectangleF(2f, this.imageRect.Bottom, itemRect.Width - 2f, itemRect.Height - this.imageRect.Height);
                }
            }

            private void CalcLayout()
            {
                if (this.IsLargeImages)
                {
                    this.CalcLargeIconsLayout();
                }
                else
                {
                    this.CalcSmallIconsLayout();
                }
            }

            private unsafe void CalcSmallIconsLayout()
            {
                int height = this.item.Bounds.Height;
                this.textRect = this.GetItemRect();
                this.textRect.Offset(-this.textRect.X, 0f);
                float x = 2f;
                if (this.CanHaveCheckBox)
                {
                    this.checkRect.Size = this.GetCheckSize();
                    this.checkRect.X = x;
                    RectangleF* efPtr1 = &this.checkRect;
                    efPtr1.Y += ((height - this.checkRect.Height) / 2f) - 1f;
                    float num3 = this.checkRect.Right + 2f;
                    x += num3;
                    RectangleF* efPtr2 = &this.textRect;
                    efPtr2.Width -= num3;
                    this.textRect.Offset(num3, 0f);
                }
                if (this.CanHaveImage)
                {
                    Size imageSize = this.ImageSize;
                    this.imageRect = new RectangleF(x, 0f, (float) imageSize.Width, (float) imageSize.Height);
                    RectangleF* efPtr3 = &this.imageRect;
                    efPtr3.Y += (height - this.imageRect.Height) / 2f;
                    x = this.imageRect.Right + 2f;
                    float num4 = imageSize.Width + 2;
                    RectangleF* efPtr4 = &this.textRect;
                    efPtr4.Width -= num4;
                    this.textRect.Offset(num4, 0f);
                }
                this.textRect.Offset(0f, -this.textRect.Y);
            }

            public static PanelBrick CreateBrick(ListViewItem item) => 
                new ListViewLinkBase.BrickFactory().CreateBrickInternal(item);

            private PanelBrick CreateBrickInternal(ListViewItem item)
            {
                PanelBrick brick = new PanelBrick();
                this.item = item;
                this.currentImageList = this.IsLargeImages ? item.ListView.LargeImageList : item.ListView.SmallImageList;
                this.textBrick = new TextBrick();
                this.textBrick.StringFormat = this.GetStringFormat();
                this.textBrick.Text = item.Text;
                this.textBrick.Sides = BorderSide.None;
                brick.Bricks.Add(this.textBrick);
                if (this.CanHaveCheckBox)
                {
                    this.checkBrick = new CheckBoxBrick();
                    this.checkBrick.Checked = item.Checked;
                    this.checkBrick.Sides = BorderSide.None;
                    brick.Bricks.Add(this.checkBrick);
                }
                if (this.CanHaveImage && this.HaveImage)
                {
                    this.imageBrick = new ImageBrick();
                    this.imageBrick.Image = this.GetImage();
                    this.imageBrick.Sides = BorderSide.None;
                    brick.Bricks.Add(this.imageBrick);
                }
                this.CalcLayout();
                this.textBrick.Rect = this.textRect;
                if (this.checkBrick != null)
                {
                    this.checkBrick.Rect = this.checkRect;
                }
                if (this.imageBrick != null)
                {
                    this.imageBrick.Rect = this.imageRect;
                }
                brick.Rect = this.GetItemRect();
                return brick;
            }

            private SizeF GetCheckSize() => 
                this.checkBrick.CheckSize;

            private Image GetImage()
            {
                Image image = null;
                if (this.CanHaveImage && this.HaveImage)
                {
                    image = this.currentImageList.Images[this.item.ImageIndex];
                }
                return image;
            }

            private unsafe RectangleF GetItemRect()
            {
                RectangleF bounds = this.item.Bounds;
                bounds.Offset(-this.Offset.X, 0f);
                if (bounds.Width > this.item.ListView.Width)
                {
                    bounds.Width = this.item.ListView.Width;
                }
                if (this.item.ListView.View == View.Details)
                {
                    if (this.item.ListView.Columns.Count > 0)
                    {
                        bounds.Width = this.item.ListView.Columns[0].Width;
                    }
                    RectangleF* efPtr1 = &bounds;
                    efPtr1.Y -= this.item.ListView.Items[0].Bounds.Y - 2;
                }
                return bounds;
            }

            private BrickStringFormat GetStringFormat()
            {
                StringFormat format;
                if (this.IsLargeImages)
                {
                    format = new StringFormat(StringFormatFlags.LineLimit) {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };
                }
                else
                {
                    format = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap) {
                        LineAlignment = StringAlignment.Near,
                        Alignment = StringAlignment.Near
                    };
                }
                format.Trimming = StringTrimming.EllipsisCharacter;
                return new BrickStringFormat(format);
            }

            private PointF Offset
            {
                get
                {
                    if (this.offset == null)
                    {
                        RectangleF itemRect = this.item.ListView.GetItemRect(0);
                        this.offset = new PointF?(itemRect.Location);
                    }
                    return this.offset.Value;
                }
            }

            private bool CanHaveCheckBox =>
                this.item.ListView.CheckBoxes;

            private bool CanHaveImage =>
                this.currentImageList != null;

            private bool HaveImage =>
                (this.item.ImageIndex >= 0) && (this.item.ImageIndex < this.currentImageList.Images.Count);

            private bool IsLargeImages =>
                this.item.ListView.View == View.LargeIcon;

            private Size ImageSize =>
                this.currentImageList.ImageSize;
        }
    }
}

