namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomStartDragResult
    {
        public CustomStartDragResult(IDataObject dataObject) : this(DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll, dataObject)
        {
        }

        public CustomStartDragResult(DragDropEffects effects, IDataObject dataObject)
        {
            this.Effects = effects;
            this.DataObject = dataObject;
        }

        public DragDropEffects Effects { get; private set; }

        public IDataObject DataObject { get; private set; }
    }
}

