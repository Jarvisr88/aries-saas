namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class OlePropertyNumericBase : OlePropertyBase
    {
        protected OlePropertyNumericBase(int propertyId, int propertyType) : base(propertyId, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertiesContainer.SetNumeric(propertyName, this.Value);
            }
        }

        public double Value { get; set; }
    }
}

