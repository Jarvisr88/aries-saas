namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    internal class AlwaysLocalizedBooleanTypeConverter : BooleanTypeConverter
    {
        protected override string GetDisplayName(DXDisplayNameAttribute attr) => 
            attr.GetLocalizedDisplayName();
    }
}

