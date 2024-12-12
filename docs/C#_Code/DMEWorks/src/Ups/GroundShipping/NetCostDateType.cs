namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class NetCostDateType
    {
        private string beginDateField;
        private string endDateField;

        public string BeginDate
        {
            get => 
                this.beginDateField;
            set => 
                this.beginDateField = value;
        }

        public string EndDate
        {
            get => 
                this.endDateField;
            set => 
                this.endDateField = value;
        }
    }
}

