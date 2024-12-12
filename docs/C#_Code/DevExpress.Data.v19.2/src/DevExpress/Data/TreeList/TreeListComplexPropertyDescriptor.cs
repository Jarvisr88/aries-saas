namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Access;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class TreeListComplexPropertyDescriptor : ComplexPropertyDescriptorReflection
    {
        public TreeListComplexPropertyDescriptor(TreeListDataControllerBase controller1, DataControllerBase controllerBase, string path);
        public TreeListComplexPropertyDescriptor(TreeListDataControllerBase controller, object sourceObject, string path);
        protected override PropertyDescriptor GetDescriptor(string name, object obj, Type type);

        protected TreeListDataControllerBase DataController { get; private set; }
    }
}

