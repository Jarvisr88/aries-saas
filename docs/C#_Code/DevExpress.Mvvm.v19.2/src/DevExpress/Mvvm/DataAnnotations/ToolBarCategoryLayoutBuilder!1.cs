namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class ToolBarCategoryLayoutBuilder<T>
    {
        private readonly ClassMetadataBuilder<T> owner;

        internal ToolBarCategoryLayoutBuilder(ClassMetadataBuilder<T> owner)
        {
            this.owner = owner;
        }

        public ToolBarPageLayoutBuilder<T> Page(string pageName) => 
            new ToolBarPageLayoutBuilder<T>(this.owner, (ToolBarCategoryLayoutBuilder<T>) this, pageName);
    }
}

