namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtValueObject
    {
        private XlValueObject valueObject = XlValueObject.Empty;
        private bool greaterThanOrEqual = true;

        public XlCondFmtValueObjectType ObjectType { get; set; }

        public bool GreaterThanOrEqual
        {
            get => 
                this.greaterThanOrEqual;
            set => 
                this.greaterThanOrEqual = value;
        }

        public XlValueObject Value
        {
            get => 
                this.valueObject;
            set
            {
                if (value == null)
                {
                    this.valueObject = XlValueObject.Empty;
                }
                else
                {
                    this.valueObject = value;
                }
            }
        }
    }
}

