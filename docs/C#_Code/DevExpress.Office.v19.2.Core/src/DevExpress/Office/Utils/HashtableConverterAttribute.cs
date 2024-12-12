namespace DevExpress.Office.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false)]
    public sealed class HashtableConverterAttribute : Attribute
    {
        private readonly string hashtableConverterTypeName;
        private bool isStatic;
        private bool collectParentProperties;

        public HashtableConverterAttribute(string hashtableConverterTypeName) : this(hashtableConverterTypeName, true, false)
        {
        }

        public HashtableConverterAttribute(string hashtableConverterTypeName, bool isStatic) : this(hashtableConverterTypeName, isStatic, false)
        {
        }

        public HashtableConverterAttribute(string hashtableConverterTypeName, bool isStatic, bool collectParentProperties)
        {
            this.hashtableConverterTypeName = hashtableConverterTypeName;
            this.isStatic = isStatic;
            this.collectParentProperties = collectParentProperties;
        }

        public string HashtableConverterTypeName =>
            this.hashtableConverterTypeName;
    }
}

