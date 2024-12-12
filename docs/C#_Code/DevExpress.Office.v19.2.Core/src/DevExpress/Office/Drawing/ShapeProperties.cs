namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class ShapeProperties : ShapePropertiesBase, ICloneable<ShapeProperties>, ISupportsCopyFrom<ShapeProperties>
    {
        public static readonly PropertyKey OutlinePropertyKey = new PropertyKey(4);
        public static readonly PropertyKey ShapeTypePropertyKey = new PropertyKey(5);
        public static readonly PropertyKey PresetAdjustListPropertyKey = new PropertyKey(6);
        public static readonly PropertyKey CustomGeometryPropertyKey = new PropertyKey(7);
        private DevExpress.Office.Drawing.Outline outline;
        private readonly ModelShapeCustomGeometry customGeometry;
        private readonly ModelShapeGuideList presetAdjustList;

        public ShapeProperties(IDocumentModel documentModel) : this(documentModel, new Transform2D(documentModel))
        {
        }

        public ShapeProperties(IDocumentModel documentModel, Transform2D transform2D) : base(documentModel, transform2D)
        {
            this.outline = new DevExpress.Office.Drawing.Outline(documentModel);
            this.outline.SetDrawingFillCore(this.GetDefaultFill());
            this.outline.Parent = base.InnerParent;
            this.outline.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnOutlineChanged);
            this.customGeometry = new ModelShapeCustomGeometry(base.DocumentModel.MainPart);
            this.customGeometry.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnCustomGeometryChanged);
            this.presetAdjustList = new ModelShapeGuideList(base.DocumentModel.MainPart);
            this.presetAdjustList.Modified += new EventHandler(this.OnPresetAdjustListModified);
        }

        public ShapeProperties Clone()
        {
            ShapeProperties properties = new ShapeProperties(base.DocumentModel, new Transform2D(base.DocumentModel));
            properties.CopyFrom(this);
            return properties;
        }

        public virtual void CopyFrom(ShapeProperties value)
        {
            Guard.ArgumentNotNull(value, "ShapeProperties");
            base.CopyFrom(value);
            this.Outline.CopyFrom(value.Outline);
            this.customGeometry.CopyFrom(value.CustomGeometry);
            this.presetAdjustList.CopyFrom(value.presetAdjustList);
        }

        public ModelShapeCustomGeometry GetShapeGeometry() => 
            (this.ShapeType == ShapePreset.None) ? this.CustomGeometry : ShapesPresetGeometry.GetCustomGeometryByPreset(this.ShapeType);

        public bool IsConnectionShape()
        {
            ShapePreset shapeType = this.ShapeType;
            if (shapeType != ShapePreset.Line)
            {
                switch (shapeType)
                {
                    case ShapePreset.StraightConnector1:
                    case ShapePreset.BentConnector2:
                    case ShapePreset.BentConnector3:
                    case ShapePreset.BentConnector4:
                    case ShapePreset.BentConnector5:
                    case ShapePreset.CurvedConnector2:
                    case ShapePreset.CurvedConnector3:
                    case ShapePreset.CurvedConnector4:
                    case ShapePreset.CurvedConnector5:
                        break;

                    default:
                        return false;
                }
            }
            return true;
        }

        private void OnCustomGeometryChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(CustomGeometryPropertyKey, sender, e);
        }

        private void OnOutlineChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(OutlinePropertyKey, sender, e);
        }

        private void OnPresetAdjustListModified(object sender, EventArgs e)
        {
            base.Notifier.OnPropertyChanged(PresetAdjustListPropertyKey);
        }

        public override void ResetToStyleCore(bool keepOutline)
        {
            base.ResetToStyleCore(keepOutline);
            if (!keepOutline)
            {
                this.Outline.ResetToStyle();
            }
        }

        private PropertyKey SetShapeTypeCore(ShapePropertiesInfo info, ShapePreset value)
        {
            info.ShapeType = value;
            return ShapeTypePropertyKey;
        }

        public void SetupShapeAdjustList(int?[] values)
        {
            ModelShapeCustomGeometry customGeometryByPreset = ShapesPresetGeometry.GetCustomGeometryByPreset(this.ShapeType);
            this.PresetAdjustList.Clear();
            for (int i = 0; i < customGeometryByPreset.AdjustValues.Count; i++)
            {
                this.PresetAdjustList.Add(((i >= values.Length) || (values[i] == null)) ? customGeometryByPreset.AdjustValues[i] : new ModelShapeGuide(customGeometryByPreset.AdjustValues[i].Name, "val " + values[i].Value));
            }
        }

        public ModelShapeCustomGeometry CustomGeometry =>
            this.customGeometry;

        public DevExpress.Office.Drawing.Outline Outline
        {
            get => 
                this.outline;
            set
            {
                this.Outline.PropertyChanged -= new EventHandler<OfficePropertyChangedEventArgs>(this.OnOutlineChanged);
                this.outline.Parent = null;
                DevExpress.Office.Drawing.Outline outline1 = value;
                if (value == null)
                {
                    DevExpress.Office.Drawing.Outline local1 = value;
                    outline1 = new DevExpress.Office.Drawing.Outline(base.DocumentModel);
                }
                this.outline = outline1;
                this.outline.Parent = base.InnerParent;
                this.Outline.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnOutlineChanged);
            }
        }

        public bool IsAutomatic =>
            (base.Fill.FillType == DrawingFillType.Automatic) && ((this.Outline.Fill.FillType == DrawingFillType.Automatic) && base.EffectStyle.IsDefault);

        public virtual ShapePreset ShapeType
        {
            get => 
                base.Info.ShapeType;
            set
            {
                if (this.ShapeType != value)
                {
                    this.SetPropertyValue<ShapePreset>(new UndoableIndexBasedObject<ShapePropertiesInfo, PropertyKey>.SetPropertyValueDelegate<ShapePreset>(this.SetShapeTypeCore), value);
                }
            }
        }

        public DevExpress.Office.Drawing.OutlineType OutlineType
        {
            get => 
                this.outline.Type;
            set => 
                this.outline.Type = value;
        }

        public DrawingColor OutlineColor
        {
            get => 
                this.outline.Color;
            set => 
                this.outline.Color = value;
        }

        public ModelShapeGuideList PresetAdjustList =>
            this.presetAdjustList;
    }
}

