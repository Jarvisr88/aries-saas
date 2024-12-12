namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1")]
    public class ErrorDetailType
    {
        private string severityField;
        private CodeType primaryErrorCodeField;
        private string minimumRetrySecondsField;
        private LocationType locationField;
        private CodeType[] subErrorCodeField;
        private AdditionalInfoType[] additionalInformationField;

        public string Severity
        {
            get => 
                this.severityField;
            set => 
                this.severityField = value;
        }

        public CodeType PrimaryErrorCode
        {
            get => 
                this.primaryErrorCodeField;
            set => 
                this.primaryErrorCodeField = value;
        }

        public string MinimumRetrySeconds
        {
            get => 
                this.minimumRetrySecondsField;
            set => 
                this.minimumRetrySecondsField = value;
        }

        public LocationType Location
        {
            get => 
                this.locationField;
            set => 
                this.locationField = value;
        }

        [XmlElement("SubErrorCode")]
        public CodeType[] SubErrorCode
        {
            get => 
                this.subErrorCodeField;
            set => 
                this.subErrorCodeField = value;
        }

        [XmlElement("AdditionalInformation")]
        public AdditionalInfoType[] AdditionalInformation
        {
            get => 
                this.additionalInformationField;
            set => 
                this.additionalInformationField = value;
        }
    }
}

