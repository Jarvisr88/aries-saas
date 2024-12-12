namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Format")]
    public abstract class FormatConditionBase : DependencyObject, IDetailElement<FormatConditionBase>, ISupportManager
    {
        private static readonly DependencyPropertyKey OwnerPropertyKey = DependencyProperty.RegisterReadOnly("Owner", typeof(FormatConditionCollection), typeof(FormatConditionBase), new PropertyMetadata(null));
        public static readonly DependencyProperty OwnerProperty = OwnerPropertyKey.DependencyProperty;
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty ExpressionProperty;
        public static readonly DependencyProperty PredefinedFormatNameProperty;
        public static readonly DependencyProperty IsEnabledProperty;
        public static readonly DependencyProperty FixedProperty;
        public static readonly DependencyProperty ApplyToRowProperty;
        private const string predefinedFormatsOwnerPath = "Owner.Owner.View.";
        private Locker serializationLocker = new Locker();

        static FormatConditionBase()
        {
            FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(FormatConditionBase), new PropertyMetadata(null, (d, e) => ((FormatConditionBase) d).OnInfoPropertyChanged(e)));
            ExpressionProperty = DependencyProperty.Register("Expression", typeof(string), typeof(FormatConditionBase), new PropertyMetadata(null, (d, e) => ((FormatConditionBase) d).OnInfoPropertyChanged(e)));
            PredefinedFormatNameProperty = DependencyProperty.Register("PredefinedFormatName", typeof(string), typeof(FormatConditionBase), new PropertyMetadata(null, (d, e) => ((FormatConditionBase) d).OnFormatNameChanged()));
            IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(FormatConditionBase), new PropertyMetadata(true, (d, e) => ((FormatConditionBase) d).OnChanged(e, FormatConditionChangeType.All)));
            FixedProperty = DependencyProperty.Register("Fixed", typeof(bool), typeof(FormatConditionBase), new PropertyMetadata(false));
            ApplyToRowProperty = DependencyProperty.Register("ApplyToRow", typeof(bool), typeof(FormatConditionBase), new PropertyMetadata(false, (d, e) => ((FormatConditionBase) d).OnChanged(e, FormatConditionChangeType.All)));
        }

        public FormatConditionBase()
        {
            this.Info = this.CreateInfo();
            this.SyncProperties();
        }

        protected abstract BaseEditUnit CreateEmptyEditUnit();
        internal abstract FormatConditionRuleBase CreateExportWrapper();
        protected abstract FormatConditionBaseInfo CreateInfo();
        internal IEnumerable<ConditionalFormatSummaryInfo> CreateSummaryItems() => 
            this.Info.CreateSummaryItems();

        BaseEditUnit ISupportManager.CreateEditUnit()
        {
            BaseEditUnit unit = this.CreateEmptyEditUnit();
            this.UpdateEditUnit(unit);
            return unit;
        }

        FormatConditionBase IDetailElement<FormatConditionBase>.CreateNewInstance(params object[] args) => 
            (FormatConditionBase) Activator.CreateInstance(base.GetType());

        public virtual string GetApplyToFieldName() => 
            this.ApplyToRow ? null : this.FieldName;

        internal IEnumerable<IColumnInfo> GetUnboundColumnInfo() => 
            this.Info.GetUnboundColumnInfo();

        internal virtual bool HasVisualSettings() => 
            base.GetValue(this.FormatPropertyForBinding) != null;

        protected void OnChanged(DependencyPropertyChangedEventArgs e, FormatConditionChangeType changeType = 3)
        {
            this.Owner.Do<FormatConditionCollection>(x => x.OnItemPropertyChanged(this, e, changeType));
        }

        protected internal static object OnCoerceFreezable(DependencyObject d, object baseValue) => 
            (!(d is FormatConditionBase) || !((FormatConditionBase) d).serializationLocker.IsLocked) ? FormatConditionBaseInfo.OnCoerceFreezable(baseValue) : baseValue;

        public void OnDeserializeEnd()
        {
            this.serializationLocker.Unlock();
            base.CoerceValue(this.FormatPropertyForBinding);
        }

        public void OnDeserializeStart()
        {
            this.serializationLocker.Lock();
        }

        protected static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FormatConditionBase base2 = (FormatConditionBase) d;
            base2.Info.FormatCore = e.NewValue as Freezable;
            base2.OnChanged(e, FormatConditionBaseInfo.GetChangeType(e));
        }

        private void OnFormatNameChanged()
        {
            this.Info.OnFormatNameChanged(this, this.PredefinedFormatName, "Owner.Owner.View.", this.FormatPropertyForBinding);
        }

        protected void OnInfoPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.SyncProperty(e.Property);
            this.OnChanged(e, FormatConditionChangeType.All);
        }

        protected void SyncIfNeeded(DependencyProperty sourceProperty, DependencyProperty targetProperty, Action syncAction)
        {
            if (ReferenceEquals(sourceProperty, targetProperty) || (sourceProperty == null))
            {
                syncAction();
            }
        }

        private void SyncProperties()
        {
            this.SyncProperty(null);
        }

        protected virtual void SyncProperty(DependencyProperty property)
        {
            this.SyncIfNeeded(property, FieldNameProperty, () => this.Info.FieldName = this.FieldName);
            this.SyncIfNeeded(property, ExpressionProperty, () => this.Info.Expression = this.Expression);
        }

        protected virtual void UpdateEditUnit(BaseEditUnit unit)
        {
            unit.Expression = this.Expression;
            unit.FieldName = this.FieldName;
            unit.PredefinedFormatName = this.PredefinedFormatName;
            unit.IsEnabled = this.IsEnabled;
            unit.ApplyToRow = this.ApplyToRow;
        }

        private bool XtraShouldSerializeFormat() => 
            string.IsNullOrEmpty(this.PredefinedFormatName);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormatConditionCollection Owner
        {
            get => 
                (FormatConditionCollection) base.GetValue(OwnerProperty);
            internal set => 
                base.SetValue(OwnerPropertyKey, value);
        }

        [XtraSerializableProperty]
        public string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        [XtraSerializableProperty]
        public string Expression
        {
            get => 
                (string) base.GetValue(ExpressionProperty);
            set => 
                base.SetValue(ExpressionProperty, value);
        }

        [XtraSerializableProperty]
        public string PredefinedFormatName
        {
            get => 
                (string) base.GetValue(PredefinedFormatNameProperty);
            set => 
                base.SetValue(PredefinedFormatNameProperty, value);
        }

        [XtraSerializableProperty]
        public bool IsEnabled
        {
            get => 
                (bool) base.GetValue(IsEnabledProperty);
            set => 
                base.SetValue(IsEnabledProperty, value);
        }

        [XtraSerializableProperty]
        public bool Fixed
        {
            get => 
                (bool) base.GetValue(FixedProperty);
            set => 
                base.SetValue(FixedProperty, value);
        }

        [XtraSerializableProperty]
        public bool ApplyToRow
        {
            get => 
                (bool) base.GetValue(ApplyToRowProperty);
            set => 
                base.SetValue(ApplyToRowProperty, value);
        }

        public abstract DependencyProperty FormatPropertyForBinding { get; }

        public string OwnerPredefinedFormatsPropertyName =>
            this.Info.OwnerPredefinedFormatsPropertyName;

        protected internal FormatConditionBaseInfo Info { get; private set; }

        internal bool IsValid =>
            this.HasVisualSettings() && (this.CanAttach && this.IsEnabled);

        protected virtual bool CanAttach =>
            true;

        bool ISupportManager.AllowUserCustomization =>
            !this.Fixed;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public virtual string TypeName
        {
            get => 
                base.GetType().Name;
            set
            {
            }
        }

        internal abstract DependencyProperty ActualAnimationSettingsProperty { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionBase.<>c <>9 = new FormatConditionBase.<>c();

            internal void <.cctor>b__72_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatConditionBase) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__72_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatConditionBase) d).OnInfoPropertyChanged(e);
            }

            internal void <.cctor>b__72_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatConditionBase) d).OnFormatNameChanged();
            }

            internal void <.cctor>b__72_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatConditionBase) d).OnChanged(e, FormatConditionChangeType.All);
            }

            internal void <.cctor>b__72_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatConditionBase) d).OnChanged(e, FormatConditionChangeType.All);
            }
        }
    }
}

