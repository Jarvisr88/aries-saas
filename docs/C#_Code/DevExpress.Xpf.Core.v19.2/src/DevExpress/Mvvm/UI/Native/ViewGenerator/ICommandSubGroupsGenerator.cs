namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public interface ICommandSubGroupsGenerator
    {
        ICommandsGenerator CreateSubGroup(string groupName);
    }
}

