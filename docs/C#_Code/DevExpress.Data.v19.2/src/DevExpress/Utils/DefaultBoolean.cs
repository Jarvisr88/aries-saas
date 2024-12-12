namespace DevExpress.Utils
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(DefaultBooleanConverter)), ResourceFinder(typeof(ResFinder))]
    public enum DefaultBoolean
    {
        True,
        False,
        Default
    }
}

