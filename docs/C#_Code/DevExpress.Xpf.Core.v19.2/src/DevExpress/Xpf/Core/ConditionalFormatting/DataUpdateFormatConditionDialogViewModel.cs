namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DataUpdateFormatConditionDialogViewModel : ConditionalFormattingDialogViewModel
    {
        protected DataUpdateFormatConditionDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_Animation_Title, ConditionalFormattingStringId.ConditionalFormatting_Animation_Description, ConditionalFormattingStringId.ConditionalFormatting_Animation_Connector)
        {
            this.SelectorItems = CreateSelectorItems().ToArray<AnimationSelectorItem>();
        }

        protected override BaseEditUnit CreateEditUnit(string fieldName)
        {
            AnimationEditUnit unit = new AnimationEditUnit();
            if (base.ApplyFormatToWholeRow)
            {
                unit.ApplyToRow = true;
            }
            AnimationSelectorItem item = this.Value as AnimationSelectorItem;
            if (item != null)
            {
                unit.Rule = item.Rule;
            }
            return unit;
        }

        protected override IList<FormatInfo> CreateFormats()
        {
            List<FormatInfo> list2 = new List<FormatInfo>();
            foreach (FormatInfo info in base.CreateFormats())
            {
                Format format = info.Format as Format;
                if ((format == null) || ((format.TextDecorations == null) || (format.TextDecorations.Count <= 0)))
                {
                    list2.Add(info);
                }
            }
            return list2;
        }

        [IteratorStateMachine(typeof(<CreateSelectorItems>d__11))]
        private static IEnumerable<AnimationSelectorItem> CreateSelectorItems()
        {
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Animation_Change, DataUpdateRule.Always);
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Animation_Increase, DataUpdateRule.Increase);
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Animation_Decrease, DataUpdateRule.Decrease);
        }

        internal override object GetInitialValue() => 
            this.SelectorItems[0];

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, DataUpdateFormatConditionDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, DataUpdateFormatConditionDialogViewModel>>(Expression.New((ConstructorInfo) methodof(DataUpdateFormatConditionDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.Selector;

        public AnimationSelectorItem[] SelectorItems { get; private set; }

        protected override bool AllowTextDecorations =>
            false;

    }
}

