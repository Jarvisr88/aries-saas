namespace DMEWorks.Maintain
{
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Forms;
    using DMEWorks.Reports;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormCrystalReport : FormStringMaintain
    {
        private IContainer components;

        public FormCrystalReport()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_crystalreport" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.cmbCategory);
        }

        private static void Appearance_CellFormatting(object sender, GridCellFormattingEventArgs e)
        {
            try
            {
                if (e.Row.Get<Report>().IsCustomReport)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetComboBoxText(this.cmbCategory, DBNull.Value);
        }

        protected override void DeleteObject(string filename)
        {
            string[] filenames = new string[] { filename };
            Cache.CrystalReports.DeleteByFilename(filenames);
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete crystal report '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Crystal report '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        protected override void InitDropdowns()
        {
            this.Load_Table_CrystalReport();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.lblCategory = new Label();
            this.lblComments = new Label();
            this.cmbCategory = new ComboBox();
            this.mnuGotoReportSelectorForm = new MenuItem();
            this.mnuActionsExport = new MenuItem();
            this.lblName = new Label();
            this.txtName = new TextBox();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            base.SuspendLayout();
            this.txtObjectID.Location = new Point(0x58, 8);
            this.txtObjectID.Size = new Size(240, 0x16);
            this.lblObjectTypeName.Size = new Size(0x48, 0x18);
            this.lblObjectTypeName.Text = "File name:";
            base.tpWorkArea.Controls.Add(this.cmbCategory);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblComments);
            base.tpWorkArea.Controls.Add(this.lblCategory);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Size = new Size(0x198, 0x10a);
            base.tpWorkArea.Controls.SetChildIndex(this.lblName, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblCategory, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblComments, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtName, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.cmbCategory, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.txtObjectID, 0);
            base.tpWorkArea.Controls.SetChildIndex(this.lblObjectTypeName, 0);
            MenuItem[] items = new MenuItem[] { this.mnuActionsExport };
            base.cmnuActions.MenuItems.AddRange(items);
            this.lblCategory.Location = new Point(0x10, 0x20);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new Size(0x40, 0x15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            this.lblCategory.TextAlign = ContentAlignment.MiddleRight;
            this.lblComments.Location = new Point(8, 0x58);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new Size(0x138, 0x38);
            this.lblComments.TabIndex = 6;
            this.lblComments.Text = "All reports supposed to be placed in reports folder near the DMEWorks.exe application. So file name is simply name of the report file without extension.";
            this.cmbCategory.Location = new Point(0x58, 0x20);
            this.cmbCategory.MaxLength = 50;
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new Size(240, 0x15);
            this.cmbCategory.TabIndex = 3;
            MenuItem[] itemArray2 = new MenuItem[] { this.mnuGotoReportSelectorForm };
            base.cmnuGoto.MenuItems.AddRange(itemArray2);
            this.mnuGotoReportSelectorForm.Index = 0;
            this.mnuGotoReportSelectorForm.Text = "Selector Form";
            this.mnuActionsExport.Index = 0;
            this.mnuActionsExport.Text = "Export";
            this.lblName.Location = new Point(0x10, 0x38);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x40, 0x15);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.txtName.Location = new Point(0x58, 0x38);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 20);
            this.txtName.TabIndex = 5;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x1a0, 0x14d);
            base.Name = "FormCrystalReport";
            this.Text = "Maintain crystal reports";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void InitPrintMenu()
        {
            ContextMenu menu = new ContextMenu {
                MenuItems = { new MenuItem("Report", new EventHandler(this.PrintReport)) }
            };
            base.SetPrintMenu(menu);
        }

        [HandleDatabaseChanged("tbl_crystalreport")]
        private void Load_Table_CrystalReport()
        {
            string[] items = Cache.CrystalReports.Select().Select<DMEWorks.Reports.Report, string>(((_Closure$__.$I47-0 == null) ? (_Closure$__.$I47-0 = new Func<DMEWorks.Reports.Report, string>(_Closure$__.$I._Lambda$__47-0)) : _Closure$__.$I47-0)).Union<string>(PredefinedReportCategories.AllCategories()).Distinct<string>(SqlStringComparer.Default).OrderBy<string, string>(((_Closure$__.$I47-1 == null) ? (_Closure$__.$I47-1 = new Func<string, string>(_Closure$__.$I._Lambda$__47-1)) : _Closure$__.$I47-1), SqlStringComparer.Default).ToArray<string>();
            this.cmbCategory.BeginUpdate();
            try
            {
                this.cmbCategory.Items.Clear();
                this.cmbCategory.Items.AddRange(items);
            }
            finally
            {
                this.cmbCategory.EndUpdate();
            }
        }

        protected override bool LoadObject(string filename)
        {
            bool flag;
            _Closure$__40-0 e$__- = new _Closure$__40-0 {
                $VB$Local_filename = filename
            };
            DMEWorks.Reports.Report report = Cache.CrystalReports.Select().FirstOrDefault<DMEWorks.Reports.Report>(new Func<DMEWorks.Reports.Report, bool>(e$__-._Lambda$__0));
            if (report == null)
            {
                flag = false;
            }
            else
            {
                this.ObjectID = report.FileName;
                Functions.SetTextBoxText(this.txtName, report.Name);
                Functions.SetComboBoxText(this.cmbCategory, report.Category);
                flag = true;
            }
            return flag;
        }

        private void mnuGoReportSelectorForm_Click(object sender, EventArgs e)
        {
            ClassGlobalObjects.ShowForm(FormFactories.FormReportSelector());
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_crystalreport" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        private void PrintReport(object sender, EventArgs e)
        {
            try
            {
                string objectID = this.ObjectID as string;
                if (!string.IsNullOrWhiteSpace(objectID))
                {
                    ClassGlobalObjects.ShowReport(objectID);
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

        protected override bool SaveObject(string filename, bool IsNew)
        {
            DMEWorks.Reports.Report report1 = new DMEWorks.Reports.Report();
            report1.Category = this.cmbCategory.Text;
            report1.FileName = filename;
            report1.IsSystem = false;
            report1.Name = this.txtName.Text;
            DMEWorks.Reports.Report report = report1;
            DMEWorks.Reports.Report[] reports = new DMEWorks.Reports.Report[] { report };
            Cache.CrystalReports.ReplaceWith(reports);
            return true;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new List<Report>().ToGridSource<Report>(new string[0]);
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            Report[] collection = Cache.CrystalReports.Select().Select<DMEWorks.Reports.Report, Report>(((_Closure$__.$I34-0 == null) ? (_Closure$__.$I34-0 = new Func<DMEWorks.Reports.Report, Report>(_Closure$__.$I._Lambda$__34-0)) : _Closure$__.$I34-0)).ToArray<Report>();
            HashSet<string> set = new HashSet<string>(SqlStringComparer.Default);
            try
            {
                foreach (string str in Directory.GetFiles(ClassGlobalObjects.CustomReportsFolder, "*.rpt"))
                {
                    set.Add(Path.GetFileNameWithoutExtension(str));
                }
            }
            catch (UnauthorizedAccessException exception1)
            {
                UnauthorizedAccessException ex = exception1;
                ProjectData.SetProjectError(ex);
                UnauthorizedAccessException exception = ex;
                ProjectData.ClearProjectError();
            }
            catch (DirectoryNotFoundException exception3)
            {
                DirectoryNotFoundException ex = exception3;
                ProjectData.SetProjectError(ex);
                DirectoryNotFoundException exception2 = ex;
                ProjectData.ClearProjectError();
            }
            foreach (Report report in collection)
            {
                report.IsCustomReport = set.Contains(report.FileName);
            }
            ((ArrayGridSource<Report>) args.Source).AppendRange(collection);
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("FileName", "FileName", 80);
            appearance.AddTextColumn("Category", "Category", 80);
            appearance.AddTextColumn("Name", "Name", 180);
            appearance.AddBoolColumn("IsSystem", "System", 60).ThreeState = false;
            appearance.CellFormatting += new EventHandler<GridCellFormattingEventArgs>(FormCrystalReport.Appearance_CellFormatting);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__36-0 e$__- = new _Closure$__36-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(string filename, bool IsNew)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                base.ValidationErrors.SetError(this.txtObjectID, "File name is empty.");
            }
            else if (string.IsNullOrEmpty(ClassGlobalObjects.FindReport(filename)))
            {
                base.ValidationErrors.SetError(this.txtObjectID, "File does not exists.");
            }
        }

        [field: AccessedThroughProperty("lblCategory")]
        private Label lblCategory { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblComments")]
        private Label lblComments { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmbCategory")]
        private ComboBox cmbCategory { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuGotoReportSelectorForm")]
        private MenuItem mnuGotoReportSelectorForm { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsExport")]
        private MenuItem mnuActionsExport { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormCrystalReport._Closure$__ $I = new FormCrystalReport._Closure$__();
            public static Func<DMEWorks.Reports.Report, FormCrystalReport.Report> $I34-0;
            public static Func<DMEWorks.Reports.Report, string> $I47-0;
            public static Func<string, string> $I47-1;

            internal FormCrystalReport.Report _Lambda$__34-0(DMEWorks.Reports.Report r)
            {
                FormCrystalReport.Report report1 = new FormCrystalReport.Report();
                report1.Category = r.Category;
                report1.FileName = r.FileName;
                report1.IsSystem = r.IsSystem;
                report1.Name = r.Name;
                return report1;
            }

            internal string _Lambda$__47-0(DMEWorks.Reports.Report r) => 
                r.Category ?? "";

            internal string _Lambda$__47-1(string r) => 
                r;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__36-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.Get<FormCrystalReport.Report>().FileName;
        }

        [CompilerGenerated]
        internal sealed class _Closure$__40-0
        {
            public string $VB$Local_filename;

            internal bool _Lambda$__0(DMEWorks.Reports.Report r) => 
                SqlString.Equals(r.FileName, this.$VB$Local_filename);
        }

        private class Report : DMEWorks.Reports.Report
        {
            public bool IsCustomReport { get; set; }
        }
    }
}

