namespace DMEWorks.Forms
{
    using System;

    internal interface IVersionStorage
    {
        bool IsNull { get; }

        object Value { get; set; }
    }
}

