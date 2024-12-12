namespace DevExpress.Office.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileDialogFilterCollection : List<FileDialogFilter>
    {
        public string CreateFilterString()
        {
            int count = base.Count;
            if (count <= 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(base[0].ToString());
            for (int i = 1; i < count; i++)
            {
                builder.Append('|');
                builder.Append(base[i].ToString());
            }
            return builder.ToString();
        }
    }
}

