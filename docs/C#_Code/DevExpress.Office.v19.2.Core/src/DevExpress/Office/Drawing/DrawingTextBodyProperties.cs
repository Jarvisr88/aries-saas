namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class DrawingTextBodyProperties : DrawingUndoableIndexBasedObject<DrawingTextBodyInfo>, ICloneable<DrawingTextBodyProperties>, ISupportsCopyFrom<DrawingTextBodyProperties>, ITextBodyOptions
    {
        private readonly DrawingTextInset inset;
        private readonly Scene3DProperties scene3d;
        private IDrawingText3D text3D;
        private IDrawingTextAutoFit autoFit;
        private DrawingPresetTextWarp presetTextWarp;
        private ModelShapeGuideList presetAdjustValues;

        public DrawingTextBodyProperties(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.inset = new DrawingTextInset(documentModel);
            this.scene3d = new Scene3DProperties(documentModel);
            this.autoFit = DrawingTextAutoFit.Automatic;
            this.text3D = DrawingText3D.Automatic;
            this.presetTextWarp = DrawingPresetTextWarp.NoShape;
            this.presetAdjustValues = new ModelShapeGuideList(base.DocumentModelPart);
        }

        public DrawingTextBodyProperties Clone()
        {
            DrawingTextBodyProperties properties = new DrawingTextBodyProperties(base.DocumentModel);
            properties.CopyFrom(this);
            return properties;
        }

        public void CopyFrom(DrawingTextBodyProperties value)
        {
            Guard.ArgumentNotNull(value, "value");
            base.CopyFrom(value);
            this.inset.CopyFrom(value.inset);
            this.scene3d.CopyFrom(value.scene3d);
            this.autoFit = value.autoFit.CloneTo(base.DocumentModel);
            this.text3D = value.text3D.CloneTo(base.DocumentModel);
            this.presetTextWarp = value.PresetTextWarp;
            this.presetAdjustValues = value.PresetAdjustValues.Clone();
        }

        public override bool Equals(object obj)
        {
            DrawingTextBodyProperties properties = obj as DrawingTextBodyProperties;
            return ((properties != null) ? (base.Info.Equals(properties.Info) && (this.inset.Equals(properties.inset) && (this.scene3d.Equals(properties.scene3d) && (this.autoFit.Equals(properties.autoFit) && this.text3D.Equals(properties.text3D))))) : false);
        }

        protected internal override UniqueItemsCache<DrawingTextBodyInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.DrawingTextBodyInfoCache;

        public override int GetHashCode() => 
            ((((base.Info.GetHashCode() ^ this.inset.GetHashCode()) ^ this.scene3d.GetHashCode()) ^ this.autoFit.GetHashCode()) ^ this.text3D.GetHashCode()) ^ this.presetTextWarp.GetHashCode();

        private bool IsValidAutoFit(IDrawingTextAutoFit value) => 
            (value.Type == DrawingTextAutoFitType.None) || ((value.Type == DrawingTextAutoFitType.Normal) || ((value.Type == DrawingTextAutoFitType.Shape) || (value.Type == DrawingTextAutoFitType.Automatic)));

        private bool IsValidText3D(IDrawingText3D value) => 
            (value.Type == DrawingText3DType.Automatic) || ((value.Type == DrawingText3DType.Shape3D) || (value.Type == DrawingText3DType.FlatText));

        public void ResetToStyle()
        {
            if (base.IsUpdateLocked)
            {
                base.Info.CopyFrom(base.DrawingCache.DrawingTextBodyInfoCache.DefaultItem);
            }
            else
            {
                this.ChangeIndexCore(0, DocumentModelChangeActions.None);
            }
            this.inset.ResetToStyle();
            this.scene3d.ResetToStyle();
            this.AutoFit = DrawingTextAutoFit.Automatic;
            this.Text3D = DrawingText3D.Automatic;
        }

        private DocumentModelChangeActions SetAnchorCenterCore(DrawingTextBodyInfo info, bool value)
        {
            info.AnchorCenter = value;
            info.HasAnchorCenter = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetAnchorCore(DrawingTextBodyInfo info, DrawingTextAnchoringType value)
        {
            info.Anchor = value;
            info.HasAnchor = true;
            return DocumentModelChangeActions.None;
        }

        private void SetAutoFit(IDrawingTextAutoFit value)
        {
            DrawingTextBodyPropertiesAutoFitChangedHistoryItem item = new DrawingTextBodyPropertiesAutoFitChangedHistoryItem(this, this.autoFit, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void SetAutoFitCore(IDrawingTextAutoFit value)
        {
            this.autoFit = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetCompatibleLineSpacingCore(DrawingTextBodyInfo info, bool value)
        {
            info.CompatibleLineSpacing = value;
            info.HasCompatibleLineSpacing = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetForceAntiAliasCore(DrawingTextBodyInfo info, bool value)
        {
            info.ForceAntiAlias = value;
            info.HasForceAntiAlias = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetFromWordArtCore(DrawingTextBodyInfo info, bool value)
        {
            info.FromWordArt = value;
            info.HasFromWordArt = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetHorizontalOverflowCore(DrawingTextBodyInfo info, DrawingTextHorizontalOverflowType value)
        {
            info.HorizontalOverflow = value;
            info.HasHorizontalOverflow = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNumberOfColumnsCore(DrawingTextBodyInfo info, int value)
        {
            info.NumberOfColumns = value;
            info.HasNumberOfColumns = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetParagraphSpacingCore(DrawingTextBodyInfo info, bool value)
        {
            info.ParagraphSpacing = value;
            info.HasParagraphSpacing = true;
            return DocumentModelChangeActions.None;
        }

        public void SetPresetTextWarpCore(int value)
        {
            this.presetTextWarp = (DrawingPresetTextWarp) value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetRightToLeftColumnsCore(DrawingTextBodyInfo info, bool value)
        {
            info.RightToLeftColumns = value;
            info.HasRightToLeftColumns = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetRotationCore(DrawingTextBodyInfo info, int value)
        {
            info.Rotation = value;
            info.HasRotation = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetSpaceBetweenColumnsCore(DrawingTextBodyInfo info, float value)
        {
            info.SpaceBetweenColumns = value;
            info.HasSpaceBetweenColumns = true;
            return DocumentModelChangeActions.None;
        }

        public void SetText3DCore(IDrawingText3D value)
        {
            this.text3D = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetTextWrappingCore(DrawingTextBodyInfo info, DrawingTextWrappingType value)
        {
            info.TextWrapping = value;
            info.HasTextWrapping = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetUprightTextCore(DrawingTextBodyInfo info, bool value)
        {
            info.UprightText = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetVerticalOverflowCore(DrawingTextBodyInfo info, DrawingTextVerticalOverflowType value)
        {
            info.VerticalOverflow = value;
            info.HasVerticalOverflow = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetVerticalTextCore(DrawingTextBodyInfo info, DrawingTextVerticalTextType value)
        {
            info.VerticalText = value;
            info.HasVerticalText = true;
            return DocumentModelChangeActions.None;
        }

        public DrawingTextInset Inset =>
            this.inset;

        public Scene3DProperties Scene3D =>
            this.scene3d;

        public ITextBodyOptions Options =>
            this;

        public DrawingPresetTextWarp PresetTextWarp
        {
            get => 
                this.presetTextWarp;
            set
            {
                if (this.presetTextWarp != value)
                {
                    ActionIntHistoryItem item = new ActionIntHistoryItem(base.DocumentModelPart, (int) this.presetTextWarp, (int) value, new Action<int>(this.SetPresetTextWarpCore));
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public ModelShapeGuideList PresetAdjustValues =>
            this.presetAdjustValues;

        public IDrawingTextAutoFit AutoFit
        {
            get => 
                this.autoFit;
            set
            {
                value ??= DrawingTextAutoFit.Automatic;
                if (!this.IsValidAutoFit(value))
                {
                    throw new ArgumentException("Wrong autofit type.");
                }
                if (!this.autoFit.Equals(value))
                {
                    this.SetAutoFit(value);
                }
            }
        }

        public DrawingTextAnchoringType Anchor
        {
            get => 
                base.Info.Anchor;
            set
            {
                if ((this.Anchor != value) || !this.Options.HasAnchor)
                {
                    this.SetPropertyValue<DrawingTextAnchoringType>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextAnchoringType>(this.SetAnchorCore), value);
                }
            }
        }

        public DrawingTextHorizontalOverflowType HorizontalOverflow
        {
            get => 
                base.Info.HorizontalOverflow;
            set
            {
                if ((this.HorizontalOverflow != value) || !this.Options.HasHorizontalOverflow)
                {
                    this.SetPropertyValue<DrawingTextHorizontalOverflowType>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextHorizontalOverflowType>(this.SetHorizontalOverflowCore), value);
                }
            }
        }

        public DrawingTextVerticalOverflowType VerticalOverflow
        {
            get => 
                base.Info.VerticalOverflow;
            set
            {
                if ((this.VerticalOverflow != value) || !this.Options.HasVerticalOverflow)
                {
                    this.SetPropertyValue<DrawingTextVerticalOverflowType>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextVerticalOverflowType>(this.SetVerticalOverflowCore), value);
                }
            }
        }

        public DrawingTextWrappingType TextWrapping
        {
            get => 
                base.Info.TextWrapping;
            set
            {
                if ((this.TextWrapping != value) || !this.Options.HasTextWrapping)
                {
                    this.SetPropertyValue<DrawingTextWrappingType>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextWrappingType>(this.SetTextWrappingCore), value);
                }
            }
        }

        public DrawingTextVerticalTextType VerticalText
        {
            get => 
                base.Info.VerticalText;
            set
            {
                if ((this.VerticalText != value) || !this.Options.HasVerticalText)
                {
                    this.SetPropertyValue<DrawingTextVerticalTextType>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextVerticalTextType>(this.SetVerticalTextCore), value);
                }
            }
        }

        public bool AnchorCenter
        {
            get => 
                base.Info.AnchorCenter;
            set
            {
                if ((this.AnchorCenter != value) || !this.Options.HasAnchorCenter)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetAnchorCenterCore), value);
                }
            }
        }

        public bool CompatibleLineSpacing
        {
            get => 
                base.Info.CompatibleLineSpacing;
            set
            {
                if ((this.CompatibleLineSpacing != value) || !this.Options.HasCompatibleLineSpacing)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetCompatibleLineSpacingCore), value);
                }
            }
        }

        public bool ForceAntiAlias
        {
            get => 
                base.Info.ForceAntiAlias;
            set
            {
                if ((this.ForceAntiAlias != value) || !this.Options.HasForceAntiAlias)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetForceAntiAliasCore), value);
                }
            }
        }

        public bool FromWordArt
        {
            get => 
                base.Info.FromWordArt;
            set
            {
                if ((this.FromWordArt != value) || !this.Options.HasFromWordArt)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetFromWordArtCore), value);
                }
            }
        }

        public bool RightToLeftColumns
        {
            get => 
                base.Info.RightToLeftColumns;
            set
            {
                if ((this.RightToLeftColumns != value) || !this.Options.HasRightToLeftColumns)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetRightToLeftColumnsCore), value);
                }
            }
        }

        public bool ParagraphSpacing
        {
            get => 
                base.Info.ParagraphSpacing;
            set
            {
                if ((this.ParagraphSpacing != value) || !this.Options.HasParagraphSpacing)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetParagraphSpacingCore), value);
                }
            }
        }

        public bool UprightText
        {
            get => 
                base.Info.UprightText;
            set
            {
                if (this.UprightText != value)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetUprightTextCore), value);
                }
            }
        }

        public int NumberOfColumns
        {
            get => 
                base.Info.NumberOfColumns;
            set
            {
                if ((this.NumberOfColumns != value) || !this.Options.HasNumberOfColumns)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetNumberOfColumnsCore), value);
                }
            }
        }

        public int Rotation
        {
            get => 
                base.Info.Rotation;
            set
            {
                if ((this.Rotation != value) || !this.Options.HasRotation)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetRotationCore), value);
                }
            }
        }

        public float SpaceBetweenColumns
        {
            get => 
                base.Info.SpaceBetweenColumns;
            set
            {
                if ((this.SpaceBetweenColumns != value) || !this.Options.HasSpaceBetweenColumns)
                {
                    this.SetPropertyValue<float>(new UndoableIndexBasedObject<DrawingTextBodyInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<float>(this.SetSpaceBetweenColumnsCore), value);
                }
            }
        }

        public IDrawingText3D Text3D
        {
            get => 
                this.text3D;
            set
            {
                value ??= DrawingText3D.Automatic;
                if (!this.IsValidText3D(value))
                {
                    throw new ArgumentException("Wrong text3d type.");
                }
                if (!ReferenceEquals(this.text3D, value))
                {
                    DrawingTextBodyPropertiesText3DChangedHistoryItem item = new DrawingTextBodyPropertiesText3DChangedHistoryItem(this, this.text3D, value);
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        bool ITextBodyOptions.HasRotation =>
            base.Info.HasRotation;

        bool ITextBodyOptions.HasParagraphSpacing =>
            base.Info.HasParagraphSpacing;

        bool ITextBodyOptions.HasVerticalOverflow =>
            base.Info.HasVerticalOverflow;

        bool ITextBodyOptions.HasHorizontalOverflow =>
            base.Info.HasHorizontalOverflow;

        bool ITextBodyOptions.HasVerticalText =>
            base.Info.HasVerticalText;

        bool ITextBodyOptions.HasTextWrapping =>
            base.Info.HasTextWrapping;

        bool ITextBodyOptions.HasNumberOfColumns =>
            base.Info.HasNumberOfColumns;

        bool ITextBodyOptions.HasSpaceBetweenColumns =>
            base.Info.HasSpaceBetweenColumns;

        bool ITextBodyOptions.HasRightToLeftColumns =>
            base.Info.HasRightToLeftColumns;

        bool ITextBodyOptions.HasFromWordArt =>
            base.Info.HasFromWordArt;

        bool ITextBodyOptions.HasAnchor =>
            base.Info.HasAnchor;

        bool ITextBodyOptions.HasAnchorCenter =>
            base.Info.HasAnchorCenter;

        bool ITextBodyOptions.HasForceAntiAlias =>
            base.Info.HasForceAntiAlias;

        bool ITextBodyOptions.HasCompatibleLineSpacing =>
            base.Info.HasCompatibleLineSpacing;

        public bool IsDefault =>
            (base.Index == 0) && (this.inset.IsDefault && (this.scene3d.IsDefault && ((this.text3D.Type == DrawingText3DType.Automatic) && (this.autoFit.Type == DrawingTextAutoFitType.Automatic))));
    }
}

