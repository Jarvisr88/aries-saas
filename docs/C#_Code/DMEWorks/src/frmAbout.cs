using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class frmAbout : DmeForm
{
    private IContainer components;
    private ToolTip ToolTip1;

    public frmAbout()
    {
        base.Load += new EventHandler(this.frmAbout_Load);
        this.InitializeComponent();
    }

    private void cmdOK_Click(object eventSender, EventArgs eventArgs)
    {
        base.Close();
    }

    protected override void Dispose(bool Disposing)
    {
        if (Disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(Disposing);
    }

    private void frmAbout_Load(object eventSender, EventArgs eventArgs)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        AssemblyProductAttribute assemblyAttribute = GetAssemblyAttribute<AssemblyProductAttribute>(executingAssembly);
        if (assemblyAttribute != null)
        {
            this.lblAppTitle.Text = assemblyAttribute.Product;
            this.Text = "About " + assemblyAttribute.Product;
        }
        AssemblyInformationalVersionAttribute attribute2 = GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(executingAssembly);
        if (attribute2 != null)
        {
            this.lblAppVersion.Text = "Version: " + attribute2.InformationalVersion;
        }
        AssemblyCopyrightAttribute attribute3 = GetAssemblyAttribute<AssemblyCopyrightAttribute>(executingAssembly);
        if (attribute3 != null)
        {
            this.lblCopyright.Text = attribute3.Copyright;
        }
        AssemblyDescriptionAttribute attribute4 = GetAssemblyAttribute<AssemblyDescriptionAttribute>(executingAssembly);
        if (attribute4 != null)
        {
            this.lblAppDescription.Text = "Description: " + attribute4.Description;
        }
        this.lblLocation.Text = "Location: " + executingAssembly.Location;
    }

    private static Att GetAssemblyAttribute<Att>(Assembly assembly) where Att: Attribute
    {
        object[] customAttributes = assembly.GetCustomAttributes(typeof(Att), false);
        return ((customAttributes != null) ? ((customAttributes.Length != 0) ? Conversions.ToGenericParameter<Att>(customAttributes[0]) : default(Att)) : default(Att));
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAbout));
        this.ToolTip1 = new ToolTip(this.components);
        this.cmdOK = new Button();
        this.picIcon = new PictureBox();
        this.lblDisclaimer = new Label();
        this.lblAppDescription = new Label();
        this.lblCopyright = new Label();
        this.lblAppVersion = new Label();
        this.lblAppTitle = new Label();
        this.Label1 = new Label();
        this.llEmail = new LinkLabel();
        this.txtPhone = new TextBox();
        this.lblLocation = new Label();
        ((ISupportInitialize) this.picIcon).BeginInit();
        base.SuspendLayout();
        this.cmdOK.BackColor = SystemColors.Control;
        this.cmdOK.Cursor = Cursors.Default;
        this.cmdOK.DialogResult = DialogResult.Cancel;
        this.cmdOK.ForeColor = SystemColors.ControlText;
        this.cmdOK.Location = new Point(440, 0x130);
        this.cmdOK.Name = "cmdOK";
        this.cmdOK.RightToLeft = RightToLeft.No;
        this.cmdOK.Size = new Size(0x48, 0x19);
        this.cmdOK.TabIndex = 9;
        this.cmdOK.Tag = "OK";
        this.cmdOK.Text = "OK";
        this.cmdOK.UseVisualStyleBackColor = false;
        this.picIcon.BackColor = Color.Transparent;
        this.picIcon.Cursor = Cursors.Hand;
        this.picIcon.ForeColor = SystemColors.ControlText;
        this.picIcon.Image = (Image) manager.GetObject("picIcon.Image");
        this.picIcon.Location = new Point(8, 8);
        this.picIcon.Name = "picIcon";
        this.picIcon.RightToLeft = RightToLeft.No;
        this.picIcon.Size = new Size(0x1f8, 0x58);
        this.picIcon.SizeMode = PictureBoxSizeMode.CenterImage;
        this.picIcon.TabIndex = 4;
        this.picIcon.TabStop = false;
        this.lblDisclaimer.BackColor = SystemColors.Control;
        this.lblDisclaimer.Cursor = Cursors.Default;
        this.lblDisclaimer.ForeColor = Color.Black;
        this.lblDisclaimer.Location = new Point(8, 0x128);
        this.lblDisclaimer.Name = "lblDisclaimer";
        this.lblDisclaimer.RightToLeft = RightToLeft.No;
        this.lblDisclaimer.Size = new Size(0x1a8, 0x40);
        this.lblDisclaimer.TabIndex = 8;
        this.lblDisclaimer.Tag = "Warning: ...";
        this.lblDisclaimer.Text = manager.GetString("lblDisclaimer.Text");
        this.lblAppDescription.ForeColor = Color.Black;
        this.lblAppDescription.Location = new Point(8, 200);
        this.lblAppDescription.Name = "lblAppDescription";
        this.lblAppDescription.Size = new Size(0x180, 0x36);
        this.lblAppDescription.TabIndex = 4;
        this.lblAppDescription.Tag = "App Description";
        this.lblAppDescription.Text = "App Description: ";
        this.lblCopyright.ForeColor = SystemColors.ControlText;
        this.lblCopyright.Location = new Point(8, 0x98);
        this.lblCopyright.Name = "lblCopyright";
        this.lblCopyright.Size = new Size(0x180, 0x15);
        this.lblCopyright.TabIndex = 2;
        this.lblCopyright.Text = "Copyright Information";
        this.lblAppVersion.ForeColor = SystemColors.ControlText;
        this.lblAppVersion.Location = new Point(8, 0x80);
        this.lblAppVersion.Name = "lblAppVersion";
        this.lblAppVersion.Size = new Size(0x180, 0x15);
        this.lblAppVersion.TabIndex = 1;
        this.lblAppVersion.Tag = "Version";
        this.lblAppVersion.Text = "Application Version";
        this.lblAppTitle.ForeColor = Color.Black;
        this.lblAppTitle.Location = new Point(8, 0x68);
        this.lblAppTitle.Name = "lblAppTitle";
        this.lblAppTitle.Size = new Size(0x180, 0x15);
        this.lblAppTitle.TabIndex = 0;
        this.lblAppTitle.Tag = "Application Title";
        this.lblAppTitle.Text = "Application Title";
        this.Label1.BorderStyle = BorderStyle.Fixed3D;
        this.Label1.Location = new Point(8, 0x120);
        this.Label1.Name = "Label1";
        this.Label1.Size = new Size(0x1f8, 3);
        this.Label1.TabIndex = 7;
        this.llEmail.Location = new Point(160, 0x100);
        this.llEmail.Name = "llEmail";
        this.llEmail.Size = new Size(0xa8, 0x17);
        this.llEmail.TabIndex = 6;
        this.llEmail.TabStop = true;
        this.llEmail.Text = "mailto:support@dmeworks.com";
        this.txtPhone.Location = new Point(8, 0x100);
        this.txtPhone.Name = "txtPhone";
        this.txtPhone.ReadOnly = true;
        this.txtPhone.Size = new Size(0x88, 20);
        this.txtPhone.TabIndex = 5;
        this.txtPhone.Text = "Call: 866-DMEWORX";
        this.lblLocation.ForeColor = SystemColors.ControlText;
        this.lblLocation.Location = new Point(8, 0xb0);
        this.lblLocation.Name = "lblLocation";
        this.lblLocation.Size = new Size(0x180, 0x15);
        this.lblLocation.TabIndex = 3;
        this.lblLocation.Text = "Location :";
        base.AcceptButton = this.cmdOK;
        this.AutoScaleBaseSize = new Size(5, 13);
        base.CancelButton = this.cmdOK;
        base.ClientSize = new Size(520, 0x16d);
        base.Controls.Add(this.lblLocation);
        base.Controls.Add(this.txtPhone);
        base.Controls.Add(this.llEmail);
        base.Controls.Add(this.Label1);
        base.Controls.Add(this.cmdOK);
        base.Controls.Add(this.picIcon);
        base.Controls.Add(this.lblDisclaimer);
        base.Controls.Add(this.lblAppDescription);
        base.Controls.Add(this.lblCopyright);
        base.Controls.Add(this.lblAppVersion);
        base.Controls.Add(this.lblAppTitle);
        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.Location = new Point(4, 0x18);
        base.Name = "frmAbout";
        this.Text = "About";
        ((ISupportInitialize) this.picIcon).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void llEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            Help.ShowHelp(this, "mailto:support@DMEWorks.com");
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            ProjectData.ClearProjectError();
        }
    }

    private void picIcon_Click(object sender, EventArgs e)
    {
        try
        {
            Help.ShowHelp(this, "http://www.dmeworks.com");
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

    [field: AccessedThroughProperty("cmdOK")]
    private Button cmdOK { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("picIcon")]
    private PictureBox picIcon { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblDisclaimer")]
    private Label lblDisclaimer { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAppDescription")]
    private Label lblAppDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblCopyright")]
    private Label lblCopyright { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAppVersion")]
    private Label lblAppVersion { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAppTitle")]
    private Label lblAppTitle { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label1")]
    private Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("llEmail")]
    private LinkLabel llEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLocation")]
    private Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtPhone")]
    private TextBox txtPhone { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }
}

