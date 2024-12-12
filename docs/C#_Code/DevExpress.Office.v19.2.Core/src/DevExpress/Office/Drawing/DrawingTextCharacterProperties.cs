namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public class DrawingTextCharacterProperties : DrawingUndoableIndexBasedObject<DrawingTextCharacterInfo>, ICloneable<DrawingTextCharacterProperties>, ISupportsCopyFrom<DrawingTextCharacterProperties>, IFillOwner, ITextCharacterOptions, IDrawingTextCharacterProperties
    {
        private IDrawingFill fill;
        private DevExpress.Office.Drawing.Outline outline;
        private ContainerEffect effects;
        private DrawingColor highlight;
        private IUnderlineFill underlineFill;
        private IStrokeUnderline strokeUnderline;
        private DrawingTextFont eastAsian;
        private DrawingTextFont latin;
        private DrawingTextFont symbol;
        private DrawingTextFont complexScript;
        private CultureInfo language;
        private CultureInfo alternateLanguage;
        private string bookmark;

        public DrawingTextCharacterProperties(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.bookmark = string.Empty;
            this.fill = DrawingFill.Automatic;
            DevExpress.Office.Drawing.Outline outline1 = new DevExpress.Office.Drawing.Outline(documentModel);
            outline1.Parent = base.InnerParent;
            this.outline = outline1;
            this.effects = new ContainerEffect(documentModel);
            DrawingColor color1 = new DrawingColor(documentModel);
            color1.Parent = base.InnerParent;
            this.highlight = color1;
            this.underlineFill = DrawingFill.Automatic;
            this.strokeUnderline = DrawingStrokeUnderline.Automatic;
            DrawingTextFont font1 = new DrawingTextFont(documentModel);
            font1.Parent = base.InnerParent;
            this.eastAsian = font1;
            DrawingTextFont font2 = new DrawingTextFont(documentModel);
            font2.Parent = base.InnerParent;
            this.latin = font2;
            DrawingTextFont font3 = new DrawingTextFont(documentModel);
            font3.Parent = base.InnerParent;
            this.symbol = font3;
            DrawingTextFont font4 = new DrawingTextFont(documentModel);
            font4.Parent = base.InnerParent;
            this.complexScript = font4;
            this.language = CultureInfo.InvariantCulture;
            this.alternateLanguage = CultureInfo.InvariantCulture;
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public DrawingTextCharacterProperties Clone()
        {
            DrawingTextCharacterProperties properties = new DrawingTextCharacterProperties(base.DocumentModel);
            properties.CopyFrom(this);
            return properties;
        }

        public void CopyFrom(DrawingTextCharacterProperties value)
        {
            Guard.ArgumentNotNull(value, "value");
            base.CopyFrom(value);
            this.fill = value.fill.CloneTo(base.DocumentModel);
            this.outline.CopyFrom(value.outline);
            this.effects.CopyFrom(value.effects);
            this.highlight.CopyFrom(value.highlight);
            this.underlineFill = value.underlineFill.CloneTo(base.DocumentModel);
            this.strokeUnderline = value.strokeUnderline.CloneTo(base.DocumentModel);
            this.bookmark = value.bookmark;
            this.complexScript.CopyFrom(value.complexScript);
            this.eastAsian.CopyFrom(value.eastAsian);
            this.latin.CopyFrom(value.latin);
            this.symbol.CopyFrom(value.symbol);
            this.language = (CultureInfo) value.language.Clone();
            this.alternateLanguage = (CultureInfo) value.alternateLanguage.Clone();
        }

        public override bool Equals(object obj)
        {
            DrawingTextCharacterProperties properties = obj as DrawingTextCharacterProperties;
            return ((properties != null) ? (base.Info.Equals(properties.Info) && (this.fill.Equals(properties.fill) && (this.outline.Equals(properties.outline) && (this.effects.Equals(properties.effects) && (this.highlight.Equals(properties.highlight) && (this.underlineFill.Equals(properties.underlineFill) && (this.strokeUnderline.Equals(properties.strokeUnderline) && ((this.bookmark == properties.bookmark) && (this.latin.Equals(properties.latin) && (this.eastAsian.Equals(properties.eastAsian) && (this.complexScript.Equals(properties.complexScript) && (this.symbol.Equals(properties.symbol) && (this.language.Equals(properties.language) && this.alternateLanguage.Equals(properties.alternateLanguage)))))))))))))) : false);
        }

        protected internal override UniqueItemsCache<DrawingTextCharacterInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.DrawingTextCharacterInfoCache;

        public override int GetHashCode() => 
            ((((((((((((base.Info.GetHashCode() ^ this.fill.GetHashCode()) ^ this.outline.GetHashCode()) ^ this.effects.GetHashCode()) ^ this.highlight.GetHashCode()) ^ this.underlineFill.GetHashCode()) ^ this.strokeUnderline.GetHashCode()) ^ this.bookmark.GetHashCode()) ^ this.latin.GetHashCode()) ^ this.eastAsian.GetHashCode()) ^ this.complexScript.GetHashCode()) ^ this.symbol.GetHashCode()) ^ this.language.GetHashCode()) ^ this.alternateLanguage.GetHashCode();

        public void ResetToStyle()
        {
            if (base.IsUpdateLocked)
            {
                base.Info.CopyFrom(base.DrawingCache.DrawingTextCharacterInfoCache.DefaultItem);
            }
            else
            {
                this.ChangeIndexCore(0, DocumentModelChangeActions.None);
            }
            this.Fill = DrawingFill.Automatic;
            this.outline.ResetToStyle();
            this.effects.Effects.Clear();
            this.highlight.Clear();
            this.UnderlineFill = DrawingFill.Automatic;
            this.StrokeUnderline = DrawingStrokeUnderline.Automatic;
            this.eastAsian.Clear();
            this.latin.Clear();
            this.symbol.Clear();
            this.complexScript.Clear();
            this.Bookmark = string.Empty;
            this.Language = CultureInfo.InvariantCulture;
            this.AlternateLanguage = CultureInfo.InvariantCulture;
        }

        public void SetAlternateLanguageCore(CultureInfo value)
        {
            this.alternateLanguage = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetApplyFontSizeCore(DrawingTextCharacterInfo info, bool value)
        {
            info.ApplyFontSize = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetBaselineCore(DrawingTextCharacterInfo info, int value)
        {
            info.Baseline = value;
            info.HasBaseline = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetBoldCore(DrawingTextCharacterInfo info, bool value)
        {
            info.Bold = value;
            info.HasBold = true;
            return DocumentModelChangeActions.None;
        }

        protected internal void SetBookmarkCore(string value)
        {
            this.bookmark = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetCapsCore(DrawingTextCharacterInfo info, DrawingTextCapsType value)
        {
            info.Caps = value;
            info.HasCaps = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetDirtyCore(DrawingTextCharacterInfo info, bool value)
        {
            info.Dirty = value;
            return DocumentModelChangeActions.None;
        }

        public void SetDrawingFillCore(IDrawingFill value)
        {
            this.fill.Parent = null;
            this.fill = value;
            this.fill.Parent = base.InnerParent;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetFontSizeCore(DrawingTextCharacterInfo info, int value)
        {
            info.FontSize = value;
            info.HasFontSize = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetItalicCore(DrawingTextCharacterInfo info, bool value)
        {
            info.Italic = value;
            info.HasItalic = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetKerningCore(DrawingTextCharacterInfo info, int value)
        {
            info.Kerning = value;
            info.HasKerning = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetKumimojiCore(DrawingTextCharacterInfo info, bool value)
        {
            info.Kumimoji = value;
            info.HasKumimoji = true;
            return DocumentModelChangeActions.None;
        }

        public void SetLanguageCore(CultureInfo value)
        {
            this.language = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetNoProofingCore(DrawingTextCharacterInfo info, bool value)
        {
            info.NoProofing = value;
            info.HasNoProofing = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNormalizeHeightCore(DrawingTextCharacterInfo info, bool value)
        {
            info.NormalizeHeight = value;
            info.HasNormalizeHeight = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetSmartTagCleanCore(DrawingTextCharacterInfo info, bool value)
        {
            info.SmartTagClean = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetSmartTagIdCore(DrawingTextCharacterInfo info, int value)
        {
            info.SmartTagId = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetSpacingCore(DrawingTextCharacterInfo info, int value)
        {
            info.Spacing = value;
            info.HasSpacing = true;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetSpellingErrorCore(DrawingTextCharacterInfo info, bool value)
        {
            info.SpellingError = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetStrikethroughCore(DrawingTextCharacterInfo info, DrawingTextStrikeType value)
        {
            info.Strikethrough = value;
            info.HasStrikethrough = true;
            return DocumentModelChangeActions.None;
        }

        public void SetStrokeUnderlineCore(IStrokeUnderline value)
        {
            this.strokeUnderline = value;
            base.InvalidateParent();
        }

        private DocumentModelChangeActions SetUnderlineCore(DrawingTextCharacterInfo info, DrawingTextUnderlineType value)
        {
            info.Underline = value;
            info.HasUnderline = true;
            return DocumentModelChangeActions.None;
        }

        public void SetUnderlineFillCore(IUnderlineFill value)
        {
            this.underlineFill = value;
            base.InvalidateParent();
        }

        public bool Kumimoji
        {
            get => 
                base.Info.Kumimoji;
            set
            {
                if ((this.Kumimoji != value) || !this.Options.HasKumimoji)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetKumimojiCore), value);
                }
            }
        }

        public bool Bold
        {
            get => 
                base.Info.Bold;
            set
            {
                if ((this.Bold != value) || !this.Options.HasBold)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetBoldCore), value);
                }
            }
        }

        public bool Italic
        {
            get => 
                base.Info.Italic;
            set
            {
                if ((this.Italic != value) || !this.Options.HasItalic)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetItalicCore), value);
                }
            }
        }

        public bool ApplyFontSize
        {
            get => 
                base.Info.ApplyFontSize;
            set
            {
                if (this.ApplyFontSize != value)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetApplyFontSizeCore), value);
                }
            }
        }

        public int FontSize
        {
            get => 
                base.Info.FontSize;
            set
            {
                if ((this.FontSize != value) || !this.Options.HasFontSize)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetFontSizeCore), value);
                }
            }
        }

        public DrawingTextUnderlineType Underline
        {
            get => 
                base.Info.Underline;
            set
            {
                if ((this.Underline != value) || !this.Options.HasUnderline)
                {
                    this.SetPropertyValue<DrawingTextUnderlineType>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextUnderlineType>(this.SetUnderlineCore), value);
                }
            }
        }

        public DrawingTextStrikeType Strikethrough
        {
            get => 
                base.Info.Strikethrough;
            set
            {
                if ((this.Strikethrough != value) || !this.Options.HasStrikethrough)
                {
                    this.SetPropertyValue<DrawingTextStrikeType>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextStrikeType>(this.SetStrikethroughCore), value);
                }
            }
        }

        public int Kerning
        {
            get => 
                base.Info.Kerning;
            set
            {
                if ((this.Kerning != value) || !this.Options.HasKerning)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetKerningCore), value);
                }
            }
        }

        public DrawingTextCapsType Caps
        {
            get => 
                base.Info.Caps;
            set
            {
                if ((this.Caps != value) || !this.Options.HasCaps)
                {
                    this.SetPropertyValue<DrawingTextCapsType>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextCapsType>(this.SetCapsCore), value);
                }
            }
        }

        public int Spacing
        {
            get => 
                base.Info.Spacing;
            set
            {
                if ((this.Spacing != value) || !this.Options.HasSpacing)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetSpacingCore), value);
                }
            }
        }

        public bool NormalizeHeight
        {
            get => 
                base.Info.NormalizeHeight;
            set
            {
                if ((this.NormalizeHeight != value) || !this.Options.HasNormalizeHeight)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNormalizeHeightCore), value);
                }
            }
        }

        public int Baseline
        {
            get => 
                base.Info.Baseline;
            set
            {
                if ((this.Baseline != value) || !this.Options.HasBaseline)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetBaselineCore), value);
                }
            }
        }

        public bool NoProofing
        {
            get => 
                base.Info.NoProofing;
            set
            {
                if ((this.NoProofing != value) || !this.Options.HasNoProofing)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoProofingCore), value);
                }
            }
        }

        public bool Dirty
        {
            get => 
                base.Info.Dirty;
            set
            {
                if (this.Dirty != value)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetDirtyCore), value);
                }
            }
        }

        public bool SpellingError
        {
            get => 
                base.Info.SpellingError;
            set
            {
                if (this.SpellingError != value)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetSpellingErrorCore), value);
                }
            }
        }

        public bool SmartTagClean
        {
            get => 
                base.Info.SmartTagClean;
            set
            {
                if (this.SmartTagClean != value)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetSmartTagCleanCore), value);
                }
            }
        }

        public int SmartTagId
        {
            get => 
                base.Info.SmartTagId;
            set
            {
                if (this.SmartTagId != value)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingTextCharacterInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<int>(this.SetSmartTagIdCore), value);
                }
            }
        }

        public CultureInfo AlternateLanguage
        {
            get => 
                this.alternateLanguage;
            set
            {
                Guard.ArgumentNotNull(value, "AlternateLanguage");
                if (!ReferenceEquals(this.alternateLanguage, value))
                {
                    this.ApplyHistoryItem(new DrawingLanguageChangedHistoryItem(this, true, this.alternateLanguage, value));
                }
            }
        }

        public CultureInfo Language
        {
            get => 
                this.language;
            set
            {
                Guard.ArgumentNotNull(value, "Language");
                if (!ReferenceEquals(this.language, value))
                {
                    this.ApplyHistoryItem(new DrawingLanguageChangedHistoryItem(this, false, this.language, value));
                }
            }
        }

        public IDrawingFill Fill
        {
            get => 
                this.fill;
            set
            {
                value ??= DrawingFill.Automatic;
                if (!this.fill.Equals(value))
                {
                    HistoryItem item = new DrawingFillPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fill, value);
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public DevExpress.Office.Drawing.Outline Outline =>
            this.outline;

        public ContainerEffect Effects =>
            this.effects;

        public DrawingColor Highlight =>
            this.highlight;

        public IUnderlineFill UnderlineFill
        {
            get => 
                this.underlineFill;
            set
            {
                value ??= DrawingFill.Automatic;
                if (!ReferenceEquals(this.underlineFill, value))
                {
                    this.ApplyHistoryItem(new DrawingUnderlineFillChangedHistoryItem(this, this.underlineFill, value));
                }
            }
        }

        public string Bookmark
        {
            get => 
                this.bookmark;
            set
            {
                value ??= string.Empty;
                if (this.bookmark != value)
                {
                    DrawingTextCharacterPropertiesBookmarkChangedHistoryItem item = new DrawingTextCharacterPropertiesBookmarkChangedHistoryItem(this, this.bookmark, value);
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public DrawingTextFont Latin =>
            this.latin;

        public DrawingTextFont EastAsian =>
            this.eastAsian;

        public DrawingTextFont ComplexScript =>
            this.complexScript;

        public DrawingTextFont Symbol =>
            this.symbol;

        public ITextCharacterOptions Options =>
            this;

        public IStrokeUnderline StrokeUnderline
        {
            get => 
                this.strokeUnderline;
            set
            {
                value ??= DrawingStrokeUnderline.Automatic;
                if (!ReferenceEquals(this.strokeUnderline, value))
                {
                    this.ApplyHistoryItem(new DrawingStrokeUnderlineChangedHistoryItem(this, this.strokeUnderline, value));
                }
            }
        }

        bool ITextCharacterOptions.HasKumimoji =>
            base.Info.HasKumimoji;

        bool ITextCharacterOptions.HasFontSize =>
            base.Info.HasFontSize;

        bool ITextCharacterOptions.HasBold =>
            base.Info.HasBold;

        bool ITextCharacterOptions.HasItalic =>
            base.Info.HasItalic;

        bool ITextCharacterOptions.HasUnderline =>
            base.Info.HasUnderline;

        bool ITextCharacterOptions.HasStrikethrough =>
            base.Info.HasStrikethrough;

        bool ITextCharacterOptions.HasKerning =>
            base.Info.HasKerning;

        bool ITextCharacterOptions.HasCaps =>
            base.Info.HasCaps;

        bool ITextCharacterOptions.HasSpacing =>
            base.Info.HasSpacing;

        bool ITextCharacterOptions.HasNormalizeHeight =>
            base.Info.HasNormalizeHeight;

        bool ITextCharacterOptions.HasBaseline =>
            base.Info.HasBaseline;

        bool ITextCharacterOptions.HasNoProofing =>
            base.Info.HasNoProofing;

        public bool IsDefault =>
            ReferenceEquals(this.fill, DrawingFill.Automatic) && (this.outline.IsDefault && ((this.effects.Effects.Count == 0) && (this.highlight.IsEmpty && (ReferenceEquals(this.underlineFill, DrawingFill.Automatic) && (ReferenceEquals(this.strokeUnderline, DrawingStrokeUnderline.Automatic) && (this.eastAsian.IsDefault && (this.latin.IsDefault && (this.symbol.IsDefault && (this.complexScript.IsDefault && ((base.Index == 0) && (string.IsNullOrEmpty(this.bookmark) && (this.language.Equals(CultureInfo.InvariantCulture) && this.alternateLanguage.Equals(CultureInfo.InvariantCulture)))))))))))));
    }
}

