namespace DMEWorks.Forms
{
    using Devart.Data.MySql;
    using Microsoft.VisualBasic.CompilerServices;
    using My.Resources;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormSessions : DmeForm
    {
        private IContainer components;
        private const string CrLf = "\r\n";

        public FormSessions()
        {
            base.Load += new EventHandler(this.FormSessions_Load);
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
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

        private void FormSessions_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadGrid();
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
            this.Grid = new FilteredGrid();
            this.ToolStrip1 = new ToolStrip();
            this.tsbRefresh = new ToolStripButton();
            this.tsbDelete = new ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0x19);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x1df, 0x14f);
            this.Grid.TabIndex = 6;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbRefresh, this.tsbDelete };
            this.ToolStrip1.Items.AddRange(toolStripItems);
            this.ToolStrip1.Location = new Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new Size(0x1df, 0x19);
            this.ToolStrip1.TabIndex = 8;
            this.ToolStrip1.Text = "ToolStrip1";
            this.tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = My.Resources.Resources.ImageRefresh;
            this.tsbRefresh.ImageTransparentColor = Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new Size(0x17, 0x16);
            this.tsbRefresh.Text = "Refresh";
            this.tsbDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = My.Resources.Resources.ImageDelete;
            this.tsbDelete.ImageTransparentColor = Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new Size(0x17, 0x16);
            this.tsbDelete.Text = "Delete";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1df, 360);
            base.Controls.Add(this.Grid);
            base.Controls.Add(this.ToolStrip1);
            base.Name = "FormSessions";
            this.Text = "Sessions";
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = true;
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.MultiSelect = true;
            Appearance.AddTextColumn("ID", "#", 60);
            Appearance.AddTextColumn("User", "User", 80);
            Appearance.AddTextColumn("Status", "Status", 80);
            Appearance.AddTextColumn("LoginTime", "Login Time", 110, Appearance.DateTimeStyle());
            Appearance.AddTextColumn("LastUpdateTime", "LastUpdateTime", 110, Appearance.DateTimeStyle());
        }

        private void LoadGrid()
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("", ClassGlobalObjects.ConnectionString_MySql))
            {
                adapter.SelectCommand.CommandText = "SELECT\r\n  tbl_sessions.ID\r\n, tbl_user.Login as `User`\r\n, CASE WHEN DATE_SUB(NOW(), INTERVAL 1 MINUTE) < LastUpdateTime THEN 'Connected'\r\n       ELSE 'Disconnected' END as Status\r\n, tbl_sessions.LoginTime\r\n, tbl_sessions.LastUpdateTime\r\nFROM tbl_sessions\r\n     LEFT JOIN tbl_user ON tbl_sessions.UserID = tbl_user.ID\r\nORDER BY LastUpdateTime";
                adapter.AcceptChangesDuringFill = true;
                adapter.Fill(dataTable);
            }
            this.Grid.GridSource = dataTable.ToGridSource();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            DataRow[] rowArray = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
            if (rowArray.Length == 0)
            {
                return;
            }
            else
            {
                DataColumn column = rowArray[0].Table.Columns["ID"];
                if ((column == null) || (MessageBox.Show("Deletion of active session will clear one slot but will force user to reconnect.\r\nAre you sure you want to proceed?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes))
                {
                    return;
                }
                else
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                        {
                            using (MySqlCommand command = new MySqlCommand("", connection))
                            {
                                command.CommandText = "DELETE FROM tbl_sessions WHERE ID = :ID";
                                command.Parameters.Add("ID", MySqlType.Int);
                                connection.Open();
                                foreach (DataRow row in rowArray)
                                {
                                    command.Parameters["ID"].Value = row[column];
                                    command.ExecuteNonQuery();
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
            }
            try
            {
                this.LoadGrid();
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

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadGrid();
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

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("ToolStrip1")]
        private ToolStrip ToolStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbRefresh")]
        private ToolStripButton tsbRefresh { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsbDelete")]
        private ToolStripButton tsbDelete { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

