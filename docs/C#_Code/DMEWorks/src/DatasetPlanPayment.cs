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

[Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("DatasetPlanPayment"), HelpKeyword("vs.data.DataSet")]
public class DatasetPlanPayment : DataSet
{
    private TableInvoiceDetailsDataTable tableTableInvoiceDetails;
    private System.Data.SchemaSerializationMode _schemaSerializationMode;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DatasetPlanPayment()
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
    protected DatasetPlanPayment(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                if (dataSet.Tables["TableInvoiceDetails"] != null)
                {
                    base.Tables.Add(new TableInvoiceDetailsDataTable(dataSet.Tables["TableInvoiceDetails"]));
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
        DatasetPlanPayment payment1 = (DatasetPlanPayment) base.Clone();
        payment1.InitVars();
        payment1.SchemaSerializationMode = this.SchemaSerializationMode;
        return payment1;
    }

    public static DatasetPlanPayment FromXml(string Xml)
    {
        using (StringReader reader = new StringReader(Xml))
        {
            DatasetPlanPayment payment1 = new DatasetPlanPayment();
            payment1.ReadXml(reader, XmlReadMode.IgnoreSchema);
            return payment1;
        }
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
        DatasetPlanPayment payment = new DatasetPlanPayment();
        XmlSchemaComplexType type2 = new XmlSchemaComplexType();
        XmlSchemaSequence sequence = new XmlSchemaSequence();
        XmlSchemaAny item = new XmlSchemaAny {
            Namespace = payment.Namespace
        };
        sequence.Items.Add(item);
        type2.Particle = sequence;
        XmlSchema schemaSerializable = payment.GetSchemaSerializable();
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
        base.DataSetName = "DatasetPlanPayment";
        base.Prefix = "";
        base.Namespace = "http://tempuri.org/DatasetPlanPayment.xsd";
        base.EnforceConstraints = true;
        this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.tableTableInvoiceDetails = new TableInvoiceDetailsDataTable();
        base.Tables.Add(this.tableTableInvoiceDetails);
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
        this.tableTableInvoiceDetails = (TableInvoiceDetailsDataTable) base.Tables["TableInvoiceDetails"];
        if (initTable && (this.tableTableInvoiceDetails != null))
        {
            this.tableTableInvoiceDetails.InitVars();
        }
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
            if (dataSet.Tables["TableInvoiceDetails"] != null)
            {
                base.Tables.Add(new TableInvoiceDetailsDataTable(dataSet.Tables["TableInvoiceDetails"]));
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
    protected override bool ShouldSerializeRelations() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeTableInvoiceDetails() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => 
        false;

    public static string ToXml(DatasetPlanPayment dataset)
    {
        using (StringWriter writer = new StringWriter())
        {
            dataset.WriteXml(writer, XmlWriteMode.IgnoreSchema);
            writer.Flush();
            return writer.ToString();
        }
    }

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TableInvoiceDetailsDataTable TableInvoiceDetails =>
        this.tableTableInvoiceDetails;

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
    public class TableInvoiceDetailsDataTable : TypedTableBase<DatasetPlanPayment.TableInvoiceDetailsRow>
    {
        private DataColumn columnID;
        private DataColumn columnPaidAmount;
        private DataColumn columnTransactionID;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler TableInvoiceDetailsRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler TableInvoiceDetailsRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler TableInvoiceDetailsRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler TableInvoiceDetailsRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public TableInvoiceDetailsDataTable()
        {
            base.TableName = "TableInvoiceDetails";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal TableInvoiceDetailsDataTable(DataTable table)
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
        protected TableInvoiceDetailsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddTableInvoiceDetailsRow(DatasetPlanPayment.TableInvoiceDetailsRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetPlanPayment.TableInvoiceDetailsRow AddTableInvoiceDetailsRow(int ID, double PaidAmount, string TransactionID)
        {
            DatasetPlanPayment.TableInvoiceDetailsRow row = (DatasetPlanPayment.TableInvoiceDetailsRow) base.NewRow();
            row.ItemArray = new object[] { ID, PaidAmount, TransactionID };
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetPlanPayment.TableInvoiceDetailsDataTable table1 = (DatasetPlanPayment.TableInvoiceDetailsDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetPlanPayment.TableInvoiceDetailsDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetPlanPayment.TableInvoiceDetailsRow FindByID(int ID)
        {
            object[] keys = new object[] { ID };
            return (DatasetPlanPayment.TableInvoiceDetailsRow) base.Rows.Find(keys);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetPlanPayment.TableInvoiceDetailsRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetPlanPayment payment = new DatasetPlanPayment();
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
                FixedValue = payment.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "TableInvoiceDetailsDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = payment.GetSchemaSerializable();
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
            this.columnPaidAmount = new DataColumn("PaidAmount", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnPaidAmount);
            this.columnTransactionID = new DataColumn("TransactionID", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnTransactionID);
            DataColumn[] columns = new DataColumn[] { this.columnID };
            base.Constraints.Add(new UniqueConstraint("PK_TableInvoices", columns, true));
            this.columnID.AllowDBNull = false;
            this.columnID.Unique = true;
            this.columnPaidAmount.AllowDBNull = false;
            this.columnTransactionID.AllowDBNull = false;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnID = base.Columns["ID"];
            this.columnPaidAmount = base.Columns["PaidAmount"];
            this.columnTransactionID = base.Columns["TransactionID"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetPlanPayment.TableInvoiceDetailsRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetPlanPayment.TableInvoiceDetailsRow NewTableInvoiceDetailsRow() => 
            (DatasetPlanPayment.TableInvoiceDetailsRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.TableInvoiceDetailsRowChangedEvent != null)
            {
                DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler tableInvoiceDetailsRowChangedEvent = this.TableInvoiceDetailsRowChangedEvent;
                if (tableInvoiceDetailsRowChangedEvent != null)
                {
                    tableInvoiceDetailsRowChangedEvent(this, new DatasetPlanPayment.TableInvoiceDetailsRowChangeEvent((DatasetPlanPayment.TableInvoiceDetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.TableInvoiceDetailsRowChangingEvent != null)
            {
                DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler tableInvoiceDetailsRowChangingEvent = this.TableInvoiceDetailsRowChangingEvent;
                if (tableInvoiceDetailsRowChangingEvent != null)
                {
                    tableInvoiceDetailsRowChangingEvent(this, new DatasetPlanPayment.TableInvoiceDetailsRowChangeEvent((DatasetPlanPayment.TableInvoiceDetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.TableInvoiceDetailsRowDeletedEvent != null)
            {
                DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler tableInvoiceDetailsRowDeletedEvent = this.TableInvoiceDetailsRowDeletedEvent;
                if (tableInvoiceDetailsRowDeletedEvent != null)
                {
                    tableInvoiceDetailsRowDeletedEvent(this, new DatasetPlanPayment.TableInvoiceDetailsRowChangeEvent((DatasetPlanPayment.TableInvoiceDetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.TableInvoiceDetailsRowDeletingEvent != null)
            {
                DatasetPlanPayment.TableInvoiceDetailsRowChangeEventHandler tableInvoiceDetailsRowDeletingEvent = this.TableInvoiceDetailsRowDeletingEvent;
                if (tableInvoiceDetailsRowDeletingEvent != null)
                {
                    tableInvoiceDetailsRowDeletingEvent(this, new DatasetPlanPayment.TableInvoiceDetailsRowChangeEvent((DatasetPlanPayment.TableInvoiceDetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveTableInvoiceDetailsRow(DatasetPlanPayment.TableInvoiceDetailsRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn IDColumn =>
            this.columnID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn PaidAmountColumn =>
            this.columnPaidAmount;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn TransactionIDColumn =>
            this.columnTransactionID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetPlanPayment.TableInvoiceDetailsRow this[int index] =>
            (DatasetPlanPayment.TableInvoiceDetailsRow) base.Rows[index];
    }

    public class TableInvoiceDetailsRow : DataRow
    {
        private DatasetPlanPayment.TableInvoiceDetailsDataTable tableTableInvoiceDetails;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal TableInvoiceDetailsRow(DataRowBuilder rb) : base(rb)
        {
            this.tableTableInvoiceDetails = (DatasetPlanPayment.TableInvoiceDetailsDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int ID
        {
            get => 
                Conversions.ToInteger(base[this.tableTableInvoiceDetails.IDColumn]);
            set => 
                base[this.tableTableInvoiceDetails.IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double PaidAmount
        {
            get => 
                Conversions.ToDouble(base[this.tableTableInvoiceDetails.PaidAmountColumn]);
            set => 
                base[this.tableTableInvoiceDetails.PaidAmountColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string TransactionID
        {
            get => 
                Conversions.ToString(base[this.tableTableInvoiceDetails.TransactionIDColumn]);
            set => 
                base[this.tableTableInvoiceDetails.TransactionIDColumn] = value;
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class TableInvoiceDetailsRowChangeEvent : EventArgs
    {
        private DatasetPlanPayment.TableInvoiceDetailsRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public TableInvoiceDetailsRowChangeEvent(DatasetPlanPayment.TableInvoiceDetailsRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetPlanPayment.TableInvoiceDetailsRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void TableInvoiceDetailsRowChangeEventHandler(object sender, DatasetPlanPayment.TableInvoiceDetailsRowChangeEvent e);
}

