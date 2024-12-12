namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Data.Extensions;
    using DevExpress.Export.Xl;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Themes;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class FormatConditionRuleIconSetExportWrapper : FormatConditionRuleBase, IFormatConditionRuleIconSet, IFormatConditionRuleBase
    {
        private readonly IconSetFormatCondition FormatCondition;
        private readonly XlCondFmtIconSetType? iconSetTypeCore;
        private IndicatorFormatConditionExportWrapperDelegate indicatorDelegate;
        private List<XlCondFmtCustomIcon> customIconsCore;

        public FormatConditionRuleIconSetExportWrapper(IconSetFormatCondition formatCondition)
        {
            this.FormatCondition = formatCondition;
            this.indicatorDelegate = new IndicatorFormatConditionExportWrapperDelegate(formatCondition);
            this.iconSetTypeCore = GetXlIconSetType(this.FormatCondition.Format);
            if (this.iconSetTypeCore == null)
            {
                this.iconSetTypeCore = -1;
            }
        }

        private static bool AreIconsEqual(ImageSource imageA, Uri imageBUri)
        {
            BitmapImage image = imageA as BitmapImage;
            return ((image != null) && ((imageBUri != null) && (image.UriSource == imageBUri)));
        }

        private static bool AreIconsEqual(ImageSource imageA, ImageSource imageB)
        {
            if (ReferenceEquals(imageA, imageB))
            {
                return true;
            }
            DrawingImage image = imageA as DrawingImage;
            DrawingImage image2 = imageB as DrawingImage;
            if ((image == null) || (image2 == null))
            {
                BitmapImage image3 = imageA as BitmapImage;
                BitmapImage image4 = imageB as BitmapImage;
                return ((image3 != null) && ((image4 != null) && (image3.UriSource == image4.UriSource)));
            }
            string iconName = IconSetExtension.GetIconName(image);
            string str2 = IconSetExtension.GetIconName(image2);
            bool flag = true;
            if (!string.IsNullOrEmpty(iconName) && !string.IsNullOrEmpty(str2))
            {
                flag = iconName.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
            }
            return (flag && ImageToByteArray(image).SequenceEqual<byte>(ImageToByteArray(image2)));
        }

        private List<XlCondFmtCustomIcon> CreateCustomIcons()
        {
            List<XlCondFmtCustomIcon> list = new List<XlCondFmtCustomIcon>();
            Func<FormatConditionCollection, IFormatConditionCollectionOwner> evaluator = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<FormatConditionCollection, IFormatConditionCollectionOwner> local1 = <>c.<>9__20_0;
                evaluator = <>c.<>9__20_0 = x => x.Owner;
            }
            Func<IFormatConditionCollectionOwner, FormatInfoCollection> func2 = <>c.<>9__20_1;
            if (<>c.<>9__20_1 == null)
            {
                Func<IFormatConditionCollectionOwner, FormatInfoCollection> local2 = <>c.<>9__20_1;
                func2 = <>c.<>9__20_1 = y => y.PredefinedIconSetFormats;
            }
            FormatInfoCollection source = this.FormatCondition.Owner.With<FormatConditionCollection, IFormatConditionCollectionOwner>(evaluator).With<IFormatConditionCollectionOwner, FormatInfoCollection>(func2);
            if (source == null)
            {
                return list;
            }
            if ((this.FormatCondition == null) || ((this.FormatCondition.Format == null) || (this.FormatCondition.Format.Elements == null)))
            {
                return list;
            }
            IconSetFormat[] formatArray = null;
            if (this.IconSetType < XlCondFmtIconSetType.Arrows3)
            {
                Func<FormatInfo, object> selector = <>c.<>9__20_2;
                if (<>c.<>9__20_2 == null)
                {
                    Func<FormatInfo, object> local3 = <>c.<>9__20_2;
                    selector = <>c.<>9__20_2 = x => x.Format;
                }
                formatArray = source.Select<FormatInfo, object>(selector).OfType<IconSetFormat>().ToArray<IconSetFormat>();
            }
            else
            {
                Func<FormatInfo, IconSetFormat> func4 = <>c.<>9__20_3;
                if (<>c.<>9__20_3 == null)
                {
                    Func<FormatInfo, IconSetFormat> local4 = <>c.<>9__20_3;
                    func4 = <>c.<>9__20_3 = x => x.Format as IconSetFormat;
                }
                IconSetFormat format = source[this.IconSetType + "IconSet"].With<FormatInfo, IconSetFormat>(func4);
                if ((format == null) || ((format.Elements == null) || format.Elements.SequenceEqual<IconSetElement>(this.FormatCondition.Format.Elements)))
                {
                    return list;
                }
                formatArray = new IconSetFormat[] { format };
            }
            using (IEnumerator<IconSetElement> enumerator = this.FormatCondition.Format.GetSortedElements().Reverse<IconSetElement>().GetEnumerator())
            {
                bool flag;
                goto TR_001D;
            TR_0008:
                if (!flag)
                {
                    return new List<XlCondFmtCustomIcon>();
                }
            TR_001D:
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IconSetElement element = enumerator.Current;
                        if (element.Icon == null)
                        {
                            list.Add(new XlCondFmtCustomIcon(0, XlCondFmtIconSetType.NoIcons));
                            continue;
                        }
                        flag = false;
                        foreach (IconSetFormat format2 in formatArray)
                        {
                            if (format2.IconSetType != null)
                            {
                                Predicate<IconSetElement> <>9__4;
                                Predicate<IconSetElement> predicate = <>9__4;
                                if (<>9__4 == null)
                                {
                                    Predicate<IconSetElement> local5 = <>9__4;
                                    predicate = <>9__4 = e => AreIconsEqual(e.Icon, element.Icon);
                                }
                                int num2 = format2.Elements.FindIndex<IconSetElement>(predicate);
                                if (num2 < 0)
                                {
                                    Predicate<IconSetElement> <>9__5;
                                    Predicate<IconSetElement> predicate4 = <>9__5;
                                    if (<>9__5 == null)
                                    {
                                        Predicate<IconSetElement> local6 = <>9__5;
                                        predicate4 = <>9__5 = e => AreIconsEqual(element.Icon, this.GetBitmapUri(e));
                                    }
                                    num2 = format2.Elements.FindIndex<IconSetElement>(predicate4);
                                }
                                if (num2 > -1)
                                {
                                    flag = true;
                                    list.Add(new XlCondFmtCustomIcon((format2.Elements.Count - num2) - 1, format2.IconSetType.Value));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        return list;
                    }
                    break;
                }
                goto TR_0008;
            }
        }

        private Uri GetBitmapUri(IconSetElement element)
        {
            if ((element == null) || (element.Icon == null))
            {
                return null;
            }
            string iconName = IconSetExtension.GetIconName(element.Icon);
            return (!string.IsNullOrEmpty(iconName) ? (!(element.Icon is BitmapImage) ? null : ((BitmapImage) element.Icon).UriSource) : QuickIconSetFormatExtension.GetUri(iconName, false));
        }

        private static XlCondFmtIconSetType? GetXlIconSetType(IconSetFormat iconSetFormat) => 
            iconSetFormat.IconSetType;

        private static byte[] ImageToByteArray(DrawingImage drawingImage)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawDrawing(drawingImage.Drawing);
                context.Close();
            }
            RenderTargetBitmap source = new RenderTargetBitmap((int) visual.Drawing.Bounds.Right, (int) visual.Drawing.Bounds.Bottom, 96.0, 96.0, PixelFormats.Pbgra32);
            source.Render(visual);
            MemoryStream stream = new MemoryStream();
            new PngBitmapEncoder { Frames = { BitmapFrame.Create(source) } }.Save(stream);
            return stream.ToArray();
        }

        public XlCondFmtIconSetType IconSetType =>
            this.iconSetTypeCore.Value;

        public bool Percent =>
            this.FormatCondition.Format.ElementThresholdType == ConditionalFormattingValueType.Percent;

        public bool Reverse =>
            false;

        public bool ShowValues =>
            true;

        public override bool IsValid =>
            (this.iconSetTypeCore != null) && this.indicatorDelegate.IsValid;

        public IList<XlCondFmtValueObject> Values
        {
            get
            {
                List<XlCondFmtValueObject> list = new List<XlCondFmtValueObject>();
                foreach (IconSetElement element in this.FormatCondition.Format.GetSortedElements().Reverse<IconSetElement>())
                {
                    XlCondFmtValueObject obj4;
                    XlCondFmtValueObjectType type = (this.FormatCondition.Format.ElementThresholdType == ConditionalFormattingValueType.Percent) ? XlCondFmtValueObjectType.Percent : XlCondFmtValueObjectType.Number;
                    if (element.Threshold != double.NegativeInfinity)
                    {
                        XlCondFmtValueObject obj1 = new XlCondFmtValueObject();
                        obj1.ObjectType = type;
                        obj1.Value = element.Threshold;
                        obj4 = obj1;
                    }
                    else
                    {
                        XlCondFmtValueObject obj3 = new XlCondFmtValueObject();
                        obj3.Value = 0.0;
                        obj3.ObjectType = type;
                        obj4 = obj3;
                    }
                    list.Add(obj4);
                }
                if (list.Count > 0)
                {
                    list[0].ObjectType = XlCondFmtValueObjectType.Percent;
                }
                return list;
            }
        }

        public IList<XlCondFmtCustomIcon> Icons
        {
            get
            {
                this.customIconsCore ??= this.CreateCustomIcons();
                return this.customIconsCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionRuleIconSetExportWrapper.<>c <>9 = new FormatConditionRuleIconSetExportWrapper.<>c();
            public static Func<FormatConditionCollection, IFormatConditionCollectionOwner> <>9__20_0;
            public static Func<IFormatConditionCollectionOwner, FormatInfoCollection> <>9__20_1;
            public static Func<FormatInfo, object> <>9__20_2;
            public static Func<FormatInfo, IconSetFormat> <>9__20_3;

            internal IFormatConditionCollectionOwner <CreateCustomIcons>b__20_0(FormatConditionCollection x) => 
                x.Owner;

            internal FormatInfoCollection <CreateCustomIcons>b__20_1(IFormatConditionCollectionOwner y) => 
                y.PredefinedIconSetFormats;

            internal object <CreateCustomIcons>b__20_2(FormatInfo x) => 
                x.Format;

            internal IconSetFormat <CreateCustomIcons>b__20_3(FormatInfo x) => 
                x.Format as IconSetFormat;
        }
    }
}

