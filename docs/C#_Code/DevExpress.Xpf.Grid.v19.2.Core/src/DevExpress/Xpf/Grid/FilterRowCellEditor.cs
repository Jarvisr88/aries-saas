namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class FilterRowCellEditor : CellEditorBase
    {
        private BaseEditSettings oldEditSettings;

        public FilterRowCellEditor()
        {
            DataViewBase.SetRowHandle(this, new DevExpress.Xpf.Data.RowHandle(this.RowHandle));
            Action<FilterRowCellEditor, object, EventArgs> onEventAction = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Action<FilterRowCellEditor, object, EventArgs> local1 = <>c.<>9__4_0;
                onEventAction = <>c.<>9__4_0 = (owner, o, e) => owner.EditSettingsChanged(o, e);
            }
            this.EditSettingsChangedEventHandler = new EditSettingsChangedEventHandler<FilterRowCellEditor>(this, onEventAction);
        }

        private static void ApplyFilterEditorEditSettings(IDefaultEditorViewInfo info, ColumnFilterMode filterMode, IBaseEdit editor, BaseEditSettings settings, bool applySettings, bool assignComboBoxSource)
        {
            if ((filterMode != ColumnFilterMode.DisplayText) || (settings is CheckEditSettings))
            {
                bool? nullable;
                if (applySettings)
                {
                    settings.ApplyToEdit(editor, false, info);
                    TextEditSettings settings2 = settings as TextEditSettings;
                    if ((settings2 == null) || !string.IsNullOrEmpty(settings2.HighlightedText))
                    {
                        BaseEditHelper.UpdateHighlightingText(editor, null);
                    }
                }
                editor.NullText = string.Empty;
                BaseEdit edit = editor as BaseEdit;
                if (edit != null)
                {
                    edit.AllowNullInput = true;
                }
                CheckEdit edit2 = editor as CheckEdit;
                if (edit2 != null)
                {
                    edit2.IsThreeState = true;
                    nullable = null;
                    edit2.IsChecked = nullable;
                }
                else
                {
                    ToggleSwitchEdit edit3 = editor as ToggleSwitchEdit;
                    if (edit3 != null)
                    {
                        edit3.IsThreeState = true;
                        nullable = null;
                        edit3.IsChecked = nullable;
                    }
                    else
                    {
                        SpinEdit edit4 = editor as SpinEdit;
                        if (edit4 != null)
                        {
                            decimal? nullable2 = null;
                            edit4.MinValue = nullable2;
                            nullable2 = null;
                            edit4.MaxValue = nullable2;
                        }
                        else
                        {
                            ComboBoxEdit edit5 = editor as ComboBoxEdit;
                            if (edit5 != null)
                            {
                                if (EnumItemsSource.IsEnumItemsSource(edit5.ItemsSource))
                                {
                                    edit5.DisplayMember = EnumSourceHelperCore.DisplayMemberName;
                                    edit5.ValueMember = EnumSourceHelperCore.ValueMemberName;
                                }
                                if (assignComboBoxSource && !(edit5.ItemsSource is FilterComboBoxItemsList))
                                {
                                    IEnumerable source = DataBindingHelper.ExtractDataSource((edit5.ItemsSource != null) ? edit5.ItemsSource : edit5.Items, true, false, false);
                                    if ((source is IListServer) || (source is IAsyncListServer))
                                    {
                                        return;
                                    }
                                    edit5.ItemsSource = new FilterComboBoxItemsList(source);
                                }
                            }
                            DateEdit edit6 = editor as DateEdit;
                            if (edit6 != null)
                            {
                                DateTime? nullable3 = null;
                                edit6.MinValue = nullable3;
                                nullable3 = null;
                                edit6.MaxValue = nullable3;
                            }
                        }
                    }
                }
            }
        }

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            if (!ReferenceEquals(this.oldEditSettings, settings))
            {
                if (this.oldEditSettings != null)
                {
                    this.EditSettingsChangedEventHandler.Unsubscribe(this.oldEditSettings);
                }
                this.EditSettingsChangedEventHandler.Subscribe(settings);
                this.oldEditSettings = settings;
            }
            IBaseEdit edit = CreateFilterRowCellEditor(base.Column, settings);
            BaseEdit edit2 = edit as BaseEdit;
            if (edit2 != null)
            {
                Binding binding = new Binding("ImmediateUpdateAutoFilter");
                binding.Source = base.Column;
                BoolToObjectConverter converter1 = new BoolToObjectConverter();
                converter1.FalseValue = PostMode.Immediate;
                converter1.TrueValue = PostMode.Delayed;
                binding.Converter = converter1;
                edit2.SetBinding(BaseEdit.EditValuePostModeProperty, binding);
                Binding binding2 = new Binding("FilterRowDelay");
                binding2.Source = base.View;
                edit2.SetBinding(BaseEdit.EditValuePostDelayProperty, binding2);
            }
            return edit;
        }

        public static IBaseEdit CreateFilterRowCellEditor(ColumnBase column, BaseEditSettings settings) => 
            CreateFilterRowCellEditor(column, column.ColumnFilterMode, settings, null, true);

        internal static IBaseEdit CreateFilterRowCellEditor(IDefaultEditorViewInfo info, ColumnFilterMode filterMode, BaseEditSettings settings, Func<IBaseEdit> createEditor, bool assignComboBoxSource)
        {
            if ((filterMode == ColumnFilterMode.DisplayText) && !(settings is CheckEditSettings))
            {
                TextEdit edit1 = new TextEdit();
                edit1.VerticalContentAlignment = settings.VerticalContentAlignment;
                TextEdit edit2 = edit1;
                BaseEditHelper.AssignViewInfoProperties(edit2, settings, info);
                return edit2;
            }
            if (!ColumnFilterInfoHelper.CanUseEditSettingsInFilterEditor(settings))
            {
                settings = new TextEditSettings();
            }
            IBaseEdit edit = (createEditor != null) ? createEditor() : settings.CreateEditor(false, info, EditorOptimizationMode.Disabled);
            BaseEditHelper.UpdateHighlightingText(edit, null);
            if (edit is ButtonEdit)
            {
                ((ButtonEdit) edit).DefaultButtonClick += delegate (object d, RoutedEventArgs e) {
                    BaseEditHelper.RaiseDefaultButtonClick((ButtonEditSettings) settings);
                };
            }
            ApplyFilterEditorEditSettings(info, filterMode, edit, settings, false, assignComboBoxSource);
            return edit;
        }

        private void EditSettingsChanged(object sender, EventArgs e)
        {
            ApplyFilterEditorEditSettings(base.Column, base.Column.ColumnFilterMode, base.editCore, base.Column.ActualEditSettings, true, true);
        }

        protected override void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, ColumnBase.AutoFilterValueProperty) && !Equals(base.Edit.EditValue, base.Column.AutoFilterValue))
            {
                base.Edit.EditValue = base.Column.AutoFilterValue;
            }
            else if (ReferenceEquals(e.Property, ColumnBase.ColumnFilterModeProperty))
            {
                this.UpdateContent(true);
            }
            else if (ReferenceEquals(e.Property, ColumnBase.AutoFilterRowDisplayTemplateProperty))
            {
                this.UpdateDisplayTemplate(true);
            }
            else if (ReferenceEquals(e.Property, ColumnBase.ActualAllowAutoFilterProperty))
            {
                base.UpdateIsEditorReadOnly();
            }
            else
            {
                base.OnColumnContentChanged(sender, e);
            }
        }

        protected override void OnEditValueChanged()
        {
            if (base.Column.ImmediateUpdateAutoFilter)
            {
                base.Column.SetAutoFilterValue(base.Edit.EditValue);
            }
        }

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.Edit.EditValue = base.Column.AutoFilterValue;
            base.OnHiddenEditor(closeEditor);
        }

        protected override bool PostEditorCore()
        {
            base.Edit.FlushPendingEditActions();
            base.Column.SetAutoFilterValue(base.Edit.EditValue);
            return true;
        }

        protected override void SetDisplayTextProvider(IBaseEdit newEdit)
        {
        }

        protected override void UpdateDisplayTemplate(bool updateForce = false)
        {
            if (updateForce || (base.Column.AutoFilterRowDisplayTemplate != null))
            {
                base.Edit.SetDisplayTemplate(base.Column.AutoFilterRowDisplayTemplate);
            }
        }

        protected override void UpdateEditContext()
        {
        }

        protected override void UpdateEditorDataContextValue(object newValue)
        {
            if ((base.Column != null) && base.Column.ImmediateUpdateAutoFilter)
            {
                base.Column.AutoFilterValue = newValue;
            }
        }

        protected override void UpdateEditTemplate()
        {
            base.Edit.SetEditTemplate(base.Column.AutoFilterRowEditTemplate);
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            if (!this.IsEditorVisible)
            {
                editor.EditValue = base.Column.AutoFilterValue;
            }
        }

        protected override void UpdateToolTip()
        {
        }

        private EditSettingsChangedEventHandler<FilterRowCellEditor> EditSettingsChangedEventHandler { get; set; }

        protected internal override int RowHandle =>
            -2147483645;

        protected override bool IsReadOnly =>
            !base.Column.ActualAllowAutoFilter || ReferenceEquals(base.View.ColumnsCore[base.Column.FieldName], null);

        protected override bool OverrideCellTemplate =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterRowCellEditor.<>c <>9 = new FilterRowCellEditor.<>c();
            public static Action<FilterRowCellEditor, object, EventArgs> <>9__4_0;

            internal void <.ctor>b__4_0(FilterRowCellEditor owner, object o, EventArgs e)
            {
                owner.EditSettingsChanged(o, e);
            }
        }

        private class FilterComboBoxItemsList : List<object>, INotifyCollectionChanged, IWeakEventListener
        {
            private CustomComboBoxItem emptyItem;
            private IEnumerable source;
            private NotifyCollectionChangedEventHandler collectionChanged;

            event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
            {
                add
                {
                    this.collectionChanged += value;
                }
                remove
                {
                    this.collectionChanged -= value;
                }
            }

            public FilterComboBoxItemsList(IEnumerable source)
            {
                CustomComboBoxItem item1 = new CustomComboBoxItem();
                item1.DisplayValue = string.Empty;
                item1.EditValue = string.Empty;
                this.emptyItem = item1;
                this.source = source;
                this.LoadItems();
                if (source is IBindingList)
                {
                    ListChangedEventManager.AddListener(source as IBindingList, this);
                }
            }

            private void LoadItems()
            {
                base.Clear();
                foreach (object obj2 in this.source)
                {
                    base.Add(obj2);
                }
                if (base.Count != 0)
                {
                    base.Insert(0, this.emptyItem);
                }
            }

            private void OnChanged()
            {
                this.LoadItems();
                if (this.collectionChanged != null)
                {
                    this.collectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }

            bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
            {
                if (!(managerType == typeof(ListChangedEventManager)))
                {
                    return false;
                }
                this.OnChanged();
                return true;
            }
        }
    }
}

