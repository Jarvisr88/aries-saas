namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors;
    using System;

    public class OperationHelper
    {
        public static string GetMenuStringByType(ClauseType type)
        {
            string str = type.ToString();
            string str2 = "FilterClause" + str;
            return (!Enum.IsDefined(typeof(EditorStringId), str2) ? str : EditorLocalizer.GetString(str2));
        }
    }
}

