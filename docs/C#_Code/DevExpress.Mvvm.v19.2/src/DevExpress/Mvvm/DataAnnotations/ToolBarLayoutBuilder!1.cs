namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class ToolBarLayoutBuilder<T>
    {
        private readonly ClassMetadataBuilder<T> owner;

        internal ToolBarLayoutBuilder(ClassMetadataBuilder<T> owner)
        {
            this.owner = owner;
        }

        public ToolBarCategoryLayoutBuilder<T> DefaultCategory() => 
            new ToolBarCategoryLayoutBuilder<T>(this.owner);
    }
}

