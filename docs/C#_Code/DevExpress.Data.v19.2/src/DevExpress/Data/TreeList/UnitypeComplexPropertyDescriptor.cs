namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public class UnitypeComplexPropertyDescriptor : TreeListComplexPropertyDescriptor
    {
        public UnitypeComplexPropertyDescriptor(TreeListDataControllerBase controller, DataControllerBase dataController, string path);
        public UnitypeComplexPropertyDescriptor(TreeListDataControllerBase controller, object sourceObject, string path);
        protected override PropertyDescriptor GetDescriptor(string name, object obj, Type type);
    }
}

