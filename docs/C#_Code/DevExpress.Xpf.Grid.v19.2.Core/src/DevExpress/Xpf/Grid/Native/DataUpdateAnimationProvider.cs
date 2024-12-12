namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class DataUpdateAnimationProvider : IDataUpdateAnimationProvider
    {
        private void AddConditionalAnimationToList(FormatConditionBaseInfo conditionInfo, List<IList<SequentialAnimationTimeline>> list, DataUpdate update)
        {
            IConditionalAnimationFactory factory = conditionInfo.CreateAnimationFactory();
            factory.UpdateContext(update);
            factory.UpdateDefaultSettings(this.CreateDefaultAnimationSettings());
            IList<SequentialAnimationTimeline> item = factory.CreateAnimations();
            if ((item != null) && (item.Count > 0))
            {
                list.Add(item);
            }
        }

        private bool AllowConditionalAnimationByDefault()
        {
            Func<ITableView, bool> evaluator = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<ITableView, bool> local1 = <>c.<>9__17_0;
                evaluator = <>c.<>9__17_0 = x => x.AnimateConditionalFormattingTransition;
            }
            return this.View.Return<ITableView, bool>(evaluator, (<>c.<>9__17_1 ??= () => true));
        }

        protected virtual bool CanCreateAnimations() => 
            this.HasDataUpdateFormatConditions() && !this.IsInDesignMode();

        private IList<SequentialAnimationTimeline> CollectConditionalAnimations(DataUpdate dataUpdate, IEnumerable<FormatConditionBaseInfo> relatedConditions)
        {
            List<IList<SequentialAnimationTimeline>> list = new List<IList<SequentialAnimationTimeline>>();
            AnimationTriggerContext context = this.CreateTriggerContext(dataUpdate);
            bool isEnabledByDefault = this.AllowConditionalAnimationByDefault();
            foreach (FormatConditionBaseInfo info in relatedConditions)
            {
                if (info.IsAnimationEnabled(isEnabledByDefault) && info.NeedFormatChange(context))
                {
                    this.AddConditionalAnimationToList(info, list, dataUpdate);
                }
            }
            Func<IList<SequentialAnimationTimeline>, IEnumerable<SequentialAnimationTimeline>> selector = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<IList<SequentialAnimationTimeline>, IEnumerable<SequentialAnimationTimeline>> local1 = <>c.<>9__7_0;
                selector = <>c.<>9__7_0 = x => x;
            }
            return list.SelectMany<IList<SequentialAnimationTimeline>, SequentialAnimationTimeline>(selector).ToArray<SequentialAnimationTimeline>();
        }

        private DefaultAnimationSettings? CreateDefaultAnimationSettings() => 
            CreateDefaultAnimationSettings(this.View);

        internal static DefaultAnimationSettings? CreateDefaultAnimationSettings(ITableView tableView)
        {
            if (tableView != null)
            {
                return new DefaultAnimationSettings(tableView.ConditionalFormattingTransitionDuration, tableView.DataUpdateAnimationShowDuration, tableView.DataUpdateAnimationHoldDuration, tableView.DataUpdateAnimationHideDuration, tableView.UseConstantDataBarAnimationSpeed);
            }
            return null;
        }

        public virtual AnimationTriggerContext CreateTriggerContext(DataUpdate dataUpdate) => 
            new AnimationTriggerContext(dataUpdate, new Func<CustomDataUpdateFormatConditionEventArgsSource, bool>(this.CustomTrigger));

        private bool CustomTrigger(CustomDataUpdateFormatConditionEventArgsSource argsSource)
        {
            Func<DataViewBase, DataControlBase> evaluator = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<DataViewBase, DataControlBase> local1 = <>c.<>9__13_0;
                evaluator = <>c.<>9__13_0 = x => x.DataControl;
            }
            Func<DataControlBase, DataControlBase> func2 = <>c.<>9__13_1;
            if (<>c.<>9__13_1 == null)
            {
                Func<DataControlBase, DataControlBase> local2 = <>c.<>9__13_1;
                func2 = <>c.<>9__13_1 = y => y.GetOriginationDataControl();
            }
            Func<DataControlBase, DataViewBase> func3 = <>c.<>9__13_2;
            if (<>c.<>9__13_2 == null)
            {
                Func<DataControlBase, DataViewBase> local3 = <>c.<>9__13_2;
                func3 = <>c.<>9__13_2 = z => z.viewCore;
            }
            DataViewBase input = this.ViewBase.With<DataViewBase, DataControlBase>(evaluator).With<DataControlBase, DataControlBase>(func2).With<DataControlBase, DataViewBase>(func3);
            if (input == null)
            {
                input = this.ViewBase;
            }
            Func<bool> fallback = <>c.<>9__13_4;
            if (<>c.<>9__13_4 == null)
            {
                Func<bool> local4 = <>c.<>9__13_4;
                fallback = <>c.<>9__13_4 = () => false;
            }
            return input.Return<DataViewBase, bool>(v => v.RaiseCustomDataUpdateFormatCondition(argsSource), fallback);
        }

        public virtual IList<IList<AnimationTimeline>> GetAnimations(Func<DataUpdate> dataUpdateAccessor, IConditionalFormattingClientBase client)
        {
            IEnumerable<FormatConditionBaseInfo> enumerable2;
            if (!this.CanCreateAnimations())
            {
                return null;
            }
            DataUpdate dataUpdate = dataUpdateAccessor();
            if (dataUpdate == null)
            {
                return null;
            }
            Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.GetRelatedConditions();
            }
            IList<FormatConditionBaseInfo> relatedConditions = client.With<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>>(evaluator);
            if (enumerable2 == null)
            {
                IList<FormatConditionBaseInfo> local2 = client.With<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>>(evaluator);
                relatedConditions = (IList<FormatConditionBaseInfo>) Enumerable.Empty<FormatConditionBaseInfo>();
            }
            IList<SequentialAnimationTimeline> animations = this.CollectConditionalAnimations(dataUpdate, relatedConditions);
            return ((animations.Count <= 0) ? null : SequentialAnimationHelper.GroupAnimationsByGeneration(animations));
        }

        private IEnumerable<FormatConditionBaseInfo> GetRelatedConditions(IConditionalFormattingClientBase client)
        {
            IEnumerable<FormatConditionBaseInfo> enumerable;
            Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x.GetRelatedConditions();
            }
            IList<FormatConditionBaseInfo> local3 = client.With<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>>(evaluator);
            if (enumerable == null)
            {
                IList<FormatConditionBaseInfo> local2 = client.With<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>>(evaluator);
                local3 = (IList<FormatConditionBaseInfo>) Enumerable.Empty<FormatConditionBaseInfo>();
            }
            return local3;
        }

        private bool HasDataUpdateFormatConditions()
        {
            Func<DataViewBase, bool> evaluator = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<DataViewBase, bool> local1 = <>c.<>9__15_0;
                evaluator = <>c.<>9__15_0 = y => y.HasDataUpdateFormatConditions();
            }
            return this.ViewBase.Return<DataViewBase, bool>(evaluator, (<>c.<>9__15_1 ??= () => true));
        }

        private bool IsInDesignMode()
        {
            Func<DataViewBase, bool> evaluator = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<DataViewBase, bool> local1 = <>c.<>9__16_0;
                evaluator = <>c.<>9__16_0 = y => y.IsDesignTime;
            }
            return this.ViewBase.Return<DataViewBase, bool>(evaluator, (<>c.<>9__16_1 ??= () => false));
        }

        public ITableView View { get; set; }

        private DataViewBase ViewBase
        {
            get
            {
                Func<ITableView, DataViewBase> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<ITableView, DataViewBase> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.ViewBase;
                }
                return this.View.With<ITableView, DataViewBase>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataUpdateAnimationProvider.<>c <>9 = new DataUpdateAnimationProvider.<>c();
            public static Func<ITableView, DataViewBase> <>9__5_0;
            public static Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> <>9__6_0;
            public static Func<IList<SequentialAnimationTimeline>, IEnumerable<SequentialAnimationTimeline>> <>9__7_0;
            public static Func<IConditionalFormattingClientBase, IList<FormatConditionBaseInfo>> <>9__8_0;
            public static Func<DataViewBase, DataControlBase> <>9__13_0;
            public static Func<DataControlBase, DataControlBase> <>9__13_1;
            public static Func<DataControlBase, DataViewBase> <>9__13_2;
            public static Func<bool> <>9__13_4;
            public static Func<DataViewBase, bool> <>9__15_0;
            public static Func<bool> <>9__15_1;
            public static Func<DataViewBase, bool> <>9__16_0;
            public static Func<bool> <>9__16_1;
            public static Func<ITableView, bool> <>9__17_0;
            public static Func<bool> <>9__17_1;

            internal bool <AllowConditionalAnimationByDefault>b__17_0(ITableView x) => 
                x.AnimateConditionalFormattingTransition;

            internal bool <AllowConditionalAnimationByDefault>b__17_1() => 
                true;

            internal IEnumerable<SequentialAnimationTimeline> <CollectConditionalAnimations>b__7_0(IList<SequentialAnimationTimeline> x) => 
                x;

            internal DataControlBase <CustomTrigger>b__13_0(DataViewBase x) => 
                x.DataControl;

            internal DataControlBase <CustomTrigger>b__13_1(DataControlBase y) => 
                y.GetOriginationDataControl();

            internal DataViewBase <CustomTrigger>b__13_2(DataControlBase z) => 
                z.viewCore;

            internal bool <CustomTrigger>b__13_4() => 
                false;

            internal DataViewBase <get_ViewBase>b__5_0(ITableView x) => 
                x.ViewBase;

            internal IList<FormatConditionBaseInfo> <GetAnimations>b__6_0(IConditionalFormattingClientBase x) => 
                x.GetRelatedConditions();

            internal IList<FormatConditionBaseInfo> <GetRelatedConditions>b__8_0(IConditionalFormattingClientBase x) => 
                x.GetRelatedConditions();

            internal bool <HasDataUpdateFormatConditions>b__15_0(DataViewBase y) => 
                y.HasDataUpdateFormatConditions();

            internal bool <HasDataUpdateFormatConditions>b__15_1() => 
                true;

            internal bool <IsInDesignMode>b__16_0(DataViewBase y) => 
                y.IsDesignTime;

            internal bool <IsInDesignMode>b__16_1() => 
                false;
        }
    }
}

