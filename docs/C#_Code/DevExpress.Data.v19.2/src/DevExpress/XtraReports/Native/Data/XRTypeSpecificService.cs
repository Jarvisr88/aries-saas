namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class XRTypeSpecificService : TypeSpecificsService
    {
        private Dictionary<TypeSpecifics, TypeSpecifics> calcDict;

        public XRTypeSpecificService();
        public override TypeSpecifics GetPropertyTypeSpecifics(PropertyDescriptor property);
        public override TypeSpecifics GetTypeSpecifics(Type type);
    }
}

