namespace #PAb
{
    using #H;
    using ActiproSoftware.Products;
    using ActiproSoftware.Products.Shared;
    using ActiproSoftware.Properties;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;

    internal class #Rqe : Form
    {
        private Timer #tCb;
        private ActiproLicense #sBb;
        private string #oVd = #G.#eg(0x1a49);
        private string #uCb = #G.#eg(0x1a9e);
        private PictureBox #Nte;
        private Label #Ote;
        private Button #Pte;
        private ImageList #sZd;
        private Button #Wce;
        private LinkLabel #Qte;
        private TabControl #DCb;
        private TabPage #Rte;
        private Label #Ste;
        private GroupBox #Tte;
        private Label #Ute;
        private TextBox #GCb;
        private Label #Vte;
        private TextBox #ICb;
        private TextBox #JCb;
        private Button #Wte;
        private Label #Xte;
        private Label #Yte;
        private TabPage #Zte;
        private ListView #ECb;
        private Label #0te;
        private ColumnHeader #1te;
        private Label #2te;
        private TextBox #HCb;
        private Button #3te;
        private PictureBox #4te;
        private Label #5te;
        private Label #6te;
        private PictureBox #7te;
        private Label #8te;
        private Label #9te;
        private IContainer #ZXd;

        private void #AVb()
        {
            Process local1 = Process.Start(this.#uCb);
        }

        private void #B5d()
        {
            this.#ZXd = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(System.Type.GetTypeFromHandle(typeof(#Rqe).TypeHandle));
            this.#Nte = new PictureBox();
            this.#Ote = new Label();
            this.#Pte = new Button();
            this.#DCb = new TabControl();
            this.#Zte = new TabPage();
            this.#7te = new PictureBox();
            this.#8te = new Label();
            this.#4te = new PictureBox();
            this.#5te = new Label();
            this.#6te = new Label();
            this.#ECb = new ListView();
            this.#1te = new ColumnHeader();
            this.#sZd = new ImageList(this.#ZXd);
            this.#0te = new Label();
            this.#9te = new Label();
            this.#Rte = new TabPage();
            this.#Tte = new GroupBox();
            this.#Wte = new Button();
            this.#Xte = new Label();
            this.#JCb = new TextBox();
            this.#Yte = new Label();
            this.#ICb = new TextBox();
            this.#2te = new Label();
            this.#Ute = new Label();
            this.#HCb = new TextBox();
            this.#GCb = new TextBox();
            this.#Ste = new Label();
            this.#Wce = new Button();
            this.#Qte = new LinkLabel();
            this.#Vte = new Label();
            this.#3te = new Button();
            ((ISupportInitialize) this.#Nte).BeginInit();
            this.#DCb.SuspendLayout();
            this.#Zte.SuspendLayout();
            ((ISupportInitialize) this.#7te).BeginInit();
            ((ISupportInitialize) this.#4te).BeginInit();
            this.#Rte.SuspendLayout();
            this.#Tte.SuspendLayout();
            base.SuspendLayout();
            this.#Nte.BorderStyle = BorderStyle.Fixed3D;
            this.#Nte.Location = new Point(8, 8);
            this.#Nte.Name = #G.#eg(0x1adb);
            this.#Nte.Size = new Size(0x1f0, 100);
            this.#Nte.TabIndex = 0;
            this.#Nte.TabStop = false;
            this.#Nte.Paint += new PaintEventHandler(this.#sxe);
            this.#Ote.AutoSize = true;
            this.#Ote.BackColor = System.Drawing.Color.Transparent;
            this.#Ote.Location = new Point(0x1c, 0x72);
            this.#Ote.Name = #G.#eg(0x1af4);
            this.#Ote.Size = new Size(0x30, 13);
            this.#Ote.TabIndex = 1;
            this.#Ote.Text = #G.#eg(0x1b05);
            this.#Pte.FlatStyle = FlatStyle.System;
            this.#Pte.Location = new Point(0x12a, 0x1b6);
            this.#Pte.Name = #G.#eg(0x1b12);
            this.#Pte.Size = new Size(100, 0x17);
            this.#Pte.TabIndex = 0x5b;
            this.#Pte.Text = #G.#eg(0x1b27);
            this.#Pte.Click += new EventHandler(this.#vxe);
            this.#DCb.Controls.Add(this.#Zte);
            this.#DCb.Controls.Add(this.#Rte);
            this.#DCb.Location = new Point(8, 0xa8);
            this.#DCb.Name = #G.#eg(0x488);
            this.#DCb.SelectedIndex = 0;
            this.#DCb.Size = new Size(0x1f0, 0x108);
            this.#DCb.TabIndex = 2;
            this.#Zte.BackColor = SystemColors.Window;
            this.#Zte.Controls.Add(this.#7te);
            this.#Zte.Controls.Add(this.#8te);
            this.#Zte.Controls.Add(this.#4te);
            this.#Zte.Controls.Add(this.#5te);
            this.#Zte.Controls.Add(this.#6te);
            this.#Zte.Controls.Add(this.#ECb);
            this.#Zte.Controls.Add(this.#0te);
            this.#Zte.Controls.Add(this.#9te);
            this.#Zte.Location = new Point(4, 0x16);
            this.#Zte.Name = #G.#eg(0x1b38);
            this.#Zte.Size = new Size(0x1e8, 0xee);
            this.#Zte.TabIndex = 4;
            this.#Zte.Text = #G.#eg(0x1b49);
            this.#7te.Image = ActiproSoftware.Properties.Resources.Unlicensed16;
            this.#7te.Location = new Point(0x193, 14);
            this.#7te.Name = #G.#eg(0x1b62);
            this.#7te.Size = new Size(0x10, 0x10);
            this.#7te.SizeMode = PictureBoxSizeMode.AutoSize;
            this.#7te.TabIndex = 14;
            this.#7te.TabStop = false;
            this.#8te.AutoSize = true;
            this.#8te.BackColor = System.Drawing.Color.Transparent;
            this.#8te.Location = new Point(0x1a1, 0x10);
            this.#8te.Name = #G.#eg(0x1b73);
            this.#8te.Size = new Size(60, 13);
            this.#8te.TabIndex = 15;
            this.#8te.Text = #G.#eg(0x1b7c);
            this.#4te.Image = ActiproSoftware.Properties.Resources.Licensed16;
            this.#4te.Location = new Point(0x14d, 14);
            this.#4te.Name = #G.#eg(0x1b8d);
            this.#4te.Size = new Size(0x10, 0x10);
            this.#4te.SizeMode = PictureBoxSizeMode.AutoSize;
            this.#4te.TabIndex = 12;
            this.#4te.TabStop = false;
            this.#5te.BackColor = System.Drawing.Color.Transparent;
            this.#5te.Font = new Font(#G.#eg(0x856), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.#5te.Location = new Point(0xf3, 0x10);
            this.#5te.Name = #G.#eg(0x1b9e);
            this.#5te.Size = new Size(0x56, 13);
            this.#5te.TabIndex = 11;
            this.#5te.Text = #G.#eg(0x1ba7);
            this.#5te.TextAlign = ContentAlignment.TopRight;
            this.#6te.BackColor = System.Drawing.Color.Transparent;
            this.#6te.Location = new Point(0x10, 0xd4);
            this.#6te.Name = #G.#eg(0x1bb4);
            this.#6te.Size = new Size(0x1c8, 0x10);
            this.#6te.TabIndex = 10;
            this.#6te.Text = #G.#eg(0x1bc9);
            this.#6te.TextAlign = ContentAlignment.TopCenter;
            this.#ECb.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            ColumnHeader[] values = new ColumnHeader[] { this.#1te };
            this.#ECb.Columns.AddRange(values);
            this.#ECb.HeaderStyle = ColumnHeaderStyle.None;
            this.#ECb.Location = new Point(0x10, 0x23);
            this.#ECb.Name = #G.#eg(0x1c26);
            this.#ECb.Size = new Size(0x1c8, 0xa7);
            this.#ECb.SmallImageList = this.#sZd;
            this.#ECb.TabIndex = 3;
            this.#ECb.UseCompatibleStateImageBehavior = false;
            this.#ECb.View = View.Details;
            this.#1te.Width = 430;
            this.#sZd.ImageStream = (ImageListStreamer) manager.GetObject(#G.#eg(0x1c3f));
            this.#sZd.TransparentColor = System.Drawing.Color.Transparent;
            this.#sZd.Images.SetKeyName(0, #G.#eg(0x1c5c));
            this.#sZd.Images.SetKeyName(1, #G.#eg(0x1c75));
            this.#0te.AutoSize = true;
            this.#0te.BackColor = System.Drawing.Color.Transparent;
            this.#0te.Location = new Point(0x10, 0x10);
            this.#0te.Name = #G.#eg(0x1c8a);
            this.#0te.Size = new Size(0x9a, 13);
            this.#0te.TabIndex = 9;
            this.#0te.Text = #G.#eg(0x1ca7);
            this.#9te.AutoSize = true;
            this.#9te.BackColor = System.Drawing.Color.Transparent;
            this.#9te.Location = new Point(0x15d, 0x10);
            this.#9te.Name = #G.#eg(0x1cd4);
            this.#9te.Size = new Size(50, 13);
            this.#9te.TabIndex = 13;
            this.#9te.Text = #G.#eg(0x1cdd);
            this.#Rte.BackColor = SystemColors.Window;
            this.#Rte.Controls.Add(this.#Tte);
            this.#Rte.Controls.Add(this.#Ste);
            this.#Rte.Location = new Point(4, 0x16);
            this.#Rte.Name = #G.#eg(0x1cea);
            this.#Rte.Size = new Size(0x1e8, 0xee);
            this.#Rte.TabIndex = 3;
            this.#Rte.Text = #G.#eg(0x1cfb);
            this.#Tte.Controls.Add(this.#Wte);
            this.#Tte.Controls.Add(this.#Xte);
            this.#Tte.Controls.Add(this.#JCb);
            this.#Tte.Controls.Add(this.#Yte);
            this.#Tte.Controls.Add(this.#ICb);
            this.#Tte.Controls.Add(this.#2te);
            this.#Tte.Controls.Add(this.#Ute);
            this.#Tte.Controls.Add(this.#HCb);
            this.#Tte.Controls.Add(this.#GCb);
            this.#Tte.FlatStyle = FlatStyle.System;
            this.#Tte.Location = new Point(20, 0x22);
            this.#Tte.Name = #G.#eg(0x1d08);
            this.#Tte.Size = new Size(0x1c0, 0x7a);
            this.#Tte.TabIndex = 0x19;
            this.#Tte.TabStop = false;
            this.#Wte.FlatStyle = FlatStyle.System;
            this.#Wte.Location = new Point(0x58, 0x58);
            this.#Wte.Name = #G.#eg(0x1d15);
            this.#Wte.Size = new Size(0x88, 0x18);
            this.#Wte.TabIndex = 0x21;
            this.#Wte.Text = #G.#eg(0x1d42);
            this.#Wte.Click += new EventHandler(this.#rxe);
            this.#Xte.AutoSize = true;
            this.#Xte.BackColor = System.Drawing.Color.Transparent;
            this.#Xte.Location = new Point(240, 0x43);
            this.#Xte.Name = #G.#eg(0x1d5b);
            this.#Xte.Size = new Size(0x39, 13);
            this.#Xte.TabIndex = 0x27;
            this.#Xte.Text = #G.#eg(0x1d84);
            this.#Xte.TextAlign = ContentAlignment.TopRight;
            this.#JCb.Location = new Point(0x130, 0x40);
            this.#JCb.Name = #G.#eg(0x1d95);
            this.#JCb.ReadOnly = true;
            this.#JCb.Size = new Size(0x40, 20);
            this.#JCb.TabIndex = 0x20;
            this.#Yte.AutoSize = true;
            this.#Yte.BackColor = System.Drawing.Color.Transparent;
            this.#Yte.Location = new Point(0x2d, 0x43);
            this.#Yte.Name = #G.#eg(0x1db6);
            this.#Yte.Size = new Size(0x29, 13);
            this.#Yte.TabIndex = 0x25;
            this.#Yte.Text = #G.#eg(0x1ddb);
            this.#Yte.TextAlign = ContentAlignment.TopRight;
            this.#ICb.Location = new Point(0x58, 0x40);
            this.#ICb.Name = #G.#eg(0x1de4);
            this.#ICb.ReadOnly = true;
            this.#ICb.Size = new Size(0x88, 20);
            this.#ICb.TabIndex = 0x1f;
            this.#2te.AutoSize = true;
            this.#2te.BackColor = System.Drawing.Color.Transparent;
            this.#2te.Location = new Point(0x11, 0x2b);
            this.#2te.Name = #G.#eg(0x1e0d);
            this.#2te.Size = new Size(0x41, 13);
            this.#2te.TabIndex = 0x22;
            this.#2te.Text = #G.#eg(0x1e2e);
            this.#2te.TextAlign = ContentAlignment.TopRight;
            this.#Ute.AutoSize = true;
            this.#Ute.BackColor = System.Drawing.Color.Transparent;
            this.#Ute.Location = new Point(0x23, 0x13);
            this.#Ute.Name = #G.#eg(0x1e3f);
            this.#Ute.Size = new Size(50, 13);
            this.#Ute.TabIndex = 0x21;
            this.#Ute.Text = #G.#eg(0x1753);
            this.#Ute.TextAlign = ContentAlignment.TopRight;
            this.#HCb.Location = new Point(0x58, 40);
            this.#HCb.MaxLength = 5;
            this.#HCb.Name = #G.#eg(0x1e5c);
            this.#HCb.ReadOnly = true;
            this.#HCb.Size = new Size(0x158, 20);
            this.#HCb.TabIndex = 0x1a;
            this.#GCb.Location = new Point(0x58, 0x10);
            this.#GCb.Name = #G.#eg(0x1e75);
            this.#GCb.ReadOnly = true;
            this.#GCb.Size = new Size(0x158, 20);
            this.#GCb.TabIndex = 0x19;
            this.#Ste.AutoSize = true;
            this.#Ste.BackColor = System.Drawing.Color.Transparent;
            this.#Ste.Location = new Point(0x10, 0x10);
            this.#Ste.Name = #G.#eg(0x1e8a);
            this.#Ste.Size = new Size(0x161, 13);
            this.#Ste.TabIndex = 14;
            this.#Ste.Text = #G.#eg(0x1ea7);
            this.#Wce.DialogResult = DialogResult.Cancel;
            this.#Wce.FlatStyle = FlatStyle.System;
            this.#Wce.Location = new Point(0x194, 0x1b6);
            this.#Wce.Name = #G.#eg(0x1f10);
            this.#Wce.Size = new Size(100, 0x17);
            this.#Wce.TabIndex = 0;
            this.#Wce.Text = #G.#eg(0x1f21);
            this.#Qte.BackColor = System.Drawing.Color.Transparent;
            this.#Qte.Location = new Point(0xd3, 0x72);
            this.#Qte.Name = #G.#eg(0x1f2a);
            this.#Qte.Size = new Size(0x108, 0x10);
            this.#Qte.TabIndex = 70;
            this.#Qte.TabStop = true;
            this.#Qte.Text = #G.#eg(0x1f47);
            this.#Qte.TextAlign = ContentAlignment.TopRight;
            this.#Qte.LinkClicked += new LinkLabelLinkClickedEventHandler(this.#txe);
            this.#Vte.BackColor = System.Drawing.Color.Transparent;
            this.#Vte.ForeColor = System.Drawing.Color.Maroon;
            this.#Vte.Location = new Point(8, 0x86);
            this.#Vte.Name = #G.#eg(0x1f5c);
            this.#Vte.Size = new Size(0x1f0, 0x20);
            this.#Vte.TabIndex = 14;
            this.#Vte.Text = #G.#eg(0x1f79);
            this.#Vte.TextAlign = ContentAlignment.MiddleCenter;
            this.#3te.FlatStyle = FlatStyle.System;
            this.#3te.Location = new Point(8, 0x1b6);
            this.#3te.Name = #G.#eg(0x1f96);
            this.#3te.Size = new Size(100, 0x17);
            this.#3te.TabIndex = 90;
            this.#3te.Text = #G.#eg(0x1fab);
            this.#3te.Click += new EventHandler(this.#uxe);
            base.AcceptButton = this.#Pte;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.CancelButton = this.#Wce;
            base.ClientSize = new Size(0x200, 470);
            base.Controls.Add(this.#3te);
            base.Controls.Add(this.#Vte);
            base.Controls.Add(this.#Wce);
            base.Controls.Add(this.#Ote);
            base.Controls.Add(this.#DCb);
            base.Controls.Add(this.#Pte);
            base.Controls.Add(this.#Nte);
            base.Controls.Add(this.#Qte);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = #G.#eg(0x1fb8);
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = #G.#eg(0x1fc9);
            ((ISupportInitialize) this.#Nte).EndInit();
            this.#DCb.ResumeLayout(false);
            this.#Zte.ResumeLayout(false);
            this.#Zte.PerformLayout();
            ((ISupportInitialize) this.#7te).EndInit();
            ((ISupportInitialize) this.#4te).EndInit();
            this.#Rte.ResumeLayout(false);
            this.#Rte.PerformLayout();
            this.#Tte.ResumeLayout(false);
            this.#Tte.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void #C5d()
        {
            this.#6te.Text = this.#sBb.AssemblyInfo.Copyright;
            this.#JCb.Text = this.#sBb.ExceptionType.ToString();
            this.#GCb.Text = this.#sBb.Licensee;
            if (this.#sBb.LicenseKey.Length > 20)
            {
                this.#HCb.Text = ((this.#sBb.SourceLocation != ActiproLicenseSourceLocation.Registry) || (this.#sBb.LicenseType != AssemblyLicenseType.Full)) ? (this.#sBb.ExpandedLicenseKey.Substring(0, 0x11) + #G.#eg(0x1002)) : this.#sBb.ExpandedLicenseKey;
            }
            this.#Vte.Text = #G.#eg(0x20ce);
            this.#ICb.Text = this.#sBb.SourceLocation.ToString();
            this.Text = #G.#eg(0x214b);
            object[] args = new object[] { this.#sBb.AssemblyInfo.Version };
            this.#Ote.Text = string.Format(CultureInfo.CurrentCulture, #G.#eg(0x2170), args);
            this.#DCb.TabPages.Remove(this.#Rte);
            this.#DCb.SelectedIndex = 0;
            ActiproSoftware.Products.Shared.AssemblyInfo instance = ActiproSoftware.Products.Shared.AssemblyInfo.Instance;
            ListViewItem item1 = new ListViewItem(#G.#eg(0x2181), 0);
            item1.Tag = #OAb.#lCb;
            item1.ToolTipText = #G.#eg(0x21a2);
            this.#ECb.Items.Add(item1);
            ListViewItem item3 = new ListViewItem(#G.#eg(0x221b), 0);
            item3.Tag = #OAb.#bRf;
            item3.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item3);
            ListViewItem item4 = new ListViewItem(#G.#eg(0x2259), 0);
            item4.Tag = #OAb.#eCb;
            item4.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item4);
            ListViewItem item5 = new ListViewItem(#G.#eg(0x226e), 0);
            item5.Tag = #OAb.#dCb;
            item5.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item5);
            ListViewItem item6 = new ListViewItem(#G.#eg(0x227f), 0);
            item6.Tag = #OAb.#bCb;
            item6.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item6);
            ListViewItem item7 = new ListViewItem(#G.#eg(0x2290), 0);
            item7.Tag = #OAb.#aCb;
            item7.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item7);
            ListViewItem item8 = new ListViewItem(#G.#eg(0x2299), 0);
            item8.Tag = #OAb.#6Bb;
            item8.ToolTipText = #G.#eg(0x2224);
            this.#ECb.Items.Add(item8);
            ListViewItem item9 = new ListViewItem(#G.#eg(0x22ae), 0);
            item9.Tag = #OAb.#mCb;
            item9.ToolTipText = #G.#eg(0x22df);
            this.#ECb.Items.Add(item9);
            ListViewItem item10 = new ListViewItem(#G.#eg(0x2340), 0);
            item10.Tag = #OAb.#aui;
            item10.ToolTipText = #G.#eg(0x22df);
            this.#ECb.Items.Add(item10);
            ListViewItem item11 = new ListViewItem(#G.#eg(0x2371), 0);
            item11.Tag = #OAb.#nCb;
            item11.ToolTipText = #G.#eg(0x22df);
            this.#ECb.Items.Add(item11);
            AssemblyLicenseType licenseType = this.#sBb.LicenseType;
            if ((licenseType != AssemblyLicenseType.Invalid) && (licenseType != AssemblyLicenseType.Evaluation))
            {
                foreach (ListViewItem item in this.#ECb.Items)
                {
                    if ((((#OAb) this.#sBb.ProductIDs) & ((#OAb) item.Tag)) == ((#OAb) item.Tag))
                    {
                        item.ImageIndex = 1;
                    }
                }
            }
            foreach (ListViewItem item2 in this.#ECb.Items)
            {
                if (((int) item2.Tag) == this.#sBb.AssemblyInfo.ProductId)
                {
                    item2.Font = new Font(item2.Font, FontStyle.Bold);
                    item2.Selected = true;
                    break;
                }
            }
            this.#tCb = new Timer();
            this.#tCb.Interval = 400;
            this.#tCb.Tick += new EventHandler(this.#qxe);
            string str = null;
            licenseType = this.#sBb.AssemblyInfo.LicenseType;
            if (licenseType == AssemblyLicenseType.Beta)
            {
                str = #G.#eg(0x239e);
                this.#tCb.Start();
            }
            else if (licenseType == AssemblyLicenseType.Prerelease)
            {
                str = #G.#eg(0x23af);
                this.#tCb.Start();
            }
            else
            {
                if (this.#sBb.LicenseType == AssemblyLicenseType.Full)
                {
                    str = #G.#eg(0x23c0);
                }
                else
                {
                    try
                    {
                        string name = #G.#eg(0x23d1) + this.#sBb.AssemblyInfo.Version.Substring(0, 4);
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(name);
                        if (key == null)
                        {
                            name = #G.#eg(0x240e) + this.#sBb.AssemblyInfo.Version.Substring(0, 4);
                            key = Registry.LocalMachine.OpenSubKey(name);
                        }
                        if (key != null)
                        {
                            str = key.GetValue(#G.#eg(0x245b)) as string;
                            key.Close();
                        }
                    }
                    catch (ArgumentNullException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (IOException)
                    {
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    catch (SecurityException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
                if ((str == null) || (string.Compare(str.Trim(), #G.#eg(0x246c), StringComparison.OrdinalIgnoreCase) != 0))
                {
                    str = #G.#eg(0x247d);
                    this.#tCb.Start();
                }
            }
            this.#Qte.Text = str;
            this.#Vte.Text = this.#sBb.GetQuickInfo();
        }

        private void #pxe(object #xhb, EventArgs #yhb)
        {
            this.#zVb();
        }

        private void #qxe(object #xhb, EventArgs #yhb)
        {
            if (this.#Qte.LinkColor == System.Drawing.Color.Blue)
            {
                this.#Qte.LinkColor = System.Drawing.Color.Red;
            }
            else
            {
                this.#Qte.LinkColor = System.Drawing.Color.Blue;
            }
        }

        private void #rxe(object #xhb, EventArgs #yhb)
        {
            string text1;
            string str = #G.#eg(0x1fde);
            try
            {
                str = Environment.OSVersion.ToString();
            }
            catch (InvalidOperationException)
            {
            }
            string[] textArray1 = new string[0x25];
            textArray1[0] = #G.#eg(0xfb0);
            textArray1[1] = this.#sBb.AssemblyInfo.Product;
            textArray1[2] = #G.#eg(0xc0e);
            textArray1[3] = this.#sBb.AssemblyInfo.Version;
            textArray1[4] = #G.#eg(0xfbd);
            textArray1[5] = this.#sBb.AssemblyInfo.LicenseType.ToString();
            textArray1[6] = #G.#eg(0xfc2);
            textArray1[7] = Environment.NewLine;
            textArray1[8] = Environment.NewLine;
            textArray1[9] = #G.#eg(0xfc7);
            textArray1[10] = this.#sBb.LicenseType.ToString();
            textArray1[11] = Environment.NewLine;
            textArray1[12] = #G.#eg(0xfdc);
            textArray1[13] = this.#GCb.Text;
            textArray1[14] = Environment.NewLine;
            textArray1[15] = #G.#eg(0xfed);
            textArray1[0x10] = this.#HCb.Text;
            textArray1[0x11] = Environment.NewLine;
            textArray1[0x12] = #G.#eg(0x1fef);
            textArray1[0x13] = this.#ICb.Text;
            textArray1[20] = Environment.NewLine;
            textArray1[0x15] = #G.#eg(0x1027);
            textArray1[0x16] = this.#sBb.Platform.ToString();
            textArray1[0x17] = Environment.NewLine;
            textArray1[0x18] = #G.#eg(0x1038);
            textArray1[0x19] = this.#sBb.OrganizationID.ToString();
            textArray1[0x1a] = Environment.NewLine;
            textArray1[0x1b] = #G.#eg(0x2008);
            textArray1[0x1c] = str;
            textArray1[0x1d] = #G.#eg(0x201d);
            Version version = Environment.Version;
            string[] textArray2 = textArray1;
            if (version != null)
            {
                text1 = version.ToString();
            }
            else
            {
                Version local2 = version;
                text1 = null;
            }
            textArray2[30] = text1;
            string[] local3 = textArray2;
            local3[0x1f] = Environment.NewLine;
            local3[0x20] = #G.#eg(0x1041);
            local3[0x21] = this.#sBb.ExceptionType.ToString();
            local3[0x22] = Environment.NewLine;
            local3[0x23] = Environment.NewLine;
            local3[0x24] = this.#Vte.Text;
            Clipboard.SetDataObject(string.Concat(local3), true);
            MessageBox.Show(#G.#eg(0x202a));
        }

        private void #sxe(object #xhb, PaintEventArgs #yhb)
        {
            string str;
            Font font;
            if (4 == 0)
            {
                string local1 = #G.#eg(0x206b);
            }
            else
            {
                str = #G.#eg(0x206b);
            }
            string s = #G.#eg(0x2084);
            LinearGradientBrush brush = new LinearGradientBrush(this.#Nte.ClientRectangle, System.Drawing.Color.FromArgb(0xc0, 0xe0, 0xf2), System.Drawing.Color.FromKnownColor(KnownColor.CornflowerBlue), 45f);
            brush.SetSigmaBellShape(0.5f, 1f);
            #yhb.Graphics.FillRectangle(brush, this.#Nte.ClientRectangle);
            brush.Dispose();
            try
            {
                font = new Font(#G.#eg(0x20c1), 32f, FontStyle.Italic);
            }
            catch
            {
                font = new Font(this.Font.FontFamily, 32f, FontStyle.Italic);
            }
            StringFormat format = new StringFormat(StringFormat.GenericTypographic) {
                Alignment = StringAlignment.Center
            };
            Rectangle clientRectangle = this.#Nte.ClientRectangle;
            clientRectangle.Inflate(-5, -5);
            clientRectangle.Offset(2, 5);
            #yhb.Graphics.DrawString(str, font, new SolidBrush(System.Drawing.Color.FromArgb(0x80, System.Drawing.Color.Gray)), clientRectangle, format);
            clientRectangle.Offset(-2, -2);
            #yhb.Graphics.DrawString(str, font, new SolidBrush(System.Drawing.Color.Black), clientRectangle, format);
            clientRectangle.Offset(1, font.Height + 1);
            #yhb.Graphics.DrawString(s, this.Font, new SolidBrush(System.Drawing.Color.FromArgb(0x80, System.Drawing.Color.Gray)), clientRectangle, format);
            clientRectangle.Offset(-1, -1);
            #yhb.Graphics.DrawString(s, this.Font, new SolidBrush(System.Drawing.Color.Black), clientRectangle, format);
        }

        private void #txe(object #xhb, LinkLabelLinkClickedEventArgs #yhb)
        {
            if (Control.ModifierKeys != (Keys.Control | Keys.Shift))
            {
                this.#AVb();
            }
            else
            {
                if (!this.#DCb.TabPages.Contains(this.#Rte))
                {
                    this.#DCb.TabPages.Add(this.#Rte);
                }
                this.#DCb.SelectedTab = this.#Rte;
            }
        }

        private void #uxe(object #xhb, EventArgs #yhb)
        {
            this.#zVb();
        }

        private void #vxe(object #xhb, EventArgs #yhb)
        {
            this.#AVb();
        }

        private void #zVb()
        {
            Process local1 = Process.Start(this.#oVd);
        }

        public #Rqe(ActiproLicense license)
        {
            this.#B5d();
            this.#sBb = license;
            this.#C5d();
        }

        protected override void Dispose(bool #Fee)
        {
            if (#Fee && (this.#ZXd != null))
            {
                this.#ZXd.Dispose();
            }
            base.Dispose(#Fee);
        }
    }
}

