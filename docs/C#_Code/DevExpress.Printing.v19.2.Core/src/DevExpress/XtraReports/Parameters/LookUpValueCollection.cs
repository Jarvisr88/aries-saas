namespace DevExpress.XtraReports.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Design.Serialization;

    [DesignerSerializer("DevExpress.XtraReports.Design.LookUpValueCollectionSerializer,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class LookUpValueCollection : Collection<LookUpValue>
    {
        public void AddRange(IEnumerable<LookUpValue> values)
        {
            foreach (LookUpValue value2 in values)
            {
                base.Add(value2);
            }
        }
    }
}

