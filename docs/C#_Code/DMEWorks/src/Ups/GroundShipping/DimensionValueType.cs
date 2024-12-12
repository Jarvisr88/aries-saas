namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class DimensionValueType
    {
        private CubeType cubeField;
        private CWTType cWTField;

        public CubeType Cube
        {
            get => 
                this.cubeField;
            set => 
                this.cubeField = value;
        }

        public CWTType CWT
        {
            get => 
                this.cWTField;
            set => 
                this.cWTField = value;
        }
    }
}

