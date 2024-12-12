namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.InteropServices;

    public class ContextMenuLayoutBuilder<T>
    {
        private readonly ClassMetadataBuilder<T> owner;

        internal ContextMenuLayoutBuilder(ClassMetadataBuilder<T> owner)
        {
            this.owner = owner;
        }

        public ContextMenuGroupLayoutBuilder<T> Group(string pageGroupName = null) => 
            new ContextMenuGroupLayoutBuilder<T>(this.owner, (ContextMenuLayoutBuilder<T>) this, pageGroupName);
    }
}

