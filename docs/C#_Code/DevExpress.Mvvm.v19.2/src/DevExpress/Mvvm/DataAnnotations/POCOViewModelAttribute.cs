namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class POCOViewModelAttribute : Attribute
    {
        public bool ImplementIDataErrorInfo;
        public bool ImplementINotifyPropertyChanging;
        public bool InvokeOnPropertyChangedMethodBeforeRaisingINPC;
    }
}

