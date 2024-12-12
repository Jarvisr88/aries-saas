namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public interface ICommandGroupsGenerator
    {
        ICommandSubGroupsGenerator CreateGroup(string groupName);
    }
}

