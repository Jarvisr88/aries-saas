namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public class GroupingInfo
    {
        private readonly string fieldName;

        public GroupingInfo(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public string FieldName =>
            this.fieldName;
    }
}

