namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class BaseEditUnit
    {
        private HashSet<string> modifiedPropertyNames = new HashSet<string>();
        private string fieldName;
        private string expression;
        private bool applyToRow;
        private bool isEnabled = true;
        private string predefinedFormatName;
        private string rowName = "PivotGridAnyFieldNameFieldName";
        private string columnName = "PivotGridAnyFieldNameFieldName";

        protected BaseEditUnit()
        {
        }

        public IModelItem BuildCondition(IConditionModelItemsBuilder builder) => 
            this.BuildCondition(builder, null);

        public abstract IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source);
        public abstract string GetDescription(IDialogContext context);
        public abstract Freezable GetFormat();
        private bool IsPropertyModified<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> expression) where TOwner: BaseEditUnit
        {
            Func<bool> fallback = <>c__40<TOwner, TProperty>.<>9__40_1;
            if (<>c__40<TOwner, TProperty>.<>9__40_1 == null)
            {
                Func<bool> local1 = <>c__40<TOwner, TProperty>.<>9__40_1;
                fallback = <>c__40<TOwner, TProperty>.<>9__40_1 = () => false;
            }
            return (expression.Body as MemberExpression).Return<MemberExpression, bool>(x => this.IsPropertyModified(x.Member.Name), fallback);
        }

        private bool IsPropertyModified(string propertyName) => 
            this.modifiedPropertyNames.Contains(propertyName);

        public void Populate(BaseEditUnit unit)
        {
            this.PopulateCore(unit, prop => unit.IsPropertyModified(prop.Name));
        }

        private void PopulateCore(BaseEditUnit unit, Func<PropertyDescriptor, bool> action)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(unit))
            {
                if (properties.Contains(descriptor) && action(descriptor))
                {
                    object obj2 = descriptor.GetValue(unit);
                    descriptor.SetValue(this, obj2);
                }
            }
        }

        protected void RegisterPropertyModification(string propertyName)
        {
            this.modifiedPropertyNames.Add(propertyName);
        }

        public void Restore(BaseEditUnit unit)
        {
            this.PopulateCore(unit, prop => unit.IsPropertyModified(prop.Name) && (!this.IsPropertyModified(prop.Name) && prop.Attributes.Contains(new ManagerRestorablePropertyAttribute())));
        }

        public void SetModelItemProperty<TOwner, TProperty>(IModelItem item, DependencyProperty property, Expression<Func<TOwner, TProperty>> expression) where TOwner: BaseEditUnit
        {
            bool flag = !(item is RuntimeModelItem);
            if (flag)
            {
                TOwner local2 = this as TOwner;
                if (local2 != null)
                {
                    TProperty local3 = expression.Compile()(local2);
                    if ((local3 is IconSetFormat) && this.IsPropertyModified<TOwner, TProperty>(expression))
                    {
                        IconSetFormat objB = local3 as IconSetFormat;
                        if (!Equals(item.Properties[property.Name].ComputedValue, objB))
                        {
                            IModelItem item2 = item.Context.CreateItem(typeof(IconSetFormat));
                            IModelItemCollection collection = item2.Properties["Elements"].Collection;
                            foreach (IconSetElement element in objB.Elements)
                            {
                                IModelItem item3 = item.Context.CreateItem(typeof(IconSetElement));
                                item3.Properties["Threshold"].SetValue(element.Threshold);
                                item3.Properties["ThresholdComparisonType"].SetValue(element.ThresholdComparisonType);
                                string iconName = IconSetExtension.GetIconName(element.Icon);
                                if (iconName != null)
                                {
                                    IconSetExtension extension1 = new IconSetExtension();
                                    extension1.Name = iconName;
                                    item3.Properties["Icon"].SetValue(extension1);
                                }
                                else
                                {
                                    Uri uri;
                                    if (!Uri.TryCreate(new ImageSourceConverter().ConvertToInvariantString(element.Icon), UriKind.Absolute, out uri) || (uri == null))
                                    {
                                        item3.Properties["Icon"].SetValue(element.Icon);
                                    }
                                    else if (!string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(uri.LocalPath)))
                                    {
                                        item3.Properties["Icon"].SetValue(element.Icon);
                                    }
                                    else
                                    {
                                        IconSetExtension extension2 = new IconSetExtension();
                                        extension2.Name = iconName;
                                        item3.Properties["Icon"].SetValue(extension2);
                                    }
                                }
                                collection.Add(item3);
                            }
                            item2.Properties["ElementThresholdType"].SetValue(objB.ElementThresholdType);
                            item2.Properties["IconSetType"].SetValue(objB.IconSetType);
                            item2.Properties["IconVerticalAlignment"].SetValue(objB.IconVerticalAlignment);
                            item.Properties[property.Name].SetValue(item2);
                            return;
                        }
                    }
                }
            }
            TOwner arg = this as TOwner;
            if ((arg != null) && this.IsPropertyModified<TOwner, TProperty>(expression))
            {
                TProperty objB = expression.Compile()(arg);
                if (!Equals(item.Properties[property.Name].ComputedValue, objB))
                {
                    item.Properties[property.Name].SetValue(objB);
                }
                if (flag && (objB is Format))
                {
                    Format format2 = objB as Format;
                    if (format2.Icon != null)
                    {
                        string iconName = IconSetExtension.GetIconName(format2.Icon);
                        if (iconName != null)
                        {
                            IModelItem item4 = item.Properties[property.Name].Value;
                            if (item4 != null)
                            {
                                IconSetExtension extension3 = new IconSetExtension();
                                extension3.Name = iconName;
                                item4.Properties["Icon"].SetValue(extension3);
                            }
                        }
                    }
                }
            }
        }

        public string FieldName
        {
            get => 
                this.fieldName;
            set
            {
                if (this.fieldName != value)
                {
                    this.fieldName = value;
                    this.RegisterPropertyModification("FieldName");
                }
            }
        }

        public string Expression
        {
            get => 
                this.expression;
            set
            {
                if (this.expression != value)
                {
                    this.expression = value;
                    this.RegisterPropertyModification("Expression");
                }
            }
        }

        [ManagerRestorableProperty]
        public bool ApplyToRow
        {
            get => 
                this.applyToRow;
            set
            {
                if (this.applyToRow != value)
                {
                    this.applyToRow = value;
                    this.RegisterPropertyModification("ApplyToRow");
                }
            }
        }

        public abstract bool CanApplyToRow { get; }

        public string PredefinedFormatName
        {
            get => 
                this.predefinedFormatName;
            set
            {
                if (this.predefinedFormatName != value)
                {
                    this.predefinedFormatName = value;
                }
                this.RegisterPropertyModification("PredefinedFormatName");
            }
        }

        [ManagerRestorableProperty]
        public string RowName
        {
            get => 
                this.rowName;
            set
            {
                if (this.rowName != value)
                {
                    this.rowName = value;
                    this.RegisterPropertyModification("RowName");
                }
            }
        }

        [ManagerRestorableProperty]
        public string ColumnName
        {
            get => 
                this.columnName;
            set
            {
                if (this.columnName != value)
                {
                    this.columnName = value;
                    this.RegisterPropertyModification("ColumnName");
                }
            }
        }

        public bool IsEnabled
        {
            get => 
                this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.RegisterPropertyModification("IsEnabled");
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__40<TOwner, TProperty> where TOwner: BaseEditUnit
        {
            public static readonly BaseEditUnit.<>c__40<TOwner, TProperty> <>9;
            public static Func<bool> <>9__40_1;

            static <>c__40()
            {
                BaseEditUnit.<>c__40<TOwner, TProperty>.<>9 = new BaseEditUnit.<>c__40<TOwner, TProperty>();
            }

            internal bool <IsPropertyModified>b__40_1() => 
                false;
        }
    }
}

