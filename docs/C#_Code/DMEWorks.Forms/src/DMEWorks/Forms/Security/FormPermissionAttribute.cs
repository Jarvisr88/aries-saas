namespace DMEWorks.Forms.Security
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class FormPermissionAttribute : Attribute
    {
        private string _expression;

        public FormPermissionAttribute()
        {
            this._expression = null;
        }

        public FormPermissionAttribute(string Expression)
        {
            this._expression = Expression;
        }

        public bool Restricted =>
            this._expression != null;

        public string Expression =>
            this._expression;
    }
}

