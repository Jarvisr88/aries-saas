namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AzureCompatibility
    {
        [Obsolete("This property has become obsolete. Use the 'DevExpress.Utils.AzureCompatibility.Enable' property instead.")]
        public static bool Enable
        {
            get => 
                DevExpress.Utils.AzureCompatibility.Enable;
            set => 
                DevExpress.Utils.AzureCompatibility.Enable = value;
        }
    }
}

