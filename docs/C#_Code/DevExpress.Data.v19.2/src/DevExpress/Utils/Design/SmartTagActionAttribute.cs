namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=true)]
    public class SmartTagActionAttribute : Attribute
    {
        private System.Type type;
        private string methodName;
        private string displayName;
        private int sortOrder;
        private SmartTagActionType finalAction;

        public SmartTagActionAttribute(string displayName) : this(null, null, displayName)
        {
        }

        public SmartTagActionAttribute(System.Type type, string methodName, string displayName) : this(type, methodName, displayName, SmartTagActionType.None)
        {
        }

        public SmartTagActionAttribute(System.Type type, string methodName, string displayName, SmartTagActionType finalAction) : this(type, methodName, displayName, -1, finalAction)
        {
        }

        public SmartTagActionAttribute(System.Type type, string methodName, string displayName, int sortOrder, SmartTagActionType finalAction)
        {
            this.type = type;
            this.methodName = methodName;
            this.displayName = displayName;
            this.sortOrder = sortOrder;
            this.finalAction = finalAction;
        }

        public System.Type Type =>
            this.type;

        public string MethodName =>
            this.methodName;

        public string DisplayName =>
            this.displayName;

        public int SortOrder =>
            this.sortOrder;

        public SmartTagActionType FinalAction =>
            this.finalAction;
    }
}

