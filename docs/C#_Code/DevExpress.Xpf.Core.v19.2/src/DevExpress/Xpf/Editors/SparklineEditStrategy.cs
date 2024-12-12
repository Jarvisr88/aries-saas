namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SparklineEditStrategy : EditStrategyBase
    {
        private DevExpress.Xpf.Editors.Range valueRange;
        private DevExpress.Xpf.Editors.Range argumentRange;

        public SparklineEditStrategy(SparklineEdit editor) : base(editor)
        {
            this.FilterChangedLocker = new Locker();
            this.valueRange = new DevExpress.Xpf.Editors.Range();
            this.argumentRange = new DevExpress.Xpf.Editors.Range();
        }

        public virtual void ArgumentMemberChanged(string argument)
        {
            this.ItemsProvider.PointArgumentMember = argument;
            base.SyncWithValue();
        }

        public object CoerceItems(object value) => 
            this.CoerceValue(SparklineEdit.ItemsProperty, value);

        public virtual void FilterCriteriaChanged(CriteriaOperator newCriteriaOperator)
        {
            this.ItemsProvider.FilterCriteria = newCriteriaOperator;
            base.SyncWithValue();
        }

        protected virtual object GetItems(object baseValue)
        {
            if (baseValue is IEnumerable)
            {
                return baseValue;
            }
            List<object> list1 = new List<object>();
            list1.Add(baseValue);
            return list1;
        }

        public virtual void ItemProviderChanged(ItemsProviderChangedEventArgs e)
        {
            if (!base.ValueContainer.HasValueCandidate)
            {
                this.FilterChangedLocker.DoIfNotLocked(new Action(this.SyncWithValue));
            }
        }

        public virtual void ItemsChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.SyncWithValue(SparklineEdit.ItemsProperty, oldValue, newValue);
        }

        public override void OnInitialized()
        {
            this.Editor.UnsubscribeToItemsProviderChanged();
            this.Editor.SubscribeToItemsProviderChanged();
            base.OnInitialized();
        }

        public virtual void PointArgumentRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            this.argumentRange = range;
            if (this.Sparkline != null)
            {
                if (this.valueRange != null)
                {
                    this.valueRange.SetContainer(this.Sparkline);
                }
                if (this.argumentRange != null)
                {
                    this.argumentRange.SetContainer(this.Sparkline);
                }
            }
            this.Sparkline.Do<SparklineControl>(x => x.ArgumentRange = this.argumentRange);
        }

        public virtual void PointArgumentSortOrderChanged(SparklineSortOrder newColumnSortOrder)
        {
            this.ItemsProvider.PointArgumentSortOrder = newColumnSortOrder;
            base.SyncWithValue();
        }

        public virtual void PointValueRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            this.valueRange = range;
            if (this.Sparkline != null)
            {
                if (this.valueRange != null)
                {
                    this.valueRange.SetContainer(this.Sparkline);
                }
                if (this.argumentRange != null)
                {
                    this.argumentRange.SetContainer(this.Sparkline);
                }
            }
            this.Sparkline.Do<SparklineControl>(x => x.ValueRange = this.valueRange);
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__13_0;
                getBaseValueHandler = <>c.<>9__13_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(SparklineEdit.ItemsProperty, getBaseValueHandler, baseValue => this.GetItems(baseValue));
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            if (this.Sparkline != null)
            {
                this.valueRange.SetContainer(this.Sparkline);
                this.argumentRange.SetContainer(this.Sparkline);
            }
            this.Sparkline.Do<SparklineControl>(x => x.Points = this.ItemsProvider.Points);
            this.Sparkline.Do<SparklineControl>(x => x.ValueRange = this.valueRange);
            this.Sparkline.Do<SparklineControl>(x => x.ArgumentRange = this.argumentRange);
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.ItemsProvider.ItemsSource = base.ValueContainer.EditValue;
            if (this.Sparkline != null)
            {
                this.valueRange.SetContainer(this.Sparkline);
                this.argumentRange.SetContainer(this.Sparkline);
            }
            this.Sparkline.Do<SparklineControl>(x => x.Points = this.ItemsProvider.Points);
            this.Sparkline.Do<SparklineControl>(x => x.ValueRange = this.valueRange);
            this.Sparkline.Do<SparklineControl>(x => x.ArgumentRange = this.argumentRange);
        }

        public virtual void ValueMemberChanged(string newValue)
        {
            this.ItemsProvider.PointValueMember = newValue;
            base.SyncWithValue();
        }

        private SparklineEdit Editor =>
            base.Editor as SparklineEdit;

        private SparklineItemsProvider ItemsProvider =>
            this.Editor.ItemsProvider;

        private SparklineControl Sparkline =>
            this.Editor.EditCore as SparklineControl;

        private Locker FilterChangedLocker { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SparklineEditStrategy.<>c <>9 = new SparklineEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__13_0;

            internal object <RegisterUpdateCallbacks>b__13_0(object baseValue) => 
                baseValue;
        }
    }
}

