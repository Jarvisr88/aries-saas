namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class OlePropertyIntBase : OlePropertyBase
    {
        protected OlePropertyIntBase(int propertyId, int propertyType) : base(propertyId, propertyType)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName))
            {
                propertiesContainer.SetNumeric(propertyName, (double) this.Value);
            }
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            8;

        public int Value { get; set; }
    }
}

