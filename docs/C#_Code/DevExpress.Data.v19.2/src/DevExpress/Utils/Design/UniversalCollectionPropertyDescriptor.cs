namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public class UniversalCollectionPropertyDescriptor : PropertyDescriptor
    {
        private object source;
        private string caption;

        public UniversalCollectionPropertyDescriptor(object source, string caption) : base(caption, attributeArray1)
        {
            Attribute[] attributeArray1 = new Attribute[] { new NotifyParentPropertyAttribute(true) };
            this.source = source;
            this.caption = caption;
        }

        public override bool CanResetValue(object component) => 
            false;

        public override object GetValue(object component) => 
            this.Source;

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        protected object Source =>
            this.source;

        protected string Caption =>
            this.caption;

        public override bool IsReadOnly =>
            false;

        public override string Category =>
            "Item";

        public override Type PropertyType =>
            this.Source.GetType();

        public override Type ComponentType =>
            this.Source.GetType();
    }
}

