namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections;

    public interface IDTEService
    {
        string[] GetClassesInfo(Type filterType, IList ignoreClassNames);

        string ProjectFullName { get; }
    }
}

