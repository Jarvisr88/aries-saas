namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class ContactType
    {
        private ForwardAgentType forwardAgentField;
        private UltimateConsigneeType ultimateConsigneeField;
        private IntermediateConsigneeType intermediateConsigneeField;
        private ProducerType producerField;
        private SoldToType soldToField;

        public ForwardAgentType ForwardAgent
        {
            get => 
                this.forwardAgentField;
            set => 
                this.forwardAgentField = value;
        }

        public UltimateConsigneeType UltimateConsignee
        {
            get => 
                this.ultimateConsigneeField;
            set => 
                this.ultimateConsigneeField = value;
        }

        public IntermediateConsigneeType IntermediateConsignee
        {
            get => 
                this.intermediateConsigneeField;
            set => 
                this.intermediateConsigneeField = value;
        }

        public ProducerType Producer
        {
            get => 
                this.producerField;
            set => 
                this.producerField = value;
        }

        public SoldToType SoldTo
        {
            get => 
                this.soldToField;
            set => 
                this.soldToField = value;
        }
    }
}

