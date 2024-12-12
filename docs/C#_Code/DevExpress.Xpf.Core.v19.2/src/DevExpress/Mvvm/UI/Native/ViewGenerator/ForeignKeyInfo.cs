namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;
    using System.Runtime.CompilerServices;

    public class ForeignKeyInfo
    {
        public ForeignKeyInfo(string foreignKeyPropertyName, string primaryKeyPropertyName)
        {
            this.ForeignKeyPropertyName = foreignKeyPropertyName;
            this.PrimaryKeyPropertyName = primaryKeyPropertyName;
        }

        public string ForeignKeyPropertyName { get; private set; }

        public string PrimaryKeyPropertyName { get; private set; }
    }
}

