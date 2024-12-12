namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public interface ISmartTagFilter
    {
        bool FilterMethod(string MethodName, object actionMethodItem);
        bool FilterProperty(MemberDescriptor descriptor);
        void SetComponent(IComponent component);
    }
}

