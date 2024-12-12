namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class MaskInfo
    {
        public const char DefaultPlaceHolderValue = '_';
        public const bool DefaultIgnoreBlankValue = true;
        public const bool DefaultSaveLiteralValue = true;
        public const bool DefaultUseAsDisplayFormatValue = false;
        public const bool DefaultAutomaticallyAdvanceCaretValue = false;
        public const bool DefaultShowPlaceHoldersValue = true;
        public const string DefaultDateTimeMaskValue = "d";

        internal MaskInfo(DevExpress.Mvvm.Native.RegExMaskType? regExMaskType, string mask, bool isDefaultMask, bool useAsDisplayFormat, CultureInfo culture, bool automaticallyAdvanceCaret, bool ignoreBlank, char placeHolder, bool saveLiteral, bool showPlaceHolders)
        {
            this.RegExMaskType = regExMaskType;
            this.Mask = mask;
            this.IsDefaultMask = isDefaultMask;
            this.UseAsDisplayFormat = useAsDisplayFormat;
            this.Culture = culture;
            this.AutomaticallyAdvanceCaret = automaticallyAdvanceCaret;
            this.IgnoreBlank = ignoreBlank;
            this.PlaceHolder = placeHolder;
            this.SaveLiteral = saveLiteral;
            this.ShowPlaceHolders = showPlaceHolders;
        }

        protected bool Equals(MaskInfo other)
        {
            DevExpress.Mvvm.Native.RegExMaskType? regExMaskType = this.RegExMaskType;
            DevExpress.Mvvm.Native.RegExMaskType? nullable2 = other.RegExMaskType;
            return (((regExMaskType.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((regExMaskType != null) == (nullable2 != null)) : false) && (string.Equals(this.Mask, other.Mask) && ((this.IsDefaultMask == other.IsDefaultMask) && ((this.UseAsDisplayFormat == other.UseAsDisplayFormat) && (Equals(this.Culture, other.Culture) && ((this.AutomaticallyAdvanceCaret == other.AutomaticallyAdvanceCaret) && ((this.IgnoreBlank == other.IgnoreBlank) && ((this.PlaceHolder == other.PlaceHolder) && ((this.SaveLiteral == other.SaveLiteral) && (this.ShowPlaceHolders == other.ShowPlaceHolders))))))))));
        }

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((MaskInfo) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (((((((((((((((((this.RegExMaskType.GetHashCode() * 0x18d) ^ ((this.Mask != null) ? this.Mask.GetHashCode() : 0)) * 0x18d) ^ this.IsDefaultMask.GetHashCode()) * 0x18d) ^ this.UseAsDisplayFormat.GetHashCode()) * 0x18d) ^ ((this.Culture != null) ? this.Culture.GetHashCode() : 0)) * 0x18d) ^ this.AutomaticallyAdvanceCaret.GetHashCode()) * 0x18d) ^ this.IgnoreBlank.GetHashCode()) * 0x18d) ^ this.PlaceHolder.GetHashCode()) * 0x18d) ^ this.SaveLiteral.GetHashCode()) * 0x18d) ^ this.ShowPlaceHolders.GetHashCode();

        public static MaskInfo GetMaskIfo(MaskAttributeBase mask, string defaulMask, bool defaultNotUseAsDisplayFormat, DevExpress.Mvvm.Native.RegExMaskType? defaultMaskType, bool allowUseMaskAsDisplayFormat)
        {
            DevExpress.Mvvm.Native.RegExMaskType? regExMaskType = GetRegExMaskType(mask, defaultMaskType);
            MaskInfo info = (mask != null) ? new MaskInfo(regExMaskType, mask.Mask, mask.IsDefaultMask(mask.Mask), allowUseMaskAsDisplayFormat && mask.UseAsDisplayFormat, mask.CultureInternal, mask.AutomaticallyAdvanceCaretInternal, mask.IgnoreBlankInternal, mask.PlaceHolderInternal, mask.SaveLiteralInternal, mask.ShowPlaceHoldersInternal) : new MaskInfo(regExMaskType, null, true, allowUseMaskAsDisplayFormat && !defaultNotUseAsDisplayFormat, null, false, true, '_', true, true);
            if (info.IsDefaultMask)
            {
                info.Mask = defaulMask;
                info.IsDefaultMask = defaultNotUseAsDisplayFormat;
            }
            return info;
        }

        public static DevExpress.Mvvm.Native.RegExMaskType? GetRegExMaskType(MaskAttributeBase mask, DevExpress.Mvvm.Native.RegExMaskType? defaultMaskType)
        {
            Func<MaskAttributeBase, RegExMaskAttributeBase> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<MaskAttributeBase, RegExMaskAttributeBase> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x as RegExMaskAttributeBase;
            }
            Func<RegExMaskAttributeBase, DevExpress.Mvvm.Native.RegExMaskType?> func2 = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Func<RegExMaskAttributeBase, DevExpress.Mvvm.Native.RegExMaskType?> local2 = <>c.<>9__8_1;
                func2 = <>c.<>9__8_1 = x => new DevExpress.Mvvm.Native.RegExMaskType?(x.RegExMaskType);
            }
            return mask.With<MaskAttributeBase, RegExMaskAttributeBase>(evaluator).Return<RegExMaskAttributeBase, DevExpress.Mvvm.Native.RegExMaskType?>(func2, () => defaultMaskType);
        }

        public DevExpress.Mvvm.Native.RegExMaskType? RegExMaskType { get; private set; }

        public string Mask { get; private set; }

        public bool IsDefaultMask { get; private set; }

        public bool UseAsDisplayFormat { get; private set; }

        public CultureInfo Culture { get; private set; }

        public bool AutomaticallyAdvanceCaret { get; private set; }

        public bool IgnoreBlank { get; private set; }

        public char PlaceHolder { get; private set; }

        public bool SaveLiteral { get; private set; }

        public bool ShowPlaceHolders { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MaskInfo.<>c <>9 = new MaskInfo.<>c();
            public static Func<MaskAttributeBase, RegExMaskAttributeBase> <>9__8_0;
            public static Func<RegExMaskAttributeBase, RegExMaskType?> <>9__8_1;

            internal RegExMaskAttributeBase <GetRegExMaskType>b__8_0(MaskAttributeBase x) => 
                x as RegExMaskAttributeBase;

            internal RegExMaskType? <GetRegExMaskType>b__8_1(RegExMaskAttributeBase x) => 
                new RegExMaskType?(x.RegExMaskType);
        }
    }
}

