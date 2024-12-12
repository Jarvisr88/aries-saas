namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract, KnownType(typeof(DBNull))]
    public class ReportParameter
    {
        public ReportParameter()
        {
        }

        public ReportParameter(ParameterPath parameterPath, LookUpValueCollection lookUpValues)
        {
            Parameter parameter = parameterPath.Parameter;
            this.Description = parameter.Description;
            this.Path = parameterPath.Path;
            this.Name = parameter.Name;
            this.Value = GetValue(parameter);
            this.MultiValue = parameter.MultiValue;
            this.AllowNull = parameter.AllowNull;
            this.Visible = parameter.Visible;
            this.LookUpValues = lookUpValues;
            this.TypeName = parameter.Type.FullName;
            this.Type = parameter.Type;
            this.Tag = parameter.Tag;
        }

        public ReportParameter(ParameterPath parameterPath, LookUpValueCollection lookUpValues, bool isFiltered) : this(parameterPath, lookUpValues)
        {
            this.IsFilteredLookUpSettings = isFiltered;
        }

        public void Assign(ReportParameter reportParameter)
        {
            this.Description = reportParameter.Description;
            this.Path = reportParameter.Path;
            this.Name = reportParameter.Name;
            this.Value = reportParameter.Value;
            this.MultiValue = reportParameter.MultiValue;
            this.AllowNull = reportParameter.AllowNull;
            this.Visible = reportParameter.Visible;
            this.LookUpValues = reportParameter.LookUpValues;
            this.TypeName = reportParameter.TypeName;
            this.IsFilteredLookUpSettings = reportParameter.IsFilteredLookUpSettings;
            this.Type = reportParameter.Type;
            this.Tag = reportParameter.Tag;
        }

        private static object GetValue(Parameter parameter) => 
            parameter.MultiValue ? ((parameter.Value as IEnumerable) ?? Array.CreateInstance(parameter.Type, 0)) : parameter.Value;

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public bool Visible { get; set; }

        [DataMember]
        public bool IsFilteredLookUpSettings { get; set; }

        [DataMember]
        public LookUpValueCollection LookUpValues { get; set; }

        [DataMember]
        public bool MultiValue { get; set; }

        [DataMember]
        public bool AllowNull { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [IgnoreDataMember, EditorBrowsable(EditorBrowsableState.Never)]
        public System.Type Type { get; set; }

        [DataMember]
        public object Tag { get; set; }
    }
}

