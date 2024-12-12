namespace DevExpress.Data.Utils
{
    using System;
    using System.Diagnostics;

    [AttributeUsage(AttributeTargets.All), Conditional("DEBUGTEST")]
    public class FxCopSpellCheckingIgnoreAttribute : Attribute
    {
    }
}

