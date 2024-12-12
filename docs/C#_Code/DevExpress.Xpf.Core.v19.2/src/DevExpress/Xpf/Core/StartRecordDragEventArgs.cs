namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class StartRecordDragEventArgs : RoutedEventArgs
    {
        private DragDropEffects allowedEffects;
        private Lazy<IDataObject> dataObject;

        public StartRecordDragEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            this.allowedEffects = DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
            Func<IDataObject> valueFactory = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<IDataObject> local1 = <>c.<>9__21_0;
                valueFactory = <>c.<>9__21_0 = (Func<IDataObject>) (() => null);
            }
            this.SetDataObjectInitializer(new Lazy<IDataObject>(valueFactory));
        }

        internal void SetDataObjectInitializer(Lazy<IDataObject> initializer)
        {
            this.dataObject = initializer;
        }

        public object DragElement { get; internal set; }

        public object[] Records { get; internal set; }

        public bool AllowDrag { get; set; }

        public DragDropEffects AllowedEffects
        {
            get => 
                this.allowedEffects;
            set => 
                this.allowedEffects = value;
        }

        public IDataObject Data
        {
            get => 
                this.dataObject.Value;
            set => 
                this.SetDataObjectInitializer(new Lazy<IDataObject>(() => value));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StartRecordDragEventArgs.<>c <>9 = new StartRecordDragEventArgs.<>c();
            public static Func<IDataObject> <>9__21_0;

            internal IDataObject <.ctor>b__21_0() => 
                null;
        }
    }
}

