namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Themes;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ConditionalFormattingDialogDirector
    {
        private IDialogContext context;
        private FrameworkElement resourceOwner;
        private IConditionalFormattingCommands commands;
        private IConditionalFormattingDialogBuilder builder;
        private IDataColumnInfo info;

        public ConditionalFormattingDialogDirector(IDialogContext context, IConditionalFormattingCommands commands, IConditionalFormattingDialogBuilder builder, FrameworkElement resourceOwner)
        {
            this.context = context;
            this.commands = commands;
            this.builder = builder;
            this.resourceOwner = resourceOwner;
            this.AllowConditionalFormattingManager = true;
        }

        private void ConfigureSplitItem(BarSplitButtonItem item, ConditionalFormattingThemeKeys templateKey, IList<object> groups)
        {
            item.ActAsDropDown = true;
            FrameworkElement element = TemplateHelper.LoadFromTemplate<FrameworkElement>((DataTemplate) this.FindResource(templateKey));
            element.DataContext = new FormatsViewModel(groups);
            PopupControlContainer container1 = new PopupControlContainer();
            container1.Content = element;
            item.PopupControl = container1;
        }

        private IList<object> ConvertToBindableItems(IEnumerable<FormatInfo> formatInfo, Func<FormatInfo, IEnumerable<ImageSource>> iconsExtractor, Func<string, string, DependencyObject> conditionCreator) => 
            (from x in formatInfo select new { 
                Name = x.DisplayName,
                Description = x.Description,
                Icons = iconsExtractor(x).ToArray<ImageSource>(),
                Command = this.commands.AddFormatCondition,
                Format = x.Format,
                FormatCondition = conditionCreator(x.FormatName, this.info.FieldName)
            }).ToArray();

        private BarButtonItem CreateBarButtonItem(BarItemLinkCollection links, string name, ConditionalFormattingStringId id, bool beginGroup, ImageSource image, ICommand command, object commandParameter = null) => 
            this.builder.CreateBarButtonItem(links, name, this.Localize(id), beginGroup, image, command, commandParameter);

        private BarButtonItem CreateBarButtonItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image, ICommand command, object commandParameter = null) => 
            this.builder.CreateBarButtonItem(links, name, content, beginGroup, image, command, commandParameter);

        private BarSplitButtonItem CreateBarSplitButtonItem(BarItemLinkCollection links, string name, ConditionalFormattingStringId id, bool beginGroup, ImageSource image) => 
            this.builder.CreateBarSplitButtonItem(links, name, this.Localize(id), beginGroup, image);

        private BarSubItem CreateBarSubItem(string name, ConditionalFormattingStringId id, bool beginGroup, ImageSource image, ICommand command) => 
            this.builder.CreateBarSubItem(name, this.Localize(id), beginGroup, image, command);

        private BarSubItem CreateBarSubItem(BarItemLinkCollection links, string name, ConditionalFormattingStringId id, bool beginGroup, ImageSource image, ICommand command) => 
            this.builder.CreateBarSubItem(links, name, this.Localize(id), beginGroup, image, command);

        protected virtual void CreateClearMenuItems(IDataColumnInfo info, BarSubItem clearRulesItem)
        {
            this.CreateBarButtonItem(clearRulesItem.ItemLinks, "ConditionalFormatting_ClearRules_FromAllColumns", ConditionalFormattingStringId.MenuColumnConditionalFormatting_ClearRules_FromAllColumns, false, null, this.commands.ClearFormatConditionsFromAllColumns, null);
            this.CreateBarButtonItem(clearRulesItem.ItemLinks, "ConditionalFormatting_ClearRules_FromCurrentColumns", ConditionalFormattingStringId.MenuColumnConditionalFormatting_ClearRules_FromCurrentColumns, false, null, this.commands.ClearFormatConditionsFromColumn, info);
        }

        private DependencyObject CreateColorScaleCondition(string formatName, string field) => 
            this.CreateCondition(formatName, field, new ColorScaleEditUnit());

        private DependencyObject CreateCondition(string formatName, string field, BaseEditUnit unit)
        {
            unit.PredefinedFormatName = formatName;
            unit.FieldName = field;
            return (unit.BuildCondition(this.context.Builder).GetCurrentValue() as DependencyObject);
        }

        private DependencyObject CreateDataBarCondition(string formatName, string field) => 
            this.CreateCondition(formatName, field, new DataBarEditUnit());

        private void CreateFormatDialogBarButtonItem(FormatConditionDialogType dialogType, BarSubItem parent, string groupName)
        {
            object[] objArray1 = new object[] { "ConditionalFormatting_", groupName, "_", dialogType };
            string name = (string) typeof(DefaultConditionalFormattingMenuItemNames).GetField(string.Concat(objArray1), BindingFlags.Public | BindingFlags.Static).GetValue(null);
            object[] objArray2 = new object[] { "MenuColumnConditionalFormatting_", groupName, "_", dialogType };
            this.CreateBarButtonItem(parent.ItemLinks, name, ConditionalFormattingLocalizer.GetString((ConditionalFormattingStringId) Enum.Parse(typeof(ConditionalFormattingStringId), string.Concat(objArray2), false)), false, this.GetConditionalFormattingMenuImage(dialogType.ToString(), "ConditionalFormatting"), this.GetShowDialogCommand(dialogType), this.info);
        }

        private DependencyObject CreateIconCondition(string formatName, string field) => 
            this.CreateCondition(formatName, field, new IconSetEditUnit());

        public void CreateMenuItems(IDataColumnInfo info)
        {
            this.info = info;
            BarSubItem item = this.CreateBarSubItem("ConditionalFormatting", ConditionalFormattingStringId.MenuColumnConditionalFormatting, true, this.GetConditionalFormattingMenuImage(string.Empty, "ConditionalFormatting"), null);
            Type fieldType = info.FieldType;
            if (fieldType != null)
            {
                BarSubItem parent = this.CreateBarSubItem(item.ItemLinks, "ConditionalFormatting_HighlightCellsRules", ConditionalFormattingStringId.MenuColumnConditionalFormatting_HighlightCellsRules, false, this.GetConditionalFormattingMenuImage("HighlightCellsRules", "ConditionalFormatting"), null);
                foreach (FormatConditionDialogType type2 in ConditionalFormattingMenuHelper.GetAvailableHighlightItems(fieldType, (!this.IsServerMode && !this.context.IsPivot) && !this.IsVirtualSource))
                {
                    this.CreateFormatDialogBarButtonItem(type2, parent, "HighlightCellsRules");
                }
                if (!this.IsVirtualSource)
                {
                    FormatConditionDialogType[] source = ConditionalFormattingMenuHelper.GetAvailableTopBottomRuleItems(fieldType, this.IsServerMode).ToArray<FormatConditionDialogType>();
                    if (source.Any<FormatConditionDialogType>())
                    {
                        BarSubItem item4 = this.CreateBarSubItem(item.ItemLinks, "ConditionalFormatting_TopBottomRules", ConditionalFormattingStringId.MenuColumnConditionalFormatting_TopBottomRules, false, this.GetConditionalFormattingMenuImage("TopBottomRules", "ConditionalFormatting"), null);
                        foreach (FormatConditionDialogType type3 in source)
                        {
                            this.CreateFormatDialogBarButtonItem(type3, item4, "TopBottomRules");
                        }
                    }
                    if (this.AllowConditionalAnimation)
                    {
                        this.CreateBarButtonItem(item.ItemLinks, "ConditionalFormatting_DataUpdateRules", ConditionalFormattingStringId.MenuColumnConditionalFormatting_DataUpdateRules, false, this.GetConditionalFormattingMenuImage("AnimationRules", "ConditionalFormatting"), this.commands.ShowDataUpdateFormatConditionDialog, info);
                    }
                    bool beginGroup = true;
                    if (ConditionalFormattingMenuHelper.ShowDatBarMenu(fieldType))
                    {
                        BarSplitButtonItem item5 = this.CreateBarSplitButtonItem(item.ItemLinks, "ConditionalFormatting_DataBars", ConditionalFormattingStringId.MenuColumnConditionalFormatting_DataBars, beginGroup, this.GetConditionalFormattingMenuImage("SolidBlueDataBar", "ConditionalFormatting"));
                        Func<FormatInfo, IEnumerable<ImageSource>> iconsExtractor = <>c.<>9__0_0;
                        if (<>c.<>9__0_0 == null)
                        {
                            Func<FormatInfo, IEnumerable<ImageSource>> local1 = <>c.<>9__0_0;
                            iconsExtractor = <>c.<>9__0_0 = (Func<FormatInfo, IEnumerable<ImageSource>>) (x => new ImageSource[] { x.Icon });
                        }
                        this.ConfigureSplitItem(item5, ConditionalFormattingThemeKeys.DataBarMenuItemContent, this.GetGroupedFormatItems(this.FormatsOwner.PredefinedDataBarFormats, iconsExtractor, new Func<string, string, DependencyObject>(this.CreateDataBarCondition)));
                        beginGroup = false;
                    }
                    if (ConditionalFormattingMenuHelper.ShowColorScaleMenu(fieldType))
                    {
                        BarSplitButtonItem item6 = this.CreateBarSplitButtonItem(item.ItemLinks, "ConditionalFormatting_ColorScales", ConditionalFormattingStringId.MenuColumnConditionalFormatting_ColorScales, beginGroup, this.GetConditionalFormattingMenuImage("GreenYellowRed", "ConditionalFormatting"));
                        this.ConfigureSplitItem(item6, ConditionalFormattingThemeKeys.ColorScaleMenuItemContent, this.GetColorScaleGroups());
                        beginGroup = false;
                    }
                    if (ConditionalFormattingMenuHelper.ShowIconSetMenu(fieldType))
                    {
                        BarSplitButtonItem item7 = this.CreateBarSplitButtonItem(item.ItemLinks, "ConditionalFormatting_IconSets", ConditionalFormattingStringId.MenuColumnConditionalFormatting_IconSets, beginGroup, this.GetConditionalFormattingMenuImage("IconSetArrows5", "ConditionalFormattins"));
                        Func<FormatInfo, IEnumerable<ImageSource>> iconsExtractor = <>c.<>9__0_1;
                        if (<>c.<>9__0_1 == null)
                        {
                            Func<FormatInfo, IEnumerable<ImageSource>> local2 = <>c.<>9__0_1;
                            iconsExtractor = <>c.<>9__0_1 = delegate (FormatInfo x) {
                                Func<IconSetElement, ImageSource> selector = <>c.<>9__0_2;
                                if (<>c.<>9__0_2 == null)
                                {
                                    Func<IconSetElement, ImageSource> local1 = <>c.<>9__0_2;
                                    selector = <>c.<>9__0_2 = y => y.Icon;
                                }
                                return (x.Format as IconSetFormat).Elements.Select<IconSetElement, ImageSource>(selector);
                            };
                        }
                        this.ConfigureSplitItem(item7, ConditionalFormattingThemeKeys.IconSetMenuItemContent, this.GetGroupedFormatItems(this.FormatsOwner.PredefinedIconSetFormats, iconsExtractor, new Func<string, string, DependencyObject>(this.CreateIconCondition)));
                    }
                }
                BarSubItem clearRulesItem = this.CreateBarSubItem(item.ItemLinks, "ConditionalFormatting_ClearRules", ConditionalFormattingStringId.MenuColumnConditionalFormatting_ClearRules, true, this.GetConditionalFormattingMenuImage("ClearRules", "ConditionalFormatting"), null);
                this.CreateClearMenuItems(info, clearRulesItem);
                if (this.AllowConditionalFormattingManager && GridAssemblyHelper.Instance.IsGridAvailable)
                {
                    this.CreateBarButtonItem(item.ItemLinks, "Manage_Rules", ConditionalFormattingStringId.MenuColumnConditionalFormatting_ManageRules, false, this.GetConditionalFormattingMenuImage("ManageRules", "ConditionalFormatting"), this.commands.ShowConditionalFormattingManager, info);
                }
            }
        }

        private object FindResource(ConditionalFormattingThemeKeys key)
        {
            ConditionalFormattingThemeKeyExtension resourceKey = new ConditionalFormattingThemeKeyExtension();
            resourceKey.ResourceKey = key;
            return this.ResourceOwner.FindResource(resourceKey);
        }

        private IList<object> GetColorScaleGroups() => 
            Enumerable.Range(0, 1).Select(delegate (int _) {
                Func<FormatInfo, IEnumerable<ImageSource>> iconsExtractor = <>c.<>9__36_1;
                if (<>c.<>9__36_1 == null)
                {
                    Func<FormatInfo, IEnumerable<ImageSource>> local1 = <>c.<>9__36_1;
                    iconsExtractor = <>c.<>9__36_1 = (Func<FormatInfo, IEnumerable<ImageSource>>) (x => new ImageSource[] { x.Icon });
                }
                return new { 
                    Header = string.Empty,
                    Items = this.ConvertToBindableItems(this.FormatsOwner.PredefinedColorScaleFormats, iconsExtractor, new Func<string, string, DependencyObject>(this.CreateColorScaleCondition))
                };
            }).ToArray();

        private ImageSource GetConditionalFormattingMenuImage(string name, string prefix = "ConditionalFormatting")
        {
            if (ApplicationThemeHelper.UseDefaultSvgImages)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = "MenuColumnConditionalFormatting";
                }
                SvgImageSourceExtension extension = new SvgImageSourceExtension {
                    Uri = new Uri("pack://application:,,,/DevExpress.Xpf.Core.v19.2;component/Core/ConditionalFormatting/Images/Menu/SVG/" + name + ".svg")
                };
                return (ImageSource) extension.ProvideValue(null);
            }
            string[] textArray1 = new string[] { ConditionalFormatResourceHelper.BasePathCore, "Menu/", prefix, name, "_16x16.png" };
            return new BitmapImage(new Uri(string.Concat(textArray1), UriKind.Absolute));
        }

        private IList<object> GetGroupedFormatItems(IEnumerable<FormatInfo> formatInfo, Func<FormatInfo, IEnumerable<ImageSource>> iconsExtractor, Func<string, string, DependencyObject> conditionCreator)
        {
            Func<FormatInfo, string> keySelector = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<FormatInfo, string> local1 = <>c.<>9__37_0;
                keySelector = <>c.<>9__37_0 = x => x.GroupName;
            }
            return (from x in formatInfo.GroupBy<FormatInfo, string>(keySelector) select new { 
                Header = x.Key,
                Items = this.ConvertToBindableItems(x, iconsExtractor, conditionCreator)
            }).ToArray();
        }

        private ICommand GetShowDialogCommand(FormatConditionDialogType dialogType)
        {
            switch (dialogType)
            {
                case FormatConditionDialogType.GreaterThan:
                    return this.commands.ShowGreaterThanFormatConditionDialog;

                case FormatConditionDialogType.LessThan:
                    return this.commands.ShowLessThanFormatConditionDialog;

                case FormatConditionDialogType.Between:
                    return this.commands.ShowBetweenFormatConditionDialog;

                case FormatConditionDialogType.EqualTo:
                    return this.commands.ShowEqualToFormatConditionDialog;

                case FormatConditionDialogType.TextThatContains:
                    return this.commands.ShowTextThatContainsFormatConditionDialog;

                case FormatConditionDialogType.ADateOccurring:
                    return this.commands.ShowADateOccurringFormatConditionDialog;

                case FormatConditionDialogType.CustomCondition:
                    return this.commands.ShowCustomConditionFormatConditionDialog;

                case FormatConditionDialogType.Top10Items:
                    return this.commands.ShowTop10ItemsFormatConditionDialog;

                case FormatConditionDialogType.Bottom10Items:
                    return this.commands.ShowBottom10ItemsFormatConditionDialog;

                case FormatConditionDialogType.Top10Percent:
                    return this.commands.ShowTop10PercentFormatConditionDialog;

                case FormatConditionDialogType.Bottom10Percent:
                    return this.commands.ShowBottom10PercentFormatConditionDialog;

                case FormatConditionDialogType.AboveAverage:
                    return this.commands.ShowAboveAverageFormatConditionDialog;

                case FormatConditionDialogType.BelowAverage:
                    return this.commands.ShowBelowAverageFormatConditionDialog;

                case FormatConditionDialogType.UniqueDuplicate:
                    return this.commands.ShowUniqueDuplicateRuleFormatConditionDialog;
            }
            throw new InvalidOperationException();
        }

        private string Localize(ConditionalFormattingStringId id) => 
            ConditionalFormattingLocalizer.GetString(id);

        private IFormatsOwner FormatsOwner =>
            this.context.PredefinedFormatsOwner;

        protected FrameworkElement ResourceOwner =>
            this.resourceOwner;

        protected IConditionalFormattingDialogBuilder Builder =>
            this.builder;

        protected IConditionalFormattingCommands Commands =>
            this.commands;

        public bool AllowConditionalFormattingManager { get; set; }

        public bool AllowConditionalAnimation { get; set; }

        public bool IsServerMode { get; set; }

        public bool IsVirtualSource { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalFormattingDialogDirector.<>c <>9 = new ConditionalFormattingDialogDirector.<>c();
            public static Func<FormatInfo, IEnumerable<ImageSource>> <>9__0_0;
            public static Func<IconSetElement, ImageSource> <>9__0_2;
            public static Func<FormatInfo, IEnumerable<ImageSource>> <>9__0_1;
            public static Func<FormatInfo, IEnumerable<ImageSource>> <>9__36_1;
            public static Func<FormatInfo, string> <>9__37_0;

            internal IEnumerable<ImageSource> <CreateMenuItems>b__0_0(FormatInfo x) => 
                new ImageSource[] { x.Icon };

            internal IEnumerable<ImageSource> <CreateMenuItems>b__0_1(FormatInfo x)
            {
                Func<IconSetElement, ImageSource> selector = <>9__0_2;
                if (<>9__0_2 == null)
                {
                    Func<IconSetElement, ImageSource> local1 = <>9__0_2;
                    selector = <>9__0_2 = y => y.Icon;
                }
                return (x.Format as IconSetFormat).Elements.Select<IconSetElement, ImageSource>(selector);
            }

            internal ImageSource <CreateMenuItems>b__0_2(IconSetElement y) => 
                y.Icon;

            internal IEnumerable<ImageSource> <GetColorScaleGroups>b__36_1(FormatInfo x) => 
                new ImageSource[] { x.Icon };

            internal string <GetGroupedFormatItems>b__37_0(FormatInfo x) => 
                x.GroupName;
        }
    }
}

