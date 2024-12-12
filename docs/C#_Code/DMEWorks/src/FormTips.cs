using DMEWorks;
using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class FormTips : DmeForm
{
    private IContainer components;
    private ArrayList Tips;
    private const string TIP_FILE = "TIPOFDAY.TXT";
    private int CurrentTip;
    private const string Key_ShowAtStartup = "FormTips.ShowAtStartup";

    public FormTips()
    {
        base.Load += new EventHandler(this.FormTips_Load);
        this.Tips = new ArrayList();
        this.InitializeComponent();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        base.Close();
    }

    private void btnNextTip_Click(object sender, EventArgs e)
    {
        this.DoNextTip();
        this.DisplayCurrentTip();
    }

    private void chbShowTipsStartup_CheckedChanged(object sender, EventArgs e)
    {
        ShowAtStartup = this.chbShowAtStartup.Checked;
    }

    public void DisplayCurrentTip()
    {
        if ((0 <= this.CurrentTip) && ((this.CurrentTip < this.Tips.Count) && (this.Tips[this.CurrentTip] is string)))
        {
            this.lblTipText.Text = Conversions.ToString(this.Tips[this.CurrentTip]);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void DoNextTip()
    {
        if (0 < this.Tips.Count)
        {
            this.CurrentTip = new Random(DateTime.Now.Ticks.GetHashCode()).Next(0, this.Tips.Count - 1);
        }
    }

    private void FormTips_Load(object sender, EventArgs e)
    {
        this.chbShowAtStartup.Checked = ShowAtStartup;
        try
        {
            this.LoadTips(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\TIPOFDAY.TXT");
            this.DoNextTip();
            this.DisplayCurrentTip();
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
        ResourceManager manager = new ResourceManager(typeof(FormTips));
        this.Label3 = new Label();
        this.lblTipText = new Label();
        this.PictureBox1 = new PictureBox();
        this.Label1 = new Label();
        this.btnNextTip = new Button();
        this.btnClose = new Button();
        this.chbShowAtStartup = new CheckBox();
        this.Panel = new GroupBox();
        this.Panel.SuspendLayout();
        base.SuspendLayout();
        this.Label3.BorderStyle = BorderStyle.Fixed3D;
        this.Label3.Location = new Point(0x70, 40);
        this.Label3.Name = "Label3";
        this.Label3.Size = new Size(0x120, 2);
        this.Label3.TabIndex = 3;
        this.lblTipText.Location = new Point(0x70, 0x38);
        this.lblTipText.Name = "lblTipText";
        this.lblTipText.Size = new Size(0x120, 120);
        this.lblTipText.TabIndex = 2;
        this.lblTipText.Text = "Something that you should know";
        this.PictureBox1.Image = (Bitmap) manager.GetObject("PictureBox1.Image");
        this.PictureBox1.Location = new Point(40, 0x30);
        this.PictureBox1.Name = "PictureBox1";
        this.PictureBox1.Size = new Size(0x20, 40);
        this.PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        this.PictureBox1.TabIndex = 1;
        this.PictureBox1.TabStop = false;
        this.Label1.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
        this.Label1.Location = new Point(0x70, 0x10);
        this.Label1.Name = "Label1";
        this.Label1.Size = new Size(0x120, 0x18);
        this.Label1.TabIndex = 0;
        this.Label1.Text = "Did you know...";
        this.btnNextTip.ImageIndex = 0;
        this.btnNextTip.Location = new Point(0xf8, 200);
        this.btnNextTip.Name = "btnNextTip";
        this.btnNextTip.Size = new Size(0x4b, 0x19);
        this.btnNextTip.TabIndex = 1;
        this.btnNextTip.Text = "Next Tip";
        this.btnClose.Location = new Point(0x150, 200);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new Size(0x4b, 0x19);
        this.btnClose.TabIndex = 2;
        this.btnClose.Text = "Close";
        this.chbShowAtStartup.Location = new Point(8, 200);
        this.chbShowAtStartup.Name = "chbShowTipsStartup";
        this.chbShowAtStartup.Size = new Size(0x80, 0x18);
        this.chbShowAtStartup.TabIndex = 3;
        this.chbShowAtStartup.Text = "Show Tips at startup";
        Control[] controls = new Control[] { this.Label3, this.lblTipText, this.PictureBox1, this.Label1 };
        this.Panel.Controls.AddRange(controls);
        this.Panel.Location = new Point(8, 8);
        this.Panel.Name = "Panel";
        this.Panel.Size = new Size(0x198, 0xb8);
        this.Panel.TabIndex = 0;
        this.Panel.TabStop = false;
        base.AcceptButton = this.btnClose;
        this.AutoScaleBaseSize = new Size(5, 13);
        base.ClientSize = new Size(0x1aa, 0xed);
        Control[] controlArray2 = new Control[] { this.chbShowAtStartup, this.btnClose, this.btnNextTip, this.Panel };
        base.Controls.AddRange(controlArray2);
        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "FormTips";
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Tip of the day...";
        this.Panel.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    private void LoadTips(string FileName)
    {
        using (FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            StreamReader reader = new StreamReader(stream);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        string str = reader.ReadLine();
                        if (str != null)
                        {
                            this.Tips.Add(str);
                        }
                        else
                        {
                            return;
                        }
                        break;
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
            }
        }
    }

    [field: AccessedThroughProperty("Label1")]
    private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("PictureBox1")]
    private PictureBox PictureBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnClose")]
    private Button btnClose { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label3")]
    private Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("btnNextTip")]
    private Button btnNextTip { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("chbShowAtStartup")]
    private CheckBox chbShowAtStartup { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblTipText")]
    private Label lblTipText { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Panel")]
    private GroupBox Panel { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public static bool ShowAtStartup
    {
        get => 
            UserSettings.GetBool("FormTips.ShowAtStartup", true);
        set => 
            UserSettings.SetBool("FormTips.ShowAtStartup", value);
    }
}

