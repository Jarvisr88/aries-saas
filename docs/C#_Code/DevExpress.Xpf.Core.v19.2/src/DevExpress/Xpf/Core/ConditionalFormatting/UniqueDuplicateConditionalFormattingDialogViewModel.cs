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

    public class UniqueDuplicateConditionalFormattingDialogViewModel : ConditionalFormattingDialogViewModel
    {
        protected UniqueDuplicateConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicate_Connector)
        {
            this.SelectorItems = this.CreateSelectorItems().ToArray<UniqueDuplicateSelectorItem>();
        }

        protected override BaseEditUnit CreateEditUnit(string fieldName)
        {
            UniqueDuplicateEditUnit unit = new UniqueDuplicateEditUnit();
            if (base.ApplyFormatToWholeRow)
            {
                unit.ApplyToRow = true;
            }
            UniqueDuplicateSelectorItem item = this.Value as UniqueDuplicateSelectorItem;
            if (item != null)
            {
                unit.Rule = item.Rule;
            }
            return unit;
        }

        [IteratorStateMachine(typeof(<CreateSelectorItems>d__12))]
        private IEnumerable<UniqueDuplicateSelectorItem> CreateSelectorItems()
        {
            yield return new UniqueDuplicateSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Duplicate, UniqueDuplicateRule.Duplicate);
            yield return new UniqueDuplicateSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_UniqueDuplicateDialog_Unique, UniqueDuplicateRule.Unique);
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
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, UniqueDuplicateConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, UniqueDuplicateConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(UniqueDuplicateConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.Selector;

        public UniqueDuplicateSelectorItem[] SelectorItems { get; private set; }


        public class UniqueDuplicateSelectorItem
        {
            private readonly ConditionalFormattingStringId stringId;

            public UniqueDuplicateSelectorItem(ConditionalFormattingStringId stringId, UniqueDuplicateRule rule)
            {
                this.stringId = stringId;
                this.Rule = rule;
            }

            public override string ToString() => 
                ConditionalFormattingLocalizer.GetString(this.stringId);

            internal UniqueDuplicateRule Rule { get; private set; }
        }
    }
}

