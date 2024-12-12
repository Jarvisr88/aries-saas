namespace DevExpress.Data
{
    using DevExpress.Utils.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    public class UnboundSourceProperty
    {
        private string name;
        private Type propertyType;
        private object userTag;
        private string displayName;
        private UnboundSourcePropertyCollection owner;

        public UnboundSourceProperty();
        public UnboundSourceProperty(string name);
        public UnboundSourceProperty(string name, Type propertyType);
        internal void SetOwner(UnboundSourcePropertyCollection owner);
        private void UpdateView();

        [Category("Data"), RefreshProperties(RefreshProperties.All)]
        public string Name { get; set; }

        [Category("Data"), DefaultValue(typeof(object)), Editor(typeof(SimpleTypeEditor), typeof(UITypeEditor)), TypeConverter(typeof(SimpleToStringTypeConverter))]
        public Type PropertyType { get; set; }

        [Category("Data"), DefaultValue((string) null), Editor(typeof(UIObjectEditor), typeof(UITypeEditor)), TypeConverter(typeof(ObjectEditorTypeConverter))]
        public object UserTag { get; set; }

        [DefaultValue(""), Category("Display")]
        public string DisplayName { get; set; }
    }
}

