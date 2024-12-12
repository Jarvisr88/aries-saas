namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class IconViewModel : FieldEditorOwner, IConditionEditor
    {
        private XlCondFmtIconSetType? exportStyle;

        protected IconViewModel(IDialogContext context) : base(context)
        {
            this.AllowPredefinedFormatNameEditing = true;
            this.ElementsViewModels = new IconItemViewModelCollection();
            FormatInfoCollection predefinedIconSetFormats = context.PredefinedFormatsOwner.PredefinedIconSetFormats;
            if (predefinedIconSetFormats != null)
            {
                Func<FormatInfo, IconFormatStyle> selector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<FormatInfo, IconFormatStyle> local1 = <>c.<>9__2_0;
                    selector = <>c.<>9__2_0 = x => new IconFormatStyle((IconSetFormat) x.Format, x.FormatName);
                }
                this.FormatStyles = predefinedIconSetFormats.Select<FormatInfo, IconFormatStyle>(selector).ToList<IconFormatStyle>();
                this.FormatStyle = this.FormatStyles.FirstOrDefault<IconFormatStyle>();
            }
        }

        private IconSetFormat CreateFormat()
        {
            IconSetFormat format1 = new IconSetFormat();
            format1.ElementThresholdType = (ConditionalFormattingValueType) this.ValueType;
            IconSetFormat format = format1;
            if (this.exportStyle != null)
            {
                format.IconSetType = this.exportStyle;
            }
            IconSetElementCollection elements = format.Elements;
            foreach (IconItemViewModel model in this.ElementsViewModels)
            {
                elements.Add(model.CreateElement());
            }
            return format;
        }

        private IconItemViewModel CreateItemViewModel(IconSetElement element, IList<ImageSource> availableIcons)
        {
            IconItemViewModel model = IconItemViewModel.Factory(this);
            model.Threshold = element.Threshold;
            model.ComparisonType = (IconComparisonType) element.ThresholdComparisonType;
            model.Icon = element.Icon;
            model.Icons = availableIcons;
            return model;
        }

        bool IConditionEditor.CanInit(BaseEditUnit unit) => 
            unit is IconSetEditUnit;

        BaseEditUnit IConditionEditor.Edit()
        {
            IconSetEditUnit unit = new IconSetEditUnit();
            base.EditIndicator(unit);
            string str = this.TryGetAppropriateDefaultIconStyleName();
            IconSetFormat format = this.CreateFormat();
            if (str != null)
            {
                unit.PredefinedFormatName = str;
            }
            else
            {
                unit.Format = format;
                unit.PredefinedFormatName = null;
            }
            return unit;
        }

        void IConditionEditor.Init(BaseEditUnit unit)
        {
            IconSetEditUnit unit2 = unit as IconSetEditUnit;
            if (unit2 != null)
            {
                base.InitIndicator(unit2);
                IconSetFormat format = unit2.Format;
                string name = unit2.PredefinedFormatName;
                if (!string.IsNullOrEmpty(name) || (format == null))
                {
                    this.FormatStyle = this.FormatStyles.FirstOrDefault<IconFormatStyle>(x => x.FormatName == name);
                }
                else
                {
                    IconFormatStyle style = new IconFormatStyle(format, string.Empty);
                    IconFormatStyle[] second = new IconFormatStyle[] { style };
                    this.FormatStyles = this.FormatStyles.Concat<IconFormatStyle>(second).ToList<IconFormatStyle>();
                    this.FormatStyle = style;
                }
            }
        }

        bool IConditionEditor.Validate() => 
            base.ValidateExpression();

        protected void OnFormatStyleChanged()
        {
            if (this.FormatStyle != null)
            {
                IconSetFormat format = this.FormatStyle.Format;
                this.ValueType = (IconValueType) format.ElementThresholdType;
                this.UpdateElementsViewModels();
                this.exportStyle = format.IconSetType;
            }
        }

        public void ReverseIcons()
        {
            Func<IconItemViewModel, ImageSource> selector = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<IconItemViewModel, ImageSource> local1 = <>c.<>9__36_0;
                selector = <>c.<>9__36_0 = x => x.Icon;
            }
            List<ImageSource> list = this.ElementsViewModels.Select<IconItemViewModel, ImageSource>(selector).Reverse<ImageSource>().ToList<ImageSource>();
            for (int i = 0; i < this.ElementsViewModels.Count; i++)
            {
                this.ElementsViewModels[i].Icon = list[i];
            }
        }

        internal void SetCustomExportStyle()
        {
            this.exportStyle = null;
        }

        private string TryGetAppropriateDefaultIconStyleName()
        {
            if (this.AllowPredefinedFormatNameEditing)
            {
                if ((this.FormatStyle == null) || string.IsNullOrEmpty(this.FormatStyle.FormatName))
                {
                    return null;
                }
                IconSetFormat format = this.FormatStyle.Format;
                if ((format != null) && (this.ValueType == ((IconValueType) ((int) format.ElementThresholdType))))
                {
                    XlCondFmtIconSetType? exportStyle = this.exportStyle;
                    XlCondFmtIconSetType? iconSetType = format.IconSetType;
                    if (!((exportStyle.GetValueOrDefault() == iconSetType.GetValueOrDefault()) ? ((exportStyle != null) != (iconSetType != null)) : true))
                    {
                        if (this.ElementsViewModels.Count != format.Elements.Count)
                        {
                            return null;
                        }
                        for (int i = 0; i < this.ElementsViewModels.Count; i++)
                        {
                            IconItemViewModel model = this.ElementsViewModels[i];
                            IconSetElement element = format.Elements[i];
                            if ((model.Threshold != element.Threshold) || (model.GetThresholdComparisonType() != element.ThresholdComparisonType))
                            {
                                return null;
                            }
                        }
                        return this.FormatStyle.FormatName;
                    }
                }
            }
            return null;
        }

        private void UpdateElementsViewModels()
        {
            this.ElementsViewModels.BeginUpdate();
            this.ElementsViewModels.Clear();
            IconSetElement[] sortedElements = this.FormatStyle.Format.GetSortedElements();
            Func<IconFormatStyle, IEnumerable<ImageSource>> selector = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<IconFormatStyle, IEnumerable<ImageSource>> local1 = <>c.<>9__30_0;
                selector = <>c.<>9__30_0 = (Func<IconFormatStyle, IEnumerable<ImageSource>>) (x => x.Icons);
            }
            IList<ImageSource> availableIcons = this.FormatStyles.SelectMany<IconFormatStyle, ImageSource>(selector).ToList<ImageSource>();
            for (int i = 0; i < sortedElements.Length; i++)
            {
                IconItemViewModel item = this.CreateItemViewModel(sortedElements[i], availableIcons);
                this.ElementsViewModels.Add(item);
                this.UpdateItemDescription(i);
            }
            this.ElementsViewModels.EndUpdate();
        }

        private void UpdateItemDescription(int index)
        {
            IconItemViewModel model = this.ElementsViewModels[index];
            if (index <= 0)
            {
                model.Description = base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_IconDescriptionValueCondition);
            }
            else
            {
                IconItemViewModel model2 = this.ElementsViewModels[index - 1];
                string str = (model2.ComparisonType == IconComparisonType.Greater) ? "<=" : "<";
                string str2 = string.Format(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_IconDescriptionCondition) + " {0} {1}", str, Math.Round(model2.Threshold));
                if (model.HasBottomLimit)
                {
                    str2 = str2 + base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_IconDescriptionConnector);
                }
                model.Description = str2;
            }
        }

        internal void UpdateNextItemDescription(IconItemViewModel sourceItem)
        {
            int index = this.ElementsViewModels.IndexOf(sourceItem) + 1;
            if ((index != 0) && (index != this.ElementsViewModels.Count))
            {
                this.UpdateItemDescription(index);
            }
        }

        public static Func<IDialogContext, IconViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, IconViewModel>(Expression.Lambda<Func<IDialogContext, IconViewModel>>(Expression.New((ConstructorInfo) methodof(IconViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public IList<IconFormatStyle> FormatStyles { get; private set; }

        public virtual IconFormatStyle FormatStyle { get; set; }

        public virtual IconValueType ValueType { get; set; }

        public IconItemViewModelCollection ElementsViewModels { get; private set; }

        public bool AllowPredefinedFormatNameEditing { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_IconDescription);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IconViewModel.<>c <>9 = new IconViewModel.<>c();
            public static Func<FormatInfo, IconFormatStyle> <>9__2_0;
            public static Func<IconFormatStyle, IEnumerable<ImageSource>> <>9__30_0;
            public static Func<IconItemViewModel, ImageSource> <>9__36_0;

            internal IconFormatStyle <.ctor>b__2_0(FormatInfo x) => 
                new IconFormatStyle((IconSetFormat) x.Format, x.FormatName);

            internal ImageSource <ReverseIcons>b__36_0(IconItemViewModel x) => 
                x.Icon;

            internal IEnumerable<ImageSource> <UpdateElementsViewModels>b__30_0(IconFormatStyle x) => 
                x.Icons;
        }
    }
}

