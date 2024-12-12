namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.InteropServices;

    public class ToolBarPageLayoutBuilder<T>
    {
        private readonly ClassMetadataBuilder<T> owner;
        private readonly ToolBarCategoryLayoutBuilder<T> parent;
        private readonly string pageName;

        internal ToolBarPageLayoutBuilder(ClassMetadataBuilder<T> owner, ToolBarCategoryLayoutBuilder<T> parent, string pageName)
        {
            this.owner = owner;
            this.parent = parent;
            this.pageName = pageName;
        }

        public ToolBarCategoryLayoutBuilder<T> EndPage() => 
            this.parent;

        public ToolBarPageGroupLayoutBuilder<T> PageGroup(string pageGroupName = null) => 
            new ToolBarPageGroupLayoutBuilder<T>(this.owner, (ToolBarPageLayoutBuilder<T>) this, this.pageName, pageGroupName);
    }
}

