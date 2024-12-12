namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Images.Service;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormSurvey : FormAutoIncrementMaintain
    {
        private IContainer components;
        private Lazy<System.Windows.Forms.OpenFileDialog> OpenFileDialog = new Lazy<System.Windows.Forms.OpenFileDialog>((_Closure$__.$I39-0 == null) ? (_Closure$__.$I39-0 = new Func<System.Windows.Forms.OpenFileDialog>(_Closure$__.$I._Lambda$__39-0)) : _Closure$__.$I39-0, false);

        public FormSurvey()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_survey" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtDescription);
            base.ChangesTracker.Subscribe(this.txtTemplate);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dialog = this.OpenFileDialog.Value;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(dialog.FileName))
                    {
                        this.txtTemplate.Text = reader.ReadToEnd();
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

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtDescription, DBNull.Value);
            Functions.SetTextBoxText(this.txtTemplate, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_location"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
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
            messages.ConfirmDeleting = $"Are you really want to delete location '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Location '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.lblName = new Label();
            this.txtTemplate = new TextBox();
            this.btnOpenFile = new Button();
            this.txtDescription = new TextBox();
            this.lblDescription = new Label();
            this.lblTemplate = new Label();
            this.btnPreview = new Button();
            this.mnuActionsPreview = new MenuItem();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.btnPreview);
            base.tpWorkArea.Controls.Add(this.btnOpenFile);
            base.tpWorkArea.Controls.Add(this.txtTemplate);
            base.tpWorkArea.Controls.Add(this.lblTemplate);
            base.tpWorkArea.Controls.Add(this.txtDescription);
            base.tpWorkArea.Controls.Add(this.lblDescription);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Size = new Size(0x240, 0x18e);
            base.tpWorkArea.Visible = true;
            MenuItem[] items = new MenuItem[] { this.mnuActionsPreview };
            base.cmnuActions.MenuItems.AddRange(items);
            this.txtName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtName.Location = new Point(0x58, 8);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(480, 20);
            this.txtName.TabIndex = 1;
            this.lblName.Location = new Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x48, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.txtTemplate.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtTemplate.Location = new Point(0x58, 0x38);
            this.txtTemplate.MaxLength = 50;
            this.txtTemplate.Multiline = true;
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.ScrollBars = ScrollBars.Both;
            this.txtTemplate.Size = new Size(480, 0x150);
            this.txtTemplate.TabIndex = 5;
            this.btnOpenFile.FlatStyle = FlatStyle.Flat;
            this.btnOpenFile.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnOpenFile.Location = new Point(8, 0x51);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new Size(0x48, 0x17);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.TabStop = false;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.txtDescription.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtDescription.Location = new Point(0x58, 0x20);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(480, 20);
            this.txtDescription.TabIndex = 3;
            this.lblDescription.Location = new Point(8, 0x20);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x48, 0x16);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = ContentAlignment.MiddleRight;
            this.lblTemplate.Location = new Point(4, 0x36);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new Size(0x4c, 0x16);
            this.lblTemplate.TabIndex = 4;
            this.lblTemplate.Text = "Template";
            this.lblTemplate.TextAlign = ContentAlignment.MiddleRight;
            this.btnPreview.FlatStyle = FlatStyle.Flat;
            this.btnPreview.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnPreview.Location = new Point(8, 0x70);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new Size(0x48, 0x17);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.TabStop = false;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.mnuActionsPreview.Index = 0;
            this.mnuActionsPreview.Text = "Preview";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x248, 0x1d9);
            base.Name = "FormSurvey";
            this.Text = "Maintain Survey";
            base.tpWorkArea.ResumeLayout(false);
            base.tpWorkArea.PerformLayout();
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_survey WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtDescription, reader["Description"]);
                            Functions.SetTextBoxText(this.txtTemplate, reader["Template"]);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void mnuActionsPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string text = this.txtTemplate.Text;
                text = Templater.GetPreview(Globals.ConnectionString, text);
                ClassGlobalObjects.ShowForm(FormFactories.FormBrowser(null, text));
            }
            catch (TemplateException exception1)
            {
                TemplateException ex = exception1;
                ProjectData.SetProjectError(ex);
                MessageBox.Show(ex.Message, "Template Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                ProjectData.ClearProjectError();
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

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_survey" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Description", MySqlType.VarChar, 200).Value = this.txtDescription.Text;
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("Template", MySqlType.Text, 0x100000).Value = this.txtTemplate.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (IsNew)
                    {
                        flag = 0 < command.ExecuteInsert("tbl_survey");
                        if (flag)
                        {
                            this.ObjectID = command.GetLastIdentity();
                        }
                    }
                    else
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_survey", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_survey"));
                    }
                }
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT\r\n  ID\r\n, Name\r\n, Description\r\nFROM tbl_survey\r\nORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 50);
            appearance.AddTextColumn("Name", "Name", 150);
            appearance.AddTextColumn("Description", "Description", 300);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__43-0 e$__- = new _Closure$__43-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                base.ValidationErrors.SetError(this.txtName, "Name cannot be empty");
            }
            try
            {
                Templater.Validate(this.txtTemplate.Text);
            }
            catch (TemplateException exception1)
            {
                TemplateException ex = exception1;
                ProjectData.SetProjectError(ex);
                TemplateException exception = ex;
                base.ValidationErrors.SetError(this.txtTemplate, exception.Message);
                ProjectData.ClearProjectError();
            }
            catch (Exception exception3)
            {
                Exception ex = exception3;
                ProjectData.SetProjectError(ex);
                base.ValidationErrors.SetError(this.txtTemplate, ex.ToString());
                base.ValidationErrors.SetIconAlignment(this.txtTemplate, ErrorIconAlignment.TopRight);
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtTemplate")]
        private TextBox txtTemplate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtDescription")]
        private TextBox txtDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblDescription")]
        private Label lblDescription { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnOpenFile")]
        private Button btnOpenFile { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("btnPreview")]
        private Button btnPreview { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblTemplate")]
        private Label lblTemplate { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("mnuActionsPreview")]
        private MenuItem mnuActionsPreview { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        protected override bool IsNew
        {
            get => 
                base.IsNew;
            set
            {
                base.IsNew = value;
                this.txtTemplate.ReadOnly = !value;
            }
        }

        [Serializable, CompilerGenerated]
        internal sealed class _Closure$__
        {
            public static readonly FormSurvey._Closure$__ $I = new FormSurvey._Closure$__();
            public static Func<OpenFileDialog> $I39-0;

            internal OpenFileDialog _Lambda$__39-0()
            {
                OpenFileDialog dialog1 = new OpenFileDialog();
                dialog1.Filter = "Html Files (*.htm, *.html)|*.htm;*.html|Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog1.FilterIndex = 1;
                return dialog1;
            }
        }

        [CompilerGenerated]
        internal sealed class _Closure$__43-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

