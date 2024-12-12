namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public abstract class LineFactoryBase
    {
        protected LineFactoryBase();
        public abstract ILine CreateBooleanLine(PropertyDescriptor property, object obj);
        public abstract ILine CreateColorPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj);
        public abstract ILine CreateComboBoxPropertyLine(IStringConverter converter, object[] values, PropertyDescriptor property, object obj);
        public abstract ILine CreateDateTimePropertyLine(IStringConverter converter, PropertyDescriptor property, object obj);
        public abstract ILine CreateEditorPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj);
        public abstract ILine CreateEmptyLine();
        public abstract ILine CreateNumericPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj);
        public abstract ILine CreateSeparatorLine();
    }
}

