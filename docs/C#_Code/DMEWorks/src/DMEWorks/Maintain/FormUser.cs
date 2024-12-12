namespace DMEWorks.Maintain
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Controls;
    using DMEWorks.Core;
    using DMEWorks.Data;
    using DMEWorks.Data.MySql;
    using DMEWorks.Details;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated, Buttons(ButtonClone=true)]
    public class FormUser : FormAutoIncrementMaintain
    {
        private IContainer components;

        public FormUser()
        {
            this.InitializeComponent();
            NavigatorOptions options = new NavigatorOptions {
                Caption = "Search",
                CreateSource = new EventHandler<CreateSourceEventArgs>(this.Search_CreateSource),
                FillSource = new EventHandler<FillSourceEventArgs>(this.Search_FillSource),
                InitializeAppearance = new Action<GridAppearanceBase>(this.Search_InitializeAppearance),
                NavigatorRowClick = new EventHandler<NavigatorRowClickEventArgs>(this.Search_NavigatorRowClick)
            };
            string[] textArray1 = new string[] { "tbl_user" };
            options.TableNames = textArray1;
            base.AddNavigator(options);
            base.ChangesTracker.Subscribe(this.txtEmail);
            base.ChangesTracker.Subscribe(this.txtLogin);
            base.ChangesTracker.Subscribe(this.txtPassword);
            this.UserLocations.Changed += new EventHandler(this.HandleControlChanged);
            this.UserPermissions.Changed += new EventHandler(this.HandleControlChanged);
        }

        protected override void ClearObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtLogin, DBNull.Value);
            Functions.SetTextBoxText(this.txtPassword, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmail, DBNull.Value);
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                this.UserPermissions.ClearData(connection);
                this.UserLocations.ClearData(connection);
            }
        }

        protected override void CloneObject()
        {
            this.ObjectID = DBNull.Value;
            Functions.SetTextBoxText(this.txtLogin, DBNull.Value);
            Functions.SetTextBoxText(this.txtPassword, DBNull.Value);
            Functions.SetTextBoxText(this.txtEmail, DBNull.Value);
        }

        protected override void DeleteObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.Parameters.Add("UserID", MySqlType.Int).Value = ID;
                    command.ExecuteDelete("tbl_user_location");
                }
                using (MySqlCommand command2 = new MySqlCommand("", connection))
                {
                    command2.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    if (0 >= command2.ExecuteDelete("tbl_user"))
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
            messages.ConfirmDeleting = $"Are you really want to delete user '{this.txtLogin.Text}'?";
            messages.DeletedSuccessfully = $"User '{this.txtLogin.Text}' was successfully deleted.";
            return messages;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.Panel1 = new Panel();
            this.txtEmail = new TextBox();
            this.lblEmail = new Label();
            this.txtPassword = new TextBox();
            this.lblPassword = new Label();
            this.txtLogin = new TextBox();
            this.lblUserName = new Label();
            this.UserPermissions = new ControlUserPermissions();
            this.TabControl1 = new TabControl();
            this.tpPermissions = new TabPage();
            this.tpLocations = new TabPage();
            this.UserLocations = new ControlUserLocations();
            base.tpWorkArea.SuspendLayout();
            ((ISupportInitialize) base.ValidationErrors).BeginInit();
            ((ISupportInitialize) base.ValidationWarnings).BeginInit();
            this.Panel1.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tpPermissions.SuspendLayout();
            this.tpLocations.SuspendLayout();
            base.SuspendLayout();
            base.tpWorkArea.Controls.Add(this.TabControl1);
            base.tpWorkArea.Controls.Add(this.Panel1);
            base.tpWorkArea.Size = new Size(0x1d8, 0x1b2);
            base.tpWorkArea.Visible = true;
            this.Panel1.Controls.Add(this.txtEmail);
            this.Panel1.Controls.Add(this.lblEmail);
            this.Panel1.Controls.Add(this.txtPassword);
            this.Panel1.Controls.Add(this.lblPassword);
            this.Panel1.Controls.Add(this.txtLogin);
            this.Panel1.Controls.Add(this.lblUserName);
            this.Panel1.Dock = DockStyle.Top;
            this.Panel1.Location = new Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new Size(0x1d8, 80);
            this.Panel1.TabIndex = 0;
            this.txtEmail.Location = new Point(0x58, 0x38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(0x108, 20);
            this.txtEmail.TabIndex = 5;
            this.lblEmail.Location = new Point(8, 0x38);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(0x48, 0x15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = ContentAlignment.MiddleRight;
            this.txtPassword.Location = new Point(0x58, 0x20);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(0x108, 20);
            this.txtPassword.TabIndex = 3;
            this.lblPassword.Location = new Point(8, 0x20);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(0x48, 0x15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.txtLogin.Location = new Point(0x58, 8);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new Size(0x108, 20);
            this.txtLogin.TabIndex = 1;
            this.lblUserName.Location = new Point(8, 8);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new Size(0x48, 0x15);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User Name";
            this.lblUserName.TextAlign = ContentAlignment.MiddleRight;
            this.UserPermissions.Dock = DockStyle.Fill;
            this.UserPermissions.Location = new Point(3, 3);
            this.UserPermissions.Name = "UserPermissions";
            this.UserPermissions.Size = new Size(0x1ca, 0x142);
            this.UserPermissions.TabIndex = 0;
            this.TabControl1.Controls.Add(this.tpPermissions);
            this.TabControl1.Controls.Add(this.tpLocations);
            this.TabControl1.Dock = DockStyle.Fill;
            this.TabControl1.Location = new Point(0, 80);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new Size(0x1d8, 0x162);
            this.TabControl1.TabIndex = 1;
            this.tpPermissions.Controls.Add(this.UserPermissions);
            this.tpPermissions.Location = new Point(4, 0x16);
            this.tpPermissions.Name = "tpPermissions";
            this.tpPermissions.Padding = new Padding(3);
            this.tpPermissions.Size = new Size(0x1d0, 0x148);
            this.tpPermissions.TabIndex = 0;
            this.tpPermissions.Text = "Permissions";
            this.tpPermissions.UseVisualStyleBackColor = true;
            this.tpLocations.Controls.Add(this.UserLocations);
            this.tpLocations.Location = new Point(4, 0x16);
            this.tpLocations.Name = "tpLocations";
            this.tpLocations.Padding = new Padding(3);
            this.tpLocations.Size = new Size(0x1d0, 0x148);
            this.tpLocations.TabIndex = 1;
            this.tpLocations.Text = "Locations";
            this.tpLocations.UseVisualStyleBackColor = true;
            this.UserLocations.Dock = DockStyle.Fill;
            this.UserLocations.Location = new Point(3, 3);
            this.UserLocations.Name = "UserLocations";
            this.UserLocations.Size = new Size(0x1ca, 0x142);
            this.UserLocations.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(480, 0x1f5);
            base.Name = "FormUser";
            this.Text = "Maintain User";
            base.tpWorkArea.ResumeLayout(false);
            ((ISupportInitialize) base.ValidationErrors).EndInit();
            ((ISupportInitialize) base.ValidationWarnings).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.tpPermissions.ResumeLayout(false);
            this.tpLocations.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override bool LoadObject(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
            {
                bool flag;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("", connection))
                {
                    command.CommandText = "SELECT * FROM tbl_user WHERE ID = :ID";
                    command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.ObjectID = reader["ID"];
                            Functions.SetTextBoxText(this.txtLogin, reader["Login"]);
                            Functions.SetTextBoxText(this.txtPassword, reader["Password"]);
                            Functions.SetTextBoxText(this.txtEmail, reader["Email"]);
                            goto TR_000C;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                return flag;
            TR_000C:
                this.UserPermissions.LoadData(connection, new int?(ID));
                this.UserLocations.LoadData(connection, new int?(ID));
                return true;
            }
        }

        protected override void OnTableUpdate()
        {
            string[] tableNames = new string[] { "tbl_user", "tbl_permissions", "tbl_user_location" };
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
                    command.Parameters.Add("Login", MySqlType.VarChar, 0x10).Value = this.txtLogin.Text;
                    command.Parameters.Add("Password", MySqlType.VarChar, 0x20).Value = this.txtPassword.Text;
                    command.Parameters.Add("Email", MySqlType.VarChar, 150).Value = this.txtEmail.Text;
                    command.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt).Value = Globals.CompanyUserID;
                    if (!IsNew)
                    {
                        command.Parameters.Add("ID", MySqlType.Int).Value = ID;
                        string[] whereParameters = new string[] { "ID" };
                        flag = 0 < command.ExecuteUpdate("tbl_user", whereParameters);
                        flag ??= (0 < command.ExecuteInsert("tbl_user"));
                    }
                    else
                    {
                        flag = 0 < command.ExecuteInsert("tbl_user");
                        if (flag)
                        {
                            ID = command.GetLastIdentity();
                            this.ObjectID = ID;
                        }
                    }
                }
                this.UserPermissions.SaveData(connection, ID);
                this.UserLocations.SaveData(connection, ID);
            }
            return flag;
        }

        private void Search_CreateSource(object sender, CreateSourceEventArgs args)
        {
            args.Source = new DataTable().ToGridSource();
        }

        private void Search_FillSource(object sender, FillSourceEventArgs args)
        {
            string selectCommandText = (string.Compare(Globals.CompanyUserName, "root", true) != 0) ? "SELECT ID, Login, Email\r\nFROM tbl_user\r\nWHERE (Login != 'root')\r\nORDER BY Login" : "SELECT ID, Login, Email\r\nFROM tbl_user\r\nORDER BY Login";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommandText, ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill((args.Source as DataTableGridSource).Table);
            }
        }

        private void Search_InitializeAppearance(GridAppearanceBase appearance)
        {
            appearance.AutoGenerateColumns = false;
            appearance.Columns.Clear();
            appearance.AddTextColumn("ID", "ID", 40);
            appearance.AddTextColumn("Login", "Login", 180);
            appearance.AddTextColumn("Email", "Email", 180);
        }

        private void Search_NavigatorRowClick(object sender, NavigatorRowClickEventArgs args)
        {
            _Closure$__55-0 e$__- = new _Closure$__55-0 {
                $VB$Local_args = args
            };
            base.OpenObject(new Func<object>(e$__-._Lambda$__0));
        }

        protected override void ValidateObject(int ID, bool IsNew)
        {
            string strA = this.txtLogin.Text.TrimEnd(new char[0]);
            if (strA.Length == 0)
            {
                base.ValidationErrors.SetError(this.txtLogin, "You should input nonempty string for user name");
            }
            else if ((string.Compare(strA, "root", true) == 0) && (string.Compare(Globals.CompanyUserName, "root", true) != 0))
            {
                base.ValidationErrors.SetError(this.txtLogin, "You are not allowed to add/edit user 'root'");
            }
            else
            {
                int? nullable = null;
                if (IsNew)
                {
                    using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand("", connection))
                        {
                            command.CommandText = "SELECT COUNT(*) as `Count` FROM tbl_user WHERE (Login = :Login)";
                            command.Parameters.Add("Login", MySqlType.VarChar, 50).Value = strA;
                            nullable = NullableConvert.ToInt32(command.ExecuteScalar());
                        }
                    }
                }
                if (0 < nullable.GetValueOrDefault(0))
                {
                    base.ValidationErrors.SetError(this.txtLogin, "User with such name already exists");
                }
            }
            try
            {
                string text = this.txtEmail.Text;
                if (0 < ((text == null) ? "" : text.Trim()).Length)
                {
                    MailAddress address1 = new MailAddress(this.txtEmail.Text);
                }
                base.ValidationErrors.SetError(this.txtEmail, "");
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                base.ValidationErrors.SetError(this.txtEmail, "Entered email address is not valid");
                ProjectData.ClearProjectError();
            }
        }

        [field: AccessedThroughProperty("Panel1")]
        private Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtPassword")]
        private TextBox txtPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblPassword")]
        private Label lblPassword { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtLogin")]
        private TextBox txtLogin { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblUserName")]
        private Label lblUserName { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("UserPermissions")]
        private ControlUserPermissions UserPermissions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("TabControl1")]
        private TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpPermissions")]
        private TabPage tpPermissions { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tpLocations")]
        private TabPage tpLocations { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("UserLocations")]
        private ControlUserLocations UserLocations { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("txtEmail")]
        private TextBox txtEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("lblEmail")]
        private Label lblEmail { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [CompilerGenerated]
        internal sealed class _Closure$__55-0
        {
            public NavigatorRowClickEventArgs $VB$Local_args;

            internal object _Lambda$__0() => 
                this.$VB$Local_args.GridRow.GetDataRow()["ID"];
        }
    }
}

