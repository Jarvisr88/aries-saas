namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ShapeBevel3DProperties : ISupportsCopyFrom<ShapeBevel3DProperties>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey HeightPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey WidthPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey PresetTypePropertyKey = new PropertyKey(2);
        public const int DefaultCoordinate = 0x129a8;
        public const PresetBevelType DefaultPresetType = PresetBevelType.Circle;
        private readonly IDocumentModel documentModel;
        private readonly PropertyChangedNotifier notifier;
        private PresetBevelType preset;
        private long width;
        private long height;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public ShapeBevel3DProperties(IDocumentModel documentModel)
        {
            this.documentModel = documentModel;
            this.notifier = new PropertyChangedNotifier(this);
            this.preset = PresetBevelType.Circle;
            this.width = 0x129a8L;
            this.height = 0x129a8L;
        }

        protected void ApplyHistoryItem(HistoryItem item)
        {
            this.documentModel.History.Add(item);
            item.Execute();
        }

        public void CopyFrom(ShapeBevel3DProperties value)
        {
            this.preset = value.preset;
            this.width = value.width;
            this.height = value.height;
        }

        public override bool Equals(object obj)
        {
            ShapeBevel3DProperties properties = obj as ShapeBevel3DProperties;
            return ((properties != null) ? ((this.Height == properties.Height) && ((this.Width == properties.Width) && (this.PresetType == properties.PresetType))) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalcHashCode32(this.Height.GetHashCode(), this.Width.GetHashCode(), this.PresetType.GetHashCode());

        protected internal void SetHeightCore(long value)
        {
            this.height = value;
            this.notifier.OnPropertyChanged(HeightPropertyKey);
        }

        protected internal void SetPresetCore(PresetBevelType value)
        {
            this.preset = value;
            this.notifier.OnPropertyChanged(PresetTypePropertyKey);
        }

        protected internal void SetWidthCore(long value)
        {
            this.width = value;
            this.notifier.OnPropertyChanged(WidthPropertyKey);
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public long Height
        {
            get => 
                this.height;
            set
            {
                if (this.height != value)
                {
                    this.ApplyHistoryItem(new ActionLongHistoryItem(this.documentModel.MainPart, this.height, value, new Action<long>(this.SetHeightCore)));
                }
            }
        }

        public long Width
        {
            get => 
                this.width;
            set
            {
                if (this.width != value)
                {
                    this.ApplyHistoryItem(new ActionLongHistoryItem(this.documentModel.MainPart, this.width, value, new Action<long>(this.SetWidthCore)));
                }
            }
        }

        public PresetBevelType PresetType
        {
            get => 
                this.preset;
            set
            {
                if (this.preset != value)
                {
                    this.ApplyHistoryItem(new Shape3DPropertiesPresetTypeHistoryItem(this, this.preset, value));
                }
            }
        }

        public bool IsDefault =>
            (this.PresetType == PresetBevelType.Circle) && ((this.Height == 0x129a8L) && (this.Width == 0x129a8L));
    }
}

