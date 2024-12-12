namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    public class SerializationProvider
    {
        private static readonly SerializationProvider instanceCore = new SerializationProvider();

        protected internal virtual bool CustomDeserializeProperty(XtraPropertyInfoEventArgs e)
        {
            RaiseEvent(e);
            return e.Handled;
        }

        protected internal virtual DependencyObject GetEventTarget(object obj) => 
            obj as DependencyObject;

        protected internal virtual string GetLayoutVersion(DependencyObject dObj) => 
            DXSerializer.GetLayoutVersion(dObj);

        protected internal virtual string GetSerializationID(DependencyObject dObj)
        {
            if (DXSerializer.GetIsProcessingSingleObject(dObj))
            {
                return DXSerializer.GetSerializationIDDefault(dObj);
            }
            string serializationID = DXSerializer.GetSerializationID(dObj);
            if (string.IsNullOrEmpty(serializationID))
            {
                serializationID = DXSerializer.GetSerializationIDDefault(dObj);
            }
            return serializationID;
        }

        protected internal virtual DependencyObject GetSource(DependencyObject dObj) => 
            dObj;

        protected internal virtual bool OnAllowProperty(AllowPropertyEventArgs e)
        {
            RaiseEvent(e);
            return e.Allow;
        }

        protected internal virtual void OnClearCollection(XtraItemRoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        protected internal virtual object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            RaiseEvent(e);
            return e.CollectionItem;
        }

        protected internal virtual object OnCreateContentPropertyValue(XtraCreateContentPropertyValueEventArgs e)
        {
            RaiseEvent(e);
            return e.PropertyValue;
        }

        protected internal virtual void OnCustomGetSerializableChildren(DependencyObject dObj, CustomGetSerializableChildrenEventArgs e)
        {
            RaiseEvent(e);
        }

        protected internal virtual void OnCustomGetSerializableProperties(DependencyObject dObj, CustomGetSerializablePropertiesEventArgs e)
        {
            RaiseEvent(e);
        }

        protected internal virtual bool? OnCustomShouldSerializeProperty(CustomShouldSerializePropertyEventArgs e)
        {
            RaiseEvent(e);
            return e.CustomShouldSerialize;
        }

        protected internal virtual void OnEndDeserializing(DependencyObject dObj, string restoredVersion)
        {
            if (dObj != null)
            {
                RaiseEvent(new EndDeserializingEventArgs(dObj, restoredVersion));
                if (restoredVersion != this.GetLayoutVersion(dObj))
                {
                    this.RaiseLayoutUpgrade(dObj, restoredVersion);
                }
            }
        }

        protected internal virtual void OnEndSerializing(DependencyObject dObj)
        {
            if (dObj != null)
            {
                RaiseEvent(new RoutedEventArgs(DXSerializer.EndSerializingEvent, dObj));
            }
        }

        protected internal virtual object OnFindCollectionItem(XtraFindCollectionItemEventArgs e)
        {
            RaiseEvent(e);
            return e.CollectionItem;
        }

        protected internal virtual void OnResetProperty(ResetPropertyEventArgs e)
        {
            object defaultValue;
            RaiseEvent(e);
            if (!e.Handled && (e.ResetPropertyMode != ResetPropertyMode.None))
            {
                DependencyObject source = e.Source as DependencyObject;
                ResetPropertyMode resetPropertyMode = e.ResetPropertyMode;
                IList list = e.Property.GetValue(e.Source) as IList;
                bool flag = (source != null) && (e.DependencyProperty != null);
                BindingMode mode = BindingMode.Default;
                if (flag)
                {
                    BindingExpression expression = source.ReadLocalValue(e.DependencyProperty) as BindingExpression;
                    if ((expression != null) && (expression.ParentBinding != null))
                    {
                        mode = expression.ParentBinding.Mode;
                        if (mode == BindingMode.Default)
                        {
                            FrameworkPropertyMetadata metadata = e.DependencyProperty.GetMetadata(source) as FrameworkPropertyMetadata;
                            mode = ((metadata == null) || !metadata.BindsTwoWayByDefault) ? BindingMode.OneWay : BindingMode.TwoWay;
                        }
                        if ((mode != BindingMode.TwoWay) && (mode != BindingMode.OneWayToSource))
                        {
                            return;
                        }
                    }
                }
                resetPropertyMode ??= ((list == null) ? (!flag ? ResetPropertyMode.SetDefaultValue : ResetPropertyMode.ClearValue) : ResetPropertyMode.ClearCollection);
                switch (resetPropertyMode)
                {
                    case ResetPropertyMode.SetDefaultValue:
                        defaultValue = null;
                        if (!flag)
                        {
                            DefaultValueAttribute attribute = e.Property.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                            if (attribute != null)
                            {
                                defaultValue = attribute.Value;
                            }
                            goto TR_0002;
                        }
                        else if (mode == BindingMode.Default)
                        {
                            defaultValue = e.DependencyProperty.GetMetadata(e.Property.ComponentType).DefaultValue;
                            goto TR_0002;
                        }
                        break;

                    case ResetPropertyMode.ClearValue:
                        if (!flag || (mode != BindingMode.Default))
                        {
                            break;
                        }
                        source.ClearValue(e.DependencyProperty);
                        return;

                    case ResetPropertyMode.ClearCollection:
                        if (list != null)
                        {
                            list.Clear();
                        }
                        break;

                    default:
                        return;
                }
            }
            return;
        TR_0002:
            e.Property.SetValue(e.Source, defaultValue);
        }

        protected internal virtual bool OnShouldSerializeCollectionItem(XtraShouldSerailizeCollectionItemEventArgs e)
        {
            RaiseEvent(e);
            return e.ShouldSerailize;
        }

        protected internal virtual bool OnShouldSerializeProperty(object obj, PropertyDescriptor prop, XtraSerializableProperty xtraSerializableProperty)
        {
            DependencyPropertyDescriptor dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(prop);
            return ((dependencyPropertyDescriptor == null) || DXSerializationContext.ShouldSerializeDependencyProeprty(dependencyPropertyDescriptor, obj));
        }

        protected internal virtual void OnStartDeserializing(DependencyObject dObj, LayoutAllowEventArgs ea)
        {
            if (dObj != null)
            {
                if (!this.RaiseBeforeLoadLayout(dObj, ea.PreviousVersion))
                {
                    ea.Allow = false;
                }
                else
                {
                    RaiseEvent(new StartDeserializingEventArgs(dObj, ea));
                }
            }
        }

        protected internal virtual void OnStartSerializing(DependencyObject dObj)
        {
            if (dObj != null)
            {
                RaiseEvent(new RoutedEventArgs(DXSerializer.StartSerializingEvent, dObj));
            }
        }

        protected virtual bool RaiseBeforeLoadLayout(object obj, string restoredVersion)
        {
            BeforeLoadLayoutEventArgs e = new BeforeLoadLayoutEventArgs(obj, restoredVersion);
            RaiseEvent(e);
            return e.Allow;
        }

        private static void RaiseEvent(RoutedEventArgs e)
        {
            UIElement source = e.Source as UIElement;
            if (source != null)
            {
                source.RaiseEvent(e);
            }
            else
            {
                FrameworkContentElement element2 = e.Source as FrameworkContentElement;
                if (element2 != null)
                {
                    element2.RaiseEvent(e);
                }
            }
        }

        protected virtual void RaiseLayoutUpgrade(object obj, string restoredVersion)
        {
            RaiseEvent(new DevExpress.Xpf.Core.Serialization.LayoutUpgradeEventArgs(obj, restoredVersion));
        }

        public static SerializationProvider Instance =>
            instanceCore;
    }
}

