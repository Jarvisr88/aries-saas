namespace DevExpress.Data.TreeList
{
    using System;

    public class TreeListSearchDisplayTextPropertyDescriptor : TreeListDisplayTextPropertyDescriptor
    {
        private string originalName;

        public TreeListSearchDisplayTextPropertyDescriptor(TreeListDataControllerBase controller, string name);
        private static string AddPrefix(string name);
        protected override object GetValueCore(TreeListNodeBase node);
    }
}

