namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class CustomAttributesEventArgs : EventArgs
    {
        private string fieldName;
        private bool isCalculatedColumn;
        private List<Attribute> attributesCore;

        public CustomAttributesEventArgs(string fieldName, bool isCalculatedColumn);

        public string FieldName { get; }

        public bool IsCalculatedColumn { get; }

        public List<Attribute> Attributes { get; }
    }
}

