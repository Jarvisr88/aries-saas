namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class SmartTagActionsProviderAttribute : Attribute
    {
        public abstract object[] GetActions(object component);

        public abstract DesignTimeActionCategory ActionsCategory { get; }
    }
}

