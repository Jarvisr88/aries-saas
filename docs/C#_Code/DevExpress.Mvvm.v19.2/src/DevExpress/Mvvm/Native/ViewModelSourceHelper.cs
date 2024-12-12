namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Reflection;

    public static class ViewModelSourceHelper
    {
        public static object Create(Type type) => 
            ViewModelSource.Create(type);

        public static ConstructorInfo FindConstructorWithAllOptionalParameters(Type type) => 
            ViewModelSource.FindConstructorWithAllOptionalParameters(type);

        public static Type GetProxyType(Type type) => 
            ViewModelSource.GetPOCOType(type, null);

        public static bool IsPOCOViewModelType(Type type) => 
            ViewModelSource.IsPOCOViewModelType(type);
    }
}

