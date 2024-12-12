namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class EditorsGeneratorBase
    {
        protected EditorsGeneratorBase()
        {
        }

        public virtual void Char(IEdmPropertyInfo property)
        {
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : typeof(TextEditSettings), this.CharInitializer(property));
        }

        internal Lazy<Initializer> Char(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.Char(property);
            }
            return new Lazy<Initializer>(() => this.CharInitializer(property));
        }

        protected internal virtual Initializer CharInitializer(IEdmPropertyInfo property) => 
            new Initializer(delegate (IModelItem container, IModelItem edit) {
                EditorInitializeHelper.InitializeCharEdit(edit);
                edit.SetValueIfNotSet(BaseEdit.AllowNullInputProperty, GetAllowNullInput(property), false);
            }, null);

        public virtual void Check(IEdmPropertyInfo property)
        {
            bool? allowNullInput = null;
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(CheckEdit) : typeof(CheckEditSettings), this.CheckInitializer(property, allowNullInput));
        }

        internal Lazy<Initializer> Check(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.Check(property);
            }
            return new Lazy<Initializer>(() => this.CheckInitializer(property, null));
        }

        protected internal virtual Initializer CheckInitializer(IEdmPropertyInfo property, bool? allowNullInput = new bool?())
        {
            bool? nullable = allowNullInput;
            bool _allowNullInput = (nullable != null) ? nullable.GetValueOrDefault() : GetAllowNullInput(property);
            return new Initializer(delegate (IModelItem container, IModelItem edit) {
                edit.SetValueIfNotSet(CheckEdit.IsThreeStateProperty, _allowNullInput, false);
            }, null);
        }

        public virtual void DateTime(IEdmPropertyInfo property)
        {
            bool? nullable = null;
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(DateEdit) : typeof(DateEditSettings), this.DateTimeInitializer(property, null, nullable));
        }

        internal Lazy<Initializer> DateTime(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.DateTime(property);
            }
            return new Lazy<Initializer>(() => this.DateTimeInitializer(property, null, null));
        }

        protected internal virtual Initializer DateTimeInitializer(IEdmPropertyInfo property, MaskInfo mask = null, bool? nullable = new bool?())
        {
            MaskInfo dateTimeMask = mask;
            if (mask == null)
            {
                MaskInfo local1 = mask;
                dateTimeMask = GetDateTimeMask(property);
            }
            MaskInfo _mask = dateTimeMask;
            bool? nullable2 = nullable;
            bool _nullable = (nullable2 != null) ? nullable2.GetValueOrDefault() : GetAllowNullInput(property);
            return new Initializer(delegate (IModelItem container, IModelItem edit) {
                edit.SetValueIfNotSet(BaseEdit.AllowNullInputProperty, _nullable, false);
                EditorInitializeHelper.InitializeDateTimeMask(edit, _mask);
            }, null);
        }

        public virtual void Enum(IEdmPropertyInfo property, Type enumType)
        {
            bool? useFlags = null;
            useFlags = null;
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(ComboBoxEdit) : typeof(ComboBoxEditSettings), this.EnumInitializer(property, enumType, useFlags, useFlags));
        }

        internal Lazy<Initializer> Enum(bool callGenerateMethod, IEdmPropertyInfo property, Type enumType)
        {
            if (callGenerateMethod)
            {
                this.Enum(property, enumType);
            }
            return new Lazy<Initializer>(delegate {
                bool? useFlags = null;
                useFlags = null;
                return this.EnumInitializer(property, enumType, useFlags, useFlags);
            });
        }

        protected internal virtual Initializer EnumInitializer(IEdmPropertyInfo property, Type enumType, bool? useFlags = new bool?(), bool? nullable = new bool?())
        {
            bool? nullable2 = nullable;
            bool _nullable = (nullable2 != null) ? nullable2.GetValueOrDefault() : GetAllowNullInput(property);
            return new Initializer(delegate (IModelItem container, IModelItem edit) {
                if (_nullable)
                {
                    edit.SetValueIfNotSet(BaseEdit.AllowNullInputProperty, false, false);
                }
                EditorInitializeHelper.InitializeEnumItemsSource(property, edit, enumType);
            }, null);
        }

        public virtual IEnumerable<IEdmPropertyInfo> FilterProperties(IEnumerable<IEdmPropertyInfo> properties) => 
            properties;

        protected abstract void GenerateEditor(IEdmPropertyInfo property, Type editType, Initializer initializer);
        public abstract void GenerateEditorFromResources(IEdmPropertyInfo property, object resourceKey, Initializer initializer);
        protected static bool GetAllowNullInput(IEdmPropertyInfo property)
        {
            bool flag = property.HasNullableType();
            return (property.HasNullableType() && !property.Attributes.Required());
        }

        protected static MaskInfo GetDateTimeMask(IEdmPropertyInfo property)
        {
            PropertyDataType dataType = property.Attributes.PropertyDataType();
            string actualMask = property.GetActualMask(GetDateTimeMask(dataType));
            RegExMaskType? defaultMaskType = null;
            return MaskInfo.GetMaskIfo(property.Attributes.Mask(), actualMask, actualMask == "d", defaultMaskType, string.IsNullOrEmpty(property.Attributes.DataFormatString));
        }

        private static string GetDateTimeMask(PropertyDataType dataType) => 
            (dataType == PropertyDataType.DateTime) ? "g" : ((dataType == PropertyDataType.Time) ? "t" : "d");

        protected static Type GetEditValueType(IEdmPropertyInfo property) => 
            property.HasNullableType() ? property.GetUnderlyingClrType() : null;

        public static IEnumerable<IEdmPropertyInfo> GetFilteredAndSortedProperties(IEnumerable<IEdmPropertyInfo> properties, bool scaffolding, bool movePropertiesWithNegativeOrderToEnd, LayoutType layoutType)
        {
            Func<IEdmPropertyInfo, bool> func1 = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<IEdmPropertyInfo, bool> local1 = <>c.<>9__15_0;
                func1 = <>c.<>9__15_0 = x => (x.Attributes.Order == null) || (x.Attributes.Order.Value >= 0);
            }
            Func<IEdmPropertyInfo, bool> isVisible = func1;
            properties = GetFilteredProperties(properties, scaffolding).ToArray<IEdmPropertyInfo>();
            IEnumerable<IEdmPropertyInfo> source = movePropertiesWithNegativeOrderToEnd ? (from x in properties
                where isVisible(x)
                select x) : properties;
            IEnumerable<IEdmPropertyInfo> second = movePropertiesWithNegativeOrderToEnd ? (from x in properties
                where !isVisible(x)
                select x) : ((IEnumerable<IEdmPropertyInfo>) new IEdmPropertyInfo[0]);
            return source.OrderBy<IEdmPropertyInfo, int>(delegate (IEdmPropertyInfo x) {
                int? order = x.Attributes.GetOrder(layoutType);
                return ((order != null) ? order.Value : 0x2710);
            }).Concat<IEdmPropertyInfo>(second);
        }

        private static IEnumerable<IEdmPropertyInfo> GetFilteredProperties(IEnumerable<IEdmPropertyInfo> properties, bool scaffolding)
        {
            Func<IEdmPropertyInfo, bool> predicate = x => (!scaffolding || x.Attributes.AllowScaffolding) ? (!x.IsForeignKey && ((x.Attributes.AutoGenerateField == null) ? true : x.Attributes.AutoGenerateField.Value)) : false;
            return properties.Where<IEdmPropertyInfo>(predicate);
        }

        protected abstract Type GetLookUpEditType();
        protected static int GetMaxLength(IEdmPropertyInfo property) => 
            property.Attributes.MaxLength();

        protected static object GetResourceContent<TColumn, TEditSettings, TEditor>(DataTemplate template)
        {
            object obj2 = template.LoadContent();
            switch (obj2)
            {
                case (TColumn _):
                    break;

                case (TEditSettings _):
                    return obj2;
                    break;

                case (TEditor _):
                    return obj2;
                    break;
            }
            return obj2;
        }

        protected internal static DataTemplate GetResourceTemplate(FrameworkElement targetObject, object resourceKey)
        {
            DataTemplate template = targetObject.TryFindResource(resourceKey) as DataTemplate;
            if (!ViewModelBase.IsInDesignMode && (template == null))
            {
                throw new InvalidOperationException($"'{resourceKey.ToString()}' resource not found");
            }
            return template;
        }

        public virtual void Hyperlink(IEdmPropertyInfo property)
        {
            Type editType = (this.Mode == EditorsGeneratorMode.Edit) ? typeof(HyperlinkEdit) : typeof(HyperlinkEditSettings);
            this.GenerateEditor(property, editType, this.HyperlinkInitializer(property));
        }

        internal Lazy<Initializer> Hyperlink(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.Hyperlink(property);
            }
            return new Lazy<Initializer>(() => this.HyperlinkInitializer(property));
        }

        protected internal virtual Initializer HyperlinkInitializer(IEdmPropertyInfo property) => 
            Initializer.Default;

        public virtual void Image(IEdmPropertyInfo property, bool readOnly)
        {
            Type editType = (this.Mode == EditorsGeneratorMode.Edit) ? typeof(PopupImageEdit) : typeof(PopupImageEditSettings);
            if (this.Target == EditorsGeneratorTarget.LayoutControl)
            {
                editType = (this.Mode == EditorsGeneratorMode.Edit) ? typeof(ImageEdit) : typeof(ImageEditSettings);
            }
            this.GenerateEditor(property, editType, this.ImageInitializer(property, readOnly));
        }

        internal Lazy<Initializer> Image(bool callGenerateMethod, IEdmPropertyInfo property, bool readOnly)
        {
            if (callGenerateMethod)
            {
                this.Image(property, readOnly);
            }
            return new Lazy<Initializer>(() => this.ImageInitializer(property, readOnly));
        }

        protected internal virtual Initializer ImageInitializer(IEdmPropertyInfo property, bool readOnly) => 
            new Initializer(null, delegate (IModelItem container) {
                if (readOnly)
                {
                    container.SetValueIfNotSet("IsReadOnly", true, false);
                    container.SetValueIfNotSet("ReadOnly", true, false);
                }
            });

        private static bool IsEdit(IModelItem edit) => 
            edit.ItemType.IsSubclassOf(typeof(BaseEdit));

        private static bool IsEditSettings(IModelItem edit) => 
            edit.ItemType.IsSubclassOf(typeof(BaseEditSettings));

        private static bool IsLookUpEdit(IModelItem edit) => 
            edit.ItemType.IsSubclassOf(typeof(LookUpEditBase));

        private static bool IsLookUpEditSettings(IModelItem edit) => 
            edit.ItemType.IsSubclassOf(typeof(LookUpEditSettingsBase));

        private static bool IsNumericEdit(IModelItem edit) => 
            IsEdit(edit) && !IsLookUpEdit(edit);

        private static bool IsNumericEditSettings(IModelItem edit) => 
            IsEditSettings(edit) && !IsLookUpEditSettings(edit);

        public virtual void LookUp(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
        {
            Type editType = null;
            editType = !string.IsNullOrEmpty(itemsSource) ? this.GetLookUpEditType() : ((this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : null);
            this.GenerateEditor(property, editType, this.LookUpInitializer(property, itemsSource, displayMember, foreignKeyInfo));
        }

        internal Lazy<Initializer> LookUp(bool callGenerateMethod, IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
        {
            if (callGenerateMethod)
            {
                this.LookUp(property, itemsSource, displayMember, foreignKeyInfo);
            }
            return new Lazy<Initializer>(() => this.LookUpInitializer(property, itemsSource, displayMember, foreignKeyInfo));
        }

        protected internal virtual Initializer LookUpInitializer(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo) => 
            new Initializer(delegate (IModelItem container, IModelItem edit) {
                if (!string.IsNullOrEmpty(itemsSource))
                {
                    edit.SetValueIfNotSet(LookUpEditSettingsBase.ItemsSourceProperty, new Binding(itemsSource), false);
                    edit.SetValueIfNotSet(LookUpEditSettingsBase.DisplayMemberProperty, displayMember, false);
                    edit.SetValueIfNotSet(LookUpEditSettingsBase.ValueMemberProperty, foreignKeyInfo.PrimaryKeyPropertyName, false);
                }
            }, delegate (IModelItem container) {
                if (string.IsNullOrEmpty(itemsSource))
                {
                    container.SetValueIfNotSet("IsReadOnly", true, false);
                    container.SetValueIfNotSet("ReadOnly", true, false);
                }
            });

        public virtual void Numeric(IEdmPropertyInfo property, MaskInfo mask)
        {
            bool? nullable = null;
            int? maxLength = null;
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : typeof(TextEditSettings), this.NumericInitializer(property, mask, null, nullable, maxLength));
        }

        internal Lazy<Initializer> Numeric(bool callGenerateMethod, IEdmPropertyInfo property, MaskInfo mask)
        {
            if (callGenerateMethod)
            {
                this.Numeric(property, mask);
            }
            return new Lazy<Initializer>(delegate {
                bool? nullable = null;
                int? maxLength = null;
                return this.NumericInitializer(property, mask, null, nullable, maxLength);
            });
        }

        protected internal virtual Initializer NumericInitializer(IEdmPropertyInfo property, MaskInfo mask, Type editValueType = null, bool? nullable = new bool?(), int? maxLength = new int?())
        {
            Type type1 = editValueType;
            if (editValueType == null)
            {
                Type local1 = editValueType;
                type1 = GetEditValueType(property);
            }
            Type _editValueType = type1;
            bool? nullable2 = nullable;
            bool _nullable = (nullable2 != null) ? nullable2.GetValueOrDefault() : GetAllowNullInput(property);
            int? nullable3 = maxLength;
            int _maxLength = (nullable3 != null) ? nullable3.GetValueOrDefault() : GetMaxLength(property);
            return new Initializer(delegate (IModelItem container, IModelItem edit) {
                if (IsNumericEdit(edit))
                {
                    EditorInitializeHelper.InitializeMask(edit, MaskType.Numeric, mask, _maxLength);
                    edit.SetValueIfNotSet(Control.HorizontalContentAlignmentProperty, EditorsSource.CurrencyValueAlignment, false);
                    edit.SetValueIfNotSet(BaseEdit.EditValueTypeProperty, _editValueType, false);
                    edit.SetValueIfNotSet(BaseEdit.AllowNullInputProperty, _nullable, false);
                }
                else if (IsNumericEditSettings(edit))
                {
                    EditorInitializeHelper.InitializeMask(edit, MaskType.Numeric, mask, _maxLength);
                    edit.SetValueIfNotSet(BaseEditSettings.AllowNullInputProperty, _nullable, false);
                }
            }, null);
        }

        public virtual void Object(IEdmPropertyInfo property)
        {
            if (this.Mode == EditorsGeneratorMode.Edit)
            {
                this.GenerateEditor(property, typeof(TextEdit), this.ObjectInitializer(property));
            }
            else
            {
                this.GenerateEditor(property, null, this.ObjectInitializer(property));
            }
        }

        internal Lazy<Initializer> Object(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.Object(property);
            }
            return new Lazy<Initializer>(() => this.ObjectInitializer(property));
        }

        protected internal virtual Initializer ObjectInitializer(IEdmPropertyInfo property) => 
            Initializer.Default;

        public virtual void Password(IEdmPropertyInfo property)
        {
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(PasswordBoxEdit) : typeof(PasswordBoxEditSettings), this.PasswordInitializer(property));
        }

        internal Lazy<Initializer> Password(bool callGenerateMethod, IEdmPropertyInfo property)
        {
            if (callGenerateMethod)
            {
                this.Password(property);
            }
            return new Lazy<Initializer>(() => this.PasswordInitializer(property));
        }

        protected internal virtual Initializer PasswordInitializer(IEdmPropertyInfo property) => 
            new Initializer(delegate (IModelItem container, IModelItem edit) {
                EditorInitializeHelper.InitializeMaxLength(edit, GetMaxLength(property));
            }, null);

        public virtual void Range(IEdmPropertyInfo property, MaskInfo mask, object minimum, object maximum)
        {
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(SpinEdit) : typeof(SpinEditSettings), this.RangeInitializer(property, mask, minimum, maximum, null));
        }

        internal Lazy<Initializer> Range(bool callGenerateMethod, IEdmPropertyInfo property, MaskInfo mask, object minimum, object maximum)
        {
            if (callGenerateMethod)
            {
                this.Range(property, mask, minimum, maximum);
            }
            return new Lazy<Initializer>(() => this.RangeInitializer(property, mask, minimum, maximum, null));
        }

        protected internal virtual Initializer RangeInitializer(IEdmPropertyInfo property, MaskInfo mask, object minimum, object maximum, Type editValueType = null)
        {
            if (editValueType == null)
            {
                if (property.HasNullableType())
                {
                    editValueType = property.GetUnderlyingClrType();
                }
                else if (property.PropertyType.IsValueType)
                {
                    editValueType = property.PropertyType;
                }
            }
            int? maxLength = null;
            return this.NumericInitializer(property, mask, editValueType, false, maxLength);
        }

        public virtual void RegExMaskText(IEdmPropertyInfo property, MaskInfo mask)
        {
            this.GenerateEditor(property, (this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : typeof(TextEditSettings), this.RegExMaskTextInitializer(property, mask));
        }

        internal Lazy<Initializer> RegExMaskText(bool callGenerateMethod, IEdmPropertyInfo property, MaskInfo mask)
        {
            if (callGenerateMethod)
            {
                this.RegExMaskText(property, mask);
            }
            return new Lazy<Initializer>(() => this.RegExMaskTextInitializer(property, mask));
        }

        protected internal virtual Initializer RegExMaskTextInitializer(IEdmPropertyInfo property, MaskInfo mask) => 
            new Initializer(delegate (IModelItem container, IModelItem edit) {
                EditorInitializeHelper.InitializeRegExMask(edit, mask, GetMaxLength(property));
            }, null);

        public virtual void Text(IEdmPropertyInfo property, bool multiline)
        {
            int maxLength = GetMaxLength(property);
            Type editType = (this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : ((maxLength > 0) ? typeof(TextEditSettings) : null);
            if (multiline)
            {
                editType = (this.Mode == EditorsGeneratorMode.Edit) ? typeof(TextEdit) : typeof(MemoEditSettings);
            }
            this.GenerateEditor(property, editType, this.TextInitializer(property, multiline, new int?(maxLength)));
        }

        internal Lazy<Initializer> Text(bool callGenerateMethod, IEdmPropertyInfo property, bool multiline)
        {
            if (callGenerateMethod)
            {
                this.Text(property, multiline);
            }
            return new Lazy<Initializer>(() => this.TextInitializer(property, multiline, null));
        }

        protected internal virtual Initializer TextInitializer(IEdmPropertyInfo property, bool multiline, int? maxLength = new int?())
        {
            int? nullable = maxLength;
            int _maxLength = (nullable != null) ? nullable.GetValueOrDefault() : GetMaxLength(property);
            return new Initializer(delegate (IModelItem container, IModelItem edit) {
                if (!multiline)
                {
                    EditorInitializeHelper.InitializeMaxLength(edit, _maxLength);
                }
                else if (!IsEdit(edit))
                {
                    if (IsEditSettings(edit))
                    {
                        EditorInitializeHelper.InitializeMaxLength(edit, _maxLength);
                    }
                }
                else
                {
                    edit.SetValueIfNotSet(TextEditBase.TextWrappingProperty, TextWrapping.Wrap, false);
                    edit.SetValueIfNotSet(TextEditBase.AcceptsReturnProperty, true, false);
                    edit.SetValue(Control.VerticalContentAlignmentProperty, VerticalAlignment.Top, false);
                    edit.SetValueIfNotSet(TextEditBase.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, false);
                    EditorInitializeHelper.InitializeMaxLength(edit, _maxLength);
                }
            }, delegate (IModelItem container) {
                if (multiline && (this.Mode == EditorsGeneratorMode.Edit))
                {
                    container.Properties["MinHeight"].SetValueIfNotSet(EditorsSource.MultilineTextMinHeight);
                    container.Properties["VerticalAlignment"].SetValue(VerticalAlignment.Stretch);
                }
            });
        }

        public abstract EditorsGeneratorTarget Target { get; }

        protected abstract EditorsGeneratorMode Mode { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorsGeneratorBase.<>c <>9 = new EditorsGeneratorBase.<>c();
            public static Func<IEdmPropertyInfo, bool> <>9__15_0;

            internal bool <GetFilteredAndSortedProperties>b__15_0(IEdmPropertyInfo x) => 
                (x.Attributes.Order == null) || (x.Attributes.Order.Value >= 0);
        }

        protected enum EditorsGeneratorMode
        {
            Edit,
            EditSettings
        }

        public enum EditorsGeneratorTarget
        {
            GridControl,
            LayoutControl,
            PropertyGrid,
            Unknown
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Initializer
        {
            public static EditorsGeneratorBase.Initializer Default;
            private readonly Action<IModelItem, IModelItem> SetEditProps;
            private readonly Action<IModelItem> SetContainerProps;
            public Initializer(Action<IModelItem, IModelItem> setEditProps = null, Action<IModelItem> setContainerProps = null)
            {
                this.SetEditProps = setEditProps;
                this.SetContainerProps = setContainerProps;
            }

            public void SetEditProperties(IModelItem container, IModelItem edit)
            {
                this.SetEditProps.Do<Action<IModelItem, IModelItem>>(x => x(container, edit));
            }

            public void SetContainerProperties(IModelItem container)
            {
                this.SetContainerProps.Do<Action<IModelItem>>(x => x(container));
            }

            public static EditorsGeneratorBase.Initializer operator +(EditorsGeneratorBase.Initializer a, EditorsGeneratorBase.Initializer b)
            {
                Action<IModelItem, IModelItem> setEditProps = null;
                Action<IModelItem> setContainerProps = null;
                if ((a.SetEditProps != null) || (b.SetEditProps != null))
                {
                    setEditProps = delegate (IModelItem container, IModelItem edit) {
                        a.SetEditProperties(container, edit);
                        b.SetEditProperties(container, edit);
                    };
                }
                if ((a.SetContainerProps != null) || (b.SetContainerProps != null))
                {
                    setContainerProps = delegate (IModelItem container) {
                        a.SetContainerProperties(container);
                        b.SetContainerProperties(container);
                    };
                }
                return new EditorsGeneratorBase.Initializer(setEditProps, setContainerProps);
            }

            static Initializer()
            {
            }
        }
    }
}

