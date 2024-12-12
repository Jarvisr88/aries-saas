namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class Shape3DProperties : ICloneable<Shape3DProperties>, ISupportsCopyFrom<Shape3DProperties>, IDrawingText3D, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey ExtrusionHeightPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey ContourWidthPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey ShapeDepthPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey PresetMaterialPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey TopBevelPropertyKey = new PropertyKey(4);
        public static readonly PropertyKey BottomBevelPropertyKey = new PropertyKey(5);
        public static readonly PropertyKey ContourColorPropertyKey = new PropertyKey(6);
        public static readonly PropertyKey ExtrusionColorPropertyKey = new PropertyKey(7);
        public const long DefaultExtrusionHeight = 0L;
        public const long DefaultContourWidth = 0L;
        public const long DefaultShapeDepth = 0L;
        public const PresetMaterialType DefaultPresetMaterialType = PresetMaterialType.WarmMatte;
        private readonly IDocumentModel documentModel;
        private readonly ShapeBevel3DProperties topBevel;
        private readonly ShapeBevel3DProperties bottomBevel;
        private readonly DrawingColor contourColor;
        private readonly DrawingColor extrusionColor;
        private PresetMaterialType preset;
        private long extrusionHeight;
        private long contourWidth;
        private long shapeDepth;
        private readonly PropertyChangedNotifier notifier;

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

        public Shape3DProperties(IDocumentModel documentModel)
        {
            this.notifier = new PropertyChangedNotifier(this);
            this.documentModel = documentModel;
            this.topBevel = new ShapeBevel3DProperties(documentModel);
            this.topBevel.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnTopBevelChanged);
            this.bottomBevel = new ShapeBevel3DProperties(documentModel);
            this.bottomBevel.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnBottomBevelChanged);
            this.contourColor = new DrawingColor(documentModel);
            this.contourColor.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnContourColorChanged);
            this.extrusionColor = new DrawingColor(documentModel);
            this.extrusionColor.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnExtrusionColorChanged);
            this.preset = PresetMaterialType.WarmMatte;
        }

        protected void ApplyHistoryItem(HistoryItem item)
        {
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public Shape3DProperties Clone()
        {
            Shape3DProperties properties = new Shape3DProperties(this.DocumentModel);
            properties.CopyFrom(this);
            return properties;
        }

        public void CopyFrom(Shape3DProperties value)
        {
            this.extrusionHeight = value.extrusionHeight;
            this.contourWidth = value.contourWidth;
            this.shapeDepth = value.shapeDepth;
            this.preset = value.preset;
            this.topBevel.CopyFrom(value.topBevel);
            this.bottomBevel.CopyFrom(value.bottomBevel);
            this.contourColor.CopyFrom(value.contourColor);
            this.extrusionColor.CopyFrom(value.extrusionColor);
        }

        IDrawingText3D IDrawingText3D.CloneTo(IDocumentModel documentModel)
        {
            Shape3DProperties properties = new Shape3DProperties(documentModel);
            properties.CopyFrom(this);
            return properties;
        }

        public override bool Equals(object obj)
        {
            Shape3DProperties properties = obj as Shape3DProperties;
            return ((properties != null) ? ((this.preset == properties.preset) && (this.topBevel.Equals(properties.topBevel) && (this.bottomBevel.Equals(properties.bottomBevel) && (this.contourColor.Equals(properties.contourColor) && (this.extrusionColor.Equals(properties.extrusionColor) && ((this.ExtrusionHeight == properties.ExtrusionHeight) && ((this.ContourWidth == properties.ContourWidth) && (this.ShapeDepth == properties.ShapeDepth)))))))) : false);
        }

        public override int GetHashCode() => 
            ((((((this.preset.GetHashCode() ^ this.extrusionHeight.GetHashCode()) ^ this.contourWidth.GetHashCode()) ^ this.shapeDepth.GetHashCode()) ^ this.topBevel.GetHashCode()) ^ this.bottomBevel.GetHashCode()) ^ this.contourColor.GetHashCode()) ^ this.extrusionColor.GetHashCode();

        private void OnBottomBevelChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(BottomBevelPropertyKey, sender, e);
        }

        private void OnContourColorChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ContourColorPropertyKey, sender, e);
        }

        private void OnExtrusionColorChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ExtrusionColorPropertyKey, sender, e);
        }

        private void OnTopBevelChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(TopBevelPropertyKey, sender, e);
        }

        protected internal void SetContourWidthCore(long value)
        {
            this.contourWidth = value;
            this.notifier.OnPropertyChanged(ContourWidthPropertyKey);
        }

        protected internal void SetExtrusionHeightCore(long value)
        {
            this.extrusionHeight = value;
            this.notifier.OnPropertyChanged(ExtrusionHeightPropertyKey);
        }

        protected internal void SetPresetCore(PresetMaterialType value)
        {
            this.preset = value;
            this.notifier.OnPropertyChanged(PresetMaterialPropertyKey);
        }

        protected internal void SetShapeDepthCore(long value)
        {
            this.shapeDepth = value;
            this.notifier.OnPropertyChanged(ShapeDepthPropertyKey);
        }

        public void Visit(IDrawingText3DVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public long ExtrusionHeight
        {
            get => 
                this.extrusionHeight;
            set
            {
                if (this.extrusionHeight != value)
                {
                    this.ApplyHistoryItem(new ActionLongHistoryItem(this.DocumentModel.MainPart, this.extrusionHeight, value, new Action<long>(this.SetExtrusionHeightCore)));
                }
            }
        }

        public long ContourWidth
        {
            get => 
                this.contourWidth;
            set
            {
                if (this.contourWidth != value)
                {
                    this.ApplyHistoryItem(new ActionLongHistoryItem(this.DocumentModel.MainPart, this.contourWidth, value, new Action<long>(this.SetContourWidthCore)));
                }
            }
        }

        public long ShapeDepth
        {
            get => 
                this.shapeDepth;
            set
            {
                if (this.shapeDepth != value)
                {
                    this.ApplyHistoryItem(new ActionLongHistoryItem(this.DocumentModel.MainPart, this.shapeDepth, value, new Action<long>(this.SetShapeDepthCore)));
                }
            }
        }

        public PresetMaterialType PresetMaterial
        {
            get => 
                this.preset;
            set
            {
                if (this.preset != value)
                {
                    this.ApplyHistoryItem(new Shape3DPropertiesPresetMaterialTypeHistoryItem(this, this.preset, value));
                }
            }
        }

        public ShapeBevel3DProperties TopBevel =>
            this.topBevel;

        public ShapeBevel3DProperties BottomBevel =>
            this.bottomBevel;

        public DrawingColor ContourColor =>
            this.contourColor;

        public DrawingColor ExtrusionColor =>
            this.extrusionColor;

        public bool IsDefault =>
            this.ContourColor.IsEmpty && (this.ExtrusionColor.IsEmpty && (this.TopBevel.IsDefault && (this.BottomBevel.IsDefault && ((this.PresetMaterial == PresetMaterialType.WarmMatte) && ((this.ExtrusionHeight == 0) && ((this.ContourWidth == 0) && (this.ShapeDepth == 0L)))))));

        DrawingText3DType IDrawingText3D.Type =>
            DrawingText3DType.Shape3D;
    }
}

