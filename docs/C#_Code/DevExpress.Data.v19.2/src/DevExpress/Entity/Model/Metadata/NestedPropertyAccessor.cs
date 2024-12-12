namespace DevExpress.Entity.Model.Metadata
{
    using System;

    internal class NestedPropertyAccessor : PropertyAccessor
    {
        private string fullName;
        private NestedPropertyAccessor nestedProperty;

        public NestedPropertyAccessor(object source, string fullName) : base(source, GetCurrentLevelPropertyName(fullName))
        {
            this.fullName = fullName;
        }

        public NestedPropertyAccessor(string fullName, Type sourceType) : base(GetCurrentLevelPropertyName(fullName), sourceType)
        {
            this.fullName = fullName;
        }

        private static string GetCurrentLevelPropertyName(string name)
        {
            int index = name.IndexOf(".");
            return ((index <= 0) ? name : name.Substring(0, index));
        }

        private string GetNestedPropertyName()
        {
            int index = this.fullName.IndexOf(".");
            return this.fullName.Remove(0, index + 1);
        }

        private bool IsComplex =>
            IsComplexPropertyName(this.fullName);

        public override object Value
        {
            get
            {
                if (!this.IsComplex)
                {
                    return base.Value;
                }
                object source = base.Value;
                if (source == null)
                {
                    return null;
                }
                this.nestedProperty ??= new NestedPropertyAccessor(source, this.GetNestedPropertyName());
                return this.nestedProperty.Value;
            }
        }
    }
}

