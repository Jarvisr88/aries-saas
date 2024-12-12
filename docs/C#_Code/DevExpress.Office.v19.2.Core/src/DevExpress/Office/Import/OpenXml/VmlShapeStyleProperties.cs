namespace DevExpress.Office.Import.OpenXml
{
    using System;
    using System.Collections.Generic;

    public class VmlShapeStyleProperties : Dictionary<string, string>
    {
        public string GetProperty(string propertyName)
        {
            string str;
            return (base.TryGetValue(propertyName, out str) ? str : string.Empty);
        }
    }
}

