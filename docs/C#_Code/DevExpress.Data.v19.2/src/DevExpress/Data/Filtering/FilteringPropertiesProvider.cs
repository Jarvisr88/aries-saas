namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Browsing.Design;
    using System;

    internal class FilteringPropertiesProvider : PropertiesProvider
    {
        public FilteringPropertiesProvider();
        protected override void SortProperties(IPropertyDescriptor[] properties);
    }
}

