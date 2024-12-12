namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomDropResult
    {
        public CustomDropResult() : this(false, null)
        {
        }

        public CustomDropResult(bool isCustom, IDataObject dataObject)
        {
            this.IsCustom = isCustom;
            this.DataObject = dataObject;
        }

        public bool IsCustom { get; private set; }

        public IDataObject DataObject { get; private set; }
    }
}

