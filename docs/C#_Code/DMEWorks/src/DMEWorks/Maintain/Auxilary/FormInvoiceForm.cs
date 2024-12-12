namespace DMEWorks.Maintain.Auxilary
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormInvoiceForm : FormAutoIncrementMaintain
    {
        private IContainer components;
        private const string TableName = "tbl_invoiceform";

        public FormInvoiceForm()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_invoiceform" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.nudMarginBottom);
            base.ChangesTracker.Subscribe(this.nudMarginRight);
            base.ChangesTracker.Subscribe(this.nudMarginTop);
            base.ChangesTracker.Subscribe(this.nudMarginLeft);
            base.ChangesTracker.Subscribe(this.txtName);
            base.ChangesTracker.Subscribe(this.txtSpecialCoding);
            base.ChangesTracker.Subscribe(this.txtReportFileName);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtName, DBNull.Value);
            Functions.SetTextBoxText(this.txtReportFileName, DBNull.Value);
            Functions.SetTextBoxText(this.txtSpecialCoding, DBNull.Value);
            Functions.SetUpDownValue(this.nudMarginTop, 0.25);
            Functions.SetUpDownValue(this.nudMarginBottom, 0.25);
            Functions.SetUpDownValue(this.nudMarginLeft, 0.25);
            Functions.SetUpDownValue(this.nudMarginRight, 0.25);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command.ExecuteDelete("tbl_invoiceform"))
                    {
                        throw new ObjectIsNotFoundException();
                    }
                }
            }
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

        protected override FormMaintainBase.StandardMessages GetMessages()
        {
            FormMaintainBase.StandardMessages messages = base.GetMessages();
            messages.ConfirmDeleting = $"Are you really want to delete invoice form '{this.txtName.Text}'?";
            messages.DeletedSuccessfully = $"Invoice form '{this.txtName.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.txtReportFileName = new TextBox();
            this.lblReportFileName = new Label();
            this.txtName = new TextBox();
            this.lblName = new Label();
            this.gbMargin = new GroupBox();
            this.nudMarginRight = new NumericUpDown();
            this.nudMarginLeft = new NumericUpDown();
            this.nudMarginBottom = new NumericUpDown();
            this.nudMarginTop = new NumericUpDown();
            this.lblMarginBottom = new Label();
            this.lblMarginLeft = new Label();
            this.lblMarginTop = new Label();
            this.lblMarginRight = new Label();
            this.lblSpecialCoding = new Label();
            this.txtSpecialCoding = new TextBox();
            base.tpWorkArea.SuspendLayout();
            this.gbMargin.SuspendLayout();
            this.nudMarginRight.BeginInit();
            this.nudMarginLeft.BeginInit();
            this.nudMarginBottom.BeginInit();
            this.nudMarginTop.BeginInit();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.txtSpecialCoding);
            base.tpWorkArea.Controls.Add(this.lblSpecialCoding);
            base.tpWorkArea.Controls.Add(this.gbMargin);
            base.tpWorkArea.Controls.Add(this.txtName);
            base.tpWorkArea.Controls.Add(this.lblName);
            base.tpWorkArea.Controls.Add(this.txtReportFileName);
            base.tpWorkArea.Controls.Add(this.lblReportFileName);
            base.tpWorkArea.Name = "tpWorkArea";
            base.tpWorkArea.Size = new Size(0x198, 290);
            base.tpWorkArea.Visible = true;
            this.txtReportFileName.AcceptsReturn = true;
            this.txtReportFileName.AutoSize = false;
            this.txtReportFileName.BackColor = SystemColors.Window;
            this.txtReportFileName.Cursor = Cursors.IBeam;
            this.txtReportFileName.ForeColor = SystemColors.WindowText;
            this.txtReportFileName.Location = new Point(0x68, 0x30);
            this.txtReportFileName.MaxLength = 50;
            this.txtReportFileName.Name = "txtReportFileName";
            this.txtReportFileName.Size = new Size(0x128, 0x16);
            this.txtReportFileName.TabIndex = 3;
            this.txtReportFileName.Text = "";
            base.ToolTip1.SetToolTip(this.txtReportFileName, "File name without extension");
            this.lblReportFileName.BackColor = Color.Transparent;
            this.lblReportFileName.BorderStyle = BorderStyle.FixedSingle;
            this.lblReportFileName.Cursor = Cursors.Default;
            this.lblReportFileName.ForeColor = SystemColors.ControlText;
            this.lblReportFileName.Location = new Point(8, 0x30);
            this.lblReportFileName.Name = "lblReportFileName";
            this.lblReportFileName.RightToLeft = RightToLeft.No;
            this.lblReportFileName.Size = new Size(0x58, 0x16);
            this.lblReportFileName.TabIndex = 2;
            this.lblReportFileName.Text = "CR filename";
            this.lblReportFileName.TextAlign = ContentAlignment.MiddleRight;
            base.ToolTip1.SetToolTip(this.lblReportFileName, "File name without extension");
            this.txtName.AcceptsReturn = true;
            this.txtName.AutoSize = false;
            this.txtName.BackColor = SystemColors.Window;
            this.txtName.Cursor = Cursors.IBeam;
            this.txtName.ForeColor = SystemColors.WindowText;
            this.txtName.Location = new Point(0x68, 0x10);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x128, 0x16);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "";
            this.lblName.BackColor = Color.Transparent;
            this.lblName.BorderStyle = BorderStyle.FixedSingle;
            this.lblName.Cursor = Cursors.Default;
            this.lblName.ForeColor = SystemColors.ControlText;
            this.lblName.Location = new Point(8, 0x10);
            this.lblName.Name = "lblName";
            this.lblName.RightToLeft = RightToLeft.No;
            this.lblName.Size = new Size(0x58, 0x16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = ContentAlignment.MiddleRight;
            this.gbMargin.Controls.Add(this.nudMarginRight);
            this.gbMargin.Controls.Add(this.nudMarginLeft);
            this.gbMargin.Controls.Add(this.nudMarginBottom);
            this.gbMargin.Controls.Add(this.nudMarginTop);
            this.gbMargin.Controls.Add(this.lblMarginBottom);
            this.gbMargin.Controls.Add(this.lblMarginLeft);
            this.gbMargin.Controls.Add(this.lblMarginTop);
            this.gbMargin.Controls.Add(this.lblMarginRight);
            this.gbMargin.Location = new Point(8, 0x70);
            this.gbMargin.Name = "gbMargin";
            this.gbMargin.Size = new Size(0x110, 0x58);
            this.gbMargin.TabIndex = 6;
            this.gbMargin.TabStop = false;
            this.gbMargin.Text = "Margins";
            this.nudMarginRight.DecimalPlaces = 2;
            int[] bits = new int[4];
            bits[0] = 5;
            bits[3] = 0x20000;
            this.nudMarginRight.Increment = new decimal(bits);
            this.nudMarginRight.Location = new Point(0xc0, 0x38);
            int[] numArray2 = new int[4];
            numArray2[0] = 10;
            this.nudMarginRight.Maximum = new decimal(numArray2);
            this.nudMarginRight.Name = "nudMarginRight";
            this.nudMarginRight.Size = new Size(0x40, 20);
            this.nudMarginRight.TabIndex = 7;
            this.nudMarginLeft.DecimalPlaces = 2;
            int[] numArray3 = new int[4];
            numArray3[0] = 5;
            numArray3[3] = 0x20000;
            this.nudMarginLeft.Increment = new decimal(numArray3);
            this.nudMarginLeft.Location = new Point(0x40, 0x38);
            int[] numArray4 = new int[4];
            numArray4[0] = 10;
            this.nudMarginLeft.Maximum = new decimal(numArray4);
            this.nudMarginLeft.Name = "nudMarginLeft";
            this.nudMarginLeft.Size = new Size(0x40, 20);
            this.nudMarginLeft.TabIndex = 3;
            this.nudMarginBottom.DecimalPlaces = 2;
            int[] numArray5 = new int[4];
            numArray5[0] = 5;
            numArray5[3] = 0x20000;
            this.nudMarginBottom.Increment = new decimal(numArray5);
            this.nudMarginBottom.Location = new Point(0xc0, 0x18);
            int[] numArray6 = new int[4];
            numArray6[0] = 10;
            this.nudMarginBottom.Maximum = new decimal(numArray6);
            this.nudMarginBottom.Name = "nudMarginBottom";
            this.nudMarginBottom.Size = new Size(0x40, 20);
            this.nudMarginBottom.TabIndex = 5;
            this.nudMarginTop.DecimalPlaces = 2;
            int[] numArray7 = new int[4];
            numArray7[0] = 5;
            numArray7[3] = 0x20000;
            this.nudMarginTop.Increment = new decimal(numArray7);
            this.nudMarginTop.Location = new Point(0x40, 0x18);
            int[] numArray8 = new int[4];
            numArray8[0] = 10;
            this.nudMarginTop.Maximum = new decimal(numArray8);
            this.nudMarginTop.Name = "nudMarginTop";
            this.nudMarginTop.Size = new Size(0x40, 20);
            this.nudMarginTop.TabIndex = 1;
            this.lblMarginBottom.BorderStyle = BorderStyle.FixedSingle;
            this.lblMarginBottom.Location = new Point(0x88, 0x18);
            this.lblMarginBottom.Name = "lblMarginBottom";
            this.lblMarginBottom.Size = new Size(0x30, 20);
            this.lblMarginBottom.TabIndex = 4;
            this.lblMarginBottom.Text = "Bottom";
            this.lblMarginBottom.TextAlign = ContentAlignment.MiddleRight;
            this.lblMarginLeft.BorderStyle = BorderStyle.FixedSingle;
            this.lblMarginLeft.Location = new Point(8, 0x38);
            this.lblMarginLeft.Name = "lblMarginLeft";
            this.lblMarginLeft.Size = new Size(0x30, 20);
            this.lblMarginLeft.TabIndex = 2;
            this.lblMarginLeft.Text = "Left";
            this.lblMarginLeft.TextAlign = ContentAlignment.MiddleRight;
            this.lblMarginTop.BorderStyle = BorderStyle.FixedSingle;
            this.lblMarginTop.Location = new Point(8, 0x18);
            this.lblMarginTop.Name = "lblMarginTop";
            this.lblMarginTop.Size = new Size(0x30, 20);
            this.lblMarginTop.TabIndex = 0;
            this.lblMarginTop.Text = "Top";
            this.lblMarginTop.TextAlign = ContentAlignment.MiddleRight;
            this.lblMarginRight.BorderStyle = BorderStyle.FixedSingle;
            this.lblMarginRight.Location = new Point(0x88, 0x38);
            this.lblMarginRight.Name = "lblMarginRight";
            this.lblMarginRight.Size = new Size(0x30, 20);
            this.lblMarginRight.TabIndex = 6;
            this.lblMarginRight.Text = "Right";
            this.lblMarginRight.TextAlign = ContentAlignment.MiddleRight;
            this.lblSpecialCoding.BorderStyle = BorderStyle.FixedSingle;
            this.lblSpecialCoding.Location = new Point(8, 80);
            this.lblSpecialCoding.Name = "lblSpecialCoding";
            this.lblSpecialCoding.Size = new Size(0x58, 0x16);
            this.lblSpecialCoding.TabIndex = 4;
            this.lblSpecialCoding.Text = "Special Coding";
            this.lblSpecialCoding.TextAlign = ContentAlignment.MiddleRight;
            this.txtSpecialCoding.AcceptsReturn = true;
            this.txtSpecialCoding.AutoSize = false;
            this.txtSpecialCoding.BackColor = SystemColors.Window;
            this.txtSpecialCoding.Cursor = Cursors.IBeam;
            this.txtSpecialCoding.ForeColor = SystemColors.WindowText;
            this.txtSpecialCoding.Location = new Point(0x68, 80);
            this.txtSpecialCoding.MaxLength = 20;
            this.txtSpecialCoding.Name = "txtSpecialCoding";
            this.txtSpecialCoding.Size = new Size(0xa8, 0x16);
            this.txtSpecialCoding.TabIndex = 5;
            this.txtSpecialCoding.Text = "";
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x1a0, 0x165);
            base.Name = "FormInvoiceForm";
            this.Text = "Maintain Invoice Form";
            base.tpWorkArea.ResumeLayout(false);
            this.gbMargin.ResumeLayout(false);
            this.nudMarginRight.EndInit();
            this.nudMarginLeft.EndInit();
            this.nudMarginBottom.EndInit();
            this.nudMarginTop.EndInit();
            base.ResumeLayout(false);
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_invoiceform WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtName, reader["Name"]);
                            Functions.SetTextBoxText(this.txtReportFileName, reader["ReportFileName"]);
                            Functions.SetTextBoxText(this.txtSpecialCoding, reader["SpecialCoding"]);
                            Functions.SetUpDownValue(this.nudMarginTop, reader["MarginTop"]);
                            Functions.SetUpDownValue(this.nudMarginBottom, reader["MarginBottom"]);
                            Functions.SetUpDownValue(this.nudMarginLeft, reader["MarginLeft"]);
                            Functions.SetUpDownValue(this.nudMarginRight, reader["MarginRight"]);
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

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_invoiceform" };
            ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
        }

        protected override bool SaveObject(int ID, bool IsNew)
        {
            bool flag;
            using (MySqlConnection connection = Globals.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("Name", MySqlType.VarChar, 50).Value = this.txtName.Text;
                    command.Parameters.Add("ReportFileName", MySqlType.VarChar, 50).Value = this.txtReportFileName.Text;
                    command.Parameters.Add("SpecialCoding", MySqlType.VarChar, 20).Value = this.txtSpecialCoding.Text;
                    command.Parameters.Add("MarginTop", MySqlType.Double).Value = Convert.ToDouble(this.nudMarginTop.Value);
                    command.Parameters.Add("MarginBottom", MySqlType.Double).Value = Convert.ToDouble(this.nudMarginBottom.Value);
                    command.Parameters.Add("MarginLeft", MySqlType.Double).Value = Convert.ToDouble(this.nudMarginLeft.Value);
                    command.Parameters.Add("MarginRight", MySqlType.Double).Value = Convert.ToDouble(this.nudMarginRight.Value);
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_invoiceform", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_invoiceform"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_invoiceform");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID, Name, ReportFileName FROM tbl_invoiceform ORDER BY Name", ClassGlobalObjects.ConnectionString_MySql))
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
            appearance.AddTextColumn("ReportFileName", "File", 100);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__68-0 e$__- = new _Closure$__68-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                base.ValidationErrors.SetError(this.txtName, "Name can not be empty");
            }
            else
            {
                int num;
                if (IsNew)
                {
                    MySqlParameter parameter1 = new MySqlParameter("Name", MySqlType.VarChar, 50);
                    parameter1.Value = this.txtName.Text;
                    MySqlParameter[] parameters = new MySqlParameter[] { parameter1 };
                    num = Convert.ToInt32(Globals.ExecuteScalar("SELECT COUNT(*) as `Count` FROM tbl_invoiceform WHERE (Name = :Name)", parameters));
                }
                else
                {
                    MySqlParameter parameter2 = new MySqlParameter("Name", MySqlType.VarChar, 50);
                    parameter2.Value = this.txtName.Text;
                    MySqlParameter[] parameters = new MySqlParameter[2];
                    parameters[0] = parameter2;
                    MySqlParameter parameter3 = new MySqlParameter("ID", MySqlType.Int);
                    parameter3.Value = ID;
                    parameters[1] = parameter3;
                    num = Convert.ToInt32(Globals.ExecuteScalar("SELECT COUNT(*) as `Count` FROM tbl_invoiceform WHERE (Name = :Name) AND (ID != :ID)", parameters));
                }
                if (0 < num)
                {
                    base.ValidationErrors.SetError(this.txtName, "Name is in use already");
                }
            }
            if (this.txtReportFileName.Text.Trim() == "")
            {
                base.ValidationErrors.SetError(this.txtReportFileName, "CR filename must be nonempty string");
            }
        }

        [field: AccessedThroughProperty("txtReportFileName")]
        private TextBox txtReportFileName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblReportFileName")]
        private Label lblReportFileName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtName")]
        private TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblName")]
        private Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("gbMargin")]
        private GroupBox gbMargin { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblSpecialCoding")]
        private Label lblSpecialCoding { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMarginRight")]
        private Label lblMarginRight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMarginTop")]
        private Label lblMarginTop { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMarginLeft")]
        private Label lblMarginLeft { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblMarginBottom")]
        private Label lblMarginBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudMarginTop")]
        private NumericUpDown nudMarginTop { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudMarginBottom")]
        private NumericUpDown nudMarginBottom { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudMarginLeft")]
        private NumericUpDown nudMarginLeft { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("nudMarginRight")]
        private NumericUpDown nudMarginRight { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtSpecialCoding")]
        private TextBox txtSpecialCoding { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__68-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

