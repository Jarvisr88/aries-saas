namespace DevExpress.Office.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JSONEnumAttribute : Attribute
    {
        private readonly int value;

        public JSONEnumAttribute(int value)
        {
            this.value = value;
        }

        public int Value =>
            this.value;
    }
}

