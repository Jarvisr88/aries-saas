namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.History;
    using System;
    using System.Collections.Generic;

    public class DrawingTextParagraphProperties : DrawingMultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>, ICloneable<DrawingTextParagraphProperties>, ISupportsCopyFrom<DrawingTextParagraphProperties>, IDrawingTextParagraphProperties, IDrawingTextSpacingsChanger, IDrawingTextSpacings, IDrawingTextMargin, IDrawingTextBullets, IDrawingTextParagraphPropertiesOptions
    {
        private static readonly DrawingTextParagraphInfoIndexAccessor textParagraphInfoIndexAccessor = new DrawingTextParagraphInfoIndexAccessor();
        private static readonly IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[] indexAccessors = GetIndexAccessors();
        public const int LineTextSpacingIndex = 0;
        public const int SpaceAfterTextSpacingIndex = 1;
        public const int SpaceBeforeTextSpacingIndex = 2;
        public const int TextSpacingCount = 3;
        private readonly DrawingTextCharacterProperties defaultCharacterProperties;
        private int textParagraphInfoIndex;
        private int[] textSpacingInfoIndexes;
        private DrawingTextSpacingPropertyChangeManager textSpacingsChangeManager;
        private readonly Dictionary<DrawingBulletType, IDrawingBullet> bullets;
        private readonly DrawingTextTabStopCollection tabStopList;

        public DrawingTextParagraphProperties(IDocumentModel documentModel) : base(documentModel)
        {
            DrawingTextCharacterProperties properties1 = new DrawingTextCharacterProperties(documentModel);
            properties1.Parent = base.InnerParent;
            this.defaultCharacterProperties = properties1;
            this.textSpacingInfoIndexes = new int[3];
            this.textSpacingsChangeManager = new DrawingTextSpacingPropertyChangeManager(this);
            this.bullets = new Dictionary<DrawingBulletType, IDrawingBullet>();
            this.AddDefaultBullets();
            this.tabStopList = new DrawingTextTabStopCollection(documentModel);
        }

        private void AddDefaultBullets()
        {
            this.SetBulletCore(DrawingBulletType.Common, DrawingBullet.Automatic);
            this.SetBulletCore(DrawingBulletType.Color, DrawingBullet.Automatic);
            this.SetBulletCore(DrawingBulletType.Typeface, DrawingBullet.Automatic);
            this.SetBulletCore(DrawingBulletType.Size, DrawingBullet.Automatic);
        }

        public void AssignTextParagraphInfoIndex(int value)
        {
            this.textParagraphInfoIndex = value;
        }

        public void AssignTextSpacingInfoIndex(int index, int value)
        {
            if (base.IsUpdateLocked && (base.DrawingCache.DrawingTextSpacingInfoCache.Count > value))
            {
                this.BatchUpdateHelper.TextSpacingInfos[index] = base.DrawingCache.DrawingTextSpacingInfoCache[value];
            }
            else
            {
                this.textSpacingInfoIndexes[index] = value;
            }
        }

        public DrawingTextParagraphProperties Clone()
        {
            DrawingTextParagraphProperties clone = new DrawingTextParagraphProperties(base.DocumentModel);
            base.CloneCore(clone);
            clone.CopyFrom(this);
            return clone;
        }

        public void CopyFrom(DrawingTextParagraphProperties value)
        {
            base.CopyFrom(value);
            this.defaultCharacterProperties.CopyFrom(value.defaultCharacterProperties);
            this.SetBulletCore(DrawingBulletType.Common, value.CommonBullet);
            this.SetBulletCore(DrawingBulletType.Color, value.ColorBullet);
            this.SetBulletCore(DrawingBulletType.Typeface, value.TypefaceBullet);
            this.SetBulletCore(DrawingBulletType.Size, value.SizeBullet);
            this.tabStopList.CopyFrom(value.TabStopList);
        }

        protected override MultiIndexBatchUpdateHelper CreateBatchInitHelper() => 
            new DrawingTextParagraphPropertiesBatchInitHelper(this);

        protected override MultiIndexBatchUpdateHelper CreateBatchUpdateHelper() => 
            new DrawingTextParagraphPropertiesBatchUpdateHelper(this);

        DrawingTextSpacingValueType IDrawingTextSpacingsChanger.GetType(int index) => 
            this.GetTextSpacingInfo(index).Type;

        int IDrawingTextSpacingsChanger.GetValue(int index) => 
            this.GetTextSpacingInfo(index).Value;

        void IDrawingTextSpacingsChanger.SetType(int index, DrawingTextSpacingValueType value)
        {
            if (this.GetTextSpacingInfo(index).Type != value)
            {
                this.SetPropertyValue<DrawingTextSpacingInfo, DrawingTextSpacingValueType>(this.GetTextSpacingInfoIndexAccessor(index), new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextSpacingInfo, DrawingTextSpacingValueType>(this.SetTypeCore), value);
            }
        }

        void IDrawingTextSpacingsChanger.SetValue(int index, int value)
        {
            if ((this.GetTextSpacingInfo(index).Value != value) && (this.GetTextSpacingInfo(index).Type != DrawingTextSpacingValueType.Automatic))
            {
                this.SetPropertyValue<DrawingTextSpacingInfo, int>(this.GetTextSpacingInfoIndexAccessor(index), new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextSpacingInfo, int>(this.SetValueCore), value);
            }
        }

        public override bool Equals(object obj)
        {
            DrawingTextParagraphProperties properties = obj as DrawingTextParagraphProperties;
            return ((properties != null) ? (base.Equals(properties) && (this.defaultCharacterProperties.Equals(properties.defaultCharacterProperties) && (this.CommonBullet.Equals(properties.CommonBullet) && (this.ColorBullet.Equals(properties.ColorBullet) && (this.TypefaceBullet.Equals(properties.TypefaceBullet) && (this.SizeBullet.Equals(properties.SizeBullet) && this.tabStopList.Equals(properties.tabStopList))))))) : false);
        }

        public override DocumentModelChangeActions GetBatchUpdateChangeActions() => 
            DocumentModelChangeActions.None;

        public override int GetHashCode() => 
            (((((base.GetHashCode() ^ this.defaultCharacterProperties.GetHashCode()) ^ this.CommonBullet.GetHashCode()) ^ this.ColorBullet.GetHashCode()) ^ this.TypefaceBullet.GetHashCode()) ^ this.SizeBullet.GetHashCode()) ^ this.tabStopList.GetHashCode();

        private static IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[] GetIndexAccessors()
        {
            IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[] baseArray = new IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[4];
            for (int i = 0; i < 3; i++)
            {
                baseArray[i] = new DrawingTextSpacingInfoIndexAccessor(i);
            }
            baseArray[3] = textParagraphInfoIndexAccessor;
            return baseArray;
        }

        public override DrawingTextParagraphProperties GetOwner() => 
            this;

        internal DrawingTextSpacingInfo GetTextSpacingInfo(int index) => 
            (this.BatchUpdateHelper == null) ? base.DrawingCache.DrawingTextSpacingInfoCache[this.TextSpacingInfoIndexes[index]] : this.BatchUpdateHelper.TextSpacingInfos[index];

        private DrawingTextSpacingInfoIndexAccessor GetTextSpacingInfoIndexAccessor(int index) => 
            (DrawingTextSpacingInfoIndexAccessor) TextParagraphPropertiesIndexAccessors[index];

        public void ResetToStyle()
        {
            base.BeginUpdate();
            try
            {
                this.TextParagraphInfo.CopyFrom(base.DrawingCache.DrawingTextParagraphInfoCache.DefaultItem);
                this.GetTextSpacingInfo(0).CopyFrom(base.DrawingCache.DrawingTextSpacingInfoCache.DefaultItem);
                this.GetTextSpacingInfo(1).CopyFrom(base.DrawingCache.DrawingTextSpacingInfoCache.DefaultItem);
                this.GetTextSpacingInfo(2).CopyFrom(base.DrawingCache.DrawingTextSpacingInfoCache.DefaultItem);
            }
            finally
            {
                base.EndUpdate();
            }
            this.defaultCharacterProperties.ResetToStyle();
            this.SetBullet(DrawingBulletType.Common, DrawingBullet.Automatic);
            this.SetBullet(DrawingBulletType.Color, DrawingBullet.Automatic);
            this.SetBullet(DrawingBulletType.Typeface, DrawingBullet.Automatic);
            this.SetBullet(DrawingBulletType.Size, DrawingBullet.Automatic);
            this.TabStopList.Clear();
        }

        private DocumentModelChangeActions SetApplyDefaultCharacterProperties(DrawingTextParagraphInfo info, bool value)
        {
            info.ApplyDefaultCharacterProperties = value;
            return DocumentModelChangeActions.None;
        }

        private void SetBullet(DrawingBulletType type, IDrawingBullet value)
        {
            if ((value.Type != DrawingBulletType.Automatic) && (type != value.Type))
            {
                throw new ArgumentException("Invalid DrawingBulletType.");
            }
            IDrawingBullet oldValue = this.bullets[type];
            if (!oldValue.Equals(value))
            {
                HistoryItem item = new DrawingTextParagraphPropertiesBulletChangedHistoryItem(this, type, oldValue, value);
                base.DocumentModel.History.Add(item);
                item.Execute();
            }
        }

        public void SetBulletCore(DrawingBulletType type, IDrawingBullet value)
        {
            this.bullets[type] = value;
        }

        private DocumentModelChangeActions SetDefaultTabSize(DrawingTextParagraphInfo info, float value)
        {
            info.HasDefaultTabSize = true;
            info.DefaultTabSize = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetEastAsianLineBreak(DrawingTextParagraphInfo info, bool value)
        {
            info.HasEastAsianLineBreak = true;
            info.EastAsianLineBreak = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetFontAlignment(DrawingTextParagraphInfo info, DrawingFontAlignmentType value)
        {
            info.HasFontAlignment = true;
            info.FontAlignment = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetHangingPunctuation(DrawingTextParagraphInfo info, bool value)
        {
            info.HasHangingPunctuation = true;
            info.HangingPunctuation = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetIndent(DrawingTextParagraphInfo info, float value)
        {
            info.HasIndent = true;
            info.Indent = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetLatinLineBreak(DrawingTextParagraphInfo info, bool value)
        {
            info.HasLatinLineBreak = true;
            info.LatinLineBreak = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetLeftMargin(DrawingTextParagraphInfo info, float value)
        {
            info.HasLeftMargin = true;
            info.LeftMargin = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetRightMargin(DrawingTextParagraphInfo info, float value)
        {
            info.HasRightMargin = true;
            info.RightMargin = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetRightToLeft(DrawingTextParagraphInfo info, bool value)
        {
            info.HasRightToLeft = true;
            info.RightToLeft = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetTextAlignment(DrawingTextParagraphInfo info, DrawingTextAlignmentType value)
        {
            info.HasTextAlignment = true;
            info.TextAlignment = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetTextIndentLevel(DrawingTextParagraphInfo info, int value)
        {
            info.HasTextIndentLevel = true;
            info.TextIndentLevel = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetTypeCore(DrawingTextSpacingInfo info, DrawingTextSpacingValueType value)
        {
            info.Type = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetValueCore(DrawingTextSpacingInfo info, int value)
        {
            info.Value = value;
            return DocumentModelChangeActions.None;
        }

        public static IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[] TextParagraphPropertiesIndexAccessors =>
            indexAccessors;

        public static DrawingTextParagraphInfoIndexAccessor TextParagraphInfoIndexAccessor =>
            textParagraphInfoIndexAccessor;

        public int TextParagraphInfoIndex =>
            this.textParagraphInfoIndex;

        public int[] TextSpacingInfoIndexes =>
            this.textSpacingInfoIndexes;

        public int LineSpacingInfoIndex =>
            this.textSpacingInfoIndexes[0];

        public int SpaceAfterInfoIndex =>
            this.textSpacingInfoIndexes[1];

        public int SpaceBeforeInfoIndex =>
            this.textSpacingInfoIndexes[2];

        protected override IIndexAccessorBase<DrawingTextParagraphProperties, DocumentModelChangeActions>[] IndexAccessors =>
            indexAccessors;

        internal DrawingTextParagraphPropertiesBatchUpdateHelper BatchUpdateHelper =>
            (DrawingTextParagraphPropertiesBatchUpdateHelper) base.BatchUpdateHelper;

        internal DrawingTextParagraphInfo TextParagraphInfo =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.TextParagraphInfo : this.TextParagraphInfoCore;

        private DrawingTextParagraphInfo TextParagraphInfoCore =>
            textParagraphInfoIndexAccessor.GetInfo(this);

        private IDrawingBullet CommonBullet =>
            this.bullets[DrawingBulletType.Common];

        private IDrawingBullet ColorBullet =>
            this.bullets[DrawingBulletType.Color];

        private IDrawingBullet TypefaceBullet =>
            this.bullets[DrawingBulletType.Typeface];

        private IDrawingBullet SizeBullet =>
            this.bullets[DrawingBulletType.Size];

        public bool IsDefault =>
            (this.TextParagraphInfoIndex == 0) && ((this.LineSpacingInfoIndex == 0) && ((this.SpaceAfterInfoIndex == 0) && ((this.SpaceBeforeInfoIndex == 0) && (this.defaultCharacterProperties.IsDefault && ((this.CommonBullet.Type == DrawingBulletType.Automatic) && ((this.ColorBullet.Type == DrawingBulletType.Automatic) && ((this.TypefaceBullet.Type == DrawingBulletType.Automatic) && ((this.SizeBullet.Type == DrawingBulletType.Automatic) && (this.TabStopList.Count == 0)))))))));

        public IDrawingTextSpacings Spacings =>
            this;

        public IDrawingTextMargin Margin =>
            this;

        public IDrawingTextBullets Bullets =>
            this;

        public DrawingTextCharacterProperties DefaultCharacterProperties =>
            this.defaultCharacterProperties;

        public DrawingTextTabStopCollection TabStopList =>
            this.tabStopList;

        public DrawingTextAlignmentType TextAlignment
        {
            get => 
                this.TextParagraphInfo.TextAlignment;
            set
            {
                if ((this.TextParagraphInfo.TextAlignment != value) || !this.TextParagraphInfo.HasTextAlignment)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, DrawingTextAlignmentType>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, DrawingTextAlignmentType>(this.SetTextAlignment), value);
                }
            }
        }

        public DrawingFontAlignmentType FontAlignment
        {
            get => 
                this.TextParagraphInfo.FontAlignment;
            set
            {
                if ((this.TextParagraphInfo.FontAlignment != value) || !this.TextParagraphInfo.HasFontAlignment)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, DrawingFontAlignmentType>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, DrawingFontAlignmentType>(this.SetFontAlignment), value);
                }
            }
        }

        public int TextIndentLevel
        {
            get => 
                this.TextParagraphInfo.TextIndentLevel;
            set
            {
                if ((this.TextParagraphInfo.TextIndentLevel != value) || !this.TextParagraphInfo.HasTextIndentLevel)
                {
                    DrawingValueChecker.CheckTextIndentLevelValue(value);
                    this.SetPropertyValue<DrawingTextParagraphInfo, int>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, int>(this.SetTextIndentLevel), value);
                }
            }
        }

        public float DefaultTabSize
        {
            get => 
                this.TextParagraphInfo.DefaultTabSize;
            set
            {
                if ((this.TextParagraphInfo.DefaultTabSize != value) || !this.TextParagraphInfo.HasDefaultTabSize)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, float>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, float>(this.SetDefaultTabSize), value);
                }
            }
        }

        public float Indent
        {
            get => 
                this.TextParagraphInfo.Indent;
            set
            {
                if ((this.TextParagraphInfo.Indent != value) || !this.TextParagraphInfo.HasIndent)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, float>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, float>(this.SetIndent), value);
                }
            }
        }

        public bool RightToLeft
        {
            get => 
                this.TextParagraphInfo.RightToLeft;
            set
            {
                if ((this.TextParagraphInfo.RightToLeft != value) || !this.TextParagraphInfo.HasRightToLeft)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, bool>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, bool>(this.SetRightToLeft), value);
                }
            }
        }

        public bool HangingPunctuation
        {
            get => 
                this.TextParagraphInfo.HangingPunctuation;
            set
            {
                if ((this.TextParagraphInfo.HangingPunctuation != value) || !this.TextParagraphInfo.HasHangingPunctuation)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, bool>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, bool>(this.SetHangingPunctuation), value);
                }
            }
        }

        public bool EastAsianLineBreak
        {
            get => 
                this.TextParagraphInfo.EastAsianLineBreak;
            set
            {
                if ((this.TextParagraphInfo.EastAsianLineBreak != value) || !this.TextParagraphInfo.HasEastAsianLineBreak)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, bool>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, bool>(this.SetEastAsianLineBreak), value);
                }
            }
        }

        public bool LatinLineBreak
        {
            get => 
                this.TextParagraphInfo.LatinLineBreak;
            set
            {
                if ((this.TextParagraphInfo.LatinLineBreak != value) || !this.TextParagraphInfo.HasLatinLineBreak)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, bool>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, bool>(this.SetLatinLineBreak), value);
                }
            }
        }

        public bool ApplyDefaultCharacterProperties
        {
            get => 
                this.TextParagraphInfo.ApplyDefaultCharacterProperties;
            set
            {
                if (this.TextParagraphInfo.ApplyDefaultCharacterProperties != value)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, bool>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, bool>(this.SetApplyDefaultCharacterProperties), value);
                }
            }
        }

        public IDrawingTextParagraphPropertiesOptions Options =>
            this;

        bool IDrawingTextParagraphPropertiesOptions.HasDefaultTabSize =>
            this.TextParagraphInfo.HasDefaultTabSize;

        bool IDrawingTextParagraphPropertiesOptions.HasEastAsianLineBreak =>
            this.TextParagraphInfo.HasEastAsianLineBreak;

        bool IDrawingTextParagraphPropertiesOptions.HasFontAlignment =>
            this.TextParagraphInfo.HasFontAlignment;

        bool IDrawingTextParagraphPropertiesOptions.HasHangingPunctuation =>
            this.TextParagraphInfo.HasHangingPunctuation;

        bool IDrawingTextParagraphPropertiesOptions.HasIndent =>
            this.TextParagraphInfo.HasIndent;

        bool IDrawingTextParagraphPropertiesOptions.HasLatinLineBreak =>
            this.TextParagraphInfo.HasLatinLineBreak;

        bool IDrawingTextParagraphPropertiesOptions.HasLeftMargin =>
            this.TextParagraphInfo.HasLeftMargin;

        bool IDrawingTextParagraphPropertiesOptions.HasRightMargin =>
            this.TextParagraphInfo.HasRightMargin;

        bool IDrawingTextParagraphPropertiesOptions.HasRightToLeft =>
            this.TextParagraphInfo.HasRightToLeft;

        bool IDrawingTextParagraphPropertiesOptions.HasTextAlignment =>
            this.TextParagraphInfo.HasTextAlignment;

        bool IDrawingTextParagraphPropertiesOptions.HasTextIndentLevel =>
            this.TextParagraphInfo.HasTextIndentLevel;

        IDrawingTextSpacing IDrawingTextSpacings.Line =>
            this.textSpacingsChangeManager.GetFormatInfo(0);

        IDrawingTextSpacing IDrawingTextSpacings.SpaceAfter =>
            this.textSpacingsChangeManager.GetFormatInfo(1);

        IDrawingTextSpacing IDrawingTextSpacings.SpaceBefore =>
            this.textSpacingsChangeManager.GetFormatInfo(2);

        float IDrawingTextMargin.Left
        {
            get => 
                this.TextParagraphInfo.LeftMargin;
            set
            {
                if ((this.TextParagraphInfo.LeftMargin != value) || !this.TextParagraphInfo.HasLeftMargin)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, float>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, float>(this.SetLeftMargin), value);
                }
            }
        }

        float IDrawingTextMargin.Right
        {
            get => 
                this.TextParagraphInfo.RightMargin;
            set
            {
                if ((this.TextParagraphInfo.RightMargin != value) || !this.TextParagraphInfo.HasRightMargin)
                {
                    this.SetPropertyValue<DrawingTextParagraphInfo, float>(TextParagraphInfoIndexAccessor, new MultiIndexObject<DrawingTextParagraphProperties, DocumentModelChangeActions>.SetPropertyValueDelegate<DrawingTextParagraphInfo, float>(this.SetRightMargin), value);
                }
            }
        }

        IDrawingBullet IDrawingTextBullets.Common
        {
            get => 
                this.CommonBullet;
            set => 
                this.SetBullet(DrawingBulletType.Common, value);
        }

        IDrawingBullet IDrawingTextBullets.Color
        {
            get => 
                this.ColorBullet;
            set => 
                this.SetBullet(DrawingBulletType.Color, value);
        }

        IDrawingBullet IDrawingTextBullets.Typeface
        {
            get => 
                this.TypefaceBullet;
            set => 
                this.SetBullet(DrawingBulletType.Typeface, value);
        }

        IDrawingBullet IDrawingTextBullets.Size
        {
            get => 
                this.SizeBullet;
            set => 
                this.SetBullet(DrawingBulletType.Size, value);
        }
    }
}

