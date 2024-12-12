namespace DevExpress.XtraEditors.Controls
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public class EditorPainterActivatorAttribute : Attribute
    {
        private Type objectType;
        private Type returnType;

        public EditorPainterActivatorAttribute(Type objectType, Type returnType);

        public Type ObjectType { get; }

        public Type ReturnType { get; }
    }
}

