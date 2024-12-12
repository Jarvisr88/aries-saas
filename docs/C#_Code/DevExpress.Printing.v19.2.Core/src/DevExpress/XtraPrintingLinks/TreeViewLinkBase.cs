namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultProperty("TreeView")]
    public class TreeViewLinkBase : LinkBase
    {
        private System.Windows.Forms.TreeView treeView;
        private float offset;

        public TreeViewLinkBase()
        {
        }

        public TreeViewLinkBase(PrintingSystemBase ps) : base(ps)
        {
        }

        protected override void BeforeCreate()
        {
            if (this.TreeView == null)
            {
                throw new NullReferenceException("The TreeView property value must not be null");
            }
            base.BeforeCreate();
            base.ps.Graph.PageUnit = GraphicsUnit.Pixel;
        }

        protected override void CreateDetail(BrickGraphics gr)
        {
            if (this.treeView.Nodes.Count != 0)
            {
                this.treeView.ExpandAll();
                if (this.treeView.Nodes[0].Bounds.Y < 0)
                {
                    this.offset = -this.treeView.Nodes[0].Bounds.Y;
                }
                this.CreateNodeBricks(gr, this.treeView.Nodes);
                this.treeView.CollapseAll();
            }
        }

        private void CreateNodeBricks(BrickGraphics gr, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                gr.DefaultBrickStyle.BackColor = (node.BackColor == Color.Empty) ? this.treeView.BackColor : node.BackColor;
                gr.DefaultBrickStyle.ForeColor = (node.ForeColor == Color.Empty) ? this.treeView.ForeColor : node.ForeColor;
                gr.DefaultBrickStyle.BorderWidth = 0f;
                PanelBrick brick = BrickFactory.CreateBrick(node);
                RectangleF rect = brick.Rect;
                rect.Offset(0f, this.offset);
                gr.DrawBrick(brick, rect);
                this.OnNodeBrickDraw(brick, node, rect);
                this.CreateNodeBricks(gr, node.Nodes);
            }
        }

        protected virtual void OnNodeBrickDraw(Brick brick, TreeNode node, RectangleF rect)
        {
        }

        public override void SetDataObject(object data)
        {
            if (data is System.Windows.Forms.TreeView)
            {
                this.treeView = data as System.Windows.Forms.TreeView;
            }
        }

        public override System.Type PrintableObjectType =>
            typeof(System.Windows.Forms.TreeView);

        [Description("Gets or sets a TreeView object to be printed via the current link."), Category("Printing"), DefaultValue((string) null)]
        public System.Windows.Forms.TreeView TreeView
        {
            get => 
                this.treeView;
            set => 
                this.treeView = value;
        }

        private class BrickFactory
        {
            private const int space = 2;
            private TreeNode node;
            private TextBrick textBrick;
            private CheckBoxBrick checkBrick;
            private ImageBrick imageBrick;
            private RectangleF textRect = RectangleF.Empty;
            private RectangleF checkRect = RectangleF.Empty;
            private RectangleF imageRect = RectangleF.Empty;

            private void CalcLayout()
            {
                this.textRect = this.GetNodeRect();
                float x = 2 + (this.node.TreeView.Indent * this.node.Level);
                if (this.CanHaveCheckBox)
                {
                    this.checkRect.Size = this.GetCheckSize();
                    this.checkRect.Y = (this.textRect.Height - this.checkRect.Height) / 2f;
                    this.checkRect.X = x;
                    x = this.checkRect.Right + 2f;
                }
                if (this.CanHaveImage)
                {
                    this.imageRect = new RectangleF(x, 0f, (float) this.ImageSize.Width, (float) this.ImageSize.Height);
                    this.imageRect.Y = (this.textRect.Height - this.imageRect.Height) / 2f;
                    x = this.imageRect.Right + 2f;
                }
                this.textRect.X = x;
                this.textRect.Y = 0f;
            }

            public static PanelBrick CreateBrick(TreeNode node) => 
                new TreeViewLinkBase.BrickFactory().CreateBrickInternal(node);

            private PanelBrick CreateBrickInternal(TreeNode node)
            {
                PanelBrick brick = new PanelBrick();
                this.node = node;
                this.textBrick = new TextBrick();
                this.textBrick.StringFormat = GetStringFormat();
                this.textBrick.Text = node.Text;
                this.textBrick.Sides = BorderSide.None;
                this.textBrick.Font = (node.NodeFont != null) ? node.NodeFont : node.TreeView.Font;
                brick.Bricks.Add(this.textBrick);
                if (this.CanHaveCheckBox)
                {
                    this.checkBrick = new CheckBoxBrick();
                    this.checkBrick.Checked = node.Checked;
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
                brick.Rect = this.GetNodeRect();
                return brick;
            }

            private SizeF GetCheckSize() => 
                this.checkBrick.CheckSize;

            private Image GetImage()
            {
                Image image = null;
                if (this.CanHaveImage && this.HaveImage)
                {
                    image = this.node.TreeView.ImageList.Images[this.node.ImageIndex];
                }
                return image;
            }

            private RectangleF GetNodeRect()
            {
                RectangleF bounds = this.node.Bounds;
                bounds.X = 0f;
                bounds.Width = this.node.TreeView.Width;
                return bounds;
            }

            private static BrickStringFormat GetStringFormat() => 
                new BrickStringFormat(new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap) { 
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                });

            private bool CanHaveCheckBox =>
                this.node.TreeView.CheckBoxes;

            private bool CanHaveImage =>
                this.node.TreeView.ImageList != null;

            private bool HaveImage =>
                (this.node.ImageIndex >= 0) && (this.node.ImageIndex < this.node.TreeView.ImageList.Images.Count);

            private Size ImageSize =>
                this.node.TreeView.ImageList.ImageSize;
        }
    }
}

