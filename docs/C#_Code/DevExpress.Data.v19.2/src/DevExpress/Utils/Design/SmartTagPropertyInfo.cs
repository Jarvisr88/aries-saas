namespace DevExpress.Utils.Design
{
    using System;
    using System.Runtime.CompilerServices;

    public class SmartTagPropertyInfo
    {
        public SmartTagPropertyInfo(string property)
        {
            this.Property = property;
        }

        public SmartTagPropertyInfo(string property, SmartTagEditorType editorType) : this(property)
        {
            this.EditorType = editorType;
        }

        public SmartTagPropertyInfo(string property, string editorType) : this(property)
        {
            this.EditorType = SmartTagEditorType.Custom;
            this.CustomEditorType = editorType;
        }

        public string Property { get; set; }

        public string CustomEditorType { get; set; }

        public SmartTagEditorType EditorType { get; set; }
    }
}

