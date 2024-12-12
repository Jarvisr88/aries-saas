namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlUserLocations : UserControl
    {
        private IContainer components;
        public const string CrLf = "\r\n";
        private readonly TableLocations FTable;

        public event EventHandler Changed;

        public ControlUserLocations()
        {
            this.InitializeComponent();
            this.InitializeGrid(this.Grid.Appearance);
            this.FTable = new TableLocations();
            this.FTable.RowChanged += new DataRowChangeEventHandler(this.Row_Changed);
            this.Grid.GridSource = this.FTable.ToGridSource();
        }

        private void ApplyToAll(bool Checked)
        {
            DataRow[] rowArray = this.Grid.GetSelectedRows().GetDataRows().ToArray<DataRow>();
            if (rowArray.Length != 0)
            {
                TableLocations table = (TableLocations) rowArray[0].Table;
                DataRow[] rowArray2 = rowArray;
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    rowArray2[i][table.Col_Selected] = Checked;
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Grid = new FilteredGrid();
            this.cmsGrid = new ContextMenuStrip(this.components);
            this.tsmiGridCheck = new ToolStripMenuItem();
            this.tsmiGridUncheck = new ToolStripMenuItem();
            this.cmsGrid.SuspendLayout();
            base.SuspendLayout();
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.Location = new Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new Size(0x1e8, 0x158);
            this.Grid.TabIndex = 5;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmiGridCheck, this.tsmiGridUncheck };
            this.cmsGrid.Items.AddRange(toolStripItems);
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new Size(0x73, 0x30);
            this.tsmiGridCheck.Name = "tsmiGridCheck";
            this.tsmiGridCheck.Size = new Size(0x72, 0x16);
            this.tsmiGridCheck.Text = "Check";
            this.tsmiGridUncheck.Name = "tsmiGridUncheck";
            this.tsmiGridUncheck.Size = new Size(0x72, 0x16);
            this.tsmiGridUncheck.Text = "Uncheck";
            base.Controls.Add(this.Grid);
            base.Name = "ControlUserLocations";
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
            Appearance.AddBoolColumn("Selected", "Selected", 60).ReadOnly = false;
            Appearance.AddTextColumn("Description", "Description", 180);
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
  `LocationID`  int(11)     NOT NULL default '0'
, `Description` varchar(50) NOT NULL default ''
, `Selected`    tinyint(1)  NOT NULL default '0'
)";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO `{str}` (`LocationID`, `Description`, `Selected`)
SELECT `ID` as `LocationID`, `Name` as `Description`, 0 as `Selected`
FROM tbl_location";
                    command.ExecuteNonQuery();
                    if (UserID != null)
                    {
                        command.CommandText = $"UPDATE `{str}` as src
       INNER JOIN `tbl_user_location` as ul ON src.LocationID = ul.LocationID
SET src.`Selected` = 1
WHERE ul.`UserID` = {UserID.Value}";
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
                    command.CommandText = "DELETE FROM `tbl_user_location` WHERE `UserID` = :UserID";
                    command.Parameters.Add("UserID", MySqlType.SmallInt).Value = UserID;
                    command.ExecuteNonQuery();
                }
                using (MySqlCommand command2 = cnn.CreateCommand())
                {
                    command2.CommandText = "INSERT INTO `tbl_user_location` (`UserID`, `LocationID`)\r\nVALUES (:UserID, :LocationID)";
                    command2.Parameters.Add("UserID", MySqlType.SmallInt);
                    command2.Parameters.Add("LocationID", MySqlType.Int);
                    foreach (DataRow row in this.FTable.Select("(Selected = true)"))
                    {
                        command2.Parameters["UserID"].Value = UserID;
                        command2.Parameters["LocationID"].Value = row[this.FTable.Col_LocationID];
                        command2.ExecuteNonQuery();
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

        private void tsmiGridCheckAll_Click(object sender, EventArgs e)
        {
            this.ApplyToAll(true);
        }

        private void tsmiGridUncheckAll_Click(object sender, EventArgs e)
        {
            this.ApplyToAll(false);
        }

        [field: AccessedThroughProperty("Grid")]
        private FilteredGrid Grid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("cmsGrid")]
        private ContextMenuStrip cmsGrid { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridCheck")]
        private ToolStripMenuItem tsmiGridCheck { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [field: AccessedThroughProperty("tsmiGridUncheck")]
        private ToolStripMenuItem tsmiGridUncheck { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        public class TableLocations : DataTable
        {
            public readonly DataColumn Col_LocationID;
            public readonly DataColumn Col_Description;
            public readonly DataColumn Col_Selected;

            public TableLocations() : base("tbl_permissions")
            {
                this.Col_LocationID = base.Columns.Add("LocationID", typeof(int));
                this.Col_LocationID.AllowDBNull = false;
                this.Col_Description = base.Columns.Add("Description", typeof(string));
                this.Col_Description.AllowDBNull = false;
                this.Col_Selected = base.Columns.Add("Selected", typeof(bool));
                this.Col_Selected.DefaultValue = false;
                this.Col_Selected.AllowDBNull = false;
                base.PrimaryKey = new DataColumn[] { this.Col_LocationID };
            }
        }
    }
}

