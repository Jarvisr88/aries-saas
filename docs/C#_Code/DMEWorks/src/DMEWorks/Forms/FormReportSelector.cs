namespace DMEWorks.Forms
{
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Reports;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormReportSelector : DmeForm
    {
        private IContainer components;

        public FormReportSelector()
        {
            base.Resize += new EventHandler(this.FormReportSelector_Resize);
            this.InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if ((this.tvReports.SelectedNode != null) && ((this.tvReports.SelectedNode.Parent != null) && (this.tvReports.SelectedNode.Tag is string)))
            {
                ClassGlobalObjects.ShowReport(Conversions.ToString(this.tvReports.SelectedNode.Tag));
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

        private void FormReportSelector_Resize(object sender, EventArgs e)
        {
            this.btnGenerate.Left = (this.btnGenerate.Parent.Width - this.btnGenerate.Left) / 2;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            TreeNode node = new TreeNode("Node1", 0, 0);
            TreeNode[] children = new TreeNode[] { node };
            TreeNode node2 = new TreeNode("Node0", 1, 2, children);
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormReportSelector));
            this.tvReports = new TreeView();
            this.ImageList1 = new ImageList(this.components);
            this.Panel1 = new Panel();
            this.btnGenerate = new Button();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.tvReports.Dock = DockStyle.Fill;
            this.tvReports.ImageIndex = 0;
            this.tvReports.ImageList = this.ImageList1;
            this.tvReports.Location = new Point(0, 0);
            this.tvReports.Name = "tvReports";
            node.ImageIndex = 0;
            node.Name = "";
            node.SelectedImageIndex = 0;
            node.Text = "Node1";
            node2.ImageIndex = 1;
            node2.Name = "";
            node2.SelectedImageIndex = 2;
            node2.Text = "Node0";
            TreeNode[] nodes = new TreeNode[] { node2 };
            this.tvReports.Nodes.AddRange(nodes);
            this.tvReports.SelectedImageIndex = 0;
            this.tvReports.Size = new Size(0x124, 0x11d);
            this.tvReports.TabIndex = 0;
            this.ImageList1.ImageStream = (ImageListStreamer) manager.GetObject("ImageList1.ImageStream");
            this.ImageList1.TransparentColor = Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.Panel1.Controls.Add(this.btnGenerate);
            this.Panel1.Dock = DockStyle.Bottom;
            this.Panel1.Location = new Point(0, 0x11d);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x124, 40);
            this.Panel1.TabIndex = 1;
            this.btnGenerate.Location = new Point(8, 8);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new Size(0x40, 0x18);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x124, 0x145);
            base.Controls.Add(this.tvReports);
            base.Controls.Add(this.Panel1);
            base.Name = "FormReportSelector";
            this.Text = "Report Selector";
            this.Panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        [HandleDatabaseChanged("tbl_crystalreport")]
        private void Load_Table_CrystalReport()
        {
            this.tvReports.BeginUpdate();
            try
            {
                IEnumerator<IGrouping<string, Report>> enumerator;
                this.tvReports.Nodes.Clear();
                ILookup<string, Report> source = Cache.CrystalReports.Select().ToLookup<Report, string>((_Closure$__.$I21-0 == null) ? (_Closure$__.$I21-0 = new Func<Report, string>(_Closure$__.$I._Lambda$__21-0)) : _Closure$__.$I21-0, SqlStringComparer.Default);
                try
                {
                    enumerator = source.OrderBy<IGrouping<string, Report>, string>(((_Closure$__.$I21-1 == null) ? (_Closure$__.$I21-1 = new Func<IGrouping<string, Report>, string>(_Closure$__.$I._Lambda$__21-1)) : _Closure$__.$I21-1), SqlStringComparer.Default).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        IEnumerator<Report> enumerator2;
                        IGrouping<string, Report> current = enumerator.Current;
                        TreeNode node = this.tvReports.Nodes.Add(current.Key);
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                        try
                        {
                            enumerator2 = current.OrderBy<Report, string>(((_Closure$__.$I21-2 == null) ? (_Closure$__.$I21-2 = new Func<Report, string>(_Closure$__.$I._Lambda$__21-2)) : _Closure$__.$I21-2), SqlStringComparer.Default).GetEnumerator();
                            while (enumerator2.MoveNext())
                            {
                                Report report = enumerator2.Current;
                                string text = !string.IsNullOrWhiteSpace(report.Name) ? report.Name : report.FileName;
                                TreeNode node1 = node.Nodes.Add(text);
                                node1.Tag = report.FileName;
                                node1.ImageIndex = 0;
                                node1.SelectedImageIndex = 0;
                            }
                        }
                        finally
                        {
                            if (enumerator2 != null)
                            {
                                enumerator2.Dispose();
                            }
                        }
                    }
                }
                finally
                {
                    if (enumerator != null)
                    {
                        enumerator.Dispose();
                    }
                }
            }
            finally
            {
                this.tvReports.EndUpdate();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SafeInvoke(new Action(this.Load_Table_CrystalReport));
        }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnGenerate")]
        private Button btnGenerate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tvReports")]
        private TreeView tvReports { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ImageList1")]
        private ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormReportSelector._Closure$__ $I = new FormReportSelector._Closure$__();
            public static Func<Report, string> $I21-0;
            public static Func<IGrouping<string, Report>, string> $I21-1;
            public static Func<Report, string> $I21-2;

            internal string _Lambda$__21-0(Report r) => 
                r.Category;

            internal string _Lambda$__21-1(IGrouping<string, Report> g) => 
                g.Key;

            internal string _Lambda$__21-2(Report r) => 
                r.Name;
        }
    }
}

