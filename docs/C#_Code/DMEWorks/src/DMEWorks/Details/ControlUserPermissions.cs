namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlUserPermissions : UserControl
    {
        private IContainer components;
        public const string CrLf = "\r\n";
        private readonly TablePermissions FTable;

        public event EventHandler Changed;

        public ControlUserPermissions()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
            this.FTable = new TablePermissions();
            this.FTable.RowChanged += new DataRowChangeEventHandler(this.Row_Changed);
            this.Grid.GridSource = this.FTable.ToGridSource();
        }

        private void CheckAll(params string[] Columns)
        {
            DataRow[] rows = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
            if (rows.Length != 0)
            {
                DataTable table = rows[0].Table;
                ArrayList list = new ArrayList(Columns.Length);
                string[] strArray = Columns;
                int index = 0;
                while (true)
                {
                    if (index >= strArray.Length)
                    {
                        if (list.Count != 0)
                        {
                            IEnumerator enumerator;
                            IEnumerator enumerator2;
                            int num = 0;
                            try
                            {
                                enumerator = list.GetEnumerator();
                                while (enumerator.MoveNext())
                                {
                                    DataColumn current = (DataColumn) enumerator.Current;
                                    num += GetCount(rows, current);
                                }
                            }
                            finally
                            {
                                if (enumerator is IDisposable)
                                {
                                    (enumerator as IDisposable).Dispose();
                                }
                            }
                            bool flag = num != (rows.Length * list.Count);
                            try
                            {
                                enumerator2 = list.GetEnumerator();
                                while (enumerator2.MoveNext())
                                {
                                    DataColumn current = (DataColumn) enumerator2.Current;
                                    SetValue(rows, current, flag);
                                }
                            }
                            finally
                            {
                                if (enumerator2 is IDisposable)
                                {
                                    (enumerator2 as IDisposable).Dispose();
                                }
                            }
                        }
                        break;
                    }
                    string str = strArray[index];
                    DataColumn column = table.Columns[str];
                    if (column != null)
                    {
                        list.Add(column);
                    }
                    index++;
                }
            }
        }

        public void ClearData(MySqlConnection cnn)
        {
            int? userID = null;
            this.LoadData(cnn, userID);
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

        private static int GetCount(DataRow[] Rows, DataColumn Column)
        {
            int num;
            if (Column == null)
            {
                throw new ArgumentNullException("Column");
            }
            if (Rows == null)
            {
                num = 0;
            }
            else if (Rows.Length == 0)
            {
                num = 0;
            }
            else
            {
                int num2 = 0;
                DataRow[] rowArray = Rows;
                int index = 0;
                while (true)
                {
                    if (index >= rowArray.Length)
                    {
                        num = num2;
                        break;
                    }
                    object obj2 = rowArray[index][Column];
                    if ((obj2 as bool) && Conversions.ToBoolean(obj2))
                    {
                        num2++;
                    }
                    index++;
                }
            }
            return num;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Grid = new FilteredGrid();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.tsmiGridAddEdit = new ToolStripMenuItem();
            this.tsmiGridDelete = new ToolStripMenuItem();
            this.tsmiGridProcess = new ToolStripMenuItem();
            this.tsmiGridView = new ToolStripMenuItem();
            this.tsmiGridAll = new ToolStripMenuItem();
            this.cmsGrid.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x1e8, 0x158);
            this.Grid.TabIndex = 5;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridAll, this.tsmiGridAddEdit, this.tsmiGridDelete, this.tsmiGridProcess, this.tsmiGridView };
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0x99, 0x88);
            this.tsmiGridAddEdit.Name = "tsmiGridAddEdit";
            this.tsmiGridAddEdit.Size = new Size(0x98, 0x16);
            this.tsmiGridAddEdit.Text = "Add, Edit";
            this.tsmiGridDelete.Name = "tsmiGridDelete";
            this.tsmiGridDelete.Size = new Size(0x98, 0x16);
            this.tsmiGridDelete.Text = "Delete";
            this.tsmiGridProcess.Name = "tsmiGridProcess";
            this.tsmiGridProcess.Size = new Size(0x98, 0x16);
            this.tsmiGridProcess.Text = "Process";
            this.tsmiGridView.Name = "tsmiGridView";
            this.tsmiGridView.Size = new Size(0x98, 0x16);
            this.tsmiGridView.Text = "View";
            this.tsmiGridAll.Name = "tsmiGridAll";
            this.tsmiGridAll.Size = new Size(0x98, 0x16);
            this.tsmiGridAll.Text = "All";
            base.Controls.Add(this.Grid);
            base.Name = "ControlUserPermissions";
            base.Size = new Size(0x1e8, 0x158);
            this.cmsGrid.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AllowEdit = true;
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.MultiSelect = true;
            Appearance.AddTextColumn("Description", "Description", 180);
            Appearance.AddBoolColumn("ADD_EDIT", "Add/Edit", 60).ReadOnly = false;
            Appearance.AddBoolColumn("DELETE", "Delete", 60).ReadOnly = false;
            Appearance.AddBoolColumn("PROCESS", "Process", 60).ReadOnly = false;
            Appearance.AddBoolColumn("VIEW", "View", 60).ReadOnly = false;
            Appearance.ContextMenuStrip = this.cmsGrid;
        }

        public void LoadData(MySqlConnection cnn, int? UserID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    command.CommandType = CommandType.Text;
                    string str = Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TEMPORARY TABLE `{str}` (
  `ID`          int(11)     NOT NULL default '0'
, `Description` varchar(50) NOT NULL default ''
, `ADD_EDIT`    tinyint(1)  NOT NULL default '0'
, `DELETE`      tinyint(1)  NOT NULL default '0'
, `PROCESS`     tinyint(1)  NOT NULL default '0'
, `VIEW`        tinyint(1)  NOT NULL default '0'
)";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO `{str}` (`ID`, `Description`)
SELECT `ID`, `Description`
FROM tbl_object";
                    command.ExecuteNonQuery();
                    if (UserID != null)
                    {
                        command.CommandText = string.Format("UPDATE `{0}`, `tbl_permissions`\r\nSET `{0}`.`ADD_EDIT` = `tbl_permissions`.`ADD_EDIT`\r\n  , `{0}`.`DELETE`   = `tbl_permissions`.`DELETE`\r\n  , `{0}`.`PROCESS`  = `tbl_permissions`.`PROCESS`\r\n  , `{0}`.`VIEW`     = `tbl_permissions`.`VIEW`\r\nWHERE (`{0}`.`ID` = `tbl_permissions`.`ObjectID`)\r\n  AND (`tbl_permissions`.`UserID` = {1})", str, UserID.Value);
                        command.ExecuteNonQuery();
                    }
                    command.CommandText = $"SELECT *
FROM `{str}`";
                    this.FTable.Clear();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(this.FTable);
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            EventHandler changedEvent = this.ChangedEvent;
            if (changedEvent != null)
            {
                changedEvent(this, EventArgs.Empty);
            }
        }

        public void SaveData(MySqlConnection cnn, int UserID)
        {
            this.Grid.EndEdit();
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.FTable.AcceptChanges();
                using (MySqlCommand command = cnn.CreateCommand())
                {
                    command.CommandText = "REPLACE INTO tbl_permissions (`UserID`, `ObjectID`, `ADD_EDIT`, `DELETE`, `PROCESS`, `VIEW`)\r\nVALUES (:UserID, :ObjectID, :ADD_EDIT, :DELETE, :PROCESS, :VIEW)";
                    command.Parameters.Add("UserID", MySqlType.SmallInt);
                    command.Parameters.Add("ObjectID", MySqlType.Int);
                    command.Parameters.Add("ADD_EDIT", MySqlType.Bit);
                    command.Parameters.Add("DELETE", MySqlType.Bit);
                    command.Parameters.Add("PROCESS", MySqlType.Bit);
                    command.Parameters.Add("VIEW", MySqlType.Bit);
                    int num2 = this.FTable.Rows.Count - 1;
                    for (int i = 0; i <= num2; i++)
                    {
                        DataRow row = this.FTable.Rows[i];
                        command.Parameters["UserID"].Value = UserID;
                        command.Parameters["ObjectID"].Value = row[this.FTable.Col_ID];
                        command.Parameters["ADD_EDIT"].Value = row[this.FTable.Col_ADD_EDIT];
                        command.Parameters["PROCESS"].Value = row[this.FTable.Col_PROCESS];
                        command.Parameters["DELETE"].Value = row[this.FTable.Col_DELETE];
                        command.Parameters["VIEW"].Value = row[this.FTable.Col_VIEW];
                        command.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        private static void SetValue(DataRow[] Rows, DataColumn Column, bool value)
        {
            if (Column == null)
            {
                throw new ArgumentNullException("Column");
            }
            if ((Rows != null) && (Rows.Length != 0))
            {
                DataRow[] rowArray = Rows;
                for (int i = 0; i < rowArray.Length; i++)
                {
                    rowArray[i][Column] = value;
                }
            }
        }

        private void tsmiGridAddEdit_Click(object sender, EventArgs e)
        {
            string[] columns = new string[] { "ADD_EDIT" };
            this.CheckAll(columns);
        }

        private void tsmiGridAll_Click(object sender, EventArgs e)
        {
            string[] columns = new string[] { "ADD_EDIT", "DELETE", "PROCESS", "VIEW" };
            this.CheckAll(columns);
        }

        private void tsmiGridDelete_Click(object sender, EventArgs e)
        {
            string[] columns = new string[] { "DELETE" };
            this.CheckAll(columns);
        }

        private void tsmiGridProcess_Click(object sender, EventArgs e)
        {
            string[] columns = new string[] { "PROCESS" };
            this.CheckAll(columns);
        }

        private void tsmiGridView_Click(object sender, EventArgs e)
        {
            string[] columns = new string[] { "VIEW" };
            this.CheckAll(columns);
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridAddEdit")]
        private ToolStripMenuItem tsmiGridAddEdit { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridDelete")]
        private ToolStripMenuItem tsmiGridDelete { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridProcess")]
        private ToolStripMenuItem tsmiGridProcess { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridView")]
        private ToolStripMenuItem tsmiGridView { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridAll")]
        private ToolStripMenuItem tsmiGridAll { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public class TablePermissions : DataTable
        {
            public readonly DataColumn Col_ID;
            public readonly DataColumn Col_Description;
            public readonly DataColumn Col_ADD_EDIT;
            public readonly DataColumn Col_DELETE;
            public readonly DataColumn Col_PROCESS;
            public readonly DataColumn Col_VIEW;

            public TablePermissions() : base("tbl_permissions")
            {
                this.Col_ID = base.Columns.Add("ID", typeof(int));
                this.Col_ID.AllowDBNull = false;
                this.Col_Description = base.Columns.Add("Description", typeof(string));
                this.Col_Description.AllowDBNull = false;
                this.Col_ADD_EDIT = base.Columns.Add("ADD_EDIT", typeof(bool));
                this.Col_ADD_EDIT.DefaultValue = false;
                this.Col_ADD_EDIT.AllowDBNull = false;
                this.Col_DELETE = base.Columns.Add("DELETE", typeof(bool));
                this.Col_DELETE.DefaultValue = false;
                this.Col_DELETE.AllowDBNull = false;
                this.Col_PROCESS = base.Columns.Add("PROCESS", typeof(bool));
                this.Col_PROCESS.DefaultValue = false;
                this.Col_PROCESS.AllowDBNull = false;
                this.Col_VIEW = base.Columns.Add("VIEW", typeof(bool));
                this.Col_VIEW.DefaultValue = false;
                this.Col_VIEW.AllowDBNull = false;
                base.PrimaryKey = new DataColumn[] { this.Col_ID };
            }
        }
    }
}

