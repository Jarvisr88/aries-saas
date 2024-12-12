using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class FormImages : DmeForm
{
    private IContainer components;

    public FormImages()
    {
        base.Load += new EventHandler(this.FormImages_Load);
        this.InitializeComponent();
    }

    private void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            Image image = this.btnConvert.Image;
            if (image != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    dialog.FilterIndex = 0;
                    dialog.DefaultExt = "bmp";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        image.Save(dialog.FileName, ImageFormat.Bmp);
                    }
                }
            }
        }
        catch (Exception exception1)
        {
            Exception ex = exception1;
            ProjectData.SetProjectError(ex);
            Exception exception = ex;
            this.ShowException(exception);
            ProjectData.ClearProjectError();
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            object selectedValue = this.ComboBox1.SelectedValue;
            if (selectedValue is Image)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Bitmap (*.bmp)|*.bmp|All files (*.*)|*.*";
                    dialog.FilterIndex = 0;
                    dialog.DefaultExt = "bmp";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ((Image) selectedValue).Save(dialog.FileName, ImageFormat.Bmp);
                    }
                }
            }
        }
        catch (Exception exception1)
        {
            Exception ex = exception1;
            ProjectData.SetProjectError(ex);
            Exception exception = ex;
            this.ShowException(exception);
            ProjectData.ClearProjectError();
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedPath;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = dialog.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            int num = this.imglButtons.Images.Count - 1;
            int num2 = 0;
            while (true)
            {
                if (num2 > num)
                {
                    int num3 = this.ilButtons.Images.Count - 1;
                    for (int i = 0; i <= num3; i++)
                    {
                        Image image2 = this.ilButtons.Images[i];
                        try
                        {
                            image2.Save(Path.Combine(selectedPath, "ilButtons" + Conversions.ToString(i) + ".png"), ImageFormat.Png);
                        }
                        catch (Exception exception2)
                        {
                            ProjectData.SetProjectError(exception2);
                            ProjectData.ClearProjectError();
                        }
                    }
                    break;
                }
                Image image = this.imglButtons.Images[num2];
                try
                {
                    image.Save(Path.Combine(selectedPath, "imglButtons" + Conversions.ToString(num2) + ".png"), ImageFormat.Png);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                num2++;
            }
        }
        catch (Exception exception3)
        {
            Exception ex = exception3;
            ProjectData.SetProjectError(ex);
            Exception exception = ex;
            this.ShowException(exception);
            ProjectData.ClearProjectError();
        }
    }

    private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        if ((0 <= e.Index) && ((e.Index < this.ComboBox1.Items.Count) && (this.ComboBox1.Items[e.Index] is Image)))
        {
            Image image = (Image) this.ComboBox1.Items[e.Index];
            e.Graphics.DrawImage(image, e.Bounds.Left + 2, e.Bounds.Top);
        }
        e.Graphics.DrawString(e.Index.ToString(), this.ComboBox1.Font, Brushes.Black, new RectangleF((float) (e.Bounds.Left + 20), (float) e.Bounds.Top, (float) (e.Bounds.Width - 20), (float) e.Bounds.Height));
        e.DrawFocusRectangle();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void FormImages_Load(object sender, EventArgs e)
    {
        try
        {
            this.ComboBox1.DataSource = this.imglButtons.Images;
            this.ComboBox1.ItemHeight = this.imglButtons.ImageSize.Height;
            this.ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
        }
        catch (Exception exception1)
        {
            Exception ex = exception1;
            ProjectData.SetProjectError(ex);
            Exception exception = ex;
            this.ShowException(exception);
            ProjectData.ClearProjectError();
        }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        ComponentResourceManager manager = new ComponentResourceManager(typeof(FormImages));
        this.imglButtons = new ImageList(this.components);
        this.ilButtons = new ImageList(this.components);
        this.ComboBox1 = new ComboBox();
        this.btnSave = new Button();
        this.btnConvert = new Button();
        this.btnSaveAll = new Button();
        base.SuspendLayout();
        this.imglButtons.ImageStream = (ImageListStreamer) manager.GetObject("imglButtons.ImageStream");
        this.imglButtons.TransparentColor = Color.Magenta;
        this.imglButtons.Images.SetKeyName(0, "");
        this.imglButtons.Images.SetKeyName(1, "");
        this.imglButtons.Images.SetKeyName(2, "");
        this.imglButtons.Images.SetKeyName(3, "");
        this.imglButtons.Images.SetKeyName(4, "");
        this.imglButtons.Images.SetKeyName(5, "");
        this.imglButtons.Images.SetKeyName(6, "");
        this.imglButtons.Images.SetKeyName(7, "");
        this.imglButtons.Images.SetKeyName(8, "");
        this.imglButtons.Images.SetKeyName(9, "");
        this.imglButtons.Images.SetKeyName(10, "");
        this.imglButtons.Images.SetKeyName(11, "");
        this.imglButtons.Images.SetKeyName(12, "");
        this.imglButtons.Images.SetKeyName(13, "");
        this.imglButtons.Images.SetKeyName(14, "");
        this.imglButtons.Images.SetKeyName(15, "");
        this.imglButtons.Images.SetKeyName(0x10, "");
        this.imglButtons.Images.SetKeyName(0x11, "");
        this.imglButtons.Images.SetKeyName(0x12, "");
        this.imglButtons.Images.SetKeyName(0x13, "");
        this.imglButtons.Images.SetKeyName(20, "");
        this.imglButtons.Images.SetKeyName(0x15, "");
        this.imglButtons.Images.SetKeyName(0x16, "");
        this.imglButtons.Images.SetKeyName(0x17, "");
        this.imglButtons.Images.SetKeyName(0x18, "");
        this.imglButtons.Images.SetKeyName(0x19, "ImageEnlarge.PNG");
        this.imglButtons.Images.SetKeyName(0x1a, "ImageOpen.PNG");
        this.imglButtons.Images.SetKeyName(0x1b, "ImageEmail.bmp");
        this.imglButtons.Images.SetKeyName(0x1c, "ImageClose.PNG");
        this.imglButtons.Images.SetKeyName(0x1d, "Refresh.bmp");
        this.ilButtons.ImageStream = (ImageListStreamer) manager.GetObject("ilButtons.ImageStream");
        this.ilButtons.TransparentColor = Color.Magenta;
        this.ilButtons.Images.SetKeyName(0, "");
        this.ilButtons.Images.SetKeyName(1, "");
        this.ilButtons.Images.SetKeyName(2, "");
        this.ilButtons.Images.SetKeyName(3, "");
        this.ilButtons.Images.SetKeyName(4, "");
        this.ilButtons.Images.SetKeyName(5, "");
        this.ilButtons.Images.SetKeyName(6, "");
        this.ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
        this.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        this.ComboBox1.ItemHeight = 15;
        this.ComboBox1.Location = new Point(8, 0x10);
        this.ComboBox1.Name = "ComboBox1";
        this.ComboBox1.Size = new Size(0x79, 0x15);
        this.ComboBox1.TabIndex = 0;
        this.btnSave.Location = new Point(0x90, 0x10);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new Size(0x4b, 0x17);
        this.btnSave.TabIndex = 1;
        this.btnSave.Text = "Save";
        this.btnConvert.Image = (Image) manager.GetObject("btnConvert.Image");
        this.btnConvert.ImageAlign = ContentAlignment.MiddleLeft;
        this.btnConvert.Location = new Point(0x90, 0x30);
        this.btnConvert.Name = "btnConvert";
        this.btnConvert.Size = new Size(0x4b, 0x17);
        this.btnConvert.TabIndex = 2;
        this.btnConvert.Text = "Save";
        this.btnConvert.TextAlign = ContentAlignment.MiddleRight;
        this.btnSaveAll.Location = new Point(8, 0x30);
        this.btnSaveAll.Name = "btnSaveAll";
        this.btnSaveAll.Size = new Size(0x4b, 0x17);
        this.btnSaveAll.TabIndex = 3;
        this.btnSaveAll.Text = "Save All";
        this.AutoScaleBaseSize = new Size(5, 13);
        base.ClientSize = new Size(0x124, 0x111);
        base.Controls.Add(this.btnSaveAll);
        base.Controls.Add(this.btnConvert);
        base.Controls.Add(this.btnSave);
        base.Controls.Add(this.ComboBox1);
        base.Name = "FormImages";
        this.Text = "FormImages";
        base.ResumeLayout(false);
    }

    [field: AccessedThroughProperty("imglButtons")]
    private ImageList imglButtons { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("ilButtons")]
    private ImageList ilButtons { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("ComboBox1")]
    private ComboBox ComboBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnSave")]
    private Button btnSave { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnSaveAll")]
    private Button btnSaveAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnConvert")]
    internal virtual Button btnConvert { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }
}

