using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class FormImageViewer : DmeForm
{
    private IContainer components;
    private int FIndex = 0;

    public FormImageViewer(Image image)
    {
        if (image == null)
        {
            throw new ArgumentNullException("image");
        }
        this.InitializeComponent();
        this.Picture.Image = image;
        this.Index = 0;
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        ComponentResourceManager manager = new ComponentResourceManager(typeof(FormImageViewer));
        this.Picture = new PictureBox();
        this.tsMultipage = new ToolStrip();
        this.tsbPrev = new ToolStripButton();
        this.tslPageNofM = new ToolStripLabel();
        this.tsbNext = new ToolStripButton();
        this.ToolStripContainer1 = new ToolStripContainer();
        ((ISupportInitialize) this.Picture).BeginInit();
        this.tsMultipage.SuspendLayout();
        this.ToolStripContainer1.ContentPanel.SuspendLayout();
        this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
        this.ToolStripContainer1.SuspendLayout();
        base.SuspendLayout();
        this.Picture.BackColor = Color.FromArgb(0xff, 240, 0xff);
        this.Picture.BorderStyle = BorderStyle.FixedSingle;
        this.Picture.Dock = DockStyle.Fill;
        this.Picture.Location = new Point(0, 0);
        this.Picture.Name = "Picture";
        this.Picture.Size = new Size(0x124, 0xf5);
        this.Picture.SizeMode = PictureBoxSizeMode.Zoom;
        this.Picture.TabIndex = 0x4c;
        this.Picture.TabStop = false;
        ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tslPageNofM, this.tsbPrev, this.tsbNext };
        this.tsMultipage.Items.AddRange(toolStripItems);
        this.tsMultipage.Location = new Point(8, 0);
        this.tsMultipage.Name = "tsMultipage";
        this.tsMultipage.Size = new Size(0xab, 0x1c);
        this.tsMultipage.TabIndex = 0x4d;
        this.tsMultipage.Text = "ToolStrip1";
        this.tsbPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.tsbPrev.Image = (Image) manager.GetObject("tsbPrev.Image");
        this.tsbPrev.ImageScaling = ToolStripItemImageScaling.None;
        this.tsbPrev.ImageTransparentColor = Color.Magenta;
        this.tsbPrev.Name = "tsbPrev";
        this.tsbPrev.Size = new Size(0x19, 0x19);
        this.tsbPrev.Text = "ToolStripButton1";
        this.tslPageNofM.Name = "tslPageNofM";
        this.tslPageNofM.Size = new Size(80, 0x19);
        this.tslPageNofM.Text = "Page 23 of 345";
        this.tsbNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.tsbNext.Image = (Image) manager.GetObject("tsbNext.Image");
        this.tsbNext.ImageScaling = ToolStripItemImageScaling.None;
        this.tsbNext.ImageTransparentColor = Color.Magenta;
        this.tsbNext.Name = "tsbNext";
        this.tsbNext.Size = new Size(0x19, 0x19);
        this.tsbNext.Text = "ToolStripButton2";
        this.ToolStripContainer1.BottomToolStripPanelVisible = false;
        this.ToolStripContainer1.ContentPanel.Controls.Add(this.Picture);
        this.ToolStripContainer1.ContentPanel.Size = new Size(0x124, 0xf5);
        this.ToolStripContainer1.Dock = DockStyle.Fill;
        this.ToolStripContainer1.LeftToolStripPanelVisible = false;
        this.ToolStripContainer1.Location = new Point(0, 0);
        this.ToolStripContainer1.Name = "ToolStripContainer1";
        this.ToolStripContainer1.RightToolStripPanelVisible = false;
        this.ToolStripContainer1.Size = new Size(0x124, 0x111);
        this.ToolStripContainer1.TabIndex = 0x4e;
        this.ToolStripContainer1.Text = "ToolStripContainer1";
        this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.tsMultipage);
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x124, 0x111);
        base.Controls.Add(this.ToolStripContainer1);
        base.Name = "FormImageViewer";
        this.Text = "Image Viewer";
        ((ISupportInitialize) this.Picture).EndInit();
        this.tsMultipage.ResumeLayout(false);
        this.tsMultipage.PerformLayout();
        this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
        this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
        this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
        this.ToolStripContainer1.ResumeLayout(false);
        this.ToolStripContainer1.PerformLayout();
        base.ResumeLayout(false);
    }

    private void tsbNext_Click(object sender, EventArgs e)
    {
        this.Index++;
    }

    private void tsbPrev_Click(object sender, EventArgs e)
    {
        this.Index--;
    }

    [field: AccessedThroughProperty("Picture")]
    private PictureBox Picture { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("tslPageNofM")]
    private ToolStripLabel tslPageNofM { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("tsbPrev")]
    private ToolStripButton tsbPrev { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("tsbNext")]
    private ToolStripButton tsbNext { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("ToolStripContainer1")]
    private ToolStripContainer ToolStripContainer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("tsMultipage")]
    private ToolStrip tsMultipage { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    private int Index
    {
        get => 
            this.FIndex;
        set
        {
            Image image = this.Picture.Image;
            int frameCount = image.GetFrameCount(FrameDimension.Page);
            if (value < 0)
            {
                value = 0;
            }
            else if (frameCount <= value)
            {
                value = frameCount - 1;
            }
            this.FIndex = value;
            try
            {
                this.Picture.Image = null;
                image.SelectActiveFrame(FrameDimension.Page, this.FIndex);
            }
            finally
            {
                this.Picture.Image = image;
            }
            this.tslPageNofM.Text = $"Page {this.FIndex + 1} of {frameCount}";
        }
    }
}

