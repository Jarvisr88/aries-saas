namespace DevExpress.Xpf.Core
{
    using System;

    public class NullColumnChooserException : Exception
    {
        public static void CheckColumnChooserNotNull(IColumnChooser columnChooser)
        {
            if (columnChooser == null)
            {
                throw new NullColumnChooserException();
            }
        }
    }
}

