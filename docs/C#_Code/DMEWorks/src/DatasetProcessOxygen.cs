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

[Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("DatasetProcessOxygen"), HelpKeyword("vs.data.DataSet")]
public class DatasetProcessOxygen : DataSet
{
    private SerialsDataTable tableSerials;
    private System.Data.SchemaSerializationMode _schemaSerializationMode;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DatasetProcessOxygen()
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
    protected DatasetProcessOxygen(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                if (dataSet.Tables["Serials"] != null)
                {
                    base.Tables.Add(new SerialsDataTable(dataSet.Tables["Serials"]));
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
        DatasetProcessOxygen oxygen1 = (DatasetProcessOxygen) base.Clone();
        oxygen1.InitVars();
        oxygen1.SchemaSerializationMode = this.SchemaSerializationMode;
        return oxygen1;
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
        DatasetProcessOxygen oxygen = new DatasetProcessOxygen();
        XmlSchemaComplexType type2 = new XmlSchemaComplexType();
        XmlSchemaSequence sequence = new XmlSchemaSequence();
        XmlSchemaAny item = new XmlSchemaAny {
            Namespace = oxygen.Namespace
        };
        sequence.Items.Add(item);
        type2.Particle = sequence;
        XmlSchema schemaSerializable = oxygen.GetSchemaSerializable();
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
        base.DataSetName = "DatasetProcessOxygen";
        base.Prefix = "";
        base.Namespace = "http://tempuri.org/DatasetProcessOxygen.xsd";
        base.EnforceConstraints = true;
        this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.tableSerials = new SerialsDataTable();
        base.Tables.Add(this.tableSerials);
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
        this.tableSerials = (SerialsDataTable) base.Tables["Serials"];
        if (initTable && (this.tableSerials != null))
        {
            this.tableSerials.InitVars();
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
            if (dataSet.Tables["Serials"] != null)
            {
                base.Tables.Add(new SerialsDataTable(dataSet.Tables["Serials"]));
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
    private bool ShouldSerializeSerials() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SerialsDataTable Serials =>
        this.tableSerials;

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
    public class SerialsDataTable : TypedTableBase<DatasetProcessOxygen.SerialsRow>
    {
        private DataColumn columnOperation;
        private DataColumn columnSerialID;
        private DataColumn columnSerialNumber;
        private DataColumn columnStatus;
        private DataColumn columnLotNumber;
        private DataColumn columnPatientName;
        private DataColumn columnWarehouseName;
        private DataColumn columnVendorName;
        private DataColumn columnSelected;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessOxygen.SerialsRowChangeEventHandler SerialsRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessOxygen.SerialsRowChangeEventHandler SerialsRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessOxygen.SerialsRowChangeEventHandler SerialsRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetProcessOxygen.SerialsRowChangeEventHandler SerialsRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public SerialsDataTable()
        {
            base.TableName = "Serials";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal SerialsDataTable(DataTable table)
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
        protected SerialsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddSerialsRow(DatasetProcessOxygen.SerialsRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessOxygen.SerialsRow AddSerialsRow(string Operation, int SerialID, string SerialNumber, string Status, string LotNumber, string PatientName, string WarehouseName, string VendorName, bool Selected)
        {
            DatasetProcessOxygen.SerialsRow row = (DatasetProcessOxygen.SerialsRow) base.NewRow();
            object[] objArray1 = new object[9];
            objArray1[0] = Operation;
            objArray1[1] = SerialID;
            objArray1[2] = SerialNumber;
            objArray1[3] = Status;
            objArray1[4] = LotNumber;
            objArray1[5] = PatientName;
            objArray1[6] = WarehouseName;
            objArray1[7] = VendorName;
            objArray1[8] = Selected;
            row.ItemArray = objArray1;
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetProcessOxygen.SerialsDataTable table1 = (DatasetProcessOxygen.SerialsDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetProcessOxygen.SerialsDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetProcessOxygen.SerialsRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetProcessOxygen oxygen = new DatasetProcessOxygen();
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
                FixedValue = oxygen.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "SerialsDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = oxygen.GetSchemaSerializable();
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
            this.columnOperation = new DataColumn("Operation", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnOperation);
            this.columnSerialID = new DataColumn("SerialID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnSerialID);
            this.columnSerialNumber = new DataColumn("SerialNumber", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnSerialNumber);
            this.columnStatus = new DataColumn("Status", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnStatus);
            this.columnLotNumber = new DataColumn("LotNumber", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnLotNumber);
            this.columnPatientName = new DataColumn("PatientName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnPatientName);
            this.columnWarehouseName = new DataColumn("WarehouseName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnWarehouseName);
            this.columnVendorName = new DataColumn("VendorName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnVendorName);
            this.columnSelected = new DataColumn("Selected", typeof(bool), null, MappingType.Element);
            base.Columns.Add(this.columnSelected);
            this.columnSelected.AllowDBNull = false;
            this.columnSelected.DefaultValue = false;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnOperation = base.Columns["Operation"];
            this.columnSerialID = base.Columns["SerialID"];
            this.columnSerialNumber = base.Columns["SerialNumber"];
            this.columnStatus = base.Columns["Status"];
            this.columnLotNumber = base.Columns["LotNumber"];
            this.columnPatientName = base.Columns["PatientName"];
            this.columnWarehouseName = base.Columns["WarehouseName"];
            this.columnVendorName = base.Columns["VendorName"];
            this.columnSelected = base.Columns["Selected"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetProcessOxygen.SerialsRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessOxygen.SerialsRow NewSerialsRow() => 
            (DatasetProcessOxygen.SerialsRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.SerialsRowChangedEvent != null)
            {
                DatasetProcessOxygen.SerialsRowChangeEventHandler serialsRowChangedEvent = this.SerialsRowChangedEvent;
                if (serialsRowChangedEvent != null)
                {
                    serialsRowChangedEvent(this, new DatasetProcessOxygen.SerialsRowChangeEvent((DatasetProcessOxygen.SerialsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.SerialsRowChangingEvent != null)
            {
                DatasetProcessOxygen.SerialsRowChangeEventHandler serialsRowChangingEvent = this.SerialsRowChangingEvent;
                if (serialsRowChangingEvent != null)
                {
                    serialsRowChangingEvent(this, new DatasetProcessOxygen.SerialsRowChangeEvent((DatasetProcessOxygen.SerialsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.SerialsRowDeletedEvent != null)
            {
                DatasetProcessOxygen.SerialsRowChangeEventHandler serialsRowDeletedEvent = this.SerialsRowDeletedEvent;
                if (serialsRowDeletedEvent != null)
                {
                    serialsRowDeletedEvent(this, new DatasetProcessOxygen.SerialsRowChangeEvent((DatasetProcessOxygen.SerialsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.SerialsRowDeletingEvent != null)
            {
                DatasetProcessOxygen.SerialsRowChangeEventHandler serialsRowDeletingEvent = this.SerialsRowDeletingEvent;
                if (serialsRowDeletingEvent != null)
                {
                    serialsRowDeletingEvent(this, new DatasetProcessOxygen.SerialsRowChangeEvent((DatasetProcessOxygen.SerialsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveSerialsRow(DatasetProcessOxygen.SerialsRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn OperationColumn =>
            this.columnOperation;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn SerialIDColumn =>
            this.columnSerialID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn SerialNumberColumn =>
            this.columnSerialNumber;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn StatusColumn =>
            this.columnStatus;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn LotNumberColumn =>
            this.columnLotNumber;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn PatientNameColumn =>
            this.columnPatientName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn WarehouseNameColumn =>
            this.columnWarehouseName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn VendorNameColumn =>
            this.columnVendorName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn SelectedColumn =>
            this.columnSelected;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessOxygen.SerialsRow this[int index] =>
            (DatasetProcessOxygen.SerialsRow) base.Rows[index];
    }

    public class SerialsRow : DataRow
    {
        private DatasetProcessOxygen.SerialsDataTable tableSerials;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal SerialsRow(DataRowBuilder rb) : base(rb)
        {
            this.tableSerials = (DatasetProcessOxygen.SerialsDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsLotNumberNull() => 
            base.IsNull(this.tableSerials.LotNumberColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsOperationNull() => 
            base.IsNull(this.tableSerials.OperationColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsPatientNameNull() => 
            base.IsNull(this.tableSerials.PatientNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsSerialIDNull() => 
            base.IsNull(this.tableSerials.SerialIDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsSerialNumberNull() => 
            base.IsNull(this.tableSerials.SerialNumberColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsStatusNull() => 
            base.IsNull(this.tableSerials.StatusColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsVendorNameNull() => 
            base.IsNull(this.tableSerials.VendorNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsWarehouseNameNull() => 
            base.IsNull(this.tableSerials.WarehouseNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetLotNumberNull()
        {
            base[this.tableSerials.LotNumberColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetOperationNull()
        {
            base[this.tableSerials.OperationColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetPatientNameNull()
        {
            base[this.tableSerials.PatientNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetSerialIDNull()
        {
            base[this.tableSerials.SerialIDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetSerialNumberNull()
        {
            base[this.tableSerials.SerialNumberColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetStatusNull()
        {
            base[this.tableSerials.StatusColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetVendorNameNull()
        {
            base[this.tableSerials.VendorNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetWarehouseNameNull()
        {
            base[this.tableSerials.WarehouseNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Operation
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.OperationColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Operation' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.OperationColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int SerialID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableSerials.SerialIDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'SerialID' in table 'Serials' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableSerials.SerialIDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string SerialNumber
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.SerialNumberColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'SerialNumber' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.SerialNumberColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Status
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.StatusColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Status' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.StatusColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string LotNumber
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.LotNumberColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'LotNumber' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.LotNumberColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string PatientName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.PatientNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'PatientName' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.PatientNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string WarehouseName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.WarehouseNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'WarehouseName' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.WarehouseNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string VendorName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableSerials.VendorNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'VendorName' in table 'Serials' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableSerials.VendorNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool Selected
        {
            get => 
                Conversions.ToBoolean(base[this.tableSerials.SelectedColumn]);
            set => 
                base[this.tableSerials.SelectedColumn] = value;
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class SerialsRowChangeEvent : EventArgs
    {
        private DatasetProcessOxygen.SerialsRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public SerialsRowChangeEvent(DatasetProcessOxygen.SerialsRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetProcessOxygen.SerialsRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void SerialsRowChangeEventHandler(object sender, DatasetProcessOxygen.SerialsRowChangeEvent e);
}

