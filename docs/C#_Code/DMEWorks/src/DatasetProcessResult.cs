using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("DatasetProcessResult"), HelpKeyword("vs.data.DataSet")]
public class DatasetProcessResult : DataSet
{
    private CustomersDataTable tableCustomers;
    private OrdersDataTable tableOrders;
    private InvoicesDataTable tableInvoices;
    private DataRelation relationFK_Customers_Orders;
    private DataRelation relationFK_Customers_Invoices;
    private System.Data.SchemaSerializationMode _schemaSerializationMode;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DatasetProcessResult()
    {
        this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        base.BeginInit();
        this.InitClass();
        CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += handler;
        base.Relations.CollectionChanged += handler;
        base.EndInit();
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected DatasetProcessResult(SerializationInfo info, StreamingContext context) : base(info, context, false)
    {
        this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        if (base.IsBinarySerialized(info, context))
        {
            this.InitVars(false);
            CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += handler2;
            this.Relations.CollectionChanged += handler2;
        }
        else
        {
            string s = Conversions.ToString(info.GetValue("XmlSchema", typeof(string)));
            if (base.DetermineSchemaSerializationMode(info, context) != System.Data.SchemaSerializationMode.IncludeSchema)
            {
                base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
            }
            else
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Customers"] != null)
                {
                    base.Tables.Add(new CustomersDataTable(dataSet.Tables["Customers"]));
                }
                if (dataSet.Tables["Orders"] != null)
                {
                    base.Tables.Add(new OrdersDataTable(dataSet.Tables["Orders"]));
                }
                if (dataSet.Tables["Invoices"] != null)
                {
                    base.Tables.Add(new InvoicesDataTable(dataSet.Tables["Invoices"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            base.GetSerializationData(info, context);
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            this.Relations.CollectionChanged += handler;
        }
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataSet Clone()
    {
        DatasetProcessResult result1 = (DatasetProcessResult) base.Clone();
        result1.InitVars();
        result1.SchemaSerializationMode = this.SchemaSerializationMode;
        return result1;
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override XmlSchema GetSchemaSerializable()
    {
        MemoryStream w = new MemoryStream();
        base.WriteXmlSchema(new XmlTextWriter(w, null));
        w.Position = 0L;
        return XmlSchema.Read(new XmlTextReader(w), null);
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
        DatasetProcessResult result = new DatasetProcessResult();
        XmlSchemaComplexType type2 = new XmlSchemaComplexType();
        XmlSchemaSequence sequence = new XmlSchemaSequence();
        XmlSchemaAny item = new XmlSchemaAny {
            Namespace = result.Namespace
        };
        sequence.Items.Add(item);
        type2.Particle = sequence;
        XmlSchema schemaSerializable = result.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            try
            {
                schemaSerializable.Write(stream);
                IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    stream2.SetLength(0L);
                    ((XmlSchema) enumerator.Current).Write(stream2);
                    if (stream.Length == stream2.Length)
                    {
                        stream.Position = 0L;
                        stream2.Position = 0L;
                        while (true)
                        {
                            if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                            {
                                continue;
                            }
                            if (stream.Position != stream.Length)
                            {
                                break;
                            }
                            return type2;
                        }
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
            }
        }
        xs.Add(schemaSerializable);
        return type2;
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
        base.DataSetName = "DatasetProcessResult";
        base.Prefix = "";
        base.Namespace = "http://tempuri.org/DatasetProcessResult.xsd";
        base.EnforceConstraints = true;
        this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.tableCustomers = new CustomersDataTable();
        base.Tables.Add(this.tableCustomers);
        this.tableOrders = new OrdersDataTable();
        base.Tables.Add(this.tableOrders);
        this.tableInvoices = new InvoicesDataTable();
        base.Tables.Add(this.tableInvoices);
        DataColumn[] parentColumns = new DataColumn[] { this.tableCustomers.IDColumn };
        DataColumn[] childColumns = new DataColumn[] { this.tableOrders.CustomerIDColumn };
        ForeignKeyConstraint constraint = new ForeignKeyConstraint("FK_Customers_Orders", parentColumns, childColumns);
        this.tableOrders.Constraints.Add(constraint);
        constraint.AcceptRejectRule = AcceptRejectRule.None;
        constraint.DeleteRule = Rule.Cascade;
        constraint.UpdateRule = Rule.Cascade;
        DataColumn[] columnArray3 = new DataColumn[] { this.tableCustomers.IDColumn };
        DataColumn[] columnArray4 = new DataColumn[] { this.tableInvoices.CustomerIDColumn };
        constraint = new ForeignKeyConstraint("FK_Customers_Invoices", columnArray3, columnArray4);
        this.tableInvoices.Constraints.Add(constraint);
        constraint.AcceptRejectRule = AcceptRejectRule.None;
        constraint.DeleteRule = Rule.Cascade;
        constraint.UpdateRule = Rule.Cascade;
        DataColumn[] columnArray5 = new DataColumn[] { this.tableCustomers.IDColumn };
        DataColumn[] columnArray6 = new DataColumn[] { this.tableOrders.CustomerIDColumn };
        this.relationFK_Customers_Orders = new DataRelation("FK_Customers_Orders", columnArray5, columnArray6, false);
        this.Relations.Add(this.relationFK_Customers_Orders);
        DataColumn[] columnArray7 = new DataColumn[] { this.tableCustomers.IDColumn };
        DataColumn[] columnArray8 = new DataColumn[] { this.tableInvoices.CustomerIDColumn };
        this.relationFK_Customers_Invoices = new DataRelation("FK_Customers_Invoices", columnArray7, columnArray8, false);
        this.Relations.Add(this.relationFK_Customers_Invoices);
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void InitializeDerivedDataSet()
    {
        base.BeginInit();
        this.InitClass();
        base.EndInit();
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars()
    {
        this.InitVars(true);
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
        this.tableCustomers = (CustomersDataTable) base.Tables["Customers"];
        if (initTable && (this.tableCustomers != null))
        {
            this.tableCustomers.InitVars();
        }
        this.tableOrders = (OrdersDataTable) base.Tables["Orders"];
        if (initTable && (this.tableOrders != null))
        {
            this.tableOrders.InitVars();
        }
        this.tableInvoices = (InvoicesDataTable) base.Tables["Invoices"];
        if (initTable && (this.tableInvoices != null))
        {
            this.tableInvoices.InitVars();
        }
        this.relationFK_Customers_Orders = this.Relations["FK_Customers_Orders"];
        this.relationFK_Customers_Invoices = this.Relations["FK_Customers_Invoices"];
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
        if (base.DetermineSchemaSerializationMode(reader) != System.Data.SchemaSerializationMode.IncludeSchema)
        {
            base.ReadXml(reader);
            this.InitVars();
        }
        else
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Customers"] != null)
            {
                base.Tables.Add(new CustomersDataTable(dataSet.Tables["Customers"]));
            }
            if (dataSet.Tables["Orders"] != null)
            {
                base.Tables.Add(new OrdersDataTable(dataSet.Tables["Orders"]));
            }
            if (dataSet.Tables["Invoices"] != null)
            {
                base.Tables.Add(new InvoicesDataTable(dataSet.Tables["Invoices"]));
            }
            base.DataSetName = dataSet.DataSetName;
            base.Prefix = dataSet.Prefix;
            base.Namespace = dataSet.Namespace;
            base.Locale = dataSet.Locale;
            base.CaseSensitive = dataSet.CaseSensitive;
            base.EnforceConstraints = dataSet.EnforceConstraints;
            base.Merge(dataSet, false, MissingSchemaAction.Add);
            this.InitVars();
        }
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
        if (e.Action == CollectionChangeAction.Remove)
        {
            this.InitVars();
        }
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeCustomers() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeInvoices() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeOrders() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeRelations() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CustomersDataTable Customers =>
        this.tableCustomers;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public OrdersDataTable Orders =>
        this.tableOrders;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InvoicesDataTable Invoices =>
        this.tableInvoices;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override System.Data.SchemaSerializationMode SchemaSerializationMode
    {
        get => 
            this._schemaSerializationMode;
        set => 
            this._schemaSerializationMode = value;
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTableCollection Tables =>
        base.Tables;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataRelationCollection Relations =>
        base.Relations;

    [Serializable, XmlSchemaProvider("GetTypedTableSchema")]
    public class CustomersDataTable : TypedTableBase<DatasetProcessResult.CustomersRow>
    {
        private DataColumn columnID;
        private DataColumn columnName;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.CustomersRowChangeEventHandler CustomersRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.CustomersRowChangeEventHandler CustomersRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.CustomersRowChangeEventHandler CustomersRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.CustomersRowChangeEventHandler CustomersRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public CustomersDataTable()
        {
            base.TableName = "Customers";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal CustomersDataTable(DataTable table)
        {
            base.TableName = table.TableName;
            if (table.CaseSensitive != table.DataSet.CaseSensitive)
            {
                base.CaseSensitive = table.CaseSensitive;
            }
            if (table.Locale.ToString() != table.DataSet.Locale.ToString())
            {
                base.Locale = table.Locale;
            }
            if (table.Namespace != table.DataSet.Namespace)
            {
                base.Namespace = table.Namespace;
            }
            base.Prefix = table.Prefix;
            base.MinimumCapacity = table.MinimumCapacity;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected CustomersDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddCustomersRow(DatasetProcessResult.CustomersRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow AddCustomersRow(int ID, string Name)
        {
            DatasetProcessResult.CustomersRow row = (DatasetProcessResult.CustomersRow) base.NewRow();
            row.ItemArray = new object[] { ID, Name };
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetProcessResult.CustomersDataTable table1 = (DatasetProcessResult.CustomersDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetProcessResult.CustomersDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow FindByID(int ID)
        {
            object[] keys = new object[] { ID };
            return (DatasetProcessResult.CustomersRow) base.Rows.Find(keys);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetProcessResult.CustomersRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetProcessResult result = new DatasetProcessResult();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = "http://www.w3.org/2001/XMLSchema",
                MinOccurs = 0M,
                MaxOccurs = decimal.MaxValue,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(item);
            XmlSchemaAny any2 = new XmlSchemaAny {
                Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                MinOccurs = 1M,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(any2);
            XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                Name = "namespace",
                FixedValue = result.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "CustomersDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = result.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        stream2.SetLength(0L);
                        ((XmlSchema) enumerator.Current).Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while (true)
                            {
                                if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                    continue;
                                }
                                if (stream.Position != stream.Length)
                                {
                                    break;
                                }
                                return type2;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type2;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitClass()
        {
            this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnID);
            this.columnName = new DataColumn("Name", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnName);
            DataColumn[] columns = new DataColumn[] { this.columnID };
            base.Constraints.Add(new UniqueConstraint("PK_Customers", columns, true));
            this.columnID.AllowDBNull = false;
            this.columnID.Unique = true;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnID = base.Columns["ID"];
            this.columnName = base.Columns["Name"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow NewCustomersRow() => 
            (DatasetProcessResult.CustomersRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetProcessResult.CustomersRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.CustomersRowChangedEvent != null)
            {
                DatasetProcessResult.CustomersRowChangeEventHandler customersRowChangedEvent = this.CustomersRowChangedEvent;
                if (customersRowChangedEvent != null)
                {
                    customersRowChangedEvent(this, new DatasetProcessResult.CustomersRowChangeEvent((DatasetProcessResult.CustomersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.CustomersRowChangingEvent != null)
            {
                DatasetProcessResult.CustomersRowChangeEventHandler customersRowChangingEvent = this.CustomersRowChangingEvent;
                if (customersRowChangingEvent != null)
                {
                    customersRowChangingEvent(this, new DatasetProcessResult.CustomersRowChangeEvent((DatasetProcessResult.CustomersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.CustomersRowDeletedEvent != null)
            {
                DatasetProcessResult.CustomersRowChangeEventHandler customersRowDeletedEvent = this.CustomersRowDeletedEvent;
                if (customersRowDeletedEvent != null)
                {
                    customersRowDeletedEvent(this, new DatasetProcessResult.CustomersRowChangeEvent((DatasetProcessResult.CustomersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.CustomersRowDeletingEvent != null)
            {
                DatasetProcessResult.CustomersRowChangeEventHandler customersRowDeletingEvent = this.CustomersRowDeletingEvent;
                if (customersRowDeletingEvent != null)
                {
                    customersRowDeletingEvent(this, new DatasetProcessResult.CustomersRowChangeEvent((DatasetProcessResult.CustomersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveCustomersRow(DatasetProcessResult.CustomersRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn IDColumn =>
            this.columnID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn NameColumn =>
            this.columnName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow this[int index] =>
            (DatasetProcessResult.CustomersRow) base.Rows[index];
    }

    public class CustomersRow : DataRow
    {
        private DatasetProcessResult.CustomersDataTable tableCustomers;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal CustomersRow(DataRowBuilder rb) : base(rb)
        {
            this.tableCustomers = (DatasetProcessResult.CustomersDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.InvoicesRow[] GetInvoicesRows() => 
            (base.Table.ChildRelations["FK_Customers_Invoices"] != null) ? ((DatasetProcessResult.InvoicesRow[]) base.GetChildRows(base.Table.ChildRelations["FK_Customers_Invoices"])) : new DatasetProcessResult.InvoicesRow[0];

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.OrdersRow[] GetOrdersRows() => 
            (base.Table.ChildRelations["FK_Customers_Orders"] != null) ? ((DatasetProcessResult.OrdersRow[]) base.GetChildRows(base.Table.ChildRelations["FK_Customers_Orders"])) : new DatasetProcessResult.OrdersRow[0];

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsNameNull() => 
            base.IsNull(this.tableCustomers.NameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetNameNull()
        {
            base[this.tableCustomers.NameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int ID
        {
            get => 
                Conversions.ToInteger(base[this.tableCustomers.IDColumn]);
            set => 
                base[this.tableCustomers.IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Name
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCustomers.NameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Name' in table 'Customers' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCustomers.NameColumn] = value;
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class CustomersRowChangeEvent : EventArgs
    {
        private DatasetProcessResult.CustomersRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public CustomersRowChangeEvent(DatasetProcessResult.CustomersRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void CustomersRowChangeEventHandler(object sender, DatasetProcessResult.CustomersRowChangeEvent e);

    [Serializable, XmlSchemaProvider("GetTypedTableSchema")]
    public class InvoicesDataTable : TypedTableBase<DatasetProcessResult.InvoicesRow>
    {
        private DataColumn columnCustomerID;
        private DataColumn columnID;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.InvoicesRowChangeEventHandler InvoicesRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.InvoicesRowChangeEventHandler InvoicesRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.InvoicesRowChangeEventHandler InvoicesRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.InvoicesRowChangeEventHandler InvoicesRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public InvoicesDataTable()
        {
            base.TableName = "Invoices";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal InvoicesDataTable(DataTable table)
        {
            base.TableName = table.TableName;
            if (table.CaseSensitive != table.DataSet.CaseSensitive)
            {
                base.CaseSensitive = table.CaseSensitive;
            }
            if (table.Locale.ToString() != table.DataSet.Locale.ToString())
            {
                base.Locale = table.Locale;
            }
            if (table.Namespace != table.DataSet.Namespace)
            {
                base.Namespace = table.Namespace;
            }
            base.Prefix = table.Prefix;
            base.MinimumCapacity = table.MinimumCapacity;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected InvoicesDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddInvoicesRow(DatasetProcessResult.InvoicesRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.InvoicesRow AddInvoicesRow(DatasetProcessResult.CustomersRow parentCustomersRowByFK_Customers_Invoices, int ID)
        {
            DatasetProcessResult.InvoicesRow row = (DatasetProcessResult.InvoicesRow) base.NewRow();
            object[] objArray1 = new object[2];
            objArray1[1] = ID;
            object[] objArray = objArray1;
            if (parentCustomersRowByFK_Customers_Invoices != null)
            {
                objArray[0] = parentCustomersRowByFK_Customers_Invoices[0];
            }
            row.ItemArray = objArray;
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetProcessResult.InvoicesDataTable table1 = (DatasetProcessResult.InvoicesDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetProcessResult.InvoicesDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetProcessResult.InvoicesRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetProcessResult result = new DatasetProcessResult();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = "http://www.w3.org/2001/XMLSchema",
                MinOccurs = 0M,
                MaxOccurs = decimal.MaxValue,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(item);
            XmlSchemaAny any2 = new XmlSchemaAny {
                Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                MinOccurs = 1M,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(any2);
            XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                Name = "namespace",
                FixedValue = result.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "InvoicesDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = result.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        stream2.SetLength(0L);
                        ((XmlSchema) enumerator.Current).Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while (true)
                            {
                                if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                    continue;
                                }
                                if (stream.Position != stream.Length)
                                {
                                    break;
                                }
                                return type2;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type2;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitClass()
        {
            this.columnCustomerID = new DataColumn("CustomerID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCustomerID);
            this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnID);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnCustomerID = base.Columns["CustomerID"];
            this.columnID = base.Columns["ID"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.InvoicesRow NewInvoicesRow() => 
            (DatasetProcessResult.InvoicesRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetProcessResult.InvoicesRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.InvoicesRowChangedEvent != null)
            {
                DatasetProcessResult.InvoicesRowChangeEventHandler invoicesRowChangedEvent = this.InvoicesRowChangedEvent;
                if (invoicesRowChangedEvent != null)
                {
                    invoicesRowChangedEvent(this, new DatasetProcessResult.InvoicesRowChangeEvent((DatasetProcessResult.InvoicesRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.InvoicesRowChangingEvent != null)
            {
                DatasetProcessResult.InvoicesRowChangeEventHandler invoicesRowChangingEvent = this.InvoicesRowChangingEvent;
                if (invoicesRowChangingEvent != null)
                {
                    invoicesRowChangingEvent(this, new DatasetProcessResult.InvoicesRowChangeEvent((DatasetProcessResult.InvoicesRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.InvoicesRowDeletedEvent != null)
            {
                DatasetProcessResult.InvoicesRowChangeEventHandler invoicesRowDeletedEvent = this.InvoicesRowDeletedEvent;
                if (invoicesRowDeletedEvent != null)
                {
                    invoicesRowDeletedEvent(this, new DatasetProcessResult.InvoicesRowChangeEvent((DatasetProcessResult.InvoicesRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.InvoicesRowDeletingEvent != null)
            {
                DatasetProcessResult.InvoicesRowChangeEventHandler invoicesRowDeletingEvent = this.InvoicesRowDeletingEvent;
                if (invoicesRowDeletingEvent != null)
                {
                    invoicesRowDeletingEvent(this, new DatasetProcessResult.InvoicesRowChangeEvent((DatasetProcessResult.InvoicesRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveInvoicesRow(DatasetProcessResult.InvoicesRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CustomerIDColumn =>
            this.columnCustomerID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn IDColumn =>
            this.columnID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.InvoicesRow this[int index] =>
            (DatasetProcessResult.InvoicesRow) base.Rows[index];
    }

    public class InvoicesRow : DataRow
    {
        private DatasetProcessResult.InvoicesDataTable tableInvoices;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal InvoicesRow(DataRowBuilder rb) : base(rb)
        {
            this.tableInvoices = (DatasetProcessResult.InvoicesDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomerIDNull() => 
            base.IsNull(this.tableInvoices.CustomerIDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsIDNull() => 
            base.IsNull(this.tableInvoices.IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomerIDNull()
        {
            base[this.tableInvoices.CustomerIDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetIDNull()
        {
            base[this.tableInvoices.IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int CustomerID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableInvoices.CustomerIDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CustomerID' in table 'Invoices' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableInvoices.CustomerIDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableInvoices.IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'ID' in table 'Invoices' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableInvoices.IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow CustomersRow
        {
            get => 
                (DatasetProcessResult.CustomersRow) base.GetParentRow(base.Table.ParentRelations["FK_Customers_Invoices"]);
            set => 
                base.SetParentRow(value, base.Table.ParentRelations["FK_Customers_Invoices"]);
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class InvoicesRowChangeEvent : EventArgs
    {
        private DatasetProcessResult.InvoicesRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public InvoicesRowChangeEvent(DatasetProcessResult.InvoicesRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.InvoicesRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void InvoicesRowChangeEventHandler(object sender, DatasetProcessResult.InvoicesRowChangeEvent e);

    [Serializable, XmlSchemaProvider("GetTypedTableSchema")]
    public class OrdersDataTable : TypedTableBase<DatasetProcessResult.OrdersRow>
    {
        private DataColumn columnCustomerID;
        private DataColumn columnID;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.OrdersRowChangeEventHandler OrdersRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.OrdersRowChangeEventHandler OrdersRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.OrdersRowChangeEventHandler OrdersRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessResult.OrdersRowChangeEventHandler OrdersRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public OrdersDataTable()
        {
            base.TableName = "Orders";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal OrdersDataTable(DataTable table)
        {
            base.TableName = table.TableName;
            if (table.CaseSensitive != table.DataSet.CaseSensitive)
            {
                base.CaseSensitive = table.CaseSensitive;
            }
            if (table.Locale.ToString() != table.DataSet.Locale.ToString())
            {
                base.Locale = table.Locale;
            }
            if (table.Namespace != table.DataSet.Namespace)
            {
                base.Namespace = table.Namespace;
            }
            base.Prefix = table.Prefix;
            base.MinimumCapacity = table.MinimumCapacity;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected OrdersDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddOrdersRow(DatasetProcessResult.OrdersRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.OrdersRow AddOrdersRow(DatasetProcessResult.CustomersRow parentCustomersRowByFK_Customers_Orders, int ID)
        {
            DatasetProcessResult.OrdersRow row = (DatasetProcessResult.OrdersRow) base.NewRow();
            object[] objArray1 = new object[2];
            objArray1[1] = ID;
            object[] objArray = objArray1;
            if (parentCustomersRowByFK_Customers_Orders != null)
            {
                objArray[0] = parentCustomersRowByFK_Customers_Orders[0];
            }
            row.ItemArray = objArray;
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetProcessResult.OrdersDataTable table1 = (DatasetProcessResult.OrdersDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetProcessResult.OrdersDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetProcessResult.OrdersRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetProcessResult result = new DatasetProcessResult();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = "http://www.w3.org/2001/XMLSchema",
                MinOccurs = 0M,
                MaxOccurs = decimal.MaxValue,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(item);
            XmlSchemaAny any2 = new XmlSchemaAny {
                Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                MinOccurs = 1M,
                ProcessContents = XmlSchemaContentProcessing.Lax
            };
            sequence.Items.Add(any2);
            XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                Name = "namespace",
                FixedValue = result.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "OrdersDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = result.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        stream2.SetLength(0L);
                        ((XmlSchema) enumerator.Current).Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while (true)
                            {
                                if ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                    continue;
                                }
                                if (stream.Position != stream.Length)
                                {
                                    break;
                                }
                                return type2;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type2;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitClass()
        {
            this.columnCustomerID = new DataColumn("CustomerID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCustomerID);
            this.columnID = new DataColumn("ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnID);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnCustomerID = base.Columns["CustomerID"];
            this.columnID = base.Columns["ID"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.OrdersRow NewOrdersRow() => 
            (DatasetProcessResult.OrdersRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetProcessResult.OrdersRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.OrdersRowChangedEvent != null)
            {
                DatasetProcessResult.OrdersRowChangeEventHandler ordersRowChangedEvent = this.OrdersRowChangedEvent;
                if (ordersRowChangedEvent != null)
                {
                    ordersRowChangedEvent(this, new DatasetProcessResult.OrdersRowChangeEvent((DatasetProcessResult.OrdersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.OrdersRowChangingEvent != null)
            {
                DatasetProcessResult.OrdersRowChangeEventHandler ordersRowChangingEvent = this.OrdersRowChangingEvent;
                if (ordersRowChangingEvent != null)
                {
                    ordersRowChangingEvent(this, new DatasetProcessResult.OrdersRowChangeEvent((DatasetProcessResult.OrdersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.OrdersRowDeletedEvent != null)
            {
                DatasetProcessResult.OrdersRowChangeEventHandler ordersRowDeletedEvent = this.OrdersRowDeletedEvent;
                if (ordersRowDeletedEvent != null)
                {
                    ordersRowDeletedEvent(this, new DatasetProcessResult.OrdersRowChangeEvent((DatasetProcessResult.OrdersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.OrdersRowDeletingEvent != null)
            {
                DatasetProcessResult.OrdersRowChangeEventHandler ordersRowDeletingEvent = this.OrdersRowDeletingEvent;
                if (ordersRowDeletingEvent != null)
                {
                    ordersRowDeletingEvent(this, new DatasetProcessResult.OrdersRowChangeEvent((DatasetProcessResult.OrdersRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveOrdersRow(DatasetProcessResult.OrdersRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CustomerIDColumn =>
            this.columnCustomerID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn IDColumn =>
            this.columnID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.OrdersRow this[int index] =>
            (DatasetProcessResult.OrdersRow) base.Rows[index];
    }

    public class OrdersRow : DataRow
    {
        private DatasetProcessResult.OrdersDataTable tableOrders;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal OrdersRow(DataRowBuilder rb) : base(rb)
        {
            this.tableOrders = (DatasetProcessResult.OrdersDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomerIDNull() => 
            base.IsNull(this.tableOrders.CustomerIDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsIDNull() => 
            base.IsNull(this.tableOrders.IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomerIDNull()
        {
            base[this.tableOrders.CustomerIDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetIDNull()
        {
            base[this.tableOrders.IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int CustomerID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableOrders.CustomerIDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CustomerID' in table 'Orders' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableOrders.CustomerIDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableOrders.IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'ID' in table 'Orders' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableOrders.IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.CustomersRow CustomersRow
        {
            get => 
                (DatasetProcessResult.CustomersRow) base.GetParentRow(base.Table.ParentRelations["FK_Customers_Orders"]);
            set => 
                base.SetParentRow(value, base.Table.ParentRelations["FK_Customers_Orders"]);
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class OrdersRowChangeEvent : EventArgs
    {
        private DatasetProcessResult.OrdersRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public OrdersRowChangeEvent(DatasetProcessResult.OrdersRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessResult.OrdersRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void OrdersRowChangeEventHandler(object sender, DatasetProcessResult.OrdersRowChangeEvent e);
}

