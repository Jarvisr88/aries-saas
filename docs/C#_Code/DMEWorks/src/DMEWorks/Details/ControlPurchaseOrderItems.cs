namespace DMEWorks.Details
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Data.MySql;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Xml;

    public class ControlPurchaseOrderItems : ControlDetails
    {
        private IContainer components;

        public ControlPurchaseOrderItems()
        {
            this.InitializeComponent();
        }

        public void AddInventoryItem(int InventoryItemID)
        {
            if ((this.AllowState & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew)
            {
                TablePurchaseOrderDetails table = this.GetTable();
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = "SELECT ID, Name, PurchasePrice FROM tbl_inventoryitem WHERE ID = :ID";
                        command.Parameters.Add("ID", MySqlType.Int).Value = InventoryItemID;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DataRow row = table.NewRow();
                                row[table.Col_InventoryItemID] = reader["ID"];
                                row[table.Col_InventoryItemName] = reader["Name"];
                                row[table.Col_Price] = reader["PurchasePrice"];
                                table.Rows.Add(row);
                            }
                        }
                    }
                }
            }
        }

        public void AddInventoryItems(XmlElement element)
        {
            if ((this.AllowState & AllowStateEnum.AllowNew) == AllowStateEnum.AllowNew)
            {
                TablePurchaseOrderDetails table = this.GetTable();
                List<DataRow> list = new List<DataRow>();
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        IEnumerator enumerator;
                        command.CommandText = "SELECT\r\n  tbl_inventoryitem.ID   as InventoryItemID\r\n, tbl_inventoryitem.Name as InventoryItemName\r\n, tbl_inventoryitem.PurchasePrice\r\n, CONCAT(tbl_customer.LastName, ', ', tbl_customer.FirstName) as CustomerName\r\n, tbl_warehouse.ID   as WarehouseID\r\n, tbl_warehouse.Name as WarehouseName\r\nFROM tbl_inventoryitem\r\n     LEFT JOIN tbl_customer ON tbl_customer.ID = :CustomerID\r\n     LEFT JOIN tbl_warehouse ON tbl_warehouse.ID = :WarehouseID\r\nWHERE tbl_inventoryitem.ID = :InventoryItemID";
                        command.Parameters.Add("InventoryItemID", MySqlType.Int);
                        command.Parameters.Add("CustomerID", MySqlType.Int);
                        command.Parameters.Add("WarehouseID", MySqlType.Int);
                        try
                        {
                            enumerator = element.SelectNodes("Item").GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                XmlElement current = (XmlElement) enumerator.Current;
                                int? nullable = ParseInt32(GetChildText(current, "InventoryItemID"));
                                int? nullable2 = ParseInt32(GetChildText(current, "CustomerID"));
                                int? nullable3 = ParseInt32(GetChildText(current, "WarehouseID"));
                                int? nullable4 = ParseInt32(GetChildText(current, "Quantity"));
                                if (nullable != null)
                                {
                                    command.Parameters["InventoryItemID"].Value = nullable;
                                    command.Parameters["CustomerID"].Value = (nullable2 != null) ? ((object) nullable2.Value) : ((object) DBNull.Value);
                                    command.Parameters["WarehouseID"].Value = (nullable3 != null) ? ((object) nullable3.Value) : ((object) DBNull.Value);
                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            DataRow row = table.NewRow();
                                            row[table.Col_InventoryItemID] = reader["InventoryItemID"];
                                            row[table.Col_InventoryItemName] = reader["InventoryItemName"];
                                            row[table.Col_Price] = reader["PurchasePrice"];
                                            row[table.Col_Customer] = reader["CustomerName"];
                                            row[table.Col_WarehouseID] = reader["WarehouseID"];
                                            row[table.Col_WarehouseName] = reader["WarehouseName"];
                                            row[table.Col_Ordered] = (nullable4 != null) ? ((object) nullable4.Value) : ((object) DBNull.Value);
                                            table.Rows.Add(row);
                                            list.Add(row);
                                        }
                                    }
                                }
                            }
                        }
                        finally
                        {
                            if (enumerator is IDisposable)
                            {
                                (enumerator as IDisposable).Dispose();
                            }
                        }
                    }
                }
                if (0 < list.Count)
                {
                    List<DataRow>.Enumerator enumerator;
                    try
                    {
                        enumerator = list.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            DataRow current = enumerator.Current;
                            base.EditRow(Convert.ToInt32(current[table.Col_RowID]));
                        }
                    }
                    finally
                    {
                        enumerator.Dispose();
                    }
                }
            }
        }

        public void ClearGrid()
        {
            base.CloseDialogs();
            TablePurchaseOrderDetails table = this.GetTable();
            table.Clear();
            table.AcceptChanges();
        }

        protected override FormDetails CreateDialog(object param) => 
            base.AddDialog(new FormPurchaseOrderDetail(this));

        protected override ControlDetails.TableDetails CreateTable() => 
            new TablePurchaseOrderDetails();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected static void GenerateDeleteCommand_PurchaseOrderDetails(MySqlCommand cmd, int PurchaseOrderID)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("PurchaseOrderID", MySqlType.Int, 4).Value = PurchaseOrderID;
            cmd.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            cmd.GenerateDeleteCommand("tbl_purchaseorderdetails");
        }

        protected static void GenerateInsertCommad_PurchaseOrderDetails(MySqlCommand cmd, int PurchaseOrderID)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("BackOrder", MySqlType.Int, 4, "BackOrder");
            cmd.Parameters.Add("Ordered", MySqlType.Int, 4, "Ordered");
            cmd.Parameters.Add("Received", MySqlType.Int, 4, "Received");
            cmd.Parameters.Add("Price", MySqlType.Double, 8, "Price");
            cmd.Parameters.Add("Customer", MySqlType.VarChar, 50, "Customer");
            cmd.Parameters.Add("DatePromised", MySqlType.Date, 0, "DatePromised");
            cmd.Parameters.Add("DateReceived", MySqlType.Date, 0, "DateReceived");
            cmd.Parameters.Add("DropShipToCustomer", MySqlType.Bit, 1, "DropShipToCustomer");
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 4, "InventoryItemID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 4, "WarehouseID");
            cmd.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            cmd.Parameters.Add("PurchaseOrderID", MySqlType.Int, 4).Value = PurchaseOrderID;
            cmd.GenerateInsertCommand("tbl_purchaseorderdetails");
        }

        protected static void GenerateUpdateCommand_PurchaseOrderDetails(MySqlCommand cmd, int PurchaseOrderID)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("BackOrder", MySqlType.Int, 4, "BackOrder");
            cmd.Parameters.Add("Ordered", MySqlType.Int, 4, "Ordered");
            cmd.Parameters.Add("Received", MySqlType.Int, 4, "Received");
            cmd.Parameters.Add("Price", MySqlType.Double, 8, "Price");
            cmd.Parameters.Add("Customer", MySqlType.VarChar, 50, "Customer");
            cmd.Parameters.Add("DatePromised", MySqlType.Date, 0, "DatePromised");
            cmd.Parameters.Add("DateReceived", MySqlType.Date, 0, "DateReceived");
            cmd.Parameters.Add("DropShipToCustomer", MySqlType.Bit, 1, "DropShipToCustomer");
            cmd.Parameters.Add("InventoryItemID", MySqlType.Int, 4, "InventoryItemID");
            cmd.Parameters.Add("WarehouseID", MySqlType.Int, 4, "WarehouseID");
            cmd.Parameters.Add("LastUpdateUserID", MySqlType.SmallInt, 2).Value = Globals.CompanyUserID;
            cmd.Parameters.Add("PurchaseOrderID", MySqlType.Int, 4).Value = PurchaseOrderID;
            cmd.Parameters.Add("ID", MySqlType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            string[] whereParameters = new string[] { "PurchaseOrderID", "ID" };
            cmd.GenerateUpdateCommand("tbl_purchaseorderdetails", whereParameters);
        }

        protected static string GetChildText(XmlNode node, string childName)
        {
            string str;
            if (node == null)
            {
                str = "";
            }
            else
            {
                XmlNode node2 = node[childName];
                str = (node2 != null) ? node2.InnerText : "";
            }
            return str;
        }

        protected static string GetPathText(XmlNode node, string path)
        {
            string str;
            if (node == null)
            {
                str = "";
            }
            else
            {
                XmlNode node2 = node.SelectSingleNode(path);
                str = (node2 != null) ? node2.InnerText : "";
            }
            return str;
        }

        protected TablePurchaseOrderDetails GetTable() => 
            (TablePurchaseOrderDetails) base.F_TableDetails;

        public double GetTotal()
        {
            double num = 0.0;
            TablePurchaseOrderDetails table = this.GetTable();
            int num2 = table.Rows.Count - 1;
            for (int i = 0; i <= num2; i++)
            {
                DataRow row = table.Rows[i];
                if ((row.RowState == DataRowState.Added) || ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Unchanged)))
                {
                    num += NullableConvert.ToDouble(row[table.Col_Price], 0.0) * NullableConvert.ToDouble(row[table.Col_Ordered], 0.0);
                }
            }
            return num;
        }

        public bool HasReceivedItems()
        {
            TablePurchaseOrderDetails table = this.GetTable();
            int num = table.Rows.Count - 1;
            int num2 = 0;
            while (true)
            {
                bool flag;
                if (num2 > num)
                {
                    flag = false;
                }
                else
                {
                    DataRow row = table.Rows[num2];
                    if (((row.RowState != DataRowState.Added) && ((row.RowState != DataRowState.Modified) && (row.RowState != DataRowState.Unchanged))) || (0.0 >= NullableConvert.ToDouble(row[table.Col_Received], 0.0)))
                    {
                        num2++;
                        continue;
                    }
                    flag = true;
                }
                return flag;
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            base.Name = "ControlPurchaseOrderItems";
        }

        protected override void InitializeGrid(FilteredGridAppearance Appearance)
        {
            Appearance.AutoGenerateColumns = false;
            Appearance.Columns.Clear();
            Appearance.AddTextColumn("BackOrder", "BackOrder", 80);
            Appearance.AddTextColumn("Customer", "Customer", 80);
            Appearance.AddTextColumn("DatePromised", "DatePromised", 80);
            Appearance.AddTextColumn("DateReceived", "DateReceived", 80);
            Appearance.AddTextColumn("DropShipToCustomer", "Drop To Customer", 80);
            Appearance.AddTextColumn("InventoryItemName", "Inventory Item", 80);
            Appearance.AddTextColumn("Ordered", "Ordered", 80);
            Appearance.AddTextColumn("Received", "Received", 80);
            Appearance.AddTextColumn("Price", "Price", 80);
            Appearance.AddTextColumn("WarehouseName", "Warehouse", 80);
        }

        public void LoadGrid(MySqlConnection cnn, int PurchaseOrderID)
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT tbl_purchaseorderdetails.BackOrder,
       tbl_purchaseorderdetails.Customer,
       tbl_purchaseorderdetails.DatePromised,
       tbl_purchaseorderdetails.DateReceived,
       tbl_purchaseorderdetails.DropShipToCustomer <> 0 as DropShipToCustomer,
       tbl_purchaseorderdetails.ID,
       tbl_inventoryitem.ID as InventoryItemID,
       tbl_inventoryitem.Name as InventoryItemName,
       tbl_purchaseorderdetails.Price,
       tbl_purchaseorderdetails.Ordered,
       tbl_purchaseorderdetails.Received,
       tbl_warehouse.ID as WarehouseID,
       tbl_warehouse.Name as WarehouseName
FROM tbl_purchaseorderdetails
     LEFT JOIN tbl_warehouse ON tbl_warehouse.ID = tbl_purchaseorderdetails.WarehouseID
     LEFT JOIN tbl_inventoryitem ON tbl_inventoryitem.ID = tbl_purchaseorderdetails.InventoryItemID
WHERE (tbl_purchaseorderdetails.PurchaseOrderID = {PurchaseOrderID})", cnn))
            {
                TablePurchaseOrderDetails table = this.GetTable();
                base.CloseDialogs();
                table.Clear();
                adapter.Fill(table);
                table.AcceptChanges();
            }
        }

        protected static int? ParseInt32(string value)
        {
            try
            {
                return new int?(int.Parse(value, CultureInfo.InvariantCulture));
            }
            catch (ArgumentNullException exception1)
            {
                ArgumentNullException ex = exception1;
                ProjectData.SetProjectError(ex);
                ArgumentNullException exception = ex;
                ProjectData.ClearProjectError();
            }
            catch (FormatException exception4)
            {
                FormatException ex = exception4;
                ProjectData.SetProjectError(ex);
                FormatException exception2 = ex;
                ProjectData.ClearProjectError();
            }
            catch (OverflowException exception5)
            {
                OverflowException ex = exception5;
                ProjectData.SetProjectError(ex);
                OverflowException exception3 = ex;
                ProjectData.ClearProjectError();
            }
            return null;
        }

        public void RunStoredProcedures(MySqlConnection cnn, int PurchaseOrderID)
        {
            using (MySqlCommand command = new MySqlCommand("", cnn))
            {
                command.CommandText = $"CALL inventory_transaction_po_refresh({PurchaseOrderID}, 'Ordered')";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL inventory_transaction_po_refresh({PurchaseOrderID}, 'Received')";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL inventory_transaction_po_refresh({PurchaseOrderID}, 'BackOrdered')";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL inventory_po_refresh({PurchaseOrderID})";
                command.ExecuteNonQuery();
                command.CommandText = $"CALL PurchaseOrder_UpdateTotals({PurchaseOrderID})";
                command.ExecuteNonQuery();
            }
        }

        public void SaveGrid(MySqlConnection cnn, int PurchaseOrderID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                this.SaveItems(cnn, PurchaseOrderID);
                this.RunStoredProcedures(cnn, PurchaseOrderID);
                UpdateSerials(cnn, PurchaseOrderID);
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

        public void SaveItems(MySqlConnection cnn, int PurchaseOrderID)
        {
            TablePurchaseOrderDetails table = this.GetTable();
            TablePurchaseOrderDetails changes = (TablePurchaseOrderDetails) table.GetChanges();
            if (changes != null)
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.InsertCommand = new MySqlCommand("", cnn);
                    GenerateInsertCommad_PurchaseOrderDetails(adapter.InsertCommand, PurchaseOrderID);
                    adapter.UpdateCommand = new MySqlCommand("", cnn);
                    GenerateUpdateCommand_PurchaseOrderDetails(adapter.UpdateCommand, PurchaseOrderID);
                    adapter.DeleteCommand = new MySqlCommand("", cnn);
                    GenerateDeleteCommand_PurchaseOrderDetails(adapter.DeleteCommand, PurchaseOrderID);
                    using (new DataAdapterEvents(adapter))
                    {
                        adapter.ContinueUpdateOnError = false;
                        adapter.Update(changes);
                        table.MergeKeys(changes);
                        table.AcceptChanges();
                    }
                }
            }
        }

        public void ShowDetails(object PurchaseOrderDetailsID)
        {
            try
            {
                DataTable tableSource = this.Grid.GetTableSource<DataTable>();
                if (tableSource != null)
                {
                    foreach (DataRow row in tableSource.Select($"[ID] = {PurchaseOrderDetailsID}", "", DataViewRowState.CurrentRows))
                    {
                        base.EditRow(Conversions.ToInteger(row["RowID"]));
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

        public static void UpdateSerials(MySqlConnection cnn, int PurchaseOrderID)
        {
            ArrayList list = new ArrayList();
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                MySqlTransaction transaction = cnn.BeginTransaction();
                try
                {
                    using (MySqlCommand command = new MySqlCommand("CALL `serials_po_refresh`(:PurchaseOrderID)", cnn, transaction))
                    {
                        command.Parameters.Add("PurchaseOrderID", MySqlType.Int).Value = PurchaseOrderID;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(NullableConvert.ToInt32(reader[0]));
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    transaction.Rollback();
                    throw;
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
            if (0 < list.Count)
            {
                string[] tableNames = new string[] { "tbl_serial" };
                ClassGlobalObjects.NotifyDatabaseChanged(tableNames);
                int num2 = list.Count - 1;
                int num = 0;
                while (true)
                {
                    if (num > num2)
                    {
                        MessageBox.Show($"Application has added {list.Count} new serials.", "Purchase Order");
                        break;
                    }
                    FormParameters @params = new FormParameters("ID", list[num]);
                    ClassGlobalObjects.ShowForm(FormFactories.FormSerial(), @params);
                    num++;
                }
            }
        }

        private class DataAdapterEvents : MySqlDataAdapterEventsBase
        {
            public DataAdapterEvents(MySqlDataAdapter da) : base(da)
            {
            }

            protected override void ProcessRowUpdated(MySqlRowUpdatedEventArgs e)
            {
                if ((e.Status == UpdateStatus.Continue) && ((e.StatementType == StatementType.Insert) && ((e.RecordsAffected == 1) && (e.Row.Table is ControlPurchaseOrderItems.TablePurchaseOrderDetails))))
                {
                    ControlPurchaseOrderItems.TablePurchaseOrderDetails table = (ControlPurchaseOrderItems.TablePurchaseOrderDetails) e.Row.Table;
                    base.cmdSelectIdentity.Connection = e.Command.Connection;
                    try
                    {
                        base.cmdSelectIdentity.Transaction = e.Command.Transaction;
                        try
                        {
                            e.Row[table.Col_ID] = Conversions.ToInteger(base.cmdSelectIdentity.ExecuteScalar());
                        }
                        finally
                        {
                            base.cmdSelectIdentity.Transaction = null;
                        }
                    }
                    finally
                    {
                        base.cmdSelectIdentity.Connection = null;
                    }
                }
            }
        }

        public class TablePurchaseOrderDetails : ControlDetails.TableDetails
        {
            public DataColumn Col_ID;
            public DataColumn Col_BackOrder;
            public DataColumn Col_Customer;
            public DataColumn Col_DatePromised;
            public DataColumn Col_DateReceived;
            public DataColumn Col_DropShipToCustomer;
            public DataColumn Col_InventoryItemID;
            public DataColumn Col_InventoryItemName;
            public DataColumn Col_Ordered;
            public DataColumn Col_Received;
            public DataColumn Col_Price;
            public DataColumn Col_WarehouseID;
            public DataColumn Col_WarehouseName;

            public TablePurchaseOrderDetails() : this("tbl_purchaseorderdetails")
            {
            }

            public TablePurchaseOrderDetails(string TableName) : base(TableName)
            {
            }

            protected override void Initialize()
            {
                base.Initialize();
                this.Col_ID = base.Columns["ID"];
                this.Col_BackOrder = base.Columns["BackOrder"];
                this.Col_Customer = base.Columns["Customer"];
                this.Col_DatePromised = base.Columns["DatePromised"];
                this.Col_DateReceived = base.Columns["DateReceived"];
                this.Col_DropShipToCustomer = base.Columns["DropShipToCustomer"];
                this.Col_InventoryItemID = base.Columns["InventoryItemID"];
                this.Col_InventoryItemName = base.Columns["InventoryItemName"];
                this.Col_Ordered = base.Columns["Ordered"];
                this.Col_Received = base.Columns["Received"];
                this.Col_Price = base.Columns["Price"];
                this.Col_WarehouseID = base.Columns["WarehouseID"];
                this.Col_WarehouseName = base.Columns["WarehouseName"];
            }

            protected override void InitializeClass()
            {
                base.InitializeClass();
                base.Columns.Add("ID", typeof(int));
                base.Columns.Add("BackOrder", typeof(int));
                base.Columns.Add("Customer", typeof(string));
                base.Columns.Add("DatePromised", typeof(DateTime));
                base.Columns.Add("DateReceived", typeof(DateTime));
                base.Columns.Add("DropShipToCustomer", typeof(bool));
                base.Columns.Add("InventoryItemID", typeof(int));
                base.Columns.Add("InventoryItemName", typeof(string));
                base.Columns.Add("Ordered", typeof(int));
                base.Columns.Add("Received", typeof(int));
                base.Columns.Add("Price", typeof(double));
                base.Columns.Add("WarehouseID", typeof(int));
                base.Columns.Add("WarehouseName", typeof(string));
            }

            public void MergeKeys(ControlPurchaseOrderItems.TablePurchaseOrderDetails table)
            {
                int num2 = table.Rows.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    DataRow row = table.Rows[i];
                    DataRow row2 = base.Rows.Find(row["RowID"]);
                    if (row2 != null)
                    {
                        row2[this.Col_ID] = row["ID"];
                    }
                }
            }
        }
    }
}

