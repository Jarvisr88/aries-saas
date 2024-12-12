namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public static class IBoundPropertyCollectionExtension
    {
        public static OperandProperty CreateDefaultProperty(this IBoundPropertyCollection self, IBoundProperty property);
        private static IBoundProperty GetDefaultColumnOnCreate(this IBoundPropertyCollection self, IBoundProperty property);
        public static IBoundProperty GetProperty(this IBoundPropertyCollection self, OperandProperty property);
    }
}

