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

[Serializable, DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), XmlRoot("DatasetSecureCareExport2"), HelpKeyword("vs.data.DataSet")]
public class DatasetSecureCareExport2 : DataSet
{
    private CMNFormDataTable tableCMNForm;
    private DetailsDataTable tableDetails;
    private System.Data.SchemaSerializationMode _schemaSerializationMode;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DatasetSecureCareExport2()
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
    protected DatasetSecureCareExport2(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                if (dataSet.Tables["CMNForm"] != null)
                {
                    base.Tables.Add(new CMNFormDataTable(dataSet.Tables["CMNForm"]));
                }
                if (dataSet.Tables["Details"] != null)
                {
                    base.Tables.Add(new DetailsDataTable(dataSet.Tables["Details"]));
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
        DatasetSecureCareExport2 export1 = (DatasetSecureCareExport2) base.Clone();
        export1.InitVars();
        export1.SchemaSerializationMode = this.SchemaSerializationMode;
        return export1;
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
        DatasetSecureCareExport2 export = new DatasetSecureCareExport2();
        XmlSchemaComplexType type2 = new XmlSchemaComplexType();
        XmlSchemaSequence sequence = new XmlSchemaSequence();
        XmlSchemaAny item = new XmlSchemaAny {
            Namespace = export.Namespace
        };
        sequence.Items.Add(item);
        type2.Particle = sequence;
        XmlSchema schemaSerializable = export.GetSchemaSerializable();
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
        base.DataSetName = "DatasetSecureCareExport2";
        base.Prefix = "";
        base.EnforceConstraints = true;
        this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.tableCMNForm = new CMNFormDataTable();
        base.Tables.Add(this.tableCMNForm);
        this.tableDetails = new DetailsDataTable();
        base.Tables.Add(this.tableDetails);
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
        this.tableCMNForm = (CMNFormDataTable) base.Tables["CMNForm"];
        if (initTable && (this.tableCMNForm != null))
        {
            this.tableCMNForm.InitVars();
        }
        this.tableDetails = (DetailsDataTable) base.Tables["Details"];
        if (initTable && (this.tableDetails != null))
        {
            this.tableDetails.InitVars();
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
            if (dataSet.Tables["CMNForm"] != null)
            {
                base.Tables.Add(new CMNFormDataTable(dataSet.Tables["CMNForm"]));
            }
            if (dataSet.Tables["Details"] != null)
            {
                base.Tables.Add(new DetailsDataTable(dataSet.Tables["Details"]));
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
    private bool ShouldSerializeCMNForm() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeDetails() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeRelations() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => 
        false;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CMNFormDataTable CMNForm =>
        this.tableCMNForm;

    [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DetailsDataTable Details =>
        this.tableDetails;

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
    public class CMNFormDataTable : TypedTableBase<DatasetSecureCareExport2.CMNFormRow>
    {
        private DataColumn columnChecked;
        private DataColumn columnCMNType;
        private DataColumn columnCMNForm_ID;
        private DataColumn columnCMNForm_HCFAType;
        private DataColumn columnCMNForm_InitialDate;
        private DataColumn columnCMNForm_RevisedDate;
        private DataColumn columnCMNForm_RecertificationDate;
        private DataColumn columnCustomer_ID;
        private DataColumn columnCustomer_FirstName;
        private DataColumn columnCustomer_LastName;
        private DataColumn columnCustomer_MiddleName;
        private DataColumn columnCustomer_WholeName;
        private DataColumn columnCustomer_Address1;
        private DataColumn columnCustomer_Address2;
        private DataColumn columnCustomer_City;
        private DataColumn columnCustomer_State;
        private DataColumn columnCustomer_Zip;
        private DataColumn columnCustomer_Phone;
        private DataColumn columnCustomer_DOB;
        private DataColumn columnCustomer_Gender;
        private DataColumn columnCustomer_Height;
        private DataColumn columnCustomer_Weight;
        private DataColumn columnCustomer_SSN;
        private DataColumn columnCustomer_HIC_Number;
        private DataColumn columnCompany_ID;
        private DataColumn columnCompany_Name;
        private DataColumn columnCompany_Address1;
        private DataColumn columnCompany_Address2;
        private DataColumn columnCompany_City;
        private DataColumn columnCompany_State;
        private DataColumn columnCompany_Zip;
        private DataColumn columnCompany_Phone;
        private DataColumn columnCompany_NSC;
        private DataColumn columnDoctor_ID;
        private DataColumn columnDoctor_FirstName;
        private DataColumn columnDoctor_LastName;
        private DataColumn columnDoctor_MiddleName;
        private DataColumn columnDoctor_WholeName;
        private DataColumn columnDoctor_Address1;
        private DataColumn columnDoctor_Address2;
        private DataColumn columnDoctor_City;
        private DataColumn columnDoctor_State;
        private DataColumn columnDoctor_Zip;
        private DataColumn columnDoctor_Phone;
        private DataColumn columnDoctor_UPIN;
        private DataColumn columnFacility_ID;
        private DataColumn columnFacility_Name;
        private DataColumn columnFacility_Address1;
        private DataColumn columnFacility_Address2;
        private DataColumn columnFacility_City;
        private DataColumn columnFacility_State;
        private DataColumn columnFacility_Zip;
        private DataColumn columnFacility_Code;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.CMNFormRowChangeEventHandler CMNFormRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.CMNFormRowChangeEventHandler CMNFormRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.CMNFormRowChangeEventHandler CMNFormRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.CMNFormRowChangeEventHandler CMNFormRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public CMNFormDataTable()
        {
            base.TableName = "CMNForm";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal CMNFormDataTable(DataTable table)
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
        protected CMNFormDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddCMNFormRow(DatasetSecureCareExport2.CMNFormRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.CMNFormRow AddCMNFormRow(bool Checked, string CMNType, int CMNForm_ID, string CMNForm_HCFAType, DateTime CMNForm_InitialDate, DateTime CMNForm_RevisedDate, DateTime CMNForm_RecertificationDate, int Customer_ID, string Customer_FirstName, string Customer_LastName, string Customer_MiddleName, string Customer_WholeName, string Customer_Address1, string Customer_Address2, string Customer_City, string Customer_State, string Customer_Zip, string Customer_Phone, DateTime Customer_DOB, string Customer_Gender, double Customer_Height, double Customer_Weight, string Customer_SSN, string Customer_HIC_Number, int Company_ID, string Company_Name, string Company_Address1, string Company_Address2, string Company_City, string Company_State, string Company_Zip, string Company_Phone, string Company_NSC, int Doctor_ID, string Doctor_FirstName, string Doctor_LastName, string Doctor_MiddleName, string Doctor_WholeName, string Doctor_Address1, string Doctor_Address2, string Doctor_City, string Doctor_State, string Doctor_Zip, string Doctor_Phone, string Doctor_UPIN, int Facility_ID, string Facility_Name, string Facility_Address1, string Facility_Address2, string Facility_City, string Facility_State, string Facility_Zip, string Facility_Code)
        {
            DatasetSecureCareExport2.CMNFormRow row = (DatasetSecureCareExport2.CMNFormRow) base.NewRow();
            object[] objArray1 = new object[0x35];
            objArray1[0] = Checked;
            objArray1[1] = CMNType;
            objArray1[2] = CMNForm_ID;
            objArray1[3] = CMNForm_HCFAType;
            objArray1[4] = CMNForm_InitialDate;
            objArray1[5] = CMNForm_RevisedDate;
            objArray1[6] = CMNForm_RecertificationDate;
            objArray1[7] = Customer_ID;
            objArray1[8] = Customer_FirstName;
            objArray1[9] = Customer_LastName;
            objArray1[10] = Customer_MiddleName;
            objArray1[11] = Customer_WholeName;
            objArray1[12] = Customer_Address1;
            objArray1[13] = Customer_Address2;
            objArray1[14] = Customer_City;
            objArray1[15] = Customer_State;
            objArray1[0x10] = Customer_Zip;
            objArray1[0x11] = Customer_Phone;
            objArray1[0x12] = Customer_DOB;
            objArray1[0x13] = Customer_Gender;
            objArray1[20] = Customer_Height;
            objArray1[0x15] = Customer_Weight;
            objArray1[0x16] = Customer_SSN;
            objArray1[0x17] = Customer_HIC_Number;
            objArray1[0x18] = Company_ID;
            objArray1[0x19] = Company_Name;
            objArray1[0x1a] = Company_Address1;
            objArray1[0x1b] = Company_Address2;
            objArray1[0x1c] = Company_City;
            objArray1[0x1d] = Company_State;
            objArray1[30] = Company_Zip;
            objArray1[0x1f] = Company_Phone;
            objArray1[0x20] = Company_NSC;
            objArray1[0x21] = Doctor_ID;
            objArray1[0x22] = Doctor_FirstName;
            objArray1[0x23] = Doctor_LastName;
            objArray1[0x24] = Doctor_MiddleName;
            objArray1[0x25] = Doctor_WholeName;
            objArray1[0x26] = Doctor_Address1;
            objArray1[0x27] = Doctor_Address2;
            objArray1[40] = Doctor_City;
            objArray1[0x29] = Doctor_State;
            objArray1[0x2a] = Doctor_Zip;
            objArray1[0x2b] = Doctor_Phone;
            objArray1[0x2c] = Doctor_UPIN;
            objArray1[0x2d] = Facility_ID;
            objArray1[0x2e] = Facility_Name;
            objArray1[0x2f] = Facility_Address1;
            objArray1[0x30] = Facility_Address2;
            objArray1[0x31] = Facility_City;
            objArray1[50] = Facility_State;
            objArray1[0x33] = Facility_Zip;
            objArray1[0x34] = Facility_Code;
            row.ItemArray = objArray1;
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetSecureCareExport2.CMNFormDataTable table1 = (DatasetSecureCareExport2.CMNFormDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetSecureCareExport2.CMNFormDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetSecureCareExport2.CMNFormRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetSecureCareExport2 export = new DatasetSecureCareExport2();
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
                FixedValue = export.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "CMNFormDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = export.GetSchemaSerializable();
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
            this.columnChecked = new DataColumn("Checked", typeof(bool), null, MappingType.Element);
            base.Columns.Add(this.columnChecked);
            this.columnCMNType = new DataColumn("CMNType", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCMNType);
            this.columnCMNForm_ID = new DataColumn("CMNForm_ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCMNForm_ID);
            this.columnCMNForm_HCFAType = new DataColumn("CMNForm_HCFAType", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCMNForm_HCFAType);
            this.columnCMNForm_InitialDate = new DataColumn("CMNForm_InitialDate", typeof(DateTime), null, MappingType.Element);
            base.Columns.Add(this.columnCMNForm_InitialDate);
            this.columnCMNForm_RevisedDate = new DataColumn("CMNForm_RevisedDate", typeof(DateTime), null, MappingType.Element);
            base.Columns.Add(this.columnCMNForm_RevisedDate);
            this.columnCMNForm_RecertificationDate = new DataColumn("CMNForm_RecertificationDate", typeof(DateTime), null, MappingType.Element);
            base.Columns.Add(this.columnCMNForm_RecertificationDate);
            this.columnCustomer_ID = new DataColumn("Customer_ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_ID);
            this.columnCustomer_FirstName = new DataColumn("Customer_FirstName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_FirstName);
            this.columnCustomer_LastName = new DataColumn("Customer_LastName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_LastName);
            this.columnCustomer_MiddleName = new DataColumn("Customer_MiddleName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_MiddleName);
            this.columnCustomer_WholeName = new DataColumn("Customer_WholeName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_WholeName);
            this.columnCustomer_Address1 = new DataColumn("Customer_Address1", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Address1);
            this.columnCustomer_Address2 = new DataColumn("Customer_Address2", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Address2);
            this.columnCustomer_City = new DataColumn("Customer_City", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_City);
            this.columnCustomer_State = new DataColumn("Customer_State", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_State);
            this.columnCustomer_Zip = new DataColumn("Customer_Zip", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Zip);
            this.columnCustomer_Phone = new DataColumn("Customer_Phone", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Phone);
            this.columnCustomer_DOB = new DataColumn("Customer_DOB", typeof(DateTime), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_DOB);
            this.columnCustomer_Gender = new DataColumn("Customer_Gender", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Gender);
            this.columnCustomer_Height = new DataColumn("Customer_Height", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Height);
            this.columnCustomer_Weight = new DataColumn("Customer_Weight", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_Weight);
            this.columnCustomer_SSN = new DataColumn("Customer_SSN", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_SSN);
            this.columnCustomer_HIC_Number = new DataColumn("Customer_HIC_Number", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCustomer_HIC_Number);
            this.columnCompany_ID = new DataColumn("Company_ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_ID);
            this.columnCompany_Name = new DataColumn("Company_Name", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_Name);
            this.columnCompany_Address1 = new DataColumn("Company_Address1", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_Address1);
            this.columnCompany_Address2 = new DataColumn("Company_Address2", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_Address2);
            this.columnCompany_City = new DataColumn("Company_City", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_City);
            this.columnCompany_State = new DataColumn("Company_State", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_State);
            this.columnCompany_Zip = new DataColumn("Company_Zip", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_Zip);
            this.columnCompany_Phone = new DataColumn("Company_Phone", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_Phone);
            this.columnCompany_NSC = new DataColumn("Company_NSC", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnCompany_NSC);
            this.columnDoctor_ID = new DataColumn("Doctor_ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_ID);
            this.columnDoctor_FirstName = new DataColumn("Doctor_FirstName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_FirstName);
            this.columnDoctor_LastName = new DataColumn("Doctor_LastName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_LastName);
            this.columnDoctor_MiddleName = new DataColumn("Doctor_MiddleName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_MiddleName);
            this.columnDoctor_WholeName = new DataColumn("Doctor_WholeName", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_WholeName);
            this.columnDoctor_Address1 = new DataColumn("Doctor_Address1", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_Address1);
            this.columnDoctor_Address2 = new DataColumn("Doctor_Address2", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_Address2);
            this.columnDoctor_City = new DataColumn("Doctor_City", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_City);
            this.columnDoctor_State = new DataColumn("Doctor_State", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_State);
            this.columnDoctor_Zip = new DataColumn("Doctor_Zip", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_Zip);
            this.columnDoctor_Phone = new DataColumn("Doctor_Phone", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_Phone);
            this.columnDoctor_UPIN = new DataColumn("Doctor_UPIN", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnDoctor_UPIN);
            this.columnFacility_ID = new DataColumn("Facility_ID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_ID);
            this.columnFacility_Name = new DataColumn("Facility_Name", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_Name);
            this.columnFacility_Address1 = new DataColumn("Facility_Address1", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_Address1);
            this.columnFacility_Address2 = new DataColumn("Facility_Address2", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_Address2);
            this.columnFacility_City = new DataColumn("Facility_City", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_City);
            this.columnFacility_State = new DataColumn("Facility_State", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_State);
            this.columnFacility_Zip = new DataColumn("Facility_Zip", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_Zip);
            this.columnFacility_Code = new DataColumn("Facility_Code", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnFacility_Code);
            DataColumn[] columns = new DataColumn[] { this.columnCMNForm_ID };
            base.Constraints.Add(new UniqueConstraint("PK_CMNForm", columns, false));
            this.columnChecked.AllowDBNull = false;
            this.columnChecked.DefaultValue = false;
            this.columnCMNForm_ID.AllowDBNull = false;
            this.columnCMNForm_ID.Unique = true;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnChecked = base.Columns["Checked"];
            this.columnCMNType = base.Columns["CMNType"];
            this.columnCMNForm_ID = base.Columns["CMNForm_ID"];
            this.columnCMNForm_HCFAType = base.Columns["CMNForm_HCFAType"];
            this.columnCMNForm_InitialDate = base.Columns["CMNForm_InitialDate"];
            this.columnCMNForm_RevisedDate = base.Columns["CMNForm_RevisedDate"];
            this.columnCMNForm_RecertificationDate = base.Columns["CMNForm_RecertificationDate"];
            this.columnCustomer_ID = base.Columns["Customer_ID"];
            this.columnCustomer_FirstName = base.Columns["Customer_FirstName"];
            this.columnCustomer_LastName = base.Columns["Customer_LastName"];
            this.columnCustomer_MiddleName = base.Columns["Customer_MiddleName"];
            this.columnCustomer_WholeName = base.Columns["Customer_WholeName"];
            this.columnCustomer_Address1 = base.Columns["Customer_Address1"];
            this.columnCustomer_Address2 = base.Columns["Customer_Address2"];
            this.columnCustomer_City = base.Columns["Customer_City"];
            this.columnCustomer_State = base.Columns["Customer_State"];
            this.columnCustomer_Zip = base.Columns["Customer_Zip"];
            this.columnCustomer_Phone = base.Columns["Customer_Phone"];
            this.columnCustomer_DOB = base.Columns["Customer_DOB"];
            this.columnCustomer_Gender = base.Columns["Customer_Gender"];
            this.columnCustomer_Height = base.Columns["Customer_Height"];
            this.columnCustomer_Weight = base.Columns["Customer_Weight"];
            this.columnCustomer_SSN = base.Columns["Customer_SSN"];
            this.columnCustomer_HIC_Number = base.Columns["Customer_HIC_Number"];
            this.columnCompany_ID = base.Columns["Company_ID"];
            this.columnCompany_Name = base.Columns["Company_Name"];
            this.columnCompany_Address1 = base.Columns["Company_Address1"];
            this.columnCompany_Address2 = base.Columns["Company_Address2"];
            this.columnCompany_City = base.Columns["Company_City"];
            this.columnCompany_State = base.Columns["Company_State"];
            this.columnCompany_Zip = base.Columns["Company_Zip"];
            this.columnCompany_Phone = base.Columns["Company_Phone"];
            this.columnCompany_NSC = base.Columns["Company_NSC"];
            this.columnDoctor_ID = base.Columns["Doctor_ID"];
            this.columnDoctor_FirstName = base.Columns["Doctor_FirstName"];
            this.columnDoctor_LastName = base.Columns["Doctor_LastName"];
            this.columnDoctor_MiddleName = base.Columns["Doctor_MiddleName"];
            this.columnDoctor_WholeName = base.Columns["Doctor_WholeName"];
            this.columnDoctor_Address1 = base.Columns["Doctor_Address1"];
            this.columnDoctor_Address2 = base.Columns["Doctor_Address2"];
            this.columnDoctor_City = base.Columns["Doctor_City"];
            this.columnDoctor_State = base.Columns["Doctor_State"];
            this.columnDoctor_Zip = base.Columns["Doctor_Zip"];
            this.columnDoctor_Phone = base.Columns["Doctor_Phone"];
            this.columnDoctor_UPIN = base.Columns["Doctor_UPIN"];
            this.columnFacility_ID = base.Columns["Facility_ID"];
            this.columnFacility_Name = base.Columns["Facility_Name"];
            this.columnFacility_Address1 = base.Columns["Facility_Address1"];
            this.columnFacility_Address2 = base.Columns["Facility_Address2"];
            this.columnFacility_City = base.Columns["Facility_City"];
            this.columnFacility_State = base.Columns["Facility_State"];
            this.columnFacility_Zip = base.Columns["Facility_Zip"];
            this.columnFacility_Code = base.Columns["Facility_Code"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.CMNFormRow NewCMNFormRow() => 
            (DatasetSecureCareExport2.CMNFormRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetSecureCareExport2.CMNFormRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.CMNFormRowChangedEvent != null)
            {
                DatasetSecureCareExport2.CMNFormRowChangeEventHandler cMNFormRowChangedEvent = this.CMNFormRowChangedEvent;
                if (cMNFormRowChangedEvent != null)
                {
                    cMNFormRowChangedEvent(this, new DatasetSecureCareExport2.CMNFormRowChangeEvent((DatasetSecureCareExport2.CMNFormRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.CMNFormRowChangingEvent != null)
            {
                DatasetSecureCareExport2.CMNFormRowChangeEventHandler cMNFormRowChangingEvent = this.CMNFormRowChangingEvent;
                if (cMNFormRowChangingEvent != null)
                {
                    cMNFormRowChangingEvent(this, new DatasetSecureCareExport2.CMNFormRowChangeEvent((DatasetSecureCareExport2.CMNFormRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.CMNFormRowDeletedEvent != null)
            {
                DatasetSecureCareExport2.CMNFormRowChangeEventHandler cMNFormRowDeletedEvent = this.CMNFormRowDeletedEvent;
                if (cMNFormRowDeletedEvent != null)
                {
                    cMNFormRowDeletedEvent(this, new DatasetSecureCareExport2.CMNFormRowChangeEvent((DatasetSecureCareExport2.CMNFormRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.CMNFormRowDeletingEvent != null)
            {
                DatasetSecureCareExport2.CMNFormRowChangeEventHandler cMNFormRowDeletingEvent = this.CMNFormRowDeletingEvent;
                if (cMNFormRowDeletingEvent != null)
                {
                    cMNFormRowDeletingEvent(this, new DatasetSecureCareExport2.CMNFormRowChangeEvent((DatasetSecureCareExport2.CMNFormRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveCMNFormRow(DatasetSecureCareExport2.CMNFormRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CheckedColumn =>
            this.columnChecked;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNTypeColumn =>
            this.columnCMNType;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNForm_IDColumn =>
            this.columnCMNForm_ID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNForm_HCFATypeColumn =>
            this.columnCMNForm_HCFAType;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNForm_InitialDateColumn =>
            this.columnCMNForm_InitialDate;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNForm_RevisedDateColumn =>
            this.columnCMNForm_RevisedDate;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNForm_RecertificationDateColumn =>
            this.columnCMNForm_RecertificationDate;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_IDColumn =>
            this.columnCustomer_ID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_FirstNameColumn =>
            this.columnCustomer_FirstName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_LastNameColumn =>
            this.columnCustomer_LastName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_MiddleNameColumn =>
            this.columnCustomer_MiddleName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_WholeNameColumn =>
            this.columnCustomer_WholeName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_Address1Column =>
            this.columnCustomer_Address1;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_Address2Column =>
            this.columnCustomer_Address2;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_CityColumn =>
            this.columnCustomer_City;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_StateColumn =>
            this.columnCustomer_State;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_ZipColumn =>
            this.columnCustomer_Zip;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_PhoneColumn =>
            this.columnCustomer_Phone;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_DOBColumn =>
            this.columnCustomer_DOB;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_GenderColumn =>
            this.columnCustomer_Gender;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_HeightColumn =>
            this.columnCustomer_Height;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_WeightColumn =>
            this.columnCustomer_Weight;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_SSNColumn =>
            this.columnCustomer_SSN;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Customer_HIC_NumberColumn =>
            this.columnCustomer_HIC_Number;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_IDColumn =>
            this.columnCompany_ID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_NameColumn =>
            this.columnCompany_Name;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_Address1Column =>
            this.columnCompany_Address1;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_Address2Column =>
            this.columnCompany_Address2;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_CityColumn =>
            this.columnCompany_City;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_StateColumn =>
            this.columnCompany_State;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_ZipColumn =>
            this.columnCompany_Zip;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_PhoneColumn =>
            this.columnCompany_Phone;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Company_NSCColumn =>
            this.columnCompany_NSC;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_IDColumn =>
            this.columnDoctor_ID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_FirstNameColumn =>
            this.columnDoctor_FirstName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_LastNameColumn =>
            this.columnDoctor_LastName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_MiddleNameColumn =>
            this.columnDoctor_MiddleName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_WholeNameColumn =>
            this.columnDoctor_WholeName;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_Address1Column =>
            this.columnDoctor_Address1;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_Address2Column =>
            this.columnDoctor_Address2;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_CityColumn =>
            this.columnDoctor_City;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_StateColumn =>
            this.columnDoctor_State;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_ZipColumn =>
            this.columnDoctor_Zip;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_PhoneColumn =>
            this.columnDoctor_Phone;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Doctor_UPINColumn =>
            this.columnDoctor_UPIN;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_IDColumn =>
            this.columnFacility_ID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_NameColumn =>
            this.columnFacility_Name;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_Address1Column =>
            this.columnFacility_Address1;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_Address2Column =>
            this.columnFacility_Address2;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_CityColumn =>
            this.columnFacility_City;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_StateColumn =>
            this.columnFacility_State;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_ZipColumn =>
            this.columnFacility_Zip;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn Facility_CodeColumn =>
            this.columnFacility_Code;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.CMNFormRow this[int index] =>
            (DatasetSecureCareExport2.CMNFormRow) base.Rows[index];
    }

    public class CMNFormRow : DataRow
    {
        private DatasetSecureCareExport2.CMNFormDataTable tableCMNForm;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal CMNFormRow(DataRowBuilder rb) : base(rb)
        {
            this.tableCMNForm = (DatasetSecureCareExport2.CMNFormDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCMNForm_HCFATypeNull() => 
            base.IsNull(this.tableCMNForm.CMNForm_HCFATypeColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCMNForm_InitialDateNull() => 
            base.IsNull(this.tableCMNForm.CMNForm_InitialDateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCMNForm_RecertificationDateNull() => 
            base.IsNull(this.tableCMNForm.CMNForm_RecertificationDateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCMNForm_RevisedDateNull() => 
            base.IsNull(this.tableCMNForm.CMNForm_RevisedDateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCMNTypeNull() => 
            base.IsNull(this.tableCMNForm.CMNTypeColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_Address1Null() => 
            base.IsNull(this.tableCMNForm.Company_Address1Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_Address2Null() => 
            base.IsNull(this.tableCMNForm.Company_Address2Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_CityNull() => 
            base.IsNull(this.tableCMNForm.Company_CityColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_IDNull() => 
            base.IsNull(this.tableCMNForm.Company_IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_NameNull() => 
            base.IsNull(this.tableCMNForm.Company_NameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_NSCNull() => 
            base.IsNull(this.tableCMNForm.Company_NSCColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_PhoneNull() => 
            base.IsNull(this.tableCMNForm.Company_PhoneColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_StateNull() => 
            base.IsNull(this.tableCMNForm.Company_StateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCompany_ZipNull() => 
            base.IsNull(this.tableCMNForm.Company_ZipColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_Address1Null() => 
            base.IsNull(this.tableCMNForm.Customer_Address1Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_Address2Null() => 
            base.IsNull(this.tableCMNForm.Customer_Address2Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_CityNull() => 
            base.IsNull(this.tableCMNForm.Customer_CityColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_DOBNull() => 
            base.IsNull(this.tableCMNForm.Customer_DOBColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_FirstNameNull() => 
            base.IsNull(this.tableCMNForm.Customer_FirstNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_GenderNull() => 
            base.IsNull(this.tableCMNForm.Customer_GenderColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_HeightNull() => 
            base.IsNull(this.tableCMNForm.Customer_HeightColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_HIC_NumberNull() => 
            base.IsNull(this.tableCMNForm.Customer_HIC_NumberColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_IDNull() => 
            base.IsNull(this.tableCMNForm.Customer_IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_LastNameNull() => 
            base.IsNull(this.tableCMNForm.Customer_LastNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_MiddleNameNull() => 
            base.IsNull(this.tableCMNForm.Customer_MiddleNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_PhoneNull() => 
            base.IsNull(this.tableCMNForm.Customer_PhoneColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_SSNNull() => 
            base.IsNull(this.tableCMNForm.Customer_SSNColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_StateNull() => 
            base.IsNull(this.tableCMNForm.Customer_StateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_WeightNull() => 
            base.IsNull(this.tableCMNForm.Customer_WeightColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_WholeNameNull() => 
            base.IsNull(this.tableCMNForm.Customer_WholeNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsCustomer_ZipNull() => 
            base.IsNull(this.tableCMNForm.Customer_ZipColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_Address1Null() => 
            base.IsNull(this.tableCMNForm.Doctor_Address1Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_Address2Null() => 
            base.IsNull(this.tableCMNForm.Doctor_Address2Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_CityNull() => 
            base.IsNull(this.tableCMNForm.Doctor_CityColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_FirstNameNull() => 
            base.IsNull(this.tableCMNForm.Doctor_FirstNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_IDNull() => 
            base.IsNull(this.tableCMNForm.Doctor_IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_LastNameNull() => 
            base.IsNull(this.tableCMNForm.Doctor_LastNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_MiddleNameNull() => 
            base.IsNull(this.tableCMNForm.Doctor_MiddleNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_PhoneNull() => 
            base.IsNull(this.tableCMNForm.Doctor_PhoneColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_StateNull() => 
            base.IsNull(this.tableCMNForm.Doctor_StateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_UPINNull() => 
            base.IsNull(this.tableCMNForm.Doctor_UPINColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_WholeNameNull() => 
            base.IsNull(this.tableCMNForm.Doctor_WholeNameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsDoctor_ZipNull() => 
            base.IsNull(this.tableCMNForm.Doctor_ZipColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_Address1Null() => 
            base.IsNull(this.tableCMNForm.Facility_Address1Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_Address2Null() => 
            base.IsNull(this.tableCMNForm.Facility_Address2Column);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_CityNull() => 
            base.IsNull(this.tableCMNForm.Facility_CityColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_CodeNull() => 
            base.IsNull(this.tableCMNForm.Facility_CodeColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_IDNull() => 
            base.IsNull(this.tableCMNForm.Facility_IDColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_NameNull() => 
            base.IsNull(this.tableCMNForm.Facility_NameColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_StateNull() => 
            base.IsNull(this.tableCMNForm.Facility_StateColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsFacility_ZipNull() => 
            base.IsNull(this.tableCMNForm.Facility_ZipColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCMNForm_HCFATypeNull()
        {
            base[this.tableCMNForm.CMNForm_HCFATypeColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCMNForm_InitialDateNull()
        {
            base[this.tableCMNForm.CMNForm_InitialDateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCMNForm_RecertificationDateNull()
        {
            base[this.tableCMNForm.CMNForm_RecertificationDateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCMNForm_RevisedDateNull()
        {
            base[this.tableCMNForm.CMNForm_RevisedDateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCMNTypeNull()
        {
            base[this.tableCMNForm.CMNTypeColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_Address1Null()
        {
            base[this.tableCMNForm.Company_Address1Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_Address2Null()
        {
            base[this.tableCMNForm.Company_Address2Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_CityNull()
        {
            base[this.tableCMNForm.Company_CityColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_IDNull()
        {
            base[this.tableCMNForm.Company_IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_NameNull()
        {
            base[this.tableCMNForm.Company_NameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_NSCNull()
        {
            base[this.tableCMNForm.Company_NSCColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_PhoneNull()
        {
            base[this.tableCMNForm.Company_PhoneColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_StateNull()
        {
            base[this.tableCMNForm.Company_StateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCompany_ZipNull()
        {
            base[this.tableCMNForm.Company_ZipColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_Address1Null()
        {
            base[this.tableCMNForm.Customer_Address1Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_Address2Null()
        {
            base[this.tableCMNForm.Customer_Address2Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_CityNull()
        {
            base[this.tableCMNForm.Customer_CityColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_DOBNull()
        {
            base[this.tableCMNForm.Customer_DOBColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_FirstNameNull()
        {
            base[this.tableCMNForm.Customer_FirstNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_GenderNull()
        {
            base[this.tableCMNForm.Customer_GenderColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_HeightNull()
        {
            base[this.tableCMNForm.Customer_HeightColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_HIC_NumberNull()
        {
            base[this.tableCMNForm.Customer_HIC_NumberColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_IDNull()
        {
            base[this.tableCMNForm.Customer_IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_LastNameNull()
        {
            base[this.tableCMNForm.Customer_LastNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_MiddleNameNull()
        {
            base[this.tableCMNForm.Customer_MiddleNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_PhoneNull()
        {
            base[this.tableCMNForm.Customer_PhoneColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_SSNNull()
        {
            base[this.tableCMNForm.Customer_SSNColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_StateNull()
        {
            base[this.tableCMNForm.Customer_StateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_WeightNull()
        {
            base[this.tableCMNForm.Customer_WeightColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_WholeNameNull()
        {
            base[this.tableCMNForm.Customer_WholeNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetCustomer_ZipNull()
        {
            base[this.tableCMNForm.Customer_ZipColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_Address1Null()
        {
            base[this.tableCMNForm.Doctor_Address1Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_Address2Null()
        {
            base[this.tableCMNForm.Doctor_Address2Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_CityNull()
        {
            base[this.tableCMNForm.Doctor_CityColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_FirstNameNull()
        {
            base[this.tableCMNForm.Doctor_FirstNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_IDNull()
        {
            base[this.tableCMNForm.Doctor_IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_LastNameNull()
        {
            base[this.tableCMNForm.Doctor_LastNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_MiddleNameNull()
        {
            base[this.tableCMNForm.Doctor_MiddleNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_PhoneNull()
        {
            base[this.tableCMNForm.Doctor_PhoneColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_StateNull()
        {
            base[this.tableCMNForm.Doctor_StateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_UPINNull()
        {
            base[this.tableCMNForm.Doctor_UPINColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_WholeNameNull()
        {
            base[this.tableCMNForm.Doctor_WholeNameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetDoctor_ZipNull()
        {
            base[this.tableCMNForm.Doctor_ZipColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_Address1Null()
        {
            base[this.tableCMNForm.Facility_Address1Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_Address2Null()
        {
            base[this.tableCMNForm.Facility_Address2Column] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_CityNull()
        {
            base[this.tableCMNForm.Facility_CityColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_CodeNull()
        {
            base[this.tableCMNForm.Facility_CodeColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_IDNull()
        {
            base[this.tableCMNForm.Facility_IDColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_NameNull()
        {
            base[this.tableCMNForm.Facility_NameColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_StateNull()
        {
            base[this.tableCMNForm.Facility_StateColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetFacility_ZipNull()
        {
            base[this.tableCMNForm.Facility_ZipColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool Checked
        {
            get => 
                Conversions.ToBoolean(base[this.tableCMNForm.CheckedColumn]);
            set => 
                base[this.tableCMNForm.CheckedColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string CMNType
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.CMNTypeColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CMNType' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.CMNTypeColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int CMNForm_ID
        {
            get => 
                Conversions.ToInteger(base[this.tableCMNForm.CMNForm_IDColumn]);
            set => 
                base[this.tableCMNForm.CMNForm_IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string CMNForm_HCFAType
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.CMNForm_HCFATypeColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CMNForm_HCFAType' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.CMNForm_HCFATypeColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DateTime CMNForm_InitialDate
        {
            get
            {
                DateTime time;
                try
                {
                    time = Conversions.ToDate(base[this.tableCMNForm.CMNForm_InitialDateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CMNForm_InitialDate' in table 'CMNForm' is DBNull.", innerException);
                }
                return time;
            }
            set => 
                base[this.tableCMNForm.CMNForm_InitialDateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DateTime CMNForm_RevisedDate
        {
            get
            {
                DateTime time;
                try
                {
                    time = Conversions.ToDate(base[this.tableCMNForm.CMNForm_RevisedDateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CMNForm_RevisedDate' in table 'CMNForm' is DBNull.", innerException);
                }
                return time;
            }
            set => 
                base[this.tableCMNForm.CMNForm_RevisedDateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DateTime CMNForm_RecertificationDate
        {
            get
            {
                DateTime time;
                try
                {
                    time = Conversions.ToDate(base[this.tableCMNForm.CMNForm_RecertificationDateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'CMNForm_RecertificationDate' in table 'CMNForm' is DBNull.", innerException);
                }
                return time;
            }
            set => 
                base[this.tableCMNForm.CMNForm_RecertificationDateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int Customer_ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableCMNForm.Customer_IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_ID' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Customer_IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_FirstName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_FirstNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_FirstName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_FirstNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_LastName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_LastNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_LastName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_LastNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_MiddleName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_MiddleNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_MiddleName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_MiddleNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_WholeName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_WholeNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_WholeName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_WholeNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_Address1
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_Address1Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Address1' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_Address1Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_Address2
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_Address2Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Address2' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_Address2Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_City
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_CityColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_City' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_CityColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_State
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_StateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_State' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_StateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_Zip
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_ZipColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Zip' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_ZipColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_Phone
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_PhoneColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Phone' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_PhoneColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DateTime Customer_DOB
        {
            get
            {
                DateTime time;
                try
                {
                    time = Conversions.ToDate(base[this.tableCMNForm.Customer_DOBColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_DOB' in table 'CMNForm' is DBNull.", innerException);
                }
                return time;
            }
            set => 
                base[this.tableCMNForm.Customer_DOBColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_Gender
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_GenderColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Gender' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_GenderColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double Customer_Height
        {
            get
            {
                double num;
                try
                {
                    num = Conversions.ToDouble(base[this.tableCMNForm.Customer_HeightColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Height' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Customer_HeightColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double Customer_Weight
        {
            get
            {
                double num;
                try
                {
                    num = Conversions.ToDouble(base[this.tableCMNForm.Customer_WeightColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_Weight' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Customer_WeightColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_SSN
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_SSNColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_SSN' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_SSNColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Customer_HIC_Number
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Customer_HIC_NumberColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Customer_HIC_Number' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Customer_HIC_NumberColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int Company_ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableCMNForm.Company_IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_ID' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Company_IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_Name
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_NameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_Name' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_NameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_Address1
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_Address1Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_Address1' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_Address1Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_Address2
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_Address2Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_Address2' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_Address2Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_City
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_CityColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_City' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_CityColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_State
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_StateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_State' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_StateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_Zip
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_ZipColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_Zip' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_ZipColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_Phone
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_PhoneColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_Phone' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_PhoneColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Company_NSC
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Company_NSCColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Company_NSC' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Company_NSCColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int Doctor_ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableCMNForm.Doctor_IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_ID' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Doctor_IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_FirstName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_FirstNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_FirstName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_FirstNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_LastName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_LastNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_LastName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_LastNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_MiddleName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_MiddleNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_MiddleName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_MiddleNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_WholeName
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_WholeNameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_WholeName' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_WholeNameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_Address1
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_Address1Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_Address1' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_Address1Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_Address2
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_Address2Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_Address2' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_Address2Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_City
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_CityColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_City' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_CityColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_State
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_StateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_State' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_StateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_Zip
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_ZipColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_Zip' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_ZipColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_Phone
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_PhoneColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_Phone' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_PhoneColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Doctor_UPIN
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Doctor_UPINColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Doctor_UPIN' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Doctor_UPINColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int Facility_ID
        {
            get
            {
                int num;
                try
                {
                    num = Conversions.ToInteger(base[this.tableCMNForm.Facility_IDColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_ID' in table 'CMNForm' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableCMNForm.Facility_IDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_Name
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_NameColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_Name' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_NameColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_Address1
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_Address1Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_Address1' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_Address1Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_Address2
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_Address2Column]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_Address2' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_Address2Column] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_City
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_CityColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_City' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_CityColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_State
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_StateColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_State' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_StateColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_Zip
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_ZipColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_Zip' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_ZipColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Facility_Code
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableCMNForm.Facility_CodeColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Facility_Code' in table 'CMNForm' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableCMNForm.Facility_CodeColumn] = value;
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class CMNFormRowChangeEvent : EventArgs
    {
        private DatasetSecureCareExport2.CMNFormRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public CMNFormRowChangeEvent(DatasetSecureCareExport2.CMNFormRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.CMNFormRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void CMNFormRowChangeEventHandler(object sender, DatasetSecureCareExport2.CMNFormRowChangeEvent e);

    [Serializable, XmlSchemaProvider("GetTypedTableSchema")]
    public class DetailsDataTable : TypedTableBase<DatasetSecureCareExport2.DetailsRow>
    {
        private DataColumn columnCMNFormID;
        private DataColumn columnBillingCode;
        private DataColumn columnItemDescription;
        private DataColumn columnQuantity;
        private DataColumn columnUnits;
        private DataColumn columnBillablePrice;
        private DataColumn columnAllowablePrice;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.DetailsRowChangeEventHandler DetailsRowChanged;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.DetailsRowChangeEventHandler DetailsRowChanging;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.DetailsRowChangeEventHandler DetailsRowDeleted;

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public event DatasetSecureCareExport2.DetailsRowChangeEventHandler DetailsRowDeleting;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DetailsDataTable()
        {
            base.TableName = "Details";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal DetailsDataTable(DataTable table)
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
        protected DetailsDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void AddDetailsRow(DatasetSecureCareExport2.DetailsRow row)
        {
            base.Rows.Add(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.DetailsRow AddDetailsRow(int CMNFormID, string BillingCode, string ItemDescription, double Quantity, string Units, double BillablePrice, double AllowablePrice)
        {
            DatasetSecureCareExport2.DetailsRow row = (DatasetSecureCareExport2.DetailsRow) base.NewRow();
            row.ItemArray = new object[] { CMNFormID, BillingCode, ItemDescription, Quantity, Units, BillablePrice, AllowablePrice };
            base.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override DataTable Clone()
        {
            DatasetSecureCareExport2.DetailsDataTable table1 = (DatasetSecureCareExport2.DetailsDataTable) base.Clone();
            table1.InitVars();
            return table1;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataTable CreateInstance() => 
            new DatasetSecureCareExport2.DetailsDataTable();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override Type GetRowType() => 
            typeof(DatasetSecureCareExport2.DetailsRow);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            DatasetSecureCareExport2 export = new DatasetSecureCareExport2();
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
                FixedValue = export.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "DetailsDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = export.GetSchemaSerializable();
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
            this.columnCMNFormID = new DataColumn("CMNFormID", typeof(int), null, MappingType.Element);
            base.Columns.Add(this.columnCMNFormID);
            this.columnBillingCode = new DataColumn("BillingCode", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnBillingCode);
            this.columnItemDescription = new DataColumn("ItemDescription", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnItemDescription);
            this.columnQuantity = new DataColumn("Quantity", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnQuantity);
            this.columnUnits = new DataColumn("Units", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnUnits);
            this.columnBillablePrice = new DataColumn("BillablePrice", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnBillablePrice);
            this.columnAllowablePrice = new DataColumn("AllowablePrice", typeof(double), null, MappingType.Element);
            base.Columns.Add(this.columnAllowablePrice);
            this.columnCMNFormID.AllowDBNull = false;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars()
        {
            this.columnCMNFormID = base.Columns["CMNFormID"];
            this.columnBillingCode = base.Columns["BillingCode"];
            this.columnItemDescription = base.Columns["ItemDescription"];
            this.columnQuantity = base.Columns["Quantity"];
            this.columnUnits = base.Columns["Units"];
            this.columnBillablePrice = base.Columns["BillablePrice"];
            this.columnAllowablePrice = base.Columns["AllowablePrice"];
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.DetailsRow NewDetailsRow() => 
            (DatasetSecureCareExport2.DetailsRow) base.NewRow();

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => 
            new DatasetSecureCareExport2.DetailsRow(builder);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.DetailsRowChangedEvent != null)
            {
                DatasetSecureCareExport2.DetailsRowChangeEventHandler detailsRowChangedEvent = this.DetailsRowChangedEvent;
                if (detailsRowChangedEvent != null)
                {
                    detailsRowChangedEvent(this, new DatasetSecureCareExport2.DetailsRowChangeEvent((DatasetSecureCareExport2.DetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.DetailsRowChangingEvent != null)
            {
                DatasetSecureCareExport2.DetailsRowChangeEventHandler detailsRowChangingEvent = this.DetailsRowChangingEvent;
                if (detailsRowChangingEvent != null)
                {
                    detailsRowChangingEvent(this, new DatasetSecureCareExport2.DetailsRowChangeEvent((DatasetSecureCareExport2.DetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.DetailsRowDeletedEvent != null)
            {
                DatasetSecureCareExport2.DetailsRowChangeEventHandler detailsRowDeletedEvent = this.DetailsRowDeletedEvent;
                if (detailsRowDeletedEvent != null)
                {
                    detailsRowDeletedEvent(this, new DatasetSecureCareExport2.DetailsRowChangeEvent((DatasetSecureCareExport2.DetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.DetailsRowDeletingEvent != null)
            {
                DatasetSecureCareExport2.DetailsRowChangeEventHandler detailsRowDeletingEvent = this.DetailsRowDeletingEvent;
                if (detailsRowDeletingEvent != null)
                {
                    detailsRowDeletingEvent(this, new DatasetSecureCareExport2.DetailsRowChangeEvent((DatasetSecureCareExport2.DetailsRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void RemoveDetailsRow(DatasetSecureCareExport2.DetailsRow row)
        {
            base.Rows.Remove(row);
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn CMNFormIDColumn =>
            this.columnCMNFormID;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn BillingCodeColumn =>
            this.columnBillingCode;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn ItemDescriptionColumn =>
            this.columnItemDescription;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn QuantityColumn =>
            this.columnQuantity;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn UnitsColumn =>
            this.columnUnits;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn BillablePriceColumn =>
            this.columnBillablePrice;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataColumn AllowablePriceColumn =>
            this.columnAllowablePrice;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Browsable(false)]
        public int Count =>
            base.Rows.Count;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.DetailsRow this[int index] =>
            (DatasetSecureCareExport2.DetailsRow) base.Rows[index];
    }

    public class DetailsRow : DataRow
    {
        private DatasetSecureCareExport2.DetailsDataTable tableDetails;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal DetailsRow(DataRowBuilder rb) : base(rb)
        {
            this.tableDetails = (DatasetSecureCareExport2.DetailsDataTable) base.Table;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsAllowablePriceNull() => 
            base.IsNull(this.tableDetails.AllowablePriceColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsBillablePriceNull() => 
            base.IsNull(this.tableDetails.BillablePriceColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsBillingCodeNull() => 
            base.IsNull(this.tableDetails.BillingCodeColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsItemDescriptionNull() => 
            base.IsNull(this.tableDetails.ItemDescriptionColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsQuantityNull() => 
            base.IsNull(this.tableDetails.QuantityColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool IsUnitsNull() => 
            base.IsNull(this.tableDetails.UnitsColumn);

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetAllowablePriceNull()
        {
            base[this.tableDetails.AllowablePriceColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetBillablePriceNull()
        {
            base[this.tableDetails.BillablePriceColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetBillingCodeNull()
        {
            base[this.tableDetails.BillingCodeColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetItemDescriptionNull()
        {
            base[this.tableDetails.ItemDescriptionColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetQuantityNull()
        {
            base[this.tableDetails.QuantityColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public void SetUnitsNull()
        {
            base[this.tableDetails.UnitsColumn] = Convert.DBNull;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public int CMNFormID
        {
            get => 
                Conversions.ToInteger(base[this.tableDetails.CMNFormIDColumn]);
            set => 
                base[this.tableDetails.CMNFormIDColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string BillingCode
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableDetails.BillingCodeColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'BillingCode' in table 'Details' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableDetails.BillingCodeColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string ItemDescription
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableDetails.ItemDescriptionColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'ItemDescription' in table 'Details' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableDetails.ItemDescriptionColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double Quantity
        {
            get
            {
                double num;
                try
                {
                    num = Conversions.ToDouble(base[this.tableDetails.QuantityColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Quantity' in table 'Details' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableDetails.QuantityColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public string Units
        {
            get
            {
                string str;
                try
                {
                    str = Conversions.ToString(base[this.tableDetails.UnitsColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'Units' in table 'Details' is DBNull.", innerException);
                }
                return str;
            }
            set => 
                base[this.tableDetails.UnitsColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double BillablePrice
        {
            get
            {
                double num;
                try
                {
                    num = Conversions.ToDouble(base[this.tableDetails.BillablePriceColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'BillablePrice' in table 'Details' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableDetails.BillablePriceColumn] = value;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public double AllowablePrice
        {
            get
            {
                double num;
                try
                {
                    num = Conversions.ToDouble(base[this.tableDetails.AllowablePriceColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    InvalidCastException ex = exception1;
                    ProjectData.SetProjectError(ex);
                    InvalidCastException innerException = ex;
                    throw new StrongTypingException("The value for column 'AllowablePrice' in table 'Details' is DBNull.", innerException);
                }
                return num;
            }
            set => 
                base[this.tableDetails.AllowablePriceColumn] = value;
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class DetailsRowChangeEvent : EventArgs
    {
        private DatasetSecureCareExport2.DetailsRow eventRow;
        private DataRowAction eventAction;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DetailsRowChangeEvent(DatasetSecureCareExport2.DetailsRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DatasetSecureCareExport2.DetailsRow Row =>
            this.eventRow;

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public DataRowAction Action =>
            this.eventAction;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void DetailsRowChangeEventHandler(object sender, DatasetSecureCareExport2.DetailsRowChangeEvent e);
}

