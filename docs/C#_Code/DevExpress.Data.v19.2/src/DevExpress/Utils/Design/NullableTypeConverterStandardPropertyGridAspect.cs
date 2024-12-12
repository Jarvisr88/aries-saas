namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    internal class NullableTypeConverterStandardPropertyGridAspect
    {
        private int propertyGridSubscribeLocker;

        private static void InvokeMethod(MethodInfo methodInfo, object obj, object[] parameters)
        {
            if (methodInfo != null)
            {
                methodInfo.Invoke(obj, parameters);
            }
        }

        private static bool IsPopUpVisible(ITypeDescriptorContext context)
        {
            object obj2 = context.GetType().GetProperty("GridEntryHost", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(context, null);
            object obj3 = obj2.GetType().GetField("dropDownHolder", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2);
            return ((obj3 != null) && ((bool) obj3.GetType().GetProperty("Visible").GetValue(obj3, null)));
        }

        public void OnConvertFrom(ITypeDescriptorContext context)
        {
            try
            {
                if ((context == null) || IsPopUpVisible(context))
                {
                    return;
                }
            }
            catch
            {
                return;
            }
            PropertyInfo property = context.GetType().GetProperty("OwnerGrid");
            if (property != null)
            {
                PropertyGrid propertyGrid = property.GetValue(context, null) as PropertyGrid;
                this.Subscribe(propertyGrid);
            }
        }

        public void OnConvertTo(ITypeDescriptorContext context)
        {
            InvokeMethod(context?.GetType().GetMethod("Refresh"), context, null);
        }

        private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            PropertyGrid propertyGrid = sender as PropertyGrid;
            if (propertyGrid != null)
            {
                this.UnSubscribe(propertyGrid);
                propertyGrid.Refresh();
            }
        }

        private void Subscribe(PropertyGrid propertyGrid)
        {
            if ((propertyGrid != null) && (this.propertyGridSubscribeLocker == 0))
            {
                propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
                this.propertyGridSubscribeLocker++;
            }
        }

        private void UnSubscribe(PropertyGrid propertyGrid)
        {
            if ((propertyGrid != null) && (this.propertyGridSubscribeLocker != 0))
            {
                propertyGrid.PropertyValueChanged -= new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
                this.propertyGridSubscribeLocker--;
            }
        }
    }
}

