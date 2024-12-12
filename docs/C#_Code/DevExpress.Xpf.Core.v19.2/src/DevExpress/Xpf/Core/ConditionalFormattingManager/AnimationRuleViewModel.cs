namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AnimationRuleViewModel : FormatEditorOwnerViewModel
    {
        private System.Windows.Duration showDuration;
        private System.Windows.Duration hideDuration;
        private AnimationEasingMode easingMode;

        protected AnimationRuleViewModel(IDialogContext context) : base(context)
        {
            this.Rules = CreateSelectorItems().ToList<AnimationSelectorItem>();
            this.Rule = this.Rules.First<AnimationSelectorItem>();
            int? currentDuration = null;
            if ((context == null) || (context.DefaultAnimationSettings == null))
            {
                this.showDuration = new System.Windows.Duration(TimeSpan.FromMilliseconds(200.0));
                this.hideDuration = new System.Windows.Duration(TimeSpan.FromMilliseconds(200.0));
            }
            else
            {
                DefaultAnimationSettings settings = context.DefaultAnimationSettings.Value;
                this.showDuration = settings.TriggerAnimationShowDuration;
                this.hideDuration = settings.TriggerAnimationHideDuration;
                currentDuration = this.GetCurrentDuration(settings.TriggerAnimationHoldDuration);
            }
            int? nullable3 = currentDuration;
            this.Duration = (nullable3 != null) ? nullable3.GetValueOrDefault() : 600;
            this.easingMode = AnimationEasingMode.Linear;
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            AnimationEditUnit unit2 = unit as AnimationEditUnit;
            if (unit2 != null)
            {
                base.AddChanges(unit2);
                unit2.FieldName = base.Context.ColumnInfo.FieldName;
                unit2.Rule = this.Rule.Rule;
                unit2.AnimationSettings = this.CreateAnimationSettings();
            }
        }

        protected override bool CanInitCore(ExpressionEditUnit unit) => 
            unit is AnimationEditUnit;

        private DataUpdateAnimationSettings CreateAnimationSettings()
        {
            DataUpdateAnimationSettings settings1 = new DataUpdateAnimationSettings();
            settings1.HoldDuration = new System.Windows.Duration(TimeSpan.FromMilliseconds((double) this.Duration));
            settings1.ShowDuration = this.showDuration;
            settings1.HideDuration = this.hideDuration;
            settings1.EasingMode = this.easingMode;
            return settings1;
        }

        protected override ExpressionEditUnit CreateEditUnit() => 
            new AnimationEditUnit();

        [IteratorStateMachine(typeof(<CreateSelectorItems>d__27))]
        private static IEnumerable<AnimationSelectorItem> CreateSelectorItems()
        {
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Manager_AnimationChange, DataUpdateRule.Always);
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Manager_AnimationIncrease, DataUpdateRule.Increase);
            yield return new AnimationSelectorItem(ConditionalFormattingStringId.ConditionalFormatting_Manager_AnimationDecrease, DataUpdateRule.Decrease);
        }

        private int? GetCurrentDuration(AnimationEditUnit unit)
        {
            DataUpdateAnimationSettings animationSettings = unit.AnimationSettings;
            if (animationSettings != null)
            {
                return this.GetCurrentDuration(animationSettings.HoldDuration);
            }
            return null;
        }

        private int? GetCurrentDuration(System.Windows.Duration duration)
        {
            if (duration.HasTimeSpan)
            {
                int num = (int) Math.Ceiling(duration.TimeSpan.TotalMilliseconds);
                if ((num >= 0) && (num <= 0x2710))
                {
                    return new int?(num);
                }
            }
            return null;
        }

        protected override void InitCore(ExpressionEditUnit unit)
        {
            AnimationEditUnit animationUnit = unit as AnimationEditUnit;
            if (animationUnit != null)
            {
                base.InitCore(animationUnit);
                AnimationSelectorItem local1 = this.Rules.FirstOrDefault<AnimationSelectorItem>(x => x.Rule == animationUnit.Rule);
                AnimationSelectorItem local3 = local1;
                if (local1 == null)
                {
                    AnimationSelectorItem local2 = local1;
                    local3 = this.Rules.First<AnimationSelectorItem>();
                }
                this.Rule = local3;
                DataUpdateAnimationSettings animationSettings = animationUnit.AnimationSettings;
                if (animationSettings != null)
                {
                    int? currentDuration = this.GetCurrentDuration(animationSettings.HoldDuration);
                    if (currentDuration != null)
                    {
                        this.Duration = currentDuration.Value;
                    }
                    this.showDuration = animationSettings.ShowDuration;
                    this.hideDuration = animationSettings.HideDuration;
                    this.easingMode = animationSettings.EasingMode;
                }
            }
        }

        public static Func<IDialogContext, AnimationRuleViewModel> Factory
        {
            get
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(IDialogContext), "x");
                System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, AnimationRuleViewModel>(System.Linq.Expressions.Expression.Lambda<Func<IDialogContext, AnimationRuleViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(AnimationRuleViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), parameters));
            }
        }

        public virtual AnimationSelectorItem Rule { get; set; }

        public IList<AnimationSelectorItem> Rules { get; private set; }

        public virtual int Duration { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_AnimationDescription);

        protected override bool AllowTextDecorations =>
            false;

    }
}

