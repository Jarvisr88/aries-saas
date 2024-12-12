namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DragEventArgsBase : RoutedEventArgs
    {
        public DragEventArgsBase(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public Type GetRecordType() => 
            GetRecordType(this.Data);

        private static Type GetRecordType(IDataObject data)
        {
            Type format = typeof(RecordDragDropData);
            if ((data != null) && data.GetDataPresent(format))
            {
                RecordDragDropData data2 = data.GetData(format) as RecordDragDropData;
                if ((data2 != null) && ((data2.Records != null) && (data2.Records.Length != 0)))
                {
                    object obj2 = data2.Records[0];
                    if (obj2 != null)
                    {
                        return obj2.GetType();
                    }
                }
            }
            return typeof(object);
        }

        public DragDropEffects AllowedEffects { get; internal set; }

        public DragDropKeyStates KeyStates { get; internal set; }

        public object TargetRecord { get; internal set; }

        public bool IsFromOutside { get; internal set; }

        public int TargetRowHandle { get; internal set; }

        public DragDropEffects Effects { get; set; }

        public DevExpress.Xpf.Core.DropPosition DropPosition { get; set; }

        public IDataObject Data { get; set; }
    }
}

