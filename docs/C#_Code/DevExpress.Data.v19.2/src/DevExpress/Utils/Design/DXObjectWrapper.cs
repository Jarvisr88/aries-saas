namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public static class DXObjectWrapper
    {
        public static object GetInstance(ITypeDescriptorContext context)
        {
            if ((context == null) || (context.Instance == null))
            {
                return null;
            }
            object instance = context.Instance;
            IDXObjectWrapper wrapper = instance as IDXObjectWrapper;
            if (wrapper != null)
            {
                instance = wrapper.SourceObject;
            }
            if (instance is Array)
            {
                Array array = (Array) instance;
                if (array.Length > 0)
                {
                    instance = array.GetValue(0);
                    wrapper = instance as IDXObjectWrapper;
                    if (wrapper != null)
                    {
                        return wrapper.SourceObject;
                    }
                }
            }
            return instance;
        }
    }
}

